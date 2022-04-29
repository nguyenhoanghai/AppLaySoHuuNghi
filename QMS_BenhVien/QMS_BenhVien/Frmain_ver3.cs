using GPRO.Core.Hai;
using Microsoft.Win32;
using Newtonsoft.Json;
using QMS_System.Data.BLL;
using QMS_System.Data.BLL.HuuNghi;
using QMS_System.Data.Enum;
using QMS_System.Data.Model;
using QMS_System.ThirdApp.Enum;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    public partial class Frmain_ver3 : Form
    {
        string currentPanel = FormPanelSate.mainMenu;
        string connectString = BaseCore.Instance.GetEntityConnectString(Application.StartupPath + "\\DATA.XML"),
            ticketTemplate = string.Empty,
            comName = string.Empty,
            errorsms = string.Empty,
            nodeServerIP = "192.168.1.10:3000";
        List<ServiceModel> services = new List<ServiceModel>();
        ButtonStyleModel btStyle = new ButtonStyleModel() { Height = 100, Width = 100, ButtonInRow = 5, Margin = 10, fontStyle = "Arial, 36pt, style=Bold", BackColor = "#ffffff", ForeColor = "#0000ff" };
        Helper.ConfigModel cfObj = null;
        public static SerialPort COM_Printer = new SerialPort();
        public static List<PrintTicketModel> printTemplates;
        List<QMS_System.Data.Model.ConfigModel> configs;
        List<ServiceDayModel> lib_Services;

        int printType = 1,
            startNumber = 1,
              printTicketReturnCurrentNumberOrServiceCode = 1,
            UseWithThirdPattern = 0,
            CheckTimeBeforePrintTicket = 0,
             vienphiId = 0,
            phatthuocId = 0,
        phatsoId = 0;
        FrmMessagebox messagebox;
        PrintModel printModel = null;

        public Frmain_ver3()
        {
            InitializeComponent();
        }

        Socket socket;
        private void ConnectSocketIO()
        {
            if (!string.IsNullOrEmpty(nodeServerIP))
            {
                try
                {
                    string _nodeIP = @"http://" + nodeServerIP;
                    // MessageBox.Show(_nodeIP);
                    socket = IO.Socket(_nodeIP);
                    socket.Connect();
                    socket.On(Socket.EVENT_CONNECT, () =>
                    {
                        SetText("Connected");
                        socket.On(Socket.EVENT_DISCONNECT, () =>
                        {
                            SetText("Disconnected");
                        });
                    });

                    // socket.Emit("client-send-id", id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtInfo_w.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lbSocketStatus.Text = text;
                if (text == "Connected")
                    lbSocketStatus.ForeColor = Color.YellowGreen;
                else
                    lbSocketStatus.ForeColor = Color.Red;
            }
        }


        private void Frmain_ver3_Load(object sender, EventArgs e)
        {
            CheckState();
            GetConfig();
            ConnectSocketIO();
            InitCOM_Printer();
        }

        private void GetConfig()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                          ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var aa=  registryKey.GetValue("QMS_BenhVien" );
                var bb=  registryKey.GetValueNames( );


                if (ConfigurationManager.AppSettings["isAdmin"] != null &&
                        !string.IsNullOrEmpty(ConfigurationManager.AppSettings["isAdmin"].ToString()) &&
                       ConfigurationManager.AppSettings["isAdmin"].ToString() == "1")
                {
                    btnTemplateEditor.Visible = true;
                    btnSetting.Visible = true;
                    btSQLConnect.Visible = true;
                }

                string filePath = Application.StartupPath + "\\Config.XML";
                cfObj = QMS_BenhVien.Helper.Helper.Instance.GetAppConfig(filePath);
                comName = cfObj.COMName;
                vienphiId = cfObj.vienphi;
                phatthuocId = cfObj.phatthuoc;
                phatsoId = cfObj.tieptan;

                if (!string.IsNullOrEmpty(cfObj.button_style))
                    btStyle = JsonConvert.DeserializeObject<ButtonStyleModel>(cfObj.button_style);

                lib_Services = BLLService.Instance.GetsForMain(connectString);
                services = BLLService.Instance.Gets(connectString);
                configs = BLLConfig.Instance.Gets(connectString, true);
                int.TryParse(GetConfigByCode(eConfigCode.PrintType), out printType);
                int.TryParse(GetConfigByCode(eConfigCode.CheckTimeBeforePrintTicket), out CheckTimeBeforePrintTicket);
                int.TryParse(GetConfigByCode(eConfigCode.PrintTicketReturnCurrentNumberOrServiceCode), out printTicketReturnCurrentNumberOrServiceCode);
                int.TryParse(GetConfigByCode(eConfigCode.StartNumber), out startNumber);
                int.TryParse(GetConfigByCode(eConfigCode.UseWithThirdPattern), out UseWithThirdPattern);
                nodeServerIP = GetConfigByCode(eConfigCode.NodeServerIP);

                printTemplates = BLLPrintTemplate.Instance.Gets(connectString).Where(x => x.IsActive).ToList();
                
            }
            catch { }
        }

        private string GetConfigByCode(string code)
        {
            if (configs != null && configs.Count > 0)
            {
                var obj = configs.FirstOrDefault(x => x.Code.Trim().ToUpper().Equals(code.Trim().ToUpper()));
                return obj != null ? obj.Value : string.Empty;
            }
            return string.Empty;
        }


        private void InitCOM_Printer()
        {
            try
            {
                COM_Printer.PortName = comName;
                COM_Printer.BaudRate = 9600;
                COM_Printer.DataBits = 8;
                COM_Printer.Parity = Parity.None;
                COM_Printer.StopBits = StopBits.One;

                try
                {
                    COM_Printer.ReadTimeout = 1;
                    COM_Printer.WriteTimeout = 1;
                    COM_Printer.Open();
                    pbPrintStatus.Image = global::QMS_BenhVien.Properties.Resources.iconfinder_printer_remote_30279;
                    //  COM_Printer.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived); 
                }
                catch (Exception)
                {
                    // MessageBox.Show("Lỗi: không thể kết nối với cổng COM Máy in, Vui lòng thử cấu hình lại kết nối", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Lấy thông tin Com Máy in bị lỗi.\n" + ex.Message, "Lỗi Com Máy in");
            }
        }

        private void _showMessage(int type, string message)
        {
            messagebox = new FrmMessagebox(type, message);
            messagebox.ShowDialog();
        }

        #region kéo form 
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        #region form event   
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            WindowState = FormWindowState.Maximized;
            btnMaximize.Visible = false;
            btnNormalSize.Visible = true;
            btnNormalSize.Location = new Point(btnMaximize.Location.X, btnMaximize.Location.Y);
            // if (permis != null)
            // checkPermission();

            CheckState();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (FrmConfirmbox.ShowDialog(this, "Có", "Không", "Bạn có thật sự muốn đóng chương trình không ?") == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            //if (permis != null)
            //checkPermission();
        }

        private void btnfullscreen_Click(object sender, EventArgs e)
        {
            btnMaximize_Click(sender, e);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            var frm = new FrmConfig_Ver3();
            frm.ShowDialog();
        }

        private void btSQLConnect_Click(object sender, EventArgs e)
        {
            var frm = new frmSQLConnect();
            frm.ShowDialog();
        }

        private void btnTemplateEditor_Click(object sender, EventArgs e)
        {
            var frm = new FrmDesignTicket();
            frm.ShowDialog();
        }

        private void btnNormalSize_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            WindowState = FormWindowState.Normal;
            btnMaximize.Visible = true;
            btnNormalSize.Visible = false;
            //if (permis != null)
            // checkPermission();

            CheckState();
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnNormalSize_Click(sender, e);
        }
        #endregion

        private void mainPanelRefresh()
        {
            int width = panelMain.Width,
                height = panelMain.Height,
                butWith = 0,
                butHeight = 0;

            butWith = ((width - 40) / 3);
            butHeight = ((height - 40) / 3);

            btnDK_KB.Width = butWith;
            btnDK_KB.Height = butHeight;
            btnDK_KB.Location = new Point(10, 10);

            btnSTTSMS.Width = butWith;
            btnSTTSMS.Height = butHeight;
            btnSTTSMS.Location = new Point(20 + butWith, 10);

            btnSTTWeb.Width = butWith;
            btnSTTWeb.Height = butHeight;
            btnSTTWeb.Location = new Point(30 + (butWith * 2), 10);


            btnBHYT.Width = butWith;
            btnBHYT.Height = butHeight;
            btnBHYT.Location = new Point(10, 20 + butHeight);

            btnTK_BHYT.Width = butWith;
            btnTK_BHYT.Height = butHeight;
            btnTK_BHYT.Location = new Point(20 + butWith, 20 + butHeight);

            btnTK_DV.Width = butWith;
            btnTK_DV.Height = butHeight;
            btnTK_DV.Location = new Point(30 + (butWith * 2), 20 + butHeight);


            btnPhatThuoc.Width = butWith;
            btnPhatThuoc.Height = butHeight;
            btnPhatThuoc.Location = new Point(10, 30 + (butHeight * 2));

            btnVienPhi.Width = butWith;
            btnVienPhi.Height = butHeight;
            btnVienPhi.Location = new Point(20 + butWith, 30 + (butHeight * 2));

            btnCanLamSan.Width = butWith;
            btnCanLamSan.Height = butHeight;
            btnCanLamSan.Location = new Point(30 + (butWith * 2), 30 + (butHeight * 2));
        }

        private void dsKhoaPanelRefresh()
        {
            int width = pnDK_Kham.Width,
                height = pnDK_Kham.Height,
                butWith = 0,
                butHeight = 0;

            butWith = ((width - 40) / 3);
            butHeight = ((height - 40) / 4);

            btPhatSo.Width = butWith;
            btPhatSo.Height = butHeight;
            btPhatSo.Location = new Point(10, 10);

            btKhoa_Noi.Width = butWith;
            btKhoa_Noi.Height = butHeight;
            btKhoa_Noi.Location = new Point(20 + butWith, 10);

            btKhoa_Ngoai.Width = butWith;
            btKhoa_Ngoai.Height = butHeight;
            btKhoa_Ngoai.Location = new Point(30 + (butWith * 2), 10);


            btKhoa_Yhct.Width = butWith;
            btKhoa_Yhct.Height = butHeight;
            btKhoa_Yhct.Location = new Point(10, 20 + butHeight);

            btKhoa_Dlieu.Width = butWith;
            btKhoa_Dlieu.Height = butHeight;
            btKhoa_Dlieu.Location = new Point(20 + butWith, 20 + butHeight);

            btKhoa_SPKhoa.Width = butWith;
            btKhoa_SPKhoa.Height = butHeight;
            btKhoa_SPKhoa.Location = new Point(30 + (butWith * 2), 20 + butHeight);


            btKhoa_Mat.Width = butWith;
            btKhoa_Mat.Height = butHeight;
            btKhoa_Mat.Location = new Point(10, 30 + (butHeight * 2));

            btKhoa_TMH.Width = butWith;
            btKhoa_TMH.Height = butHeight;
            btKhoa_TMH.Location = new Point(20 + butWith, 30 + (butHeight * 2));

            btKhoa_RHM.Width = butWith;
            btKhoa_RHM.Height = butHeight;
            btKhoa_RHM.Location = new Point(30 + (butWith * 2), 30 + (butHeight * 2));

            btKhoa_ThanKinh.Width = butWith;
            btKhoa_ThanKinh.Height = butHeight;
            btKhoa_ThanKinh.Location = new Point(10, 40 + (butHeight * 3));

            btback_khoa.Width = butWith;
            btback_khoa.Height = butHeight;
            btback_khoa.Location = new Point(20 + butWith, 40 + (butHeight * 3));

            //btback_khoa.Width = butWith;
            //btback_khoa.Height = butHeight;
            //btback_khoa.Location = new Point(30 + (butWith * 2), 40 + (butHeight * 3));
        }

        private void CheckState()
        {
            switch (currentPanel)
            {
                case FormPanelSate.mainMenu:
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnCLS.Visible = false;
                    pnDK_Kham.Visible = false;
                    pnDS_PK.Visible = false;
                    pnCLS_PK.Visible = false;

                    pnMenu.Visible = true;
                    pnMenu.Dock = DockStyle.Fill;
                    mainPanelRefresh();
                    label3.Text = "ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG";
                    break;
                case FormPanelSate.dkKham:
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnCLS.Visible = false;
                    pnMenu.Visible = false;
                    pnDS_PK.Visible = false;
                    pnCLS_PK.Visible = false;

                    pnDK_Kham.Visible = true;
                    pnDK_Kham.Dock = DockStyle.Fill;
                    dsKhoaPanelRefresh();
                    label3.Text = "ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG";
                    break;
                case FormPanelSate.dkSMS:
                    pnMenu.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnCLS.Visible = false;
                    pnDK_Kham.Visible = false;
                    pnDS_PK.Visible = false;
                    pnCLS_PK.Visible = false;

                    pnSTT_SMS.Visible = true;
                    pnSTT_SMS.Dock = DockStyle.Fill;
                    txtPhone.Focus();
                    label3.Text = ("Tra cứu và cấp số thứ tự đặt hẹn qua sms").ToUpper();
                    break;
                case FormPanelSate.dkWeb:
                    pnMenu.Visible = false;
                    pnSTT_SMS.Visible = false;
                    pnCLS.Visible = false;
                    pnDK_Kham.Visible = false;
                    pnDS_PK.Visible = false;
                    pnCLS_PK.Visible = false;

                    pnSTT_Web.Visible = true;
                    pnSTT_Web.Dock = DockStyle.Fill;
                    txtPhone_Web.Focus();
                    label3.Text = ("Tra cứu và cấp số thứ tự đặt hẹn qua website").ToUpper();
                    break;
                case FormPanelSate.cls:
                    pnMenu.Visible = false;
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnDK_Kham.Visible = false;
                    pnDS_PK.Visible = false;
                    pnCLS_PK.Visible = false;

                    pnCLS.Visible = true;
                    pnCLS.Dock = DockStyle.Fill;
                    label3.Text = ("ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG khoa cận lâm sàn").ToUpper();
                    break;
                case FormPanelSate.dsPKham:
                    pnMenu.Visible = false;
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnDK_Kham.Visible = false;
                    pnCLS.Visible = false;
                    pnCLS_PK.Visible = false;

                    pnDS_PK.Visible = true;
                    pnDS_PK.Dock = DockStyle.Fill;
                    label3.Text = ("ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG  ").ToUpper();
                    break;
                case FormPanelSate.cls_PK:
                    pnMenu.Visible = false;
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnDK_Kham.Visible = false;
                    pnDS_PK.Visible = false;
                    pnCLS.Visible = false;

                    pnCLS_PK.Visible = true;
                    pnCLS_PK.Dock = DockStyle.Fill;
                    label3.Text = ("ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG khoa cận lâm sàn").ToUpper();
                    break;
            }
        }

        #region doi mau nut

        private void ButtonEffect_MouseDown(ButtonControl button)
        {
            // this.buttonControl1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            // this.buttonControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            button.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            //   button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            button.BorderColor = Color.Red;
            //  this.buttonControl1.BackgroundColor = Color.YellowGreen;

            button.ForeColor = Color.White;
            // this.buttonControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(107)))));

            //  button.ButtonText = "buttonControl1_MouseDown";
        }

        private void ButtonEffect_MouseUp(ButtonControl button, Color _backgroundColor, Color _foreColor, Color _boderColor)
        {
            // buttonControl1.BackgroundColor = Color.Silver;
            //  this.buttonControl1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(191)))), ((int)(((byte)(107)))));
            button.BackgroundColor = _backgroundColor;
            button.BorderColor = _boderColor;
            button.ForeColor = _foreColor;
        }

        private void btnDK_KB_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnDK_KB);
        }

        private void btnSTTSMS_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnSTTSMS);
        }

        private void btnSTTWeb_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnSTTWeb);
        }

        private void btnBHYT_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnBHYT);
        }

        private void btnTK_BHYT_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnTK_BHYT);
        }

        private void btnTK_DV_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnTK_DV);
        }

        private void btnPhatThuoc_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnPhatThuoc);
        }

        private void btnVienPhi_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnVienPhi);
        }

        private void btnCanLamSan_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnCanLamSan);
        }

        private void btnDK_KB_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnDK_KB, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnSTTSMS_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnSTTSMS, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnSTTWeb_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnSTTWeb, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnBHYT_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnBHYT, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnTK_BHYT_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnTK_BHYT, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnTK_DV_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnTK_DV, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnPhatThuoc_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnPhatThuoc, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnVienPhi_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnVienPhi, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnCanLamSan_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnCanLamSan, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_Noi_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_Noi);
        }

        private void btK_Noi_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_Noi, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_Ngoai_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_Ngoai);
        }

        private void btK_Ngoai_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_Ngoai, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }
         
        private void btK_SPKhoa_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_SPKhoa);
        }

        private void btK_SPKhoa_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_SPKhoa, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_Yhct_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_Yhct);
        }

        private void btK_Yhct_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_Yhct, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_Dlieu_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_Dlieu);
        }

        private void btK_Dlieu_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_Dlieu, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_TMH_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_TMH);
        }

        private void btK_TMH_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_TMH, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_RHM_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_RHM);
        }

        private void btK_RHM_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_RHM, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btK_Mat_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_Mat);
        }

        private void btK_Mat_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_Mat, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPrint_SMS_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btPrint_SMS, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPrint_SMS_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btPrint_SMS);
        }

        private void btnPrint_w_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnPrint_w);
        }

        private void btnPrint_w_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnPrint_w, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPhatSo_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btPhatSo);
        }

        private void btPhatSo_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btPhatSo, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btKhoa_ThanKinh_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKhoa_ThanKinh);
        }

        private void btKhoa_ThanKinh_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKhoa_ThanKinh, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btCLS_NoiSoi_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btCLS_NoiSoi);
        }

        private void btCLS_NoiSoi_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btCLS_NoiSoi, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btCLS_HinhAnh_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btCLS_HinhAnh);
        }

        private void btCLS_HinhAnh_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btCLS_HinhAnh, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btCLS_TDChucNang_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btCLS_TDChucNang);
        }

        private void btCLS_TDChucNang_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btCLS_TDChucNang, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }
        #endregion

        #region main menu events
        private void btnDK_KB_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.dkKham;
            CheckState();
        }

        private void btnSTTSMS_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.dkSMS;
            CheckState();
        }

        private void btnSTTWeb_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.dkWeb;
            CheckState();
        }

        private void btnPhatThuoc_Click(object sender, EventArgs e)
        {
            //inphieu
            // có 1 quầy phát
            PrintTicket(phatthuocId, DateTime.Now, "", "", 0, "", "");
        }

        private void btnVienPhi_Click(object sender, EventArgs e)
        {
            //inphieu
            // có 1 quầy thu phí
            PrintTicket(vienphiId, DateTime.Now, "", "", 0, "", "");
        }

        private void btnCanLamSan_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.cls;
            CheckState();
        }
        #endregion

        #region panel pkham events       
        private void btBack_DSPK_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.dkKham;
            CheckState();
        }

        private void buttonControlPK_MouseDown(object sender, EventArgs e)
        {
            ButtonControl btn = (ButtonControl)sender;
            ButtonEffect_MouseDown(btn);
        }

        private void buttonControl_MouseUp(object sender, EventArgs e)
        {
            ButtonControl btn = (ButtonControl)sender;
            ButtonEffect_MouseUp(btn, btn.BackColor, btn.ForeColor, Color.Silver);
        }

        private void btPKham_Click(object sender, EventArgs e)
        {
            int pkId = Convert.ToInt32(((System.Windows.Forms.Control)sender).Name);
            PrintTicket(pkId, DateTime.Now, "", "", 0, "", "");
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            switch (currentPanel)
            {
                case FormPanelSate.dsPKham:
                    currentPanel = FormPanelSate.dkKham;
                    break;
                case FormPanelSate.cls_PK:
                    currentPanel = FormPanelSate.cls;
                    break;
            }

            CheckState();
        }
        #endregion

        #region panel khoa kham events
        private void btPhatSo_Click(object sender, EventArgs e)
        {
            //in phiếu tiếp bệnh
            PrintTicket(phatsoId, DateTime.Now, "", "", 0, "", "");
        }

        private void btK_Noi_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.KhoaNoi, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_Ngoai_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.KhoaNgoai, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_Nhi_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.Nhi, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_SPKhoa_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.SanPhuKhoa, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btKhoa_ThanKinh_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.ThanKinh, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_Yhct_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.YHCT, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_Dlieu_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.DaLieu, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_TMH_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.TMH, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_RHM_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.RHM, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btK_Mat_Click(object sender, EventArgs e)
        {
            pnDS_PK.Controls.Clear();
            pnDS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.Mat, pnDS_PK, FormPanelSate.dsPKham);
        }

        private void btnback_dkkham_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.mainMenu;
            CheckState();
        }
        #endregion

        #region panel stt sms events
        private void btnFindSMS_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                btPrint_SMS.Enabled = false;
                printModel = null;
                var tickets = BLLHuuNghi.Instance.FindTickets(connectString, txtPhone.Text);
                if (tickets.Count > 0)
                {
                    btPrint_SMS.Enabled = true;

                    var ser = services.FirstOrDefault(x => x.Id == tickets[0].ServiceId);
                    txtKhoa_s.Text = GetKhoa(ser.ServiceType);
                    txtPKham_s.Text = tickets[0].TenDichVu;
                    txtInfo_s.Text = "số phiếu: " + tickets[0].STT + " | Giờ hẹn: " + tickets[0].SoXe;
                    printModel = tickets[0];
                }
                else
                    _showMessage((int)eMessageType.error, "Không tìm thấy thông tin đăng ký với số điện thoại : " + txtPhone.Text);
            }
            else
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số điện thoại đăng ký.");
                txtPhone.Focus();
            }
        }

        private void btPrint_SMS_Click(object sender, EventArgs e)
        {
            if (printModel != null)
                PrintWithNoBorad(printModel);
        }

        private void btBack_SMS_Click(object sender, EventArgs e)
        {
            printModel = null;
            currentPanel = FormPanelSate.mainMenu;
            txtPhone.Text = "";
            txtKhoa_s.Text = "";
            txtPKham_s.Text = "";
            txtInfo_s.Text = "";
            btPrint_SMS.Enabled = false;
            CheckState();
        }
        #endregion

        #region panel stt web events         
        private void btnFindWeb_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone_Web.Text))
            {
                btnPrint_w.Enabled = false;
                printModel = null;
                var tickets = BLLHuuNghi.Instance.FindTickets(connectString, txtPhone_Web.Text);
                if (tickets.Count > 0)
                {
                    btnPrint_w.Enabled = true;
                    var ser = services.FirstOrDefault(x => x.Id == tickets[0].ServiceId);
                    txtKhoa_w.Text = GetKhoa(ser.ServiceType);
                    txtPKham_w.Text = tickets[0].TenDichVu;
                    txtInfo_w.Text = "số phiếu: " + tickets[0].STT + " | Giờ hẹn: " + tickets[0].SoXe;
                    txtDOB_w.Text = tickets[0].DOB.ToString();
                    txtName_w.Text = tickets[0].TenKH;
                    printModel = tickets[0];
                }
                else
                    _showMessage((int)eMessageType.error, "Không tìm thấy thông tin đăng ký với số điện thoại : " + txtPhone_Web.Text);
            }
            else
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số điện thoại đăng ký.");
                txtPhone_Web.Focus();
            }
        }

        private void btnPrint_w_Click(object sender, EventArgs e)
        {
            if (printModel != null)
                PrintWithNoBorad(printModel);
        }

        private void btnBack_web_Click(object sender, EventArgs e)
        {
            printModel = null;
            currentPanel = FormPanelSate.mainMenu;
            txtPhone_Web.Text = "";
            txtKhoa_w.Text = "";
            txtPKham_w.Text = "";
            txtInfo_w.Text = "";
            txtName_w.Text = "";
            txtDOB_w.Text = "";
            btnPrint_w.Enabled = false;
            CheckState();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region panel khoa CLS events
        private void btCLS_NoiSoi_Click(object sender, EventArgs e)
        {
            pnCLS_PK.Controls.Clear();
            pnCLS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.CLS_NoiSoi, pnCLS_PK, FormPanelSate.cls_PK);
        }

        private void btCLS_TDChucNang_Click(object sender, EventArgs e)
        {
            pnCLS_PK.Controls.Clear();
            pnCLS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.CLS_ThamDoChucNang, pnCLS_PK, FormPanelSate.cls_PK);
        }

        private void btCLS_HinhAnh_Click(object sender, EventArgs e)
        {
            pnCLS_PK.Controls.Clear();
            pnCLS_PK.Dock = DockStyle.Fill;
            GenerateButton((int)eKhoa.CLS_CDHinhAnh, pnCLS_PK, FormPanelSate.cls_PK);
        }

        private void btCLS_back_Click(object sender, EventArgs e)
        {
            currentPanel = FormPanelSate.mainMenu;
            CheckState();
        }
        #endregion

        #region In Phiếu
        private void PrintTicket(int serviceId, DateTime ServeTime, string tenBN, string diaChi, int namSinh, string maBN, string soDienThoai)
        {
            int lastTicket = 0,
                newNumber = -1,
            nghiepVu = 0;
            string printStr = string.Empty,
                tenquay = string.Empty;
            bool err = false;
            ServiceDayModel serObj = null;
            DateTime now = DateTime.Now;
            var printModel = new PrintModel();
            List<int> counterIds = null;

            //lay stt kham benh
            switch (printType)
            {
                case (int)ePrintType.TheoTungDichVu:
                    #region
                    serObj = lib_Services.FirstOrDefault(x => x.Id == serviceId);
                    if (serObj == null)
                        errorsms = "Dịch vụ số " + serviceId + " không tồn tại. Xin quý khách vui lòng chọn dịch vụ khác.";
                    else
                    {
                        if (CheckTimeBeforePrintTicket == 1 && serObj.Shifts.FirstOrDefault(x => now.TimeOfDay >= x.Start.TimeOfDay && now.TimeOfDay <= x.End.TimeOfDay) == null)
                            // temp.Add(SoundLockPrintTicket);
                            errorsms = "Dịch vụ số " + serviceId + " đã ngưng cấp số. Xin quý khách vui lòng đến vào buổi giao dịch sau.";
                        else
                        {
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, serObj.StartNumber, 0, now, printType, ServeTime.TimeOfDay, tenBN, diaChi, namSinh, maBN, "", "", "", "", "", (int)eDailyRequireType.KhamBenh, soDienThoai);
                            if (rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data_3);
                                tenquay = rs.Data_2;
                                counterIds = (List<int>)rs.Data_4;

                                printModel.TenDichVu = (serObj != null ? serObj.Name : "");
                                printModel.NoteDV = (serObj != null ? serObj.Note : "");
                                printModel.TenQuay = rs.Data_2;
                                printModel.STT = newNumber;

                                printModel.SoXe = "";
                                printModel.MaKH = maBN;
                                printModel.TenKH = tenBN;
                                printModel.DOB = namSinh;
                                printModel.DiaChi = diaChi;
                                printModel.MaDV = "";
                                printModel.Phone = soDienThoai;
                                printModel.ServiceId = (serObj != null ? serObj.Id : 0);
                            }
                            else
                                errorsms = rs.Errors[0].Message;
                            //   MessageBox.Show(rs.Errors[0].Message, rs.Errors[0].MemberName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    #endregion
                    break;
                case (int)ePrintType.BatDauChung:
                    #region MyRegion
                    serObj = lib_Services.FirstOrDefault(x => x.Id == serviceId);
                    if (serObj == null)
                        errorsms = "Dịch vụ số " + serviceId + " không tồn tại. Xin quý khách vui lòng chọn dịch vụ khác.";
                    else
                    {
                        if (CheckTimeBeforePrintTicket == 1 && serObj.Shifts.FirstOrDefault(x => now.TimeOfDay >= x.Start.TimeOfDay && now.TimeOfDay <= x.End.TimeOfDay) == null)
                            //temp.Add(FrmMain.SoundLockPrintTicket);
                            errorsms = "Dịch vụ số " + serviceId + " đã ngưng cấp số. Xin quý khách vui lòng đến vào buổi giao dịch sau.";
                        else
                        {
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, startNumber, 0, now, printType, (ServeTime != null ? ServeTime.TimeOfDay : serObj.TimeProcess.TimeOfDay), tenBN, diaChi, namSinh, maBN, "", "", "", "", "", (int)eDailyRequireType.KhamBenh, soDienThoai);
                            if (rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data_3);
                                tenquay = rs.Data_2;
                                counterIds = (List<int>)rs.Data_4;

                                printModel.TenDichVu = (serObj != null ? serObj.Name : "");
                                printModel.NoteDV = (serObj != null ? serObj.Note : "");
                                printModel.TenQuay = rs.Data_2;
                                printModel.STT = newNumber;

                                printModel.SoXe = "";
                                printModel.MaKH = maBN;
                                printModel.TenKH = tenBN;
                                printModel.DOB = namSinh;
                                printModel.DiaChi = diaChi;
                                printModel.MaDV = "";
                                printModel.Phone = soDienThoai;
                                printModel.ServiceId = (serObj != null ? serObj.Id : 0);
                            }
                            else
                                errorsms = rs.Errors[0].Message;
                            //  MessageBox.Show(rs.Errors[0].Message, rs.Errors[0].MemberName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    #endregion
                    break;
                case (int)ePrintType.TheoGioiHanSoPhieu:

                    break;
            }

            if (newNumber >= 0)
            {
                errorsms = printStr.ToString();
                try
                {
                    PrintWithNoBorad(printModel);
                    try
                    {
                        if (counterIds != null)
                            socket.Emit("qms-system-refresh-lcd", string.Join(",", counterIds));
                    }
                    catch { }
                }
                catch (Exception)
                {
                }

                //bool kq = false;
                //switch (cfObj.appType)
                //{
                //    case 0: //phong khám
                //        if (requireType == (int)eDailyRequireType.KhamBenh && serviceId != cfObj.tieptan)
                //        {
                //            lbStatus.Text = "Bắt đầu gửi tiếp nhận lên HIS";
                //            kq = serviceClient.Luu_TiepNhan(mabenhnhan, ngaysinh, (cbGioitinh.Text == "Nam" ? "T" : "G"), txtBHYT.Text, bhFrom, bhTo, selectedDichVu.Id, selectedPhongKham.Id, 0, newNumber.ToString());
                //            string abc = String.Join("Luu_TiepNhan({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", mabenhnhan, ngaysinh.ToString("dd/MM/yyyy"), (cbGioitinh.Text == "Nam" ? "T" : "G"), txtBHYT.Text, bhFrom.ToString("dd/MM/yyyy"), bhTo.ToString("dd/MM/yyyy"), selectedDichVu.Id.ToString(), selectedPhongKham.Id.ToString(), "0", newNumber.ToString());
                //            MessageBox.Show(abc);
                //            if (!kq)
                //            {
                //                //_showMessage(4, "Lưu tiếp nhận lên HIS bị lỗi");
                //            }
                //            else
                //            {
                //                lbStatus.Text = "Gửi tiếp nhận lên HIS thành công.!";
                //                ClearForm();
                //            }
                //        }
                //        else
                //            ClearForm();
                //        break;
                //    case 1: //cls
                //        var ser = BLLService.Instance.Get(connectString, serviceId);
                //        kq = serviceClient.SendSoThuTuToHIS(mabenhnhan, ser.Code, newNumber.ToString());
                //        if (!kq)
                //        {
                //            //_showMessage(4, "Lưu tiếp nhận lên HIS bị lỗi");
                //        }
                //        else
                //            CLSClear();
                //        break;
                //    case 2:
                //    case 3:
                //    case 4:
                //        clear_VpPt(); break;
                //}

            }
        }

        private void PrintWithNoBorad(PrintModel printModel)
        {
            var now = DateTime.Now; 
            checkCOM:
            if (!COM_Printer.IsOpen)
            {
                try
                {
                    COM_Printer.Open();
                }
                catch (Exception e)
                {
                    _showMessage((int)eMessageType.error, "Không thể mở được COM máy in. Vui lòng kiễm tra lại COM máy in" );
                    goto finish;
                }

                // LogWriter.LogWrite(string.Format("func PrintWithNoBorad: Restart COM Máy in {0}", DateTime.Now.ToString("dd/MM/YYYY HH:mm:ss")));
                goto checkCOM;
            }
            var _temp = printTemplates.Where(x => x._ServiceIds.Contains(printModel.ServiceId) && x.IsActive).ToList();

            if (COM_Printer.IsOpen && _temp.Count > 0)
            {
                for (int i = 0; i < _temp.Count; i++)
                {
                    var template = _temp[i].PrintTemplate;
                    template = template.Replace("[canh-giua]", "\x1b\x61\x01|+|");
                    template = template.Replace("[canh-trai]", "\x1b\x61\x00|+|");
                    template = template.Replace("[1x1]", "\x1d\x21\x00|+|");
                    template = template.Replace("[2x1]", "\x1d\x21\x01|+|");
                    template = template.Replace("[3x1]", "\x1d\x21\x02|+|");
                    template = template.Replace("[2x2]", "\x1d\x21\x11|+|");
                    template = template.Replace("[3x3]", "\x1d\x21\x22|+|");

                    template = template.Replace("[STT]", printModel.STT.ToString());
                    template = template.Replace("[ten-quay]", printModel.TenQuay);
                    template = template.Replace("[ten-dich-vu]", printModel.TenDichVu);
                    template = template.Replace("[ghi-chu-dich-vu]", printModel.NoteDV);
                    template = template.Replace("[ngay]", ("Ngày: " + now.ToString("dd/MM/yyyy")));
                    template = template.Replace("[gio]", ("Giờ: " + now.ToString("HH:mm")));
                    template = template.Replace("[dang-goi]", "đang gọi: " + printModel.STTHienTai);


                    template = template.Replace("[so-xe]", getStringValue(printModel.SoXe));
                    template = template.Replace("[phone]", getStringValue(printModel.Phone));
                    template = template.Replace("[ma-kh]", getStringValue(printModel.MaKH));
                    template = template.Replace("[ten-kh]", getStringValue(printModel.TenKH));
                    template = template.Replace("[dia-chi]", getStringValue(printModel.DiaChi));
                    template = template.Replace("[ma-dv]", getStringValue(printModel.MaDV));
                    template = template.Replace("[dob]", getIntValue(printModel.DOB));

                    template = template.Replace("[cat-giay]", "\x1b\x69|+|");

                    var arr = template.Split(new string[] { "|+|" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    for (int ii = 0; ii < printTemplates[i].PrintPages; ii++)
                    {
                        for (int iii = 0; iii < arr.Length; iii++)
                        {
                            // frmMain.COM_Printer.Write(arr[i]);
                            try
                            {
                                BaseCore.Instance.PrintTicketTCVN3(COM_Printer, arr[iii]);
                            }
                            catch (Exception ex)
                            {
                                //  LogWriter.LogWrite(("COM write error: " + ex.Message));
                            }

                            //sleep
                            Thread.Sleep(50);
                        }
                    }
                }
            }
            else
                errorsms = "Cổng COM máy in hiện tại chưa kết nối. Vui lòng kiểm tra lại COM máy in";
            finish:
            int a = 1;
        }

        private string getStringValue(string value)
        {
            return !string.IsNullOrEmpty(value) ? (value + "\n") : "";
        }

        private string getIntValue(int value)
        {
            return value != null && value > 0 ? (value.ToString() + "\n") : "";
        }
        #endregion

        private string RandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        private void GenerateButton(int khoa, Panel panel, string panelState)
        {
            var sers = services.Where(x => x.ServiceType == khoa).ToList();
            if (sers.Count > 0)
            {
                int socot = btStyle.ButtonInRow;
                int sodong = (int)Math.Ceiling((sers.Count + 1) / (double)socot),
                sizeCot = (panel.Width - ((socot + 1) * btStyle.Margin)) / socot,
                sizeDong = 100 / sodong;
                FontConverter converter = new FontConverter();
                ButtonControl btn;
                int pointX = btStyle.Margin, pointY = btStyle.Margin;
                int index = 0;

                for (int i = 0; i < sodong; i++)
                {
                    for (int ii = 0; ii < socot; ii++)
                    {
                        if (index < sers.Count)
                        {
                            var found = sers[index];
                            btn = new ButtonControl();
                            btn.Name = found.Id.ToString();
                            btn.ButtonText = (found.Name).ToUpper();
                            btn.Cursor = System.Windows.Forms.Cursors.Hand;
                            btn.Width = btStyle.Width;
                            btn.Height = btStyle.Height;
                            btn.Location = new Point(pointX, pointY);
                            try
                            {
                                btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
                            }
                            catch (Exception) { }

                            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
                            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);

                            btn.Click += new System.EventHandler(this.btPKham_Click);
                            btn.MouseDown += new MouseEventHandler(this.buttonControlPK_MouseDown);
                            btn.MouseUp += new MouseEventHandler(this.buttonControl_MouseUp);
                            pointX += btStyle.Width + btStyle.Margin;
                            panel.Controls.Add(btn);
                            index++;
                        }
                        else
                        {
                            break;
                        }

                        if (ii == socot - 1)
                        {
                            pointY += btStyle.Height + btStyle.Margin;
                            pointX = btStyle.Margin;
                        }

                    }
                }

                btn = new ButtonControl();
                btn.Name = RandomString();
                btn.ButtonText = "Quay lại";
                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                btn.Width = btStyle.Width;
                btn.Height = btStyle.Height;
                btn.Location = new Point(pointX, pointY);
                try
                {
                    btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
                }
                catch (Exception) { }

                btn.BackColor = Color.Transparent;
                btn.BackgroundColor = Color.WhiteSmoke;
                btn.BorderColor = Color.FromArgb(0, 80, 200);
                btn.ForeColor = Color.DimGray;

                btn.Click += new System.EventHandler(this.btBack_Click);
                btn.MouseDown += new MouseEventHandler(this.buttonControlPK_MouseDown);
                btn.MouseUp += new MouseEventHandler(this.buttonControl_MouseUp);
                pointX += btStyle.Width + btStyle.Margin;
                panel.Controls.Add(btn);


                currentPanel = panelState;
                CheckState();
            }
        }

        private string GetKhoa(int serviceType)
        {
            string khoa = string.Empty;
            switch (serviceType)
            {
                case 2: khoa = "Khoa nội"; break;
                case 3: khoa = "Khoa ngoại"; break;
                case 4: khoa = "Cận lâm sàn"; break;
                case 5: khoa = "Khoa Sản - Phụ khoa"; break;
                case 6: khoa = "Khoa Da liễu"; break;
                case 7: khoa = "Khoa thần kinh"; break;
                case 8: khoa = "Khoa Nhi"; break;
                case 9: khoa = "Khoa Mắt"; break;
                case 10: khoa = "Khoa Răng Hàm Mặt"; break;
                case 11: khoa = "Khoa Tai Mũi Họng"; break;
                case 12: khoa = "Khoa YHCT"; break;
                case 13: khoa = "Khoa Nội soi (CLS)"; break;
                case 14: khoa = "Khoa Thăm dò chức năng (CLS)"; break;
                case 15: khoa = "Khoa Chuẩn đoán hình ảnh (CLS)"; break;
            }
            return khoa;
        }
    }

    public static class FormPanelSate
    {
        public const string mainMenu = "MainMenu";
        public const string dkKham = "dkKham";
        public const string dkSMS = "dkSMS";
        public const string dkWeb = "dkWeb";
        public const string cls = "cls";
        public const string cls_PK = "cls_PK";
        public const string dsPKham = "dsPKham";
        public const string dsPThuoc = "dsPThuoc";

    }

    public enum eKhoa
    {
        KhoaNoi = 2,
        KhoaNgoai = 3,
        //CanLamSan = 4,
        SanPhuKhoa = 5,
        DaLieu = 6,
        ThanKinh = 7,
        Nhi = 8,
        Mat = 9,
        RHM = 10,
        TMH = 11,
        YHCT = 12,
        CLS_NoiSoi = 13,
        CLS_ThamDoChucNang = 14,
        CLS_CDHinhAnh = 15
    }
}
