namespace QMS_BenhVien
{
    partial class FrmMain_Socket_TV2
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lbSocketStatus = new System.Windows.Forms.Label();
            this.pbPrintStatus = new System.Windows.Forms.PictureBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.pnDS_PK = new System.Windows.Forms.Panel();
            this.pnDK_Kham = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTemplateEditor = new System.Windows.Forms.PictureBox();
            this.btSQLConnect = new System.Windows.Forms.PictureBox();
            this.btnSetting = new System.Windows.Forms.PictureBox();
            this.btnNormalSize = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnfullscreen = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.btnMaximize = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.timerReset = new System.Windows.Forms.Timer(this.components);
            this.btBack_DSPK = new QMS_BenhVien.ButtonControl();
            this.buttonControl3 = new QMS_BenhVien.ButtonControl();
            this.btKhoaNoiSoi = new QMS_BenhVien.ButtonControl();
            this.btKhoaCLS = new QMS_BenhVien.ButtonControl();
            this.btKhoa_Ngoai = new QMS_BenhVien.ButtonControl();
            this.btKhoa_Mat = new QMS_BenhVien.ButtonControl();
            this.btKhoa_Noi = new QMS_BenhVien.ButtonControl();
            this.btKhoa_RHM = new QMS_BenhVien.ButtonControl();
            this.btKhoa_TMH = new QMS_BenhVien.ButtonControl();
            this.btKhoa_SPKhoa = new QMS_BenhVien.ButtonControl();
            this.btKhoa_Dlieu = new QMS_BenhVien.ButtonControl();
            this.btKhoa_Yhct = new QMS_BenhVien.ButtonControl();
            this.panel2.SuspendLayout();
            this.panelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrintStatus)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.pnDS_PK.SuspendLayout();
            this.pnDK_Kham.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTemplateEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSQLConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNormalSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnfullscreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panelStatus);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1221, 857);
            this.panel2.TabIndex = 8;
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.Blue;
            this.panelStatus.Controls.Add(this.lbSocketStatus);
            this.panelStatus.Controls.Add(this.pbPrintStatus);
            this.panelStatus.Controls.Add(this.lbStatus);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Location = new System.Drawing.Point(0, 820);
            this.panelStatus.Margin = new System.Windows.Forms.Padding(4);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1221, 37);
            this.panelStatus.TabIndex = 2;
            // 
            // lbSocketStatus
            // 
            this.lbSocketStatus.AutoSize = true;
            this.lbSocketStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSocketStatus.ForeColor = System.Drawing.Color.White;
            this.lbSocketStatus.Location = new System.Drawing.Point(54, 9);
            this.lbSocketStatus.Name = "lbSocketStatus";
            this.lbSocketStatus.Size = new System.Drawing.Size(108, 18);
            this.lbSocketStatus.TabIndex = 34;
            this.lbSocketStatus.Text = "Disconnected";
            this.lbSocketStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbPrintStatus
            // 
            this.pbPrintStatus.Image = global::QMS_BenhVien.Properties.Resources.iconfinder_printer_delete_36361;
            this.pbPrintStatus.Location = new System.Drawing.Point(3, 2);
            this.pbPrintStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbPrintStatus.Name = "pbPrintStatus";
            this.pbPrintStatus.Size = new System.Drawing.Size(45, 32);
            this.pbPrintStatus.TabIndex = 33;
            this.pbPrintStatus.TabStop = false;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.White;
            this.lbStatus.Location = new System.Drawing.Point(180, 6);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(65, 23);
            this.lbStatus.TabIndex = 32;
            this.lbStatus.Text = " status";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelMain);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 146);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1221, 711);
            this.panel4.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.pnDS_PK);
            this.panelMain.Controls.Add(this.pnDK_Kham);
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1221, 673);
            this.panelMain.TabIndex = 21;
            // 
            // pnDS_PK
            // 
            this.pnDS_PK.AutoScroll = true;
            this.pnDS_PK.Controls.Add(this.btBack_DSPK);
            this.pnDS_PK.Controls.Add(this.buttonControl3);
            this.pnDS_PK.Location = new System.Drawing.Point(30, 554);
            this.pnDS_PK.Margin = new System.Windows.Forms.Padding(4);
            this.pnDS_PK.Name = "pnDS_PK";
            this.pnDS_PK.Size = new System.Drawing.Size(78, 112);
            this.pnDS_PK.TabIndex = 50;
            // 
            // pnDK_Kham
            // 
            this.pnDK_Kham.AutoScroll = true;
            this.pnDK_Kham.Controls.Add(this.btKhoaNoiSoi);
            this.pnDK_Kham.Controls.Add(this.btKhoaCLS);
            this.pnDK_Kham.Controls.Add(this.btKhoa_Ngoai);
            this.pnDK_Kham.Controls.Add(this.btKhoa_Mat);
            this.pnDK_Kham.Controls.Add(this.btKhoa_Noi);
            this.pnDK_Kham.Controls.Add(this.btKhoa_RHM);
            this.pnDK_Kham.Controls.Add(this.btKhoa_TMH);
            this.pnDK_Kham.Controls.Add(this.btKhoa_SPKhoa);
            this.pnDK_Kham.Controls.Add(this.btKhoa_Dlieu);
            this.pnDK_Kham.Controls.Add(this.btKhoa_Yhct);
            this.pnDK_Kham.Location = new System.Drawing.Point(11, 6);
            this.pnDK_Kham.Margin = new System.Windows.Forms.Padding(4);
            this.pnDK_Kham.Name = "pnDK_Kham";
            this.pnDK_Kham.Size = new System.Drawing.Size(1176, 524);
            this.pnDK_Kham.TabIndex = 49;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1221, 146);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DeepPink;
            this.label3.Location = new System.Drawing.Point(3, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1213, 46);
            this.label3.TabIndex = 19;
            this.label3.Text = "ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1215, 66);
            this.label1.TabIndex = 3;
            this.label1.Text = "BỆNH VIỆN TRÀ VINH KÍNH CHÀO QUÝ KHÁCH";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.panel1.Controls.Add(this.btnTemplateEditor);
            this.panel1.Controls.Add(this.btSQLConnect);
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Controls.Add(this.btnNormalSize);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnfullscreen);
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnMaximize);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 46);
            this.panel1.TabIndex = 7;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnTemplateEditor
            // 
            this.btnTemplateEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplateEditor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemplateEditor.Image = global::QMS_BenhVien.Properties.Resources.iconfinder_ticket_54267__1_;
            this.btnTemplateEditor.Location = new System.Drawing.Point(977, 9);
            this.btnTemplateEditor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTemplateEditor.Name = "btnTemplateEditor";
            this.btnTemplateEditor.Size = new System.Drawing.Size(28, 26);
            this.btnTemplateEditor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnTemplateEditor.TabIndex = 9;
            this.btnTemplateEditor.TabStop = false;
            this.btnTemplateEditor.Visible = false;
            this.btnTemplateEditor.Click += new System.EventHandler(this.btnTemplateEditor_Click);
            // 
            // btSQLConnect
            // 
            this.btSQLConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSQLConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSQLConnect.Image = global::QMS_BenhVien.Properties.Resources.db;
            this.btSQLConnect.Location = new System.Drawing.Point(1013, 9);
            this.btSQLConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btSQLConnect.Name = "btSQLConnect";
            this.btSQLConnect.Size = new System.Drawing.Size(28, 26);
            this.btSQLConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btSQLConnect.TabIndex = 8;
            this.btSQLConnect.TabStop = false;
            this.btSQLConnect.Visible = false;
            this.btSQLConnect.Click += new System.EventHandler(this.btSQLConnect_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.Image = global::QMS_BenhVien.Properties.Resources.iconfinder_Options_105206;
            this.btnSetting.Location = new System.Drawing.Point(1047, 9);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(28, 26);
            this.btnSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSetting.TabIndex = 7;
            this.btnSetting.TabStop = false;
            this.btnSetting.Visible = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnNormalSize
            // 
            this.btnNormalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNormalSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNormalSize.Image = global::QMS_BenhVien.Properties.Resources.res;
            this.btnNormalSize.Location = new System.Drawing.Point(815, 9);
            this.btnNormalSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNormalSize.Name = "btnNormalSize";
            this.btnNormalSize.Size = new System.Drawing.Size(28, 26);
            this.btnNormalSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnNormalSize.TabIndex = 6;
            this.btnNormalSize.TabStop = false;
            this.btnNormalSize.Visible = false;
            this.btnNormalSize.Click += new System.EventHandler(this.btnNormalSize_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QMS_BenhVien.Properties.Resources.icon;
            this.pictureBox1.Location = new System.Drawing.Point(5, 6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(45, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "GPRO Gọi số - Hệ thống cấp số tự động QMS-482PT";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnfullscreen
            // 
            this.btnfullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnfullscreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnfullscreen.Image = global::QMS_BenhVien.Properties.Resources.fullscreen_icon;
            this.btnfullscreen.Location = new System.Drawing.Point(1081, 9);
            this.btnfullscreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnfullscreen.Name = "btnfullscreen";
            this.btnfullscreen.Size = new System.Drawing.Size(28, 26);
            this.btnfullscreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnfullscreen.TabIndex = 3;
            this.btnfullscreen.TabStop = false;
            this.btnfullscreen.Click += new System.EventHandler(this.btnfullscreen_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.Image = global::QMS_BenhVien.Properties.Resources.minimazar;
            this.btnMinimize.Location = new System.Drawing.Point(1113, 9);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 26);
            this.btnMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.Image = global::QMS_BenhVien.Properties.Resources.maxi;
            this.btnMaximize.Location = new System.Drawing.Point(1149, 9);
            this.btnMaximize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(28, 26);
            this.btnMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximize.TabIndex = 1;
            this.btnMaximize.TabStop = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::QMS_BenhVien.Properties.Resources.x2;
            this.btnClose.InitialImage = global::QMS_BenhVien.Properties.Resources.cerrar;
            this.btnClose.Location = new System.Drawing.Point(1185, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 26);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timerReset
            // 
            this.timerReset.Interval = 2000;
            // 
            // btBack_DSPK
            // 
            this.btBack_DSPK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btBack_DSPK.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.btBack_DSPK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btBack_DSPK.BorderRadius = 20;
            this.btBack_DSPK.BorderThickness = 3;
            this.btBack_DSPK.ButtonText = "QUAY LẠI";
            this.btBack_DSPK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btBack_DSPK.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBack_DSPK.ForeColor = System.Drawing.Color.DimGray;
            this.btBack_DSPK.Location = new System.Drawing.Point(-244, 235);
            this.btBack_DSPK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btBack_DSPK.Name = "btBack_DSPK";
            this.btBack_DSPK.Size = new System.Drawing.Size(549, 178);
            this.btBack_DSPK.TabIndex = 45;
            // 
            // buttonControl3
            // 
            this.buttonControl3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonControl3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.buttonControl3.BorderColor = System.Drawing.Color.Silver;
            this.buttonControl3.BorderRadius = 20;
            this.buttonControl3.BorderThickness = 3;
            this.buttonControl3.ButtonText = "ds pk";
            this.buttonControl3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonControl3.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonControl3.ForeColor = System.Drawing.Color.Yellow;
            this.buttonControl3.Location = new System.Drawing.Point(-170, 39);
            this.buttonControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonControl3.Name = "buttonControl3";
            this.buttonControl3.Size = new System.Drawing.Size(403, 108);
            this.buttonControl3.TabIndex = 38;
            // 
            // btKhoaNoiSoi
            // 
            this.btKhoaNoiSoi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoaNoiSoi.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoaNoiSoi.BorderColor = System.Drawing.Color.Silver;
            this.btKhoaNoiSoi.BorderRadius = 20;
            this.btKhoaNoiSoi.BorderThickness = 3;
            this.btKhoaNoiSoi.ButtonText = "KHOA NỘI SOI - THĂM DÒ CHỨC NĂNG";
            this.btKhoaNoiSoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoaNoiSoi.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoaNoiSoi.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoaNoiSoi.Location = new System.Drawing.Point(-42, 329);
            this.btKhoaNoiSoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoaNoiSoi.Name = "btKhoaNoiSoi";
            this.btKhoaNoiSoi.Size = new System.Drawing.Size(403, 178);
            this.btKhoaNoiSoi.TabIndex = 48;
            this.btKhoaNoiSoi.Click += new System.EventHandler(this.btKhoaNoiSoi_Click);
            this.btKhoaNoiSoi.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoaNoiSoi_MouseDown);
            this.btKhoaNoiSoi.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoaNoiSoi_MouseUp);
            // 
            // btKhoaCLS
            // 
            this.btKhoaCLS.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoaCLS.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoaCLS.BorderColor = System.Drawing.Color.Silver;
            this.btKhoaCLS.BorderRadius = 20;
            this.btKhoaCLS.BorderThickness = 3;
            this.btKhoaCLS.ButtonText = "KHOA CẬN LÂM SÀNG";
            this.btKhoaCLS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoaCLS.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoaCLS.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoaCLS.Location = new System.Drawing.Point(788, 233);
            this.btKhoaCLS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoaCLS.Name = "btKhoaCLS";
            this.btKhoaCLS.Size = new System.Drawing.Size(403, 178);
            this.btKhoaCLS.TabIndex = 47;
            this.btKhoaCLS.Click += new System.EventHandler(this.btKhoaCLS_Click);
            this.btKhoaCLS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoaCLS_MouseDown);
            this.btKhoaCLS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoaCLS_MouseUp);
            // 
            // btKhoa_Ngoai
            // 
            this.btKhoa_Ngoai.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_Ngoai.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_Ngoai.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_Ngoai.BorderRadius = 20;
            this.btKhoa_Ngoai.BorderThickness = 3;
            this.btKhoa_Ngoai.ButtonText = "KHOA NGOẠI";
            this.btKhoa_Ngoai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_Ngoai.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_Ngoai.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_Ngoai.Location = new System.Drawing.Point(373, 17);
            this.btKhoa_Ngoai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_Ngoai.Name = "btKhoa_Ngoai";
            this.btKhoa_Ngoai.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_Ngoai.TabIndex = 36;
            this.btKhoa_Ngoai.Click += new System.EventHandler(this.btKhoa_Ngoai_Click);
            this.btKhoa_Ngoai.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Ngoai_MouseDown);
            this.btKhoa_Ngoai.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Ngoai_MouseUp);
            // 
            // btKhoa_Mat
            // 
            this.btKhoa_Mat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_Mat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_Mat.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_Mat.BorderRadius = 20;
            this.btKhoa_Mat.BorderThickness = 3;
            this.btKhoa_Mat.ButtonText = "KHOA MẮT";
            this.btKhoa_Mat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_Mat.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_Mat.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_Mat.Location = new System.Drawing.Point(788, 123);
            this.btKhoa_Mat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_Mat.Name = "btKhoa_Mat";
            this.btKhoa_Mat.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_Mat.TabIndex = 43;
            this.btKhoa_Mat.Click += new System.EventHandler(this.btKhoa_Mat_Click);
            this.btKhoa_Mat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Mat_MouseDown);
            this.btKhoa_Mat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Mat_MouseUp);
            // 
            // btKhoa_Noi
            // 
            this.btKhoa_Noi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_Noi.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_Noi.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_Noi.BorderRadius = 20;
            this.btKhoa_Noi.BorderThickness = 3;
            this.btKhoa_Noi.ButtonText = "KHOA KHÁM BỆNH";
            this.btKhoa_Noi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_Noi.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_Noi.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_Noi.Location = new System.Drawing.Point(-42, 17);
            this.btKhoa_Noi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_Noi.Name = "btKhoa_Noi";
            this.btKhoa_Noi.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_Noi.TabIndex = 35;
            this.btKhoa_Noi.Click += new System.EventHandler(this.btKhoa_Noi_Click);
            this.btKhoa_Noi.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Noi_MouseDown);
            this.btKhoa_Noi.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Noi_MouseUp);
            // 
            // btKhoa_RHM
            // 
            this.btKhoa_RHM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_RHM.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_RHM.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_RHM.BorderRadius = 20;
            this.btKhoa_RHM.BorderThickness = 3;
            this.btKhoa_RHM.ButtonText = "KHOA RĂNG HÀM MẶT";
            this.btKhoa_RHM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_RHM.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_RHM.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_RHM.Location = new System.Drawing.Point(373, 233);
            this.btKhoa_RHM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_RHM.Name = "btKhoa_RHM";
            this.btKhoa_RHM.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_RHM.TabIndex = 42;
            this.btKhoa_RHM.Click += new System.EventHandler(this.btKhoa_RHM_Click);
            this.btKhoa_RHM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_RHM_MouseDown);
            this.btKhoa_RHM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_RHM_MouseUp);
            // 
            // btKhoa_TMH
            // 
            this.btKhoa_TMH.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_TMH.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_TMH.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_TMH.BorderRadius = 20;
            this.btKhoa_TMH.BorderThickness = 3;
            this.btKhoa_TMH.ButtonText = "KHOA TAI MŨI HỌNG";
            this.btKhoa_TMH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_TMH.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_TMH.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_TMH.Location = new System.Drawing.Point(-42, 233);
            this.btKhoa_TMH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_TMH.Name = "btKhoa_TMH";
            this.btKhoa_TMH.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_TMH.TabIndex = 41;
            this.btKhoa_TMH.Click += new System.EventHandler(this.btKhoa_TMH_Click);
            this.btKhoa_TMH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_TMH_MouseDown);
            this.btKhoa_TMH.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_TMH_MouseUp);
            // 
            // btKhoa_SPKhoa
            // 
            this.btKhoa_SPKhoa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_SPKhoa.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_SPKhoa.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_SPKhoa.BorderRadius = 20;
            this.btKhoa_SPKhoa.BorderThickness = 3;
            this.btKhoa_SPKhoa.ButtonText = "KHOA VẬT LÝ TRỊ LIỆU KKB";
            this.btKhoa_SPKhoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_SPKhoa.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_SPKhoa.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_SPKhoa.Location = new System.Drawing.Point(-42, 123);
            this.btKhoa_SPKhoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_SPKhoa.Name = "btKhoa_SPKhoa";
            this.btKhoa_SPKhoa.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_SPKhoa.TabIndex = 38;
            this.btKhoa_SPKhoa.Click += new System.EventHandler(this.btKhoa_SPKhoa_Click);
            this.btKhoa_SPKhoa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_SPKhoa_MouseDown);
            this.btKhoa_SPKhoa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_SPKhoa_MouseUp);
            // 
            // btKhoa_Dlieu
            // 
            this.btKhoa_Dlieu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_Dlieu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_Dlieu.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_Dlieu.BorderRadius = 20;
            this.btKhoa_Dlieu.BorderThickness = 3;
            this.btKhoa_Dlieu.ButtonText = "KHOA VẬT LÝ TRỊ LIỆU KKA";
            this.btKhoa_Dlieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_Dlieu.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_Dlieu.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_Dlieu.Location = new System.Drawing.Point(373, 123);
            this.btKhoa_Dlieu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_Dlieu.Name = "btKhoa_Dlieu";
            this.btKhoa_Dlieu.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_Dlieu.TabIndex = 40;
            this.btKhoa_Dlieu.Click += new System.EventHandler(this.btKhoa_Dlieu_Click);
            this.btKhoa_Dlieu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Dlieu_MouseDown);
            this.btKhoa_Dlieu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Dlieu_MouseUp);
            // 
            // btKhoa_Yhct
            // 
            this.btKhoa_Yhct.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btKhoa_Yhct.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btKhoa_Yhct.BorderColor = System.Drawing.Color.Silver;
            this.btKhoa_Yhct.BorderRadius = 20;
            this.btKhoa_Yhct.BorderThickness = 3;
            this.btKhoa_Yhct.ButtonText = "Y HỌC CỔ TRUYỀN";
            this.btKhoa_Yhct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btKhoa_Yhct.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKhoa_Yhct.ForeColor = System.Drawing.Color.Yellow;
            this.btKhoa_Yhct.Location = new System.Drawing.Point(788, 17);
            this.btKhoa_Yhct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btKhoa_Yhct.Name = "btKhoa_Yhct";
            this.btKhoa_Yhct.Size = new System.Drawing.Size(403, 178);
            this.btKhoa_Yhct.TabIndex = 39;
            this.btKhoa_Yhct.Click += new System.EventHandler(this.btKhoa_Yhct_Click);
            this.btKhoa_Yhct.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Yhct_MouseDown);
            this.btKhoa_Yhct.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btKhoa_Yhct_MouseUp);
            // 
            // FrmMain_Socket_TV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 903);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain_Socket_TV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GPRO-QMS-482PT";
            this.Load += new System.EventHandler(this.FrmMain_TV2_Load);
            this.panel2.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrintStatus)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.pnDS_PK.ResumeLayout(false);
            this.pnDK_Kham.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTemplateEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSQLConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNormalSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnfullscreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lbSocketStatus;
        private System.Windows.Forms.PictureBox pbPrintStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnTemplateEditor;
        private System.Windows.Forms.PictureBox btSQLConnect;
        private System.Windows.Forms.PictureBox btnSetting;
        private System.Windows.Forms.PictureBox btnNormalSize;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnfullscreen;
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.PictureBox btnMaximize;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Timer timerReset;
        private System.Windows.Forms.Panel pnDK_Kham;
        private ButtonControl btKhoa_Ngoai;
        private ButtonControl btKhoa_Mat;
        private ButtonControl btKhoa_Noi;
        private ButtonControl btKhoa_RHM;
        private ButtonControl btKhoa_TMH;
        private ButtonControl btKhoa_SPKhoa;
        private ButtonControl btKhoa_Dlieu;
        private ButtonControl btKhoa_Yhct;
        private System.Windows.Forms.Panel pnDS_PK;
        private ButtonControl btBack_DSPK;
        private ButtonControl buttonControl3;
        private ButtonControl btKhoaNoiSoi;
        private ButtonControl btKhoaCLS;
    }
}