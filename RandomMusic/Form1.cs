using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.Lame; // ต้องติดตั้ง Package NAudio.Lame แล้ว

namespace RandomMusic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ...
        }

        // ----------------------------------------------------------------------
        // 1. Event Handlers (ส่วนนี้คงเดิม)
        // ----------------------------------------------------------------------

        private void btnSelectInputFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    txtInputFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    txtOutputFolder.Text = fbd.SelectedPath;
            }
        }

        // ----------------------------------------------------------------------
        // 2. Logic หลัก: ปุ่มเริ่มสุ่มและสร้างไฟล์
        // ----------------------------------------------------------------------

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInputFolder.Text) || string.IsNullOrWhiteSpace(txtOutputFolder.Text))
            {
                MessageBox.Show("กรุณาเลือกโฟลเดอร์ Input และ Output ก่อน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnGenerate.Enabled = false;
            lblStatus.Text = "สถานะ: กำลังเตรียมการ...";

            string inputPath = txtInputFolder.Text;
            string outputPath = txtOutputFolder.Text;
            int totalFiles = (int)numericUpDownNumFiles.Value;
            int clipsPerFile = (int)numericUpDownClipsPerFile.Value;
            bool success = true;

            await Task.Run(() =>
            {
                try
                {
                    success = GenerateRandomAudioFiles(inputPath, outputPath, totalFiles, clipsPerFile);
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show($"เกิดข้อผิดพลาดที่ไม่คาดคิด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                    success = false;
                }
            });

            btnGenerate.Enabled = true;
            if (success)
            {
                lblStatus.Text = $"สถานะ: เสร็จสมบูรณ์! (สร้างไฟล์ {totalFiles} ไฟล์สำเร็จ)";
                MessageBox.Show($"สร้างไฟล์เสียง {totalFiles} ไฟล์เสร็จสิ้นแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = "สถานะ: ล้มเหลว (ตรวจสอบข้อผิดพลาดด้านบน)";
            }
        }

        // ----------------------------------------------------------------------
        // 3. Helper Functions 
        // ----------------------------------------------------------------------

        /// <summary>
        /// สุ่มเลือกไฟล์เสียงจากโฟลเดอร์ โดยรองรับ MP3, WAV และ M4A 
        /// </summary>
        private List<string> SelectRandomFiles(string folderPath, int numberOfClips)
        {
            var searchPatterns = new[] { "*.mp3", "*.wav", "*.m4a" };
            var allAudioFiles = searchPatterns.SelectMany(pattern =>
                Directory.GetFiles(folderPath, pattern, SearchOption.TopDirectoryOnly)
            ).ToList();

            if (allAudioFiles.Count == 0)
            {
                throw new FileNotFoundException("ไม่พบไฟล์เสียง MP3, WAV หรือ M4A ในโฟลเดอร์ Input");
            }

            Random random = new Random();
            var selectedFiles = new List<string>();

            // ใช้วิธีสุ่มแบบไม่ซ้ำกัน
            if (allAudioFiles.Count >= numberOfClips)
            {
                var availableIndices = Enumerable.Range(0, allAudioFiles.Count).ToList();
                for (int i = 0; i < numberOfClips; i++)
                {
                    int randomIndex = random.Next(availableIndices.Count);
                    selectedFiles.Add(allAudioFiles[availableIndices[randomIndex]]);
                    availableIndices.RemoveAt(randomIndex);
                }
            }
            else
            {
                // หากไฟล์ไม่พอ ให้สุ่มทั้งหมดที่มี และเตือน
                this.Invoke((MethodInvoker)delegate {
                    MessageBox.Show($"ไฟล์ในโฟลเดอร์มีไม่พอ ({allAudioFiles.Count} ไฟล์) แต่จะพยายามสุ่มเท่าที่มี", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                });
                selectedFiles.AddRange(allAudioFiles);
            }

            return selectedFiles;
        }

        /// <summary>
        /// รวมไฟล์เสียงที่สุ่มได้เข้าด้วยกันโดยใช้ NAudio และบันทึกเป็นไฟล์ WAV ชั่วคราว
        /// </summary>
        private string MergeAndSaveToWav(List<string> inputFiles, string outputDirectory)
        {
            // กำหนดรูปแบบเสียงมาตรฐานที่ใช้ในการรวม (44.1kHz, Stereo, 16bit)
            int sampleRate = 44100;
            int channels = 2;
            var outputFormat = new WaveFormat(sampleRate, 16, channels);

            // Path ไฟล์ WAV ชั่วคราว
            string tempWavPath = Path.Combine(outputDirectory, $"temp_merge_{Guid.NewGuid()}.wav");

            // กำหนดขนาด Buffer สำหรับการอ่านข้อมูล 
            int bufferSize = outputFormat.AverageBytesPerSecond;
            byte[] buffer = new byte[bufferSize];

            using (var writer = new WaveFileWriter(tempWavPath, outputFormat))
            {
                foreach (string inputFile in inputFiles)
                {
                    try
                    {
                        // 1. อ่านไฟล์ด้วย MediaFoundationReader (รองรับ MP3, WAV, M4A)
                        using (var reader = new MediaFoundationReader(inputFile))
                        {
                            // 2. ใช้ MediaFoundationResampler เพื่อ Resample/Convert รูปแบบเสียงให้ตรงกับ outputFormat 
                            using (var resampler = new MediaFoundationResampler(reader, outputFormat))
                            {
                                int bytesRead;
                                // 3. วนลูปอ่านข้อมูลจาก Resampler และเขียนลงใน WaveFileWriter
                                while ((bytesRead = resampler.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    writer.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // ดักจับข้อผิดพลาดหากไฟล์ใดไฟล์หนึ่งเสียหายหรือไม่สามารถอ่านได้
                        throw new Exception($"ไม่สามารถอ่านไฟล์ {Path.GetFileName(inputFile)} ได้: {ex.Message}");
                    }
                }
            }
            return tempWavPath;
        }


        /// <summary>
        /// ฟังก์ชันหลักที่วนลูปสร้างไฟล์ทั้งหมด 
        /// *** ใช้ NAudio.Lame ในการ Encode MP3 แทน FFmpeg ***
        /// </summary>
        /// <summary>
        /// ฟังก์ชันหลักที่วนลูปสร้างไฟล์ทั้งหมด 
        /// *** ใช้ DateTime Stamp เพื่อให้ชื่อไฟล์ไม่ทับกัน ***
        /// </summary>
        private bool GenerateRandomAudioFiles(string inputFolderPath, string outputDirectory, int totalOutputFiles, int clipsPerFile)
        {
            for (int i = 1; i <= totalOutputFiles; i++)
            {
                string tempWavPath = null;
                try
                {
                    // 1. สุ่มเลือกไฟล์
                    List<string> randomClips = SelectRandomFiles(inputFolderPath, clipsPerFile);

                    // 2. กำหนดชื่อไฟล์ผลลัพธ์โดยใช้ Timestamp
                    // ใช้ DateTime.Now เพื่อให้ได้เวลาปัจจุบัน และแปลงเป็นสตริงที่ใช้ในชื่อไฟล์ได้
                    // "yyyyMMdd_HHmmssfff" ให้ผลลัพธ์: ปีเดือนวัน_ชั่วโมงนาทีวินาทีมิลลิวินาที 
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

                    // เพิ่มตัวนับ i เพื่อให้ชื่อไฟล์ไม่ซ้ำกันแม้จะสร้างไฟล์หลายไฟล์ในวินาทีเดียวกัน
                    string outputFileName = $"MergedAudio_{timestamp}_{i}.mp3";
                    string outputFilePath = Path.Combine(outputDirectory, outputFileName);

                    // 3. รวมไฟล์เสียงทั้งหมดด้วย NAudio แล้วบันทึกเป็น WAV ชั่วคราว
                    this.Invoke((MethodInvoker)delegate { lblStatus.Text = $"สถานะ: กำลังรวมไฟล์ {i}/{totalOutputFiles} ({outputFileName}) ด้วย NAudio"; });
                    tempWavPath = MergeAndSaveToWav(randomClips, outputDirectory);

                    // 4. ***ขั้นตอนการ Encode MP3 ด้วย NAudio.Lame***
                    this.Invoke((MethodInvoker)delegate { lblStatus.Text = $"สถานะ: กำลัง Encode ไฟล์ที่ {i}/{totalOutputFiles} ({outputFileName})"; });

                    // เปิดไฟล์ WAV ชั่วคราวที่เพิ่งสร้าง
                    using (var reader = new WaveFileReader(tempWavPath))
                    {
                        // ใช้ LameMP3FileWriter ที่รับ LAMEPreset.V8
                        using (var writer = new LameMP3FileWriter(outputFilePath, reader.WaveFormat, NAudio.Lame.LAMEPreset.V8))
                        {
                            reader.CopyTo(writer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        // เปลี่ยนการแสดงผลเป็นไฟล์ที่ i เพื่อให้ผู้ใช้ทราบว่าล้มเหลวที่ไฟล์ลำดับใด
                        MessageBox.Show($"เกิดข้อผิดพลาดในขั้นตอนการสร้างไฟล์ที่ {i}: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                    return false;
                }
                finally
                {
                    // 5. ลบไฟล์ WAV ชั่วคราวเสมอ
                    if (tempWavPath != null && File.Exists(tempWavPath))
                    {
                        try { File.Delete(tempWavPath); } catch { /* พยายามลบ แต่ไม่จำเป็นต้องแจ้งเตือนหากล้มเหลว */ }
                    }
                }
            }
            return true; // สำเร็จทั้งหมด
        }
    }
}