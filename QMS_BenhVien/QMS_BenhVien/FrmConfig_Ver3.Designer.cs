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
            this.cbPhatso = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btRefesh)).BeginInit();
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
            this.chkStartWithWindows.Location = new System.Drawing.Point(395, 143);
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
            this.btnClose.Location = new System.Drawing.Point(405, 182);
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
            this.btnSave.Location = new System.Drawing.Point(519, 182);
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
            this.cbphatthuoc.Location = new System.Drawing.Point(144, 96);
            this.cbphatthuoc.Name = "cbphatthuoc";
            this.cbphatthuoc.Size = new System.Drawing.Size(180, 24);
            this.cbphatthuoc.TabIndex = 58;
            this.cbphatthuoc.ValueMember = "Id";
            // 
            // cbvienphi
            // 
            this.cbvienphi.DisplayMember = "Name";
            this.cbvienphi.FormattingEnabled = true;
            this.cbvienphi.Location = new System.Drawing.Point(143, 66);
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
            this.label9.Location = new System.Drawing.Point(37, 94);
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
            this.label8.Location = new System.Drawing.Point(56, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 22);
            this.label8.TabIndex = 55;
            this.label8.Text = "Viện phí";
            // 
            // cbPhatso
            // 
            this.cbPhatso.DisplayMember = "Name";
            this.cbPhatso.FormattingEnabled = true;
            this.cbPhatso.Location = new System.Drawing.Point(144, 36);
            this.cbPhatso.Name = "cbPhatso";
            this.cbPhatso.Size = new System.Drawing.Size(180, 24);
            this.cbPhatso.TabIndex = 60;
            this.cbPhatso.ValueMember = "Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(62, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 59;
            this.label2.Text = "Phát số";
            // 
            // FrmConfig_Ver3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 231);
            this.Controls.Add(this.cbPhatso);
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
        private System.Windows.Forms.ComboBox cbPhatso;
        private System.Windows.Forms.Label label2;
    }
}