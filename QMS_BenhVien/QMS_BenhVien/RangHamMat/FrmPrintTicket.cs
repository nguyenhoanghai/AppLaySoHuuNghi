using API_KetNoi.Models;
using GPRO.Core.Hai;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using Newtonsoft.Json;
using QMS_System.Data.BLL;
using QMS_System.Data.BLL.HuuNghi;
using QMS_System.Data.Enum;
using QMS_System.Data.Model;
using QMS_System.ThirdApp.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QMS_BenhVien.RangHamMat
{
    public partial class FrmPrintTicket : Form
    {
        string currentPanel = FormPrintTicketState.searchCCCD;
        string connectString = BaseCore.Instance.GetEntityConnectString(Application.StartupPath + "\\DATA.XML"),
             comName = string.Empty,
            errorsms = string.Empty,
            apiAddress = string.Empty;

        List<TinhThanhModel> tinhs = new List<TinhThanhModel>();
        List<QuanHuyenModel> quans = new List<QuanHuyenModel>();
        List<PhuongXaModel> xas = new List<PhuongXaModel>();
        FrmMessagebox messagebox;
        Helper.ConfigModel cfObj = null;
        public static SerialPort COM_Printer = new SerialPort();
        public static List<PrintTicketModel> printTemplates;
        List<QMS_System.Data.Model.ConfigModel> configs;
        List<ServiceDayModel> lib_Services;
        int printType = 1,
           startNumber = 1,
             printTicketReturnCurrentNumberOrServiceCode = 1,
           UseWithThirdPattern = 0,
           CheckTimeBeforePrintTicket = 0;
        static LoadingFunc loadingFunc = new LoadingFunc();
        Thread threadCallAPI, threadAddNew;
        static SearchModel searchModel = null;
        BenhNhanModel benhNhanModel = null;
        public static double giayHeight = 1, giayWidth = 1;
        public int serKhuAId = 0, addNewType = 0, serKhuCId = 0;

        public FrmPrintTicket()
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
            var frm = new FrmSetting();
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

        private void CheckState()
        {
            switch (currentPanel)
            {
                case FormPrintTicketState.searchCCCD:
                    pnAddNew.Visible = false;
                    groupBox1.Visible = true;
                    groupBox1.Dock = DockStyle.Fill;

                    if (chkSearchByCCCD.Checked)
                    {
                        pnByCCCD.Visible = true;
                        chkByName.Checked = false;
                        pnByCCCD.Dock = DockStyle.Fill;
                        pnSearchName.Visible = false;
                    }
                    if (chkByName.Checked)
                    {
                        chkSearchByCCCD.Checked = false;
                        pnSearchName.Visible = true;
                        pnSearchName.Dock = DockStyle.Fill;
                        pnByCCCD.Visible = false;
                    }
                    break;
                case FormPrintTicketState.addNew:
                    groupBox1.Visible = false;
                    pnAddNew.Visible = true;
                    pnAddNew.Dock = DockStyle.Fill;
                    break;
            }
        }

        #endregion

        private void FrmPrintTicket_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["isAdmin"] != null &&
                       !string.IsNullOrEmpty(ConfigurationManager.AppSettings["isAdmin"].ToString()) &&
                      ConfigurationManager.AppSettings["isAdmin"].ToString() == "1")
            {
                btnSetting.Visible = true;
                btSQLConnect.Visible = true;
            }
            groupBox1.Dock = DockStyle.Fill;
            CheckState();
            GetConfig();
            CallAPI();
            GenerateDate();
            btnMaximize_Click(sender, e);
            // customCheckbox1.OnLoad();
        }

        #region Config

        private void GetConfig()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                          ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                string filePath = Application.StartupPath + "\\Config.XML";
                cfObj = QMS_BenhVien.Helper.Helper.Instance.GetAppConfig(filePath);
                apiAddress = cfObj.COMName.Trim();
                serKhuAId = cfObj.tieptan;
                serKhuCId = cfObj.phatthuoc;
                giayHeight = Convert.ToDouble(cfObj.giayHeight);
                giayWidth = Convert.ToDouble(cfObj.giayWidth);

                lib_Services = BLLService.Instance.GetsForMain(connectString);
                //services = BLLService.Instance.Gets(connectString);
                configs = BLLConfig.Instance.Gets(connectString, true);
                int.TryParse(GetConfigByCode(eConfigCode.PrintType), out printType);
                int.TryParse(GetConfigByCode(eConfigCode.CheckTimeBeforePrintTicket), out CheckTimeBeforePrintTicket);
                int.TryParse(GetConfigByCode(eConfigCode.PrintTicketReturnCurrentNumberOrServiceCode), out printTicketReturnCurrentNumberOrServiceCode);
                int.TryParse(GetConfigByCode(eConfigCode.StartNumber), out startNumber);
                int.TryParse(GetConfigByCode(eConfigCode.UseWithThirdPattern), out UseWithThirdPattern);
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

        #endregion

        #region AddNew
        private void btSendAPI_Click(object sender, EventArgs e)
        {
            AddNewBN();
        }

        private void AddNewBN()
        {
            if (CheckValid())
            {
                benhNhanModel = new BenhNhanModel();
                benhNhanModel.socmnd = txtCCCD.Text;
                benhNhanModel.hoten = txtName.Text;
                benhNhanModel.ngaysinh = cbDay.Text + "/" + cbMonth.Text + "/" + cbYear.Text;
                benhNhanModel.dienthoai = txtPhone.Text;
                benhNhanModel.cholam = txtPlace.Text;
                benhNhanModel.thon = txtThon.Text;
                benhNhanModel.madantoc = (cbDanToc.SelectedItem != null ? ((DanTocModel)cbDanToc.SelectedItem).madantoc : "");
                benhNhanModel.phai = (cbSex.SelectedItem != null ? Convert.ToInt32(((GioiTinhModel)cbSex.SelectedItem).id) : 0);
                benhNhanModel.mann = (cbJob.SelectedItem != null ? ((NgheNghiepModel)cbJob.SelectedItem).mann : "");
                // model.vungmien = (cbDanToc.SelectedItem != null ? ((DanTocModel)cbDanToc.SelectedItem).madantoc : "");
                benhNhanModel.matt = (cbProvince.SelectedItem != null ? ((TinhThanhModel)cbProvince.SelectedItem).matt : "");
                benhNhanModel.maqu = (cbDistrict.SelectedItem != null ? ((QuanHuyenModel)cbDistrict.SelectedItem).maqu : "");
                benhNhanModel.maphuongxa = (cbXa.SelectedItem != null ? ((PhuongXaModel)cbXa.SelectedItem).maphuongxa : "");
                benhNhanModel.namsinh = cbYear.Text;

                threadAddNew = new Thread(this.AddNewAsync);
                threadAddNew.IsBackground = true;
                threadAddNew.Start();
                FrmLoading frmLoading = new FrmLoading();
                frmLoading.ShowDialog();
            }
        }

        private async void AddNewAsync()
        {
            var client = APIClient.Instance.InitAPI(apiAddress);
            // tim theo cccd trước
            searchModel = new SearchModel();
            searchModel.IsCCCD = true;
            searchModel.CCCD = txtCCCD.Text; 
            string query = "tblbenhnhandksearch?jsonSearchModel=" + JsonConvert.SerializeObject(searchModel);
            string response = await client.GetStringAsync(query);
            var searchRecords = JsonConvert.DeserializeObject<List<BenhNhanModel>>(response);
            if (searchRecords != null && searchRecords.Count > 0)
            {
                closeForm("FrmLoading");
                threadAddNew.Abort();
                _showMessage((int)eMessageType.error, "Số CCCD/CMND/Mã định danh này đã được đăng ký trên hệ thống. Vui lòng nhấn nút QUAY LẠI TRA CỨU để tra cứu thông tin.");
            }
            else
            {
                query = "tblbenhnhanadd?jsonBNObject=" + JsonConvert.SerializeObject(benhNhanModel);
                response = await client.GetStringAsync(query);
                var maBN = JsonConvert.DeserializeObject<string>(response);
                closeForm("FrmLoading");
                threadAddNew.Abort();

                if (!string.IsNullOrEmpty(maBN))
                {
                    switch (addNewType)
                    {
                        case 1:
                            PrintTicket(serKhuAId, benhNhanModel.hoten.ToUpper(), benhNhanModel.thon, Convert.ToInt32(benhNhanModel.namsinh), maBN, benhNhanModel.dienthoai);
                            benhNhanModel = null; break;
                        case 2:
                            InPhieuDungDriver(0, 0, benhNhanModel.ngaysinh, maBN, benhNhanModel.hoten, "Quý Khách vui lòng di chuyển đến quầy tiếp tân tầng 2 khu A. Xin cảm ơn.!", DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 1, -1);
                            _showMessage((int)eMessageType.info, "Quý Khách vui lòng di chuyển đến quầy tiếp tân tầng 2 khu A. Xin cảm ơn.!");
                            break;
                        case 3:
                            InPhieuDungDriver(0, 0, benhNhanModel.ngaysinh, maBN, benhNhanModel.hoten, "Quý Khách vui lòng di chuyển đến quầy tiếp tân khu B. Xin cảm ơn.!", DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 1, -1);
                            _showMessage((int)eMessageType.info, "Quý Khách vui lòng di chuyển đến quầy tiếp tân khu B. Xin cảm ơn.!");
                            break;
                        case 4:
                            InPhieuDungDriver(0, 0, benhNhanModel.ngaysinh, maBN, benhNhanModel.hoten, "Quý Khách vui lòng di chuyển đến quầy tiếp tân tầng 4 khu A. Xin cảm ơn.!", DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 1, -1);
                            _showMessage((int)eMessageType.info, "Quý Khách vui lòng di chuyển đến quầy tiếp tân tầng 4 khu A. Xin cảm ơn.!");
                            break;
                    }
                }
                else
                {
                    _showMessage((int)eMessageType.error, "Gửi thông tin thất bại. Vui lòng kiểm tra lại thông tin.");
                }
            }

        }

        bool isNumber(string value)
        {
            int number = 0;
            return int.TryParse(value, out number);
        }

        private bool CheckValid()
        {
            var flag = true;
            if (string.IsNullOrEmpty(txtCCCD.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số CCCD/CMND/Mã định danh cá nhân.");
                flag = false;
            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập họ tên.");
                flag = false;
            }
            else if (string.IsNullOrEmpty(cbDay.Text) || !isNumber(cbDay.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng chọn ngày sinh.");
                flag = false;
            }
            else if (string.IsNullOrEmpty(cbMonth.Text) || !isNumber(cbMonth.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng chọn tháng sinh.");
                flag = false;
            }
            else if (string.IsNullOrEmpty(cbYear.Text) || !isNumber(cbYear.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng chọn năm sinh.");
                flag = false;
            }
            else if (string.IsNullOrEmpty(txtPhone.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số điện thoại.");
                flag = false;
            }
            else if (txtPhone.Text.Trim().Length < 10)
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập đúng số điện thoại.");
                flag = false;
            }

            else if (string.IsNullOrEmpty(txtThon.Text))
            {
                _showMessage((int)eMessageType.error, "Vui lòng nhập số nhà, thôn, phố.");
                flag = false;
            }
            return flag;
        }

        #endregion

        private void GenerateDate()
        {
            try
            {
                List<ModelSelectItem> ngays, thangs, nams;
                ngays = new List<ModelSelectItem>();
                thangs = new List<ModelSelectItem>();
                nams = new List<ModelSelectItem>();

                for (int i = 1; i <= 31; i++)
                {
                    string num = (i < 10 ? ("0" + i) : i.ToString());
                    ngays.Add(new ModelSelectItem() { Id = i, Name = num });
                }
                for (int i = 1; i <= 12; i++)
                {
                    string num = (i < 10 ? ("0" + i) : i.ToString());
                    thangs.Add(new ModelSelectItem() { Id = i, Name = num });
                }
                for (int i = 1920; i <= DateTime.Now.Year; i++)
                {
                    nams.Add(new ModelSelectItem() { Id = i, Name = i.ToString() });
                }

                cbNgay_key.DataSource = ngays;
                cbDay.DataSource = ngays;
                cbThang_key.DataSource = thangs;
                cbMonth.DataSource = thangs;
                cbNam_key.DataSource = nams;
                cbNam_key.SelectedValue = (DateTime.Now.Year);
                cbYear.DataSource = nams;
                cbYear.SelectedValue = (DateTime.Now.Year);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btSearchByCCCDForm_Click(object sender, EventArgs e)
        {
            currentPanel = FormPrintTicketState.searchCCCD;
            CheckState();
        }

        private void btChangeAddNew_Click(object sender, EventArgs e)
        {
            currentPanel = FormPrintTicketState.addNew;
            CheckState();
        }

        #region Search BN 
        private void btSearchByCCCD_Click(object sender, EventArgs e)
        {
            //search by name
            if (chkByName.Checked)
            {
                if (!string.IsNullOrEmpty(txtname_key.Text))
                {
                    string aaa = BaseCore.Instance.ConvertVN(txtname_key.Text);

                    aaa = aaa.Replace(" ", "").ToUpper();
                    bool flag = true;
                    if (string.IsNullOrEmpty(cbNgay_key.Text) || !isNumber(cbNgay_key.Text))
                    {
                        _showMessage((int)eMessageType.error, "Vui lòng chọn ngày sinh.");
                        flag = false;
                    }
                    else if (string.IsNullOrEmpty(cbThang_key.Text) || !isNumber(cbThang_key.Text))
                    {
                        _showMessage((int)eMessageType.error, "Vui lòng chọn tháng sinh.");
                        flag = false;
                    }
                    else if (string.IsNullOrEmpty(cbNam_key.Text) || !isNumber(cbNam_key.Text))
                    {
                        _showMessage((int)eMessageType.error, "Vui lòng chọn năm sinh.");
                        flag = false;
                    }

                    if (flag)
                    {
                        searchModel = new SearchModel();
                        searchModel.IsCCCD = false;
                        searchModel.FullName = BaseCore.Instance.ConvertVN(txtname_key.Text).Replace(" ", "").ToUpper();//  txtname_key.Text;
                        searchModel.DateOfBirth = cbNgay_key.Text + "/" + cbThang_key.Text + "/" + cbNam_key.Text;
                        threadCallAPI = new Thread(this.SearchBN);
                        threadCallAPI.IsBackground = true;
                        threadCallAPI.Start();
                        FrmLoading frmLoading = new FrmLoading();
                        frmLoading.ShowDialog();
                    }
                }
                else
                {
                    _showMessage((int)eMessageType.error, "Vui lòng nhập họ tên bệnh nhân cần tìm.");
                }
            }
            else  //search by cccd
            {
                if (!string.IsNullOrEmpty(txtCCCD_key.Text))
                {
                    searchModel = new SearchModel();
                    searchModel.DateOfBirth = string.Empty; searchModel.DateOfBirth = string.Empty;
                    searchModel.IsCCCD = true;
                    searchModel.CCCD = txtCCCD_key.Text;
                    threadCallAPI = new Thread(this.SearchBN);
                    threadCallAPI.IsBackground = true;
                    threadCallAPI.Start();
                    FrmLoading frmLoading = new FrmLoading();
                    frmLoading.ShowDialog();
                }
                else
                {
                    _showMessage((int)eMessageType.error, "Vui lòng nhập số CMND/CCCD/Mã định danh cần tìm.");
                }
            }

            //InPhieuDungDriver(9999, 9998, "Tiep benh", "Tiep benh", "Nguyen van A", "BV RHM", "01/01/0001 01:01");
        }

        private async void SearchBN()
        {
            var client = APIClient.Instance.InitAPI(apiAddress);
            string usr = "tblbenhnhandksearch?jsonSearchModel=" + JsonConvert.SerializeObject(searchModel);
            var response = await client.GetStringAsync(usr);
            var data = JsonConvert.DeserializeObject<List<BenhNhanModel>>(response);
            closeForm("FrmLoading");
            threadCallAPI.Abort();
            if (data != null && data.Count > 0)
            {
                var f = new FrmSearchResult(data, this);
                f.ShowDialog();
            }
            else
            {
                if (searchModel.IsCCCD)
                {
                    _showMessage((int)eMessageType.error, "Không tìm thấy thông tin bệnh nhân với CCCD/CMND/Mã định danh vừa nhập. Vui lòng kiểm tra lại thông tin.");

                }
                else
                {
                    _showMessage((int)eMessageType.error, "Không tìm thấy thông tin bệnh nhân với họ tên và ngày sinh vừa nhập. Vui lòng kiểm tra lại thông tin.");

                }
            }

        }

        #endregion

        #region CALL COMBOX API
        private async void CallAPI()
        {
            try
            {
                //www.api.benhvienranghammat.vn:6633
                HttpClient client = APIClient.Instance.InitAPI(apiAddress);
                var response = await client.GetStringAsync("tblphuongxa");
                xas = JsonConvert.DeserializeObject<List<PhuongXaModel>>(response);

                response = await client.GetStringAsync("tblgioitinh");
                var sex = JsonConvert.DeserializeObject<List<GioiTinhModel>>(response);
                cbSex.DataSource = sex;
                cbSex.SelectedValue = "0";

                response = await client.GetStringAsync("tbldantoc");
                var dantoc = JsonConvert.DeserializeObject<List<DanTocModel>>(response);
                cbDanToc.DataSource = dantoc;
                cbDanToc.SelectedValue = "25";

                response = await client.GetStringAsync("tblnghenghiep");
                var jobs = JsonConvert.DeserializeObject<List<NgheNghiepModel>>(response);
                cbJob.DataSource = jobs;
                cbJob.SelectedValue = "99";

                response = await client.GetStringAsync("tblquanhuyen");
                quans = JsonConvert.DeserializeObject<List<QuanHuyenModel>>(response);

                response = await client.GetStringAsync("tbltinhthanh");
                tinhs = JsonConvert.DeserializeObject<List<TinhThanhModel>>(response);
                cbProvince.DataSource = tinhs;
                cbProvince.SelectedValue = "701";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kết nối API lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (TinhThanhModel)cbProvince.SelectedItem;
            if (selected != null)
            {
                cbDistrict.DataSource = null;
                var _huyens = quans.Where(x => x.matt == selected.matt).ToList();
                cbDistrict.DataSource = _huyens;
                cbDistrict.ValueMember = "maqu";
                cbDistrict.DisplayMember = "tenquan";
            }
        }

        private void cbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (QuanHuyenModel)cbDistrict.SelectedItem;
            if (selected != null)
            {
                cbXa.DataSource = null;
                var _xas = xas.Where(x => x.maqu == selected.maqu).ToList();
                cbXa.DataSource = _xas;
                cbXa.ValueMember = "maphuongxa";
                cbXa.DisplayMember = "tenpxa";
            }
        }
        #endregion

        #region button color  
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


        private void btSearchByCCCD_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btSearchByCCCD);
        }

        private void btSearchByCCCD_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btSearchByCCCD, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btRHM_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btRHM);
        }

        private void btKTC_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKTC);
        }

        private void btHamEch_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btHamEch);
        }

        private void btTQuat_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btTQuat);
        }


        private void btAddNewForm_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btAddNewForm);
        }

        private void btAddNewForm_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btAddNewForm, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }



        private void btRHM_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btRHM, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btKTC_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKTC, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }
        private void btTQuat_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btTQuat, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }
        private void btHamEch_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btHamEch, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }
        #endregion

        #region In Phiếu

        public void PrintTicket(int serviceId, string tenBN, string diaChi, int namSinh, string maBN, string soDienThoai)
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
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, serObj.StartNumber, 0, now, printType, serObj.TimeProcess.TimeOfDay, tenBN, diaChi, namSinh, maBN, "", "", "", "", "", (int)eDailyRequireType.KhamBenh, soDienThoai);
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
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, startNumber, 0, now, printType, serObj.TimeProcess.TimeOfDay, tenBN, diaChi, namSinh, maBN, "", "", "", "", "", (int)eDailyRequireType.KhamBenh, soDienThoai);
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
            //TODO Test
            //MessageBox.Show("so phieu: " + newNumber);
            if (newNumber >= 0)
            {

                //TODO Đóng form result sau khi nhấn nút in phiếu
                /*
                 FormCollection fc = Application.OpenForms;
                for (int i = 0; i < fc.Count; i++)
                {
                    if (fc[i].Name == "FrmSearchResult")
                    {
                        fc[i].Close();
                    }
                }
                */

                errorsms = printStr.ToString();
                try
                {
                    var template = printTemplates.FirstOrDefault(x => x._ServiceIds.Contains(serviceId));
                    InPhieuDungDriver(printModel.STT, printModel.STTHienTai, printModel.TenQuay, maBN, printModel.TenKH, ("Bệnh viện Răng hàm mặt TW").ToUpper(), now.ToString("dd/MM/yyyy HH:mm"), template != null ? template.PrintPages : 1, serviceId);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void InPhieuDungDriver(int newNum, int oldNum, string tenquay, string tendichvu, string hoten, string tieude, string ngaygio, int solien, int serviceId)
        {
            LocalReport localReport = new LocalReport();
            try
            {
                //link cài report viewer cho máy client
                //https://www.microsoft.com/en-us/download/details.aspx?id=6442

                string fullPath = "";
                fullPath = Application.StartupPath + "\\RDLC_Template\\rhmReport.rdlc";
                if (serviceId == serKhuCId)
                    fullPath = Application.StartupPath + "\\RDLC_Template\\rhmReport_khuC.rdlc";

                if (serviceId == -1) // in phiếu rỗng
                    fullPath = Application.StartupPath + "\\RDLC_Template\\rhmReport_null.rdlc";

                // MessageBox.Show(fullPath);
                localReport.ReportPath = fullPath;

                ReportParameter[] reportParameters = new ReportParameter[5];
                reportParameters[0] = new ReportParameter("TenDV", tendichvu.ToUpper());
                reportParameters[1] = new ReportParameter("TieuDe", tieude.ToUpper());
                if (serviceId == -1)
                    reportParameters[2] = new ReportParameter("Stt", tenquay);
                else
                    reportParameters[2] = new ReportParameter("Stt", newNum.ToString());
                reportParameters[3] = new ReportParameter("TenBN", hoten.ToUpper());
                reportParameters[4] = new ReportParameter("NgayGio", ngaygio.ToUpper());

                localReport.SetParameters(reportParameters);

                for (int i = 0; i < solien; i++)
                {
                    PrintToPrinter(localReport);
                }

                // ClearForm();
                timerReset.Enabled = true;
            }
            catch (Exception ex)
            {
                localReport.Dispose();
                throw ex;
            }
        }

        #region Printer
        private static List<Stream> m_streams;


        private void btClear_cccd_Click(object sender, EventArgs e)
        {
            txtCCCD_key.Text = "";
            txtname_key.Text = "";
            if (chkByName.Checked)
                txtname_key.Focus();
            else
                txtCCCD_key.Focus();
        }

        private void btClear_name_Click(object sender, EventArgs e)
        {
            txtname_key.Text = "";
            var today = DateTime.Now;
            cbNgay_key.SelectedValue = today.Day;
            cbThang_key.SelectedValue = today.Month;
            cbNam_key.SelectedValue = today.Year;
        }

        private void btClear_add_Click(object sender, EventArgs e)
        {
            txtCCCD.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtPlace.Text = "";
            txtThon.Text = "";
        }

        private void customCheckbox1_Click(object sender, EventArgs e)
        {
            //if (chkSearchByCCCD.Checked)
            //{
            //    pnByCCCD.Visible = true;
            //    chkByName.Checked = false;
            //    pnByCCCD.Dock = DockStyle.Fill;
            //    pnSearchName.Visible = false;
            //    txtname_key.Focus();
            //}
            //else
            //{
            //    //if (!chkByName.Checked)
            //    //    chkSearchByCCCD.Checked = true;
            //    pnByCCCD.Visible = false;
            //}
        }

        private void chkByName_Click(object sender, EventArgs e)
        {
            //if (chkByName.Checked)
            //{
            //    chkSearchByCCCD.Checked = false;
            //    pnSearchName.Visible = true;
            //    pnSearchName.Dock = DockStyle.Fill;
            //    txtCCCD_key.Focus();
            //    pnByCCCD.Visible = false;
            //}
            //else
            //{
            //    //if (!chkSearchByCCCD.Checked)
            //    //    chkByName.Checked = true;
            //    pnSearchName.Visible = false;
            //}

        }

        private void btAddNewForm_Click(object sender, EventArgs e)
        {
            currentPanel = FormPrintTicketState.addNew;
            CheckState();
        }

        private void btRHM_Click(object sender, EventArgs e)
        {
            addNewType = 1; //in so khu a tt
            AddNewBN();
        }

        private void btKTC_Click(object sender, EventArgs e)
        {
            addNewType = 2; //in so khu b tt
            AddNewBN();
        }

        private void btTQuat_Click(object sender, EventArgs e)
        {
            addNewType = 3; //in so khu b tt
            AddNewBN();
        }

        private void chkSearchByCCCD_MouseDown(object sender, MouseEventArgs e)
        {
            if (chkSearchByCCCD.Checked)
            {
                pnByCCCD.Visible = true;
                chkByName.Checked = false;
                pnByCCCD.Dock = DockStyle.Fill;
                pnSearchName.Visible = false;
                txtname_key.Focus();
            }
            else
            {
                //if (!chkByName.Checked)
                //    chkSearchByCCCD.Checked = true;
                pnByCCCD.Visible = false;
            }
        }

        private void chkByName_MouseDown(object sender, MouseEventArgs e)
        {
            if (chkByName.Checked)
            {
                chkSearchByCCCD.Checked = false;
                pnSearchName.Visible = true;
                pnSearchName.Dock = DockStyle.Fill;
                txtCCCD_key.Focus();
                pnByCCCD.Visible = false;
            }
            else
            {
                //if (!chkSearchByCCCD.Checked)
                //    chkByName.Checked = true;
                pnSearchName.Visible = false;
            }
        }
         

        private void btHamEch_Click(object sender, EventArgs e)
        {
            addNewType = 4; //in so khu b tt
            AddNewBN();
        }

        private static int m_currentPageIndex = 0;
        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);
        }


        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{giayWidth}cm</PageWidth>
                <PageHeight>{giayHeight}cm</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            deviceInfo = deviceInfo.Replace("{giayHeight}", giayHeight.ToString());
            deviceInfo = deviceInfo.Replace("{giayWidth}", giayWidth.ToString());
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print();
            }
        }

        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
        #endregion
        #endregion

        private void _showMessage(int type, string message)
        {
            messagebox = new FrmMessagebox(type, message);
            messagebox.ShowDialog();
        }

        public static void showLoading()
        {
            loadingFunc.Show();
        }

        public void closeForm(string formName)
        {
            try
            {
                FormCollection fc = Application.OpenForms;
                for (int i = 0; i < fc.Count; i++)
                {
                    if (fc[i].Name == formName)
                    {
                        this.Invoke((MethodInvoker)delegate
                       {
                           fc[i].Close();
                       });

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    public static class FormPrintTicketState
    {
        public const string searchCCCD = "CCCD";
        public const string searchName = "searchName";
        public const string addNew = "addNew";

    }
}
