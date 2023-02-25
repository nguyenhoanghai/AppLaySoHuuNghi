
namespace QMS_BenhVien.RangHamMat
{
    partial class FrmSetting
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
            this.cbTTKhuA = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numWidth = new System.Windows.Forms.TextBox();
            this.numHeight = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.btnChangePicture = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAPI = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbKhuC = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbTTKhuA
            // 
            this.cbTTKhuA.DisplayMember = "Name";
            this.cbTTKhuA.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTTKhuA.FormattingEnabled = true;
            this.cbTTKhuA.Location = new System.Drawing.Point(9, 34);
            this.cbTTKhuA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbTTKhuA.Name = "cbTTKhuA";
            this.cbTTKhuA.Size = new System.Drawing.Size(281, 26);
            this.cbTTKhuA.TabIndex = 62;
            this.cbTTKhuA.ValueMember = "Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 18);
            this.label2.TabIndex = 61;
            this.label2.Text = "Dịch vụ tiếp tân khu A";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numWidth);
            this.groupBox2.Controls.Add(this.numHeight);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 181);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(284, 68);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cỡ giấy in";
            // 
            // numWidth
            // 
            this.numWidth.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWidth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numWidth.Location = new System.Drawing.Point(195, 25);
            this.numWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(68, 26);
            this.numWidth.TabIndex = 38;
            // 
            // numHeight
            // 
            this.numHeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numHeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numHeight.Location = new System.Drawing.Point(56, 25);
            this.numHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(68, 26);
            this.numHeight.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(135, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 18);
            this.label11.TabIndex = 37;
            this.label11.Text = "Ngang";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(15, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 18);
            this.label10.TabIndex = 36;
            this.label10.Text = "Cao";
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartWithWindows.Location = new System.Drawing.Point(9, 299);
            this.chkStartWithWindows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(216, 22);
            this.chkStartWithWindows.TabIndex = 71;
            this.chkStartWithWindows.Text = "Khởi động cùng Windows";
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // btnChangePicture
            // 
            this.btnChangePicture.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePicture.Location = new System.Drawing.Point(8, 261);
            this.btnChangePicture.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChangePicture.Name = "btnChangePicture";
            this.btnChangePicture.Size = new System.Drawing.Size(284, 32);
            this.btnChangePicture.TabIndex = 73;
            this.btnChangePicture.Text = "Chọn ảnh nền";
            this.btnChangePicture.UseVisualStyleBackColor = true;
            this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 18);
            this.label1.TabIndex = 74;
            this.label1.Text = "Máy chủ API";
            // 
            // txtAPI
            // 
            this.txtAPI.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPI.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtAPI.Location = new System.Drawing.Point(8, 149);
            this.txtAPI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAPI.Name = "txtAPI";
            this.txtAPI.Size = new System.Drawing.Size(284, 26);
            this.txtAPI.TabIndex = 39;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(69, 361);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 32);
            this.btnClose.TabIndex = 76;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Location = new System.Drawing.Point(184, 361);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 32);
            this.btnSave.TabIndex = 75;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbKhuC
            // 
            this.cbKhuC.DisplayMember = "Name";
            this.cbKhuC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKhuC.FormattingEnabled = true;
            this.cbKhuC.Location = new System.Drawing.Point(9, 91);
            this.cbKhuC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbKhuC.Name = "cbKhuC";
            this.cbKhuC.Size = new System.Drawing.Size(281, 26);
            this.cbKhuC.TabIndex = 80;
            this.cbKhuC.ValueMember = "Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(9, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 18);
            this.label4.TabIndex = 79;
            this.label4.Text = "Dịch vụ Khu C";
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 399);
            this.Controls.Add(this.cbKhuC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAPI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChangePicture);
            this.Controls.Add(this.chkStartWithWindows);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbTTKhuA);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTTKhuA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox numWidth;
        private System.Windows.Forms.TextBox numHeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.Button btnChangePicture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAPI;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbKhuC;
        private System.Windows.Forms.Label label4;
    }
}