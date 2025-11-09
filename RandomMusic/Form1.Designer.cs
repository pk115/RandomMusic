
namespace RandomMusic
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // --- Components Declaration ---
            this.components = new System.ComponentModel.Container();
            this.btnSelectInputFolder = new System.Windows.Forms.Button();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.labelInputFolder = new System.Windows.Forms.Label();
            this.labelOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.labelNumFiles = new System.Windows.Forms.Label();
            this.numericUpDownNumFiles = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownClipsPerFile = new System.Windows.Forms.NumericUpDown();
            this.labelClipsPerFile = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClipsPerFile)).BeginInit();
            this.SuspendLayout();

            // --- btnSelectInputFolder ---
            this.btnSelectInputFolder.Location = new System.Drawing.Point(380, 20);
            this.btnSelectInputFolder.Name = "btnSelectInputFolder";
            this.btnSelectInputFolder.Size = new System.Drawing.Size(95, 23);
            this.btnSelectInputFolder.TabIndex = 0;
            this.btnSelectInputFolder.Text = "เลือกโฟลเดอร์";
            this.btnSelectInputFolder.UseVisualStyleBackColor = true;
            this.btnSelectInputFolder.Click += new System.EventHandler(this.btnSelectInputFolder_Click);

            // --- txtInputFolder ---
            this.txtInputFolder.Location = new System.Drawing.Point(120, 22);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.ReadOnly = true;
            this.txtInputFolder.Size = new System.Drawing.Size(250, 20);
            this.txtInputFolder.TabIndex = 1;

            // --- labelInputFolder ---
            this.labelInputFolder.AutoSize = true;
            this.labelInputFolder.Location = new System.Drawing.Point(20, 25);
            this.labelInputFolder.Name = "labelInputFolder";
            this.labelInputFolder.Size = new System.Drawing.Size(89, 13);
            this.labelInputFolder.Text = "โฟลเดอร์ Input:";

            // --- labelOutputFolder ---
            this.labelOutputFolder.AutoSize = true;
            this.labelOutputFolder.Location = new System.Drawing.Point(20, 65);
            this.labelOutputFolder.Name = "labelOutputFolder";
            this.labelOutputFolder.Size = new System.Drawing.Size(97, 13);
            this.labelOutputFolder.Text = "โฟลเดอร์ Output:";

            // --- txtOutputFolder ---
            this.txtOutputFolder.Location = new System.Drawing.Point(120, 62);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(250, 20);
            this.txtOutputFolder.TabIndex = 3;

            // --- btnSelectOutputFolder ---
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(380, 60);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(95, 23);
            this.btnSelectOutputFolder.TabIndex = 2;
            this.btnSelectOutputFolder.Text = "เลือกโฟลเดอร์";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);

            // --- labelNumFiles (จำนวนไฟล์ Output) ---
            this.labelNumFiles.AutoSize = true;
            this.labelNumFiles.Location = new System.Drawing.Point(20, 105);
            this.labelNumFiles.Name = "labelNumFiles";
            this.labelNumFiles.Size = new System.Drawing.Size(90, 13);
            this.labelNumFiles.Text = "จำนวนไฟล์ที่ต่อ:";

            // --- numericUpDownNumFiles ---
            this.numericUpDownNumFiles.Location = new System.Drawing.Point(120, 103);
            this.numericUpDownNumFiles.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDownNumFiles.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numericUpDownNumFiles.Name = "numericUpDownNumFiles";
            this.numericUpDownNumFiles.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownNumFiles.TabIndex = 4;
            this.numericUpDownNumFiles.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // --- labelClipsPerFile (จำนวนคลิปต่อ 1 ไฟล์) ---
            this.labelClipsPerFile.AutoSize = true;
            this.labelClipsPerFile.Location = new System.Drawing.Point(200, 105);
            this.labelClipsPerFile.Name = "labelClipsPerFile";
            this.labelClipsPerFile.Size = new System.Drawing.Size(126, 13);
            this.labelClipsPerFile.Text = "จำนวนคลิปเสียงต่อไฟล์:";

            // --- numericUpDownClipsPerFile ---
            this.numericUpDownClipsPerFile.Location = new System.Drawing.Point(335, 103);
            this.numericUpDownClipsPerFile.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDownClipsPerFile.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numericUpDownClipsPerFile.Name = "numericUpDownClipsPerFile";
            this.numericUpDownClipsPerFile.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownClipsPerFile.TabIndex = 5;
            this.numericUpDownClipsPerFile.Value = new decimal(new int[] { 2, 0, 0, 0 });

            // --- btnGenerate (ปุ่มเริ่ม) ---
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(20, 145);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(455, 35);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "🚀 เริ่มสุ่มและสร้างไฟล์เสียง";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            // --- lblStatus (แสดงสถานะ) ---
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 200);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 13);
            this.lblStatus.Text = "สถานะ: พร้อมทำงาน";

            // --- Form1 (Container) ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 230); // ปรับขนาดให้พอดี
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.numericUpDownClipsPerFile);
            this.Controls.Add(this.labelClipsPerFile);
            this.Controls.Add(this.numericUpDownNumFiles);
            this.Controls.Add(this.labelNumFiles);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnSelectOutputFolder);
            this.Controls.Add(this.labelOutputFolder);
            this.Controls.Add(this.txtInputFolder);
            this.Controls.Add(this.btnSelectInputFolder);
            this.Controls.Add(this.labelInputFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; // ล็อกขนาดฟอร์ม
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Random MP3 Merger";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClipsPerFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // --- Declaration of Components (ต้องประกาศใน Form1.Designer.cs ส่วนบน) ---
        private System.Windows.Forms.Button btnSelectInputFolder;
        private System.Windows.Forms.TextBox txtInputFolder;
        private System.Windows.Forms.Label labelInputFolder;
        private System.Windows.Forms.Label labelOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Label labelNumFiles;
        private System.Windows.Forms.NumericUpDown numericUpDownNumFiles;
        private System.Windows.Forms.NumericUpDown numericUpDownClipsPerFile;
        private System.Windows.Forms.Label labelClipsPerFile;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblStatus;
        #endregion
    }
}

