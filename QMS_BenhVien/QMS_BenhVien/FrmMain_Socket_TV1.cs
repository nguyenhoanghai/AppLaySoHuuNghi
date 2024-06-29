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
    public partial class FrmMain_Socket_TV1 : Form
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
            khamBHYTId = 0,
            khamDVId = 0,
        khamUTId = 0,
            pThuocUTId = 0,
            pThuocKoUTId = 0,
            tieuduongId = 0;
        FrmMessagebox messagebox;
        PrintModel printModel = null;
        //TODO 
        public static bool appPhatThuoc = true;

        public FrmMain_Socket_TV1()
        {
            InitializeComponent();
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

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnNormalSize_Click(sender, e);
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


        #endregion

        private void CheckState()
        {
            switch (currentPanel)
            {
                case FormPanelSate.mainMenu:
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;

                    pnMenu.Visible = true;
                    pnMenu.Dock = DockStyle.Fill;
                    mainPanelRefresh();
                    label3.Text = "ĐĂNG KÝ LẤY SỐ THỨ TỰ TỰ ĐỘNG";
                    break;

                case FormPanelSate.dkSMS:
                    pnMenu.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnSTT_SMS.Visible = true;
                    pnSTT_SMS.Dock = DockStyle.Fill;
                    txtPhone.Focus();
                    label3.Text = ("Tra cứu và cấp số thứ tự đặt hẹn qua sms").ToUpper();
                    break;
                case FormPanelSate.dkWeb:
                    pnMenu.Visible = false;
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = true;
                    pnSTT_Web.Dock = DockStyle.Fill;
                    txtPhone_Web.Focus();
                    label3.Text = ("Tra cứu và cấp số thứ tự đặt hẹn qua website").ToUpper();
                    break;
                case FormPanelSate.dsPThuoc:
                    pnMenu.Visible = false;
                    pnSTT_SMS.Visible = false;
                    pnSTT_Web.Visible = false;
                    pnPhatThuoc.Visible = true;
                    pnPhatThuoc.Dock = DockStyle.Fill;
                    phatThuocPanelRefresh();
                    break;
            }
        }

        private void mainPanelRefresh()
        {
            int width = panelMain.Width,
                height = panelMain.Height,
                butWith = 0,
                butHeight = 0;

            butWith = ((width - 40) / 2);
            butHeight = ((height - 40) / 4);

            btnKhamUT.Width = butWith;
            btnKhamUT.Height = butHeight;
            btnKhamUT.Location = new Point(10, 10);
            btnBHYT.Width = butWith;
            btnBHYT.Height = butHeight;
            btnBHYT.Location = new Point(20 + butWith, 10);


            btnKhamDV.Width = butWith;
            btnKhamDV.Height = butHeight;
            btnKhamDV.Location = new Point(10, 20 + butHeight);
            btnVienPhi.Width = butWith;
            btnVienPhi.Height = butHeight;
            btnVienPhi.Location = new Point(20 + butWith, 20 + butHeight);

            btnSTTSMS.Width = butWith;
            btnSTTSMS.Height = butHeight;
            btnSTTSMS.Location = new Point(10, 30 + (butHeight * 2));
            btnSTTWeb.Width = butWith;
            btnSTTWeb.Height = butHeight;
            btnSTTWeb.Location = new Point(20 + butWith, 30 + (butHeight * 2));

            btTimmachTieuDuong.Width = butWith;
            btTimmachTieuDuong.Height = butHeight;
            btTimmachTieuDuong.Location = new Point(10, 40 + (butHeight * 3));

        }

        private void phatThuocPanelRefresh()
        {
            int width = panelMain.Width,
                height = panelMain.Height,
                butWith = 0,
                butHeight = 0;

            butWith = ((width - 140) / 2);
            butHeight = ((height - 140) / 2);

            btPhatThuocUT.Width = butWith;
            btPhatThuocUT.Height = butHeight;
            btPhatThuocUT.Location = new Point(butWith / 2 + 70, butHeight / 2 - 70);


            btPhatThuoc.Width = butWith;
            btPhatThuoc.Height = butHeight;
            btPhatThuoc.Location = new Point(butWith / 2 + 70, butHeight + 90);

        }


        #region màu nút

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
            ButtonEffect_MouseDown(btnKhamUT);
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
            ButtonEffect_MouseDown(btnKhamDV);
        }

        private void btnVienPhi_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnVienPhi);
        }

        private void btPhatThuocUT_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btPhatThuocUT);
        }

        private void btPhatThuoc_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btPhatThuoc);
        }

        private void btPhatThuocUT_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btPhatThuocUT, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPhatThuoc_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btPhatThuoc, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnDK_KB_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnKhamUT, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
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
            ButtonEffect_MouseUp(btnKhamDV, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnVienPhi_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnVienPhi, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btnPrint_w_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btnPrint_w);
        }

        private void btnPrint_w_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnPrint_w, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPrint_SMS_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btPrint_SMS, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPrint_SMS_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btPrint_SMS);
        }

        #endregion

        private void _showMessage(int type, string message)
        {
            messagebox = new FrmMessagebox(type, message);
            messagebox.ShowDialog();
        }


        private void FrmMain_TV1_Load(object sender, EventArgs e)
        {
            CheckState();
            GetConfig();
            ConnectSocketIO();
            InitCOM_Printer();

            btnMaximize_Click(sender, e);
        }

        private void GetConfig()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                          ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var aa = registryKey.GetValue("QMS_BenhVien");
                var bb = registryKey.GetValueNames();


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
                khamBHYTId = cfObj.laymau;
                khamDVId = cfObj.ketqua;
                khamUTId = cfObj.tieptan;
                pThuocUTId = cfObj.phatthuoc;
                pThuocKoUTId = cfObj.xquang;
                tieuduongId = cfObj.PhatSo;

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


                if (appPhatThuoc)
                {
                    currentPanel = FormPanelSate.dsPThuoc;
                }
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

        #region SocketIO
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
                        lbSocketStatus.ForeColor = Color.White;
                        socket.On(Socket.EVENT_DISCONNECT, () =>
                        {
                            SetText("Thiết bị mất kết nối máy chủ.");
                            panelStatus.BackColor = Color.Red;
                        });
                    });

                    socket.On("server-send-socket-counter-soft", (socketId) =>
                    {
                        SetText(socketId.ToString());
                    });
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
                lbSocketStatus.Text = text + " | IPAddress: " + BaseCore.Instance.GetLocalIPAddress();
                if (text != "Thiết bị mất kết nối máy chủ.")
                {
                    panelStatus.BackColor = Color.Blue;
                }
                else
                    panelStatus.BackColor = Color.Red;
            }
        }

        #endregion

        private void btnKhamUT_Click(object sender, EventArgs e)
        {
            PrintTicket(khamUTId, DateTime.Now, "", "", 0, "", "");
        }

        private void btnBHYT_Click(object sender, EventArgs e)
        {
            PrintTicket(khamBHYTId, DateTime.Now, "", "", 0, "", "");
        }

        private void btnKhamDV_Click(object sender, EventArgs e)
        {
            PrintTicket(khamDVId, DateTime.Now, "", "", 0, "", "");
        }

        private void btnVienPhi_Click(object sender, EventArgs e)
        {
            //inphieu
            // có 1 quầy thu phí
            PrintTicket(vienphiId, DateTime.Now, "", "", 0, "", "");
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


        private void btTimmachTieuDuong_Click(object sender, EventArgs e)
        {
            //inphieu
            // có 1 quầy thu phí
            PrintTicket(tieuduongId, DateTime.Now, "", "", 0, "", "");
        }

        private void btTimmachTieuDuong_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btTimmachTieuDuong);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btTimmachTieuDuong_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btTimmachTieuDuong, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPhatThuocUT_Click(object sender, EventArgs e)
        {
            PrintTicket(pThuocUTId, DateTime.Now, "", "", 0, "", "");
        }

        private void btPhatThuoc_Click(object sender, EventArgs e)
        {
            PrintTicket(pThuocKoUTId, DateTime.Now, "", "", 0, "", "");
        }

        #region panel stt sms events
        private void btnFindSMS_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                btPrint_SMS.Enabled = false;
                printModel = null;
                /*
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
                */

                var resp = BLLRegisterOnline.Instance.Find_Kios(connectString, txtPhone.Text, DateTime.Now.ToString("d/M/yyyy"), 8);
                if (resp.IsSuccess)
                {
                    btPrint_SMS.Enabled = true;
                    _phone = resp.Data_2;
                    _name = resp.Data_1;
                    _ticket = (int)resp.Data;

                    txtInfo_s.Text = "số phiếu: " + _ticket;
                }
                else
                {
                    _showMessage((int)eMessageType.error, "Không tìm thấy thông tin đăng ký với số điện thoại : " + txtPhone_Web.Text);
                    //_showMessage((int)eMessageType.error, resp.Errors[0].Message);
                }
            }
            else
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số điện thoại đăng ký.");
                txtPhone.Focus();
            }
        }

        private void btPrint_SMS_Click(object sender, EventArgs e)
        {
            var resp = BLLRegisterOnline.Instance.PrintTicket(connectString, _phone, _name, 8, _ticket);
            if (resp.IsSuccess)
            {
                PrintWithNoBorad((PrintModel)resp.Data);
                try
                {
                    var counterIds = (List<int>)resp.Data_4;
                    if (counterIds != null)
                        socket.Emit("qms-system-refresh-lcd", string.Join(",", counterIds));
                }
                catch { }
            }
            else
            {
                _showMessage((int)eMessageType.error, resp.Errors[0].Message);
            }
        }

        private void btBack_SMS_Click(object sender, EventArgs e)
        {
            printModel = null;
            currentPanel = FormPanelSate.mainMenu;
            txtPhone.Text = ""; 
            txtInfo_s.Text = "";
            btPrint_SMS.Enabled = false;
            CheckState();
            _ticket = 0; _phone = ""; _name = ""; 
        }
        #endregion

        #region panel stt web events         
        private void btnFindWeb_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone_Web.Text))
            {
                btnPrint_w.Enabled = false;
                printModel = null;
                /*
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
                */

                var resp = BLLRegisterOnline.Instance.Find_Kios(connectString, txtPhone_Web.Text, DateTime.Now.ToString("d/M/yyyy"), 8);
                if (resp.IsSuccess)
                {
                    btnPrint_w.Enabled = true;
                    _phone = resp.Data_2;
                    _name = resp.Data_1;
                    _ticket = (int)resp.Data;

                    txtName_w.Text = _name;
                    txtInfo_w.Text = "số phiếu: " + _ticket  ;
                }
                else
                {
                    _showMessage((int)eMessageType.error, "Không tìm thấy thông tin đăng ký với số điện thoại : " + txtPhone_Web.Text);  
                   // _showMessage((int)eMessageType.error, resp.Errors[0].Message);  
                }
            }
            else
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số điện thoại đăng ký.");
                txtPhone_Web.Focus();
            }
        }

        string _phone = "", _name = "";
        int _ticket = 0;

        private void btnPrint_w_Click(object sender, EventArgs e)
        {
            // if (printModel != null)
            //     PrintWithNoBorad(printModel);

            var resp = BLLRegisterOnline.Instance.PrintTicket(connectString, _phone, _name, 8, _ticket);
            if (resp.IsSuccess)
            {
                PrintWithNoBorad((PrintModel)resp.Data);
                try
                {
                  var counterIds = (List<int>)resp.Data_4;
                    if (counterIds != null)
                        socket.Emit("qms-system-refresh-lcd", string.Join(",", counterIds));
                }
                catch { }
            }
            else
            {
                _showMessage((int)eMessageType.error, resp.Errors[0].Message);
            }
        }

        private void btnBack_web_Click(object sender, EventArgs e)
        {
            printModel = null;
            currentPanel = FormPanelSate.mainMenu;
            txtPhone_Web.Text = ""; 
            txtInfo_w.Text = "";
            txtName_w.Text = ""; 
            btnPrint_w.Enabled = false;
            _phone = "";
            _name = "";
            _ticket = 0;
            CheckState();
        }

        #endregion

        #region In Phiếu
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
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, serObj.StartNumber, 0, now, printType, null, tenBN, diaChi, namSinh, maBN, "", "", "", "", "", (int)eDailyRequireType.KhamBenh, soDienThoai);
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
                                printModel.GioPVDK = rs.str1;
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
                                printModel.GioPVDK = rs.str1;
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
            //TODO Test
            //MessageBox.Show("so phieu: " + newNumber);
            if (newNumber >= 0)
            {
                errorsms = printStr.ToString();
                try
                {
                    PrintWithNoBorad(printModel);
                    //TODO Test
                    //MessageBox.Show(string.Join(",", counterIds));
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
            //MessageBox.Show("PrintWithNoBorad"  );
            if (COM_Printer != null)
            {
                var now = DateTime.Now;
                checkCOM:
                if (!COM_Printer.IsOpen)
                {
                    try
                    {
                        COM_Printer.Open();
                    }
                    catch (Exception ex)
                    {
                        _showMessage((int)eMessageType.error, "Không thể mở được COM máy in. Vui lòng kiễm tra lại COM máy in");
                        goto finish;
                    }

                    // LogWriter.LogWrite(string.Format("func PrintWithNoBorad: Restart COM Máy in {0}", DateTime.Now.ToString("dd/MM/YYYY HH:mm:ss")));
                    goto checkCOM;
                }


                var _temp = printTemplates.Where(x => x._ServiceIds.Contains(printModel.ServiceId) && x.IsActive).ToList();
                //TODO Test
                //MessageBox.Show("số mẫu:" + _temp.Count);
                if (COM_Printer.IsOpen && _temp.Count > 0)
                {
                    for (int i = 0; i < _temp.Count; i++)
                    {
                        //TODO Test
                        //MessageBox.Show(_temp[i].PrintTemplate);
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
                        template = template.Replace("[gio-phuc-vu]", "Giờ phục vụ dự kiến: " + printModel.GioPVDK);

                        template = template.Replace("[so-xe]", getStringValue(printModel.SoXe));
                        template = template.Replace("[phone]", getStringValue(printModel.Phone));
                        template = template.Replace("[ma-kh]", getStringValue(printModel.MaKH));
                        template = template.Replace("[ten-kh]", getStringValue(printModel.TenKH));
                        template = template.Replace("[dia-chi]", getStringValue(printModel.DiaChi));
                        template = template.Replace("[ma-dv]", getStringValue(printModel.MaDV));
                        template = template.Replace("[dob]", getIntValue(printModel.DOB));

                        template = template.Replace("[cat-giay]", "\x1b\x69|+|");

                        var arr = template.Split(new string[] { "|+|" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        for (int ii = 0; ii < _temp[i].PrintPages; ii++)
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
            else
                errorsms = "Cổng COM máy in hiện tại chưa kết nối. Vui lòng kiểm tra lại COM máy in";
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
}
