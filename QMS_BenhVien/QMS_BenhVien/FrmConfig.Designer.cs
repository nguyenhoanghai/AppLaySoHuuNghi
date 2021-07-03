namespace QMS_BenhVien
{
    partial class FrmConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPermissions = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cbPhongkham = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cblaymau = new System.Windows.Forms.ComboBox();
            this.cbketqua = new System.Windows.Forms.ComboBox();
            this.cbxquang = new System.Windows.Forms.ComboBox();
            this.cbsieuam = new System.Windows.Forms.ComboBox();
            this.cbvienphi = new System.Windows.Forms.ComboBox();
            this.cbphatthuoc = new System.Windows.Forms.ComboBox();
            this.cbtieptan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsolien = new System.Windows.Forms.NumericUpDown();
            this.btnChangePicture = new System.Windows.Forms.Button();
            this.btnsettingnut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numWidth = new System.Windows.Forms.TextBox();
            this.numHeight = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbAppType = new System.Windows.Forms.ComboBox();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.numResetForm = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.cbCTRoom = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPhongkham.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsolien)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResetForm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPermissions);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(255, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Khoa";
            // 
            // chkPermissions
            // 
            this.chkPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPermissions.Location = new System.Drawing.Point(4, 25);
            this.chkPermissions.Name = "chkPermissions";
            this.chkPermissions.Size = new System.Drawing.Size(247, 318);
            this.chkPermissions.TabIndex = 6;
            // 
            // cbPhongkham
            // 
            this.cbPhongkham.EditValue = "";
            this.cbPhongkham.Location = new System.Drawing.Point(293, 37);
            this.cbPhongkham.Name = "cbPhongkham";
            this.cbPhongkham.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPhongkham.Properties.Appearance.Options.UseFont = true;
            this.cbPhongkham.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPhongkham.Size = new System.Drawing.Size(290, 26);
            this.cbPhongkham.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(287, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Phòng khám";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(393, 519);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Location = new System.Drawing.Point(507, 519);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(290, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Phòng lấy mẫu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(290, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 22);
            this.label5.TabIndex = 13;
            this.label5.Text = "Phòng trả KQ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(290, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 22);
            this.label6.TabIndex = 17;
            this.label6.Text = "Phòng X-Quang";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(290, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 22);
            this.label7.TabIndex = 15;
            this.label7.Text = "Phòng siêu âm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(290, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 22);
            this.label4.TabIndex = 21;
            this.label4.Text = "Tiếp tân";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(290, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 22);
            this.label8.TabIndex = 19;
            this.label8.Text = "Phòng viện phí";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(289, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 22);
            this.label9.TabIndex = 23;
            this.label9.Text = "Phòng phát thuốc";
            // 
            // cblaymau
            // 
            this.cblaymau.DisplayMember = "Name";
            this.cblaymau.FormattingEnabled = true;
            this.cblaymau.Location = new System.Drawing.Point(435, 65);
            this.cblaymau.Name = "cblaymau";
            this.cblaymau.Size = new System.Drawing.Size(180, 30);
            this.cblaymau.TabIndex = 24;
            this.cblaymau.ValueMember = "Id";
            this.cblaymau.SelectedIndexChanged += new System.EventHandler(this.cblaymau_SelectedIndexChanged);
            // 
            // cbketqua
            // 
            this.cbketqua.FormattingEnabled = true;
            this.cbketqua.Location = new System.Drawing.Point(435, 96);
            this.cbketqua.Name = "cbketqua";
            this.cbketqua.Size = new System.Drawing.Size(180, 30);
            this.cbketqua.TabIndex = 25;
            // 
            // cbxquang
            // 
            this.cbxquang.DisplayMember = "Name";
            this.cbxquang.FormattingEnabled = true;
            this.cbxquang.Location = new System.Drawing.Point(435, 127);
            this.cbxquang.Name = "cbxquang";
            this.cbxquang.Size = new System.Drawing.Size(180, 30);
            this.cbxquang.TabIndex = 26;
            this.cbxquang.ValueMember = "Id";
            // 
            // cbsieuam
            // 
            this.cbsieuam.DisplayMember = "Name";
            this.cbsieuam.FormattingEnabled = true;
            this.cbsieuam.Location = new System.Drawing.Point(435, 159);
            this.cbsieuam.Name = "cbsieuam";
            this.cbsieuam.Size = new System.Drawing.Size(180, 30);
            this.cbsieuam.TabIndex = 27;
            this.cbsieuam.ValueMember = "Id";
            // 
            // cbvienphi
            // 
            this.cbvienphi.DisplayMember = "Name";
            this.cbvienphi.FormattingEnabled = true;
            this.cbvienphi.Location = new System.Drawing.Point(435, 190);
            this.cbvienphi.Name = "cbvienphi";
            this.cbvienphi.Size = new System.Drawing.Size(180, 30);
            this.cbvienphi.TabIndex = 28;
            this.cbvienphi.ValueMember = "Id";
            // 
            // cbphatthuoc
            // 
            this.cbphatthuoc.DisplayMember = "Name";
            this.cbphatthuoc.FormattingEnabled = true;
            this.cbphatthuoc.Location = new System.Drawing.Point(435, 221);
            this.cbphatthuoc.Name = "cbphatthuoc";
            this.cbphatthuoc.Size = new System.Drawing.Size(180, 30);
            this.cbphatthuoc.TabIndex = 29;
            this.cbphatthuoc.ValueMember = "Id";
            // 
            // cbtieptan
            // 
            this.cbtieptan.DisplayMember = "Name";
            this.cbtieptan.FormattingEnabled = true;
            this.cbtieptan.Location = new System.Drawing.Point(435, 252);
            this.cbtieptan.Name = "cbtieptan";
            this.cbtieptan.Size = new System.Drawing.Size(180, 30);
            this.cbtieptan.TabIndex = 30;
            this.cbtieptan.ValueMember = "Id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(290, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 22);
            this.label1.TabIndex = 31;
            this.label1.Text = "Số liên in phiếu";
            // 
            // txtsolien
            // 
            this.txtsolien.Location = new System.Drawing.Point(435, 316);
            this.txtsolien.Name = "txtsolien";
            this.txtsolien.Size = new System.Drawing.Size(43, 29);
            this.txtsolien.TabIndex = 32;
            this.txtsolien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnChangePicture
            // 
            this.btnChangePicture.Location = new System.Drawing.Point(291, 452);
            this.btnChangePicture.Name = "btnChangePicture";
            this.btnChangePicture.Size = new System.Drawing.Size(322, 32);
            this.btnChangePicture.TabIndex = 33;
            this.btnChangePicture.Text = "Chọn ảnh nền";
            this.btnChangePicture.UseVisualStyleBackColor = true;
            this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click);
            // 
            // btnsettingnut
            // 
            this.btnsettingnut.Location = new System.Drawing.Point(14, 436);
            this.btnsettingnut.Name = "btnsettingnut";
            this.btnsettingnut.Size = new System.Drawing.Size(181, 32);
            this.btnsettingnut.TabIndex = 34;
            this.btnsettingnut.Text = "Cấu hình nút dịch vụ";
            this.btnsettingnut.UseVisualStyleBackColor = true;
            this.btnsettingnut.Click += new System.EventHandler(this.btnsettingnut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numWidth);
            this.groupBox2.Controls.Add(this.numHeight);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(291, 383);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 63);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cỡ giấy in";
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(251, 25);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(68, 29);
            this.numWidth.TabIndex = 38;
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(55, 25);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(68, 29);
            this.numHeight.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(169, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 22);
            this.label11.TabIndex = 37;
            this.label11.Text = "Ngang";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(6, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 22);
            this.label10.TabIndex = 36;
            this.label10.Text = "Cao";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(12, 362);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 22);
            this.label12.TabIndex = 36;
            this.label12.Text = "Loại KIOSK";
            // 
            // cbAppType
            // 
            this.cbAppType.DisplayMember = "Name";
            this.cbAppType.FormattingEnabled = true;
            this.cbAppType.Location = new System.Drawing.Point(12, 387);
            this.cbAppType.Name = "cbAppType";
            this.cbAppType.Size = new System.Drawing.Size(251, 30);
            this.cbAppType.TabIndex = 37;
            this.cbAppType.ValueMember = "Id";
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.Location = new System.Drawing.Point(294, 490);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(248, 26);
            this.chkStartWithWindows.TabIndex = 38;
            this.chkStartWithWindows.Text = "Khởi động cùng Windows";
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // numResetForm
            // 
            this.numResetForm.Location = new System.Drawing.Point(435, 348);
            this.numResetForm.Name = "numResetForm";
            this.numResetForm.Size = new System.Drawing.Size(76, 29);
            this.numResetForm.TabIndex = 40;
            this.numResetForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(290, 350);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(143, 22);
            this.label13.TabIndex = 39;
            this.label13.Text = "TGian xóa trắng";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(517, 350);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 22);
            this.label14.TabIndex = 41;
            this.label14.Text = "giây";
            // 
            // btRefresh
            // 
            this.btRefresh.Image = global::QMS_BenhVien.Properties.Resources.if_Synchronize_27883;
            this.btRefresh.Location = new System.Drawing.Point(588, 36);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(27, 23);
            this.btRefresh.TabIndex = 42;
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // cbCTRoom
            // 
            this.cbCTRoom.DisplayMember = "Name";
            this.cbCTRoom.FormattingEnabled = true;
            this.cbCTRoom.Location = new System.Drawing.Point(435, 283);
            this.cbCTRoom.Name = "cbCTRoom";
            this.cbCTRoom.Size = new System.Drawing.Size(180, 30);
            this.cbCTRoom.TabIndex = 44;
            this.cbCTRoom.ValueMember = "Id";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(290, 286);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 22);
            this.label15.TabIndex = 43;
            this.label15.Text = "Phòng CT";
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 558);
            this.Controls.Add(this.cbCTRoom);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.numResetForm);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.chkStartWithWindows);
            this.Controls.Add(this.cbAppType);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnsettingnut);
            this.Controls.Add(this.btnChangePicture);
            this.Controls.Add(this.txtsolien);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbtieptan);
            this.Controls.Add(this.cbphatthuoc);
            this.Controls.Add(this.cbvienphi);
            this.Controls.Add(this.cbsieuam);
            this.Controls.Add(this.cbxquang);
            this.Controls.Add(this.cbketqua);
            this.Controls.Add(this.cblaymau);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbPhongkham);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmConfig";
            this.Text = "Cấu hình hệ thống";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPermissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPhongkham.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsolien)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResetForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbPhongkham;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private DevExpress.XtraEditors.CheckedListBoxControl chkPermissions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cblaymau;
        private System.Windows.Forms.ComboBox cbketqua;
        private System.Windows.Forms.ComboBox cbxquang;
        private System.Windows.Forms.ComboBox cbsieuam;
        private System.Windows.Forms.ComboBox cbvienphi;
        private System.Windows.Forms.ComboBox cbphatthuoc;
        private System.Windows.Forms.ComboBox cbtieptan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtsolien;
        private System.Windows.Forms.Button btnChangePicture;
        private System.Windows.Forms.Button btnsettingnut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox numWidth;
        private System.Windows.Forms.TextBox numHeight;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbAppType;
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.NumericUpDown numResetForm;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.ComboBox cbCTRoom;
        private System.Windows.Forms.Label label15;
    }
}