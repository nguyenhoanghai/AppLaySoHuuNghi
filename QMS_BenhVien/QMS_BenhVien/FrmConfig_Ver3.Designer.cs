namespace QMS_BenhVien
{
    partial class FrmConfig_Ver3
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
            this.btnsettingnut = new System.Windows.Forms.Button();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.btnChangePicture = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCOMPrint = new System.Windows.Forms.ComboBox();
            this.btRefesh = new System.Windows.Forms.PictureBox();
            this.cbphatthuoc = new System.Windows.Forms.ComboBox();
            this.cbvienphi = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbKhamUT = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbKhamBHYT = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbKhamKoBHYT = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPThuocUT = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTieuDuong = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numWidth = new System.Windows.Forms.TextBox();
            this.numHeight = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btRefesh)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnsettingnut
            // 
            this.btnsettingnut.Location = new System.Drawing.Point(395, 67);
            this.btnsettingnut.Name = "btnsettingnut";
            this.btnsettingnut.Size = new System.Drawing.Size(232, 32);
            this.btnsettingnut.TabIndex = 35;
            this.btnsettingnut.Text = "Cấu hình nút dịch vụ";
            this.btnsettingnut.UseVisualStyleBackColor = true;
            this.btnsettingnut.Click += new System.EventHandler(this.btnsettingnut_Click);
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.Location = new System.Drawing.Point(395, 236);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(189, 21);
            this.chkStartWithWindows.TabIndex = 42;
            this.chkStartWithWindows.Text = "Khởi động cùng Windows";
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // btnChangePicture
            // 
            this.btnChangePicture.Location = new System.Drawing.Point(395, 105);
            this.btnChangePicture.Name = "btnChangePicture";
            this.btnChangePicture.Size = new System.Drawing.Size(232, 32);
            this.btnChangePicture.TabIndex = 41;
            this.btnChangePicture.Text = "Chọn ảnh nền";
            this.btnChangePicture.UseVisualStyleBackColor = true;
            this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(405, 278);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 32);
            this.btnClose.TabIndex = 40;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Location = new System.Drawing.Point(519, 278);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 32);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(396, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 47;
            this.label1.Text = "COM máy in";
            // 
            // cbCOMPrint
            // 
            this.cbCOMPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCOMPrint.FormattingEnabled = true;
            this.cbCOMPrint.Location = new System.Drawing.Point(395, 33);
            this.cbCOMPrint.Name = "cbCOMPrint";
            this.cbCOMPrint.Size = new System.Drawing.Size(189, 28);
            this.cbCOMPrint.TabIndex = 48;
            // 
            // btRefesh
            // 
            this.btRefesh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btRefesh.Image = global::QMS_BenhVien.Properties.Resources.if_Synchronize_27883;
            this.btRefesh.Location = new System.Drawing.Point(590, 33);
            this.btRefesh.Name = "btRefesh";
            this.btRefesh.Size = new System.Drawing.Size(37, 28);
            this.btRefesh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btRefesh.TabIndex = 54;
            this.btRefesh.TabStop = false;
            this.btRefesh.Click += new System.EventHandler(this.btRefesh_Click);
            // 
            // cbphatthuoc
            // 
            this.cbphatthuoc.DisplayMember = "Name";
            this.cbphatthuoc.FormattingEnabled = true;
            this.cbphatthuoc.Location = new System.Drawing.Point(186, 71);
            this.cbphatthuoc.Name = "cbphatthuoc";
            this.cbphatthuoc.Size = new System.Drawing.Size(180, 24);
            this.cbphatthuoc.TabIndex = 58;
            this.cbphatthuoc.ValueMember = "Id";
            // 
            // cbvienphi
            // 
            this.cbvienphi.DisplayMember = "Name";
            this.cbvienphi.FormattingEnabled = true;
            this.cbvienphi.Location = new System.Drawing.Point(185, 41);
            this.cbvienphi.Name = "cbvienphi";
            this.cbvienphi.Size = new System.Drawing.Size(180, 24);
            this.cbvienphi.TabIndex = 57;
            this.cbvienphi.ValueMember = "Id";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(80, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 22);
            this.label9.TabIndex = 56;
            this.label9.Text = "Phát thuốc";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(99, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 22);
            this.label8.TabIndex = 55;
            this.label8.Text = "Viện phí";
            // 
            // cbKhamUT
            // 
            this.cbKhamUT.DisplayMember = "Name";
            this.cbKhamUT.FormattingEnabled = true;
            this.cbKhamUT.Location = new System.Drawing.Point(186, 11);
            this.cbKhamUT.Name = "cbKhamUT";
            this.cbKhamUT.Size = new System.Drawing.Size(180, 24);
            this.cbKhamUT.TabIndex = 60;
            this.cbKhamUT.ValueMember = "Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(58, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 22);
            this.label2.TabIndex = 59;
            this.label2.Text = "Khám ưu tiên";
            // 
            // cbKhamBHYT
            // 
            this.cbKhamBHYT.DisplayMember = "Name";
            this.cbKhamBHYT.FormattingEnabled = true;
            this.cbKhamBHYT.Location = new System.Drawing.Point(186, 131);
            this.cbKhamBHYT.Name = "cbKhamBHYT";
            this.cbKhamBHYT.Size = new System.Drawing.Size(180, 24);
            this.cbKhamBHYT.TabIndex = 62;
            this.cbKhamBHYT.ValueMember = "Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(66, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 22);
            this.label3.TabIndex = 61;
            this.label3.Text = "Khám BHYT";
            // 
            // cbKhamKoBHYT
            // 
            this.cbKhamKoBHYT.DisplayMember = "Name";
            this.cbKhamKoBHYT.FormattingEnabled = true;
            this.cbKhamKoBHYT.Location = new System.Drawing.Point(186, 161);
            this.cbKhamKoBHYT.Name = "cbKhamKoBHYT";
            this.cbKhamKoBHYT.Size = new System.Drawing.Size(180, 24);
            this.cbKhamKoBHYT.TabIndex = 64;
            this.cbKhamKoBHYT.ValueMember = "Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(10, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 22);
            this.label4.TabIndex = 63;
            this.label4.Text = "Khám không BHYT";
            // 
            // cbPThuocUT
            // 
            this.cbPThuocUT.DisplayMember = "Name";
            this.cbPThuocUT.FormattingEnabled = true;
            this.cbPThuocUT.Location = new System.Drawing.Point(185, 101);
            this.cbPThuocUT.Name = "cbPThuocUT";
            this.cbPThuocUT.Size = new System.Drawing.Size(180, 24);
            this.cbPThuocUT.TabIndex = 66;
            this.cbPThuocUT.ValueMember = "Id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(17, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 22);
            this.label5.TabIndex = 65;
            this.label5.Text = "Phát thuốc ưu tiên";
            // 
            // cbTieuDuong
            // 
            this.cbTieuDuong.DisplayMember = "Name";
            this.cbTieuDuong.FormattingEnabled = true;
            this.cbTieuDuong.Location = new System.Drawing.Point(186, 191);
            this.cbTieuDuong.Name = "cbTieuDuong";
            this.cbTieuDuong.Size = new System.Drawing.Size(180, 24);
            this.cbTieuDuong.TabIndex = 68;
            this.cbTieuDuong.ValueMember = "Id";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(10, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 22);
            this.label6.TabIndex = 67;
            this.label6.Text = "T.Mạch tiểu đường";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numWidth);
            this.groupBox2.Controls.Add(this.numHeight);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(395, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 87);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cỡ giấy in";
            // 
            // numWidth
            // 
            this.numWidth.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWidth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numWidth.Location = new System.Drawing.Point(91, 53);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(68, 23);
            this.numWidth.TabIndex = 38;
            // 
            // numHeight
            // 
            this.numHeight.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numHeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.numHeight.Location = new System.Drawing.Point(91, 25);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(68, 23);
            this.numHeight.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(9, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 17);
            this.label11.TabIndex = 37;
            this.label11.Text = "Ngang";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(29, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 17);
            this.label10.TabIndex = 36;
            this.label10.Text = "Cao";
            // 
            // FrmConfig_Ver3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 326);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbTieuDuong);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbPThuocUT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbKhamKoBHYT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbKhamBHYT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbKhamUT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbphatthuoc);
            this.Controls.Add(this.cbvienphi);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btRefesh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCOMPrint);
            this.Controls.Add(this.chkStartWithWindows);
            this.Controls.Add(this.btnChangePicture);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnsettingnut);
            this.Name = "FrmConfig_Ver3";
            this.Text = "Cấu hình hệ thống";
            this.Load += new System.EventHandler(this.FrmConfig_Ver3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btRefesh)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnsettingnut;
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.Button btnChangePicture;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCOMPrint;
        private System.Windows.Forms.PictureBox btRefesh;
        private System.Windows.Forms.ComboBox cbphatthuoc;
        private System.Windows.Forms.ComboBox cbvienphi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbKhamUT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbKhamBHYT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbKhamKoBHYT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPThuocUT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTieuDuong;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox numWidth;
        private System.Windows.Forms.TextBox numHeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}