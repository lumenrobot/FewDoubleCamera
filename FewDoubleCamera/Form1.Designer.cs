namespace FewDoubleCamera
{
    partial class FormDuble
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
            this.components = new System.ComponentModel.Container();
            this.iB = new Emgu.CV.UI.ImageBox();
            this.cBKamera = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lt = new System.Windows.Forms.Label();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.btnAmbil = new System.Windows.Forms.Button();
            this.textBoxNama = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSimpan = new System.Windows.Forms.Button();
            this.ib2 = new Emgu.CV.UI.ImageBox();
            this.activateMessagingBtn = new System.Windows.Forms.Button();
            this.stopMessagingBtn = new System.Windows.Forms.Button();
            this.messagingTimer = new System.Windows.Forms.Timer(this.components);
            this.startStreamingBtn = new System.Windows.Forms.Button();
            this.captureTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.iB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib2)).BeginInit();
            this.SuspendLayout();
            // 
            // iB
            // 
            this.iB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iB.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            this.iB.Location = new System.Drawing.Point(12, 40);
            this.iB.Name = "iB";
            this.iB.Size = new System.Drawing.Size(401, 320);
            this.iB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iB.TabIndex = 2;
            this.iB.TabStop = false;
            // 
            // cBKamera
            // 
            this.cBKamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBKamera.FormattingEnabled = true;
            this.cBKamera.Items.AddRange(new object[] {
            "0",
            "1"});
            this.cBKamera.Location = new System.Drawing.Point(191, 12);
            this.cBKamera.Name = "cBKamera";
            this.cBKamera.Size = new System.Drawing.Size(121, 21);
            this.cBKamera.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "PilihKamera Yang Akan Digunakan";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(318, 12);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(170, 23);
            this.bStart.TabIndex = 5;
            this.bStart.Text = "Start Deteksi";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(619, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Total Terdeteksi";
            // 
            // lt
            // 
            this.lt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lt.AutoSize = true;
            this.lt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt.Location = new System.Drawing.Point(687, 27);
            this.lt.Name = "lt";
            this.lt.Size = new System.Drawing.Size(16, 16);
            this.lt.TabIndex = 7;
            this.lt.Text = "0";
            // 
            // imageBox1
            // 
            this.imageBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox1.Location = new System.Drawing.Point(553, 53);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(150, 150);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // btnAmbil
            // 
            this.btnAmbil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAmbil.BackColor = System.Drawing.SystemColors.Control;
            this.btnAmbil.Location = new System.Drawing.Point(419, 209);
            this.btnAmbil.Name = "btnAmbil";
            this.btnAmbil.Size = new System.Drawing.Size(150, 23);
            this.btnAmbil.TabIndex = 8;
            this.btnAmbil.Text = "Ambil Sample";
            this.btnAmbil.UseVisualStyleBackColor = false;
            this.btnAmbil.Click += new System.EventHandler(this.btnAmbil_Click);
            // 
            // textBoxNama
            // 
            this.textBoxNama.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNama.Location = new System.Drawing.Point(723, 27);
            this.textBoxNama.Name = "textBoxNama";
            this.textBoxNama.Size = new System.Drawing.Size(161, 20);
            this.textBoxNama.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(757, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nama Dari Wajah Sampe";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // buttonSimpan
            // 
            this.buttonSimpan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSimpan.Location = new System.Drawing.Point(723, 53);
            this.buttonSimpan.Name = "buttonSimpan";
            this.buttonSimpan.Size = new System.Drawing.Size(161, 23);
            this.buttonSimpan.TabIndex = 11;
            this.buttonSimpan.Text = "Simpan Data Pelatihan";
            this.buttonSimpan.UseVisualStyleBackColor = true;
            this.buttonSimpan.Click += new System.EventHandler(this.button1_Click);
            // 
            // ib2
            // 
            this.ib2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ib2.Location = new System.Drawing.Point(575, 157);
            this.ib2.Name = "ib2";
            this.ib2.Size = new System.Drawing.Size(309, 203);
            this.ib2.TabIndex = 2;
            this.ib2.TabStop = false;
            // 
            // activateMessagingBtn
            // 
            this.activateMessagingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activateMessagingBtn.Location = new System.Drawing.Point(419, 238);
            this.activateMessagingBtn.Name = "activateMessagingBtn";
            this.activateMessagingBtn.Size = new System.Drawing.Size(150, 23);
            this.activateMessagingBtn.TabIndex = 12;
            this.activateMessagingBtn.Text = "Start Incoming";
            this.activateMessagingBtn.UseVisualStyleBackColor = true;
            this.activateMessagingBtn.Click += new System.EventHandler(this.activateMessagingBtn_Click);
            // 
            // stopMessagingBtn
            // 
            this.stopMessagingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopMessagingBtn.Location = new System.Drawing.Point(419, 293);
            this.stopMessagingBtn.Name = "stopMessagingBtn";
            this.stopMessagingBtn.Size = new System.Drawing.Size(150, 23);
            this.stopMessagingBtn.TabIndex = 13;
            this.stopMessagingBtn.Text = "Stop Messaging";
            this.stopMessagingBtn.UseVisualStyleBackColor = true;
            this.stopMessagingBtn.Click += new System.EventHandler(this.stopMessagingBtn_Click);
            // 
            // messagingTimer
            // 
            this.messagingTimer.Interval = 500;
            this.messagingTimer.Tick += new System.EventHandler(this.messagingTimer_Tick);
            // 
            // startStreamingBtn
            // 
            this.startStreamingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startStreamingBtn.Location = new System.Drawing.Point(419, 267);
            this.startStreamingBtn.Name = "startStreamingBtn";
            this.startStreamingBtn.Size = new System.Drawing.Size(150, 23);
            this.startStreamingBtn.TabIndex = 14;
            this.startStreamingBtn.Text = "Start Streaming";
            this.startStreamingBtn.UseVisualStyleBackColor = true;
            this.startStreamingBtn.Click += new System.EventHandler(this.startStreamingBtn_Click);
            // 
            // captureTimer
            // 
            this.captureTimer.Interval = 400;
            this.captureTimer.Tick += new System.EventHandler(this.captureTimer_Tick);
            // 
            // FormDuble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 372);
            this.Controls.Add(this.startStreamingBtn);
            this.Controls.Add(this.stopMessagingBtn);
            this.Controls.Add(this.activateMessagingBtn);
            this.Controls.Add(this.buttonSimpan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNama);
            this.Controls.Add(this.btnAmbil);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.lt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBKamera);
            this.Controls.Add(this.iB);
            this.Controls.Add(this.ib2);
            this.Name = "FormDuble";
            this.Text = "DubleKamera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDuble_FormClosing);
            this.Load += new System.EventHandler(this.FormDuble_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormDuble_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.iB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ib2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox iB;
        private System.Windows.Forms.ComboBox cBKamera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lt;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button btnAmbil;
        private System.Windows.Forms.TextBox textBoxNama;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSimpan;
        private Emgu.CV.UI.ImageBox ib2;
        private System.Windows.Forms.Button activateMessagingBtn;
        private System.Windows.Forms.Button stopMessagingBtn;
        private System.Windows.Forms.Timer messagingTimer;
        private System.Windows.Forms.Button startStreamingBtn;
        private System.Windows.Forms.Timer captureTimer;
    }
}

