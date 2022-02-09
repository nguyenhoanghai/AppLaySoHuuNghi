using GPRO.Core.Hai;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using QMS_BenhVien.Helper;
using QMS_BenhVien.Model;
using QMS_System.Data.BLL;
using QMS_System.Data.BLL.HuuNghi;
using QMS_System.Data.Enum;
using QMS_System.Data.Model;
using QMS_System.Data.Model.HuuNghi;
using QMS_System.ThirdApp.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    public partial class Frmain_ver2 : Form
    {
        ToolTip tt;
        public static string ticketTemplate = string.Empty;
        public static int solien = 1;
        List<int> serviceIds = new List<int>();
        int[] permis = null;
        List<ServiceDayModel> lib_Services;
        List<QMS_System.Data.Model.ConfigModel> configs;
        int countQuetBHYT = 0,
            printType = 1,
            startNumber = 1,
              printTicketReturnCurrentNumberOrServiceCode = 1,
            UseWithThirdPattern = 0,
            CheckTimeBeforePrintTicket = 0,
            selectedServiceId = 0,
            sieuamId = 0,
            xquangId = 0,
            laymauId = 0,
            nhanKqId = 0,
            vienphiId = 0,
            phatthuocId = 0,
            tieptanId = 0,
            ctRoomId = 0;
        bool isDK_KetLuan = false;
        public static string connectString = BaseCore.Instance.GetEntityConnectString(Application.StartupPath + "\\DATA.XML"),
             comName = string.Empty,
             errorsms = string.Empty;
        ButtonStyleModel btStyle = new ButtonStyleModel() { Height = 100, Width = 100, ButtonInRow = 5, Margin = 10, fontStyle = "Arial, 36pt, style=Bold", BackColor = "#ffffff", ForeColor = "#0000ff" };
        HISReference.HISServiceClient serviceClient = new HISReference.HISServiceClient();
        string mabenhnhan = string.Empty;

        List<KhoaModel> khoas = new List<KhoaModel>();
        List<DMBenhVien> dMBenhViens = new List<DMBenhVien>();
        KhoaModel selectedKhoa = null;
        DichVuModel selectedDichVu = null;
        PhongKhamModel selectedPhongKham = null;
        public static string giayHeight = "1", giayWidth = "1";
        Helper.ConfigModel cfObj = null;

        FrmMessagebox messagebox;
        List<DatHenModel> dsDatHens = new List<DatHenModel>();
        DateTime bhFrom_congBHYT, bhTo_congBHYT, bhFrom, bhTo, ngaysinh_congBHYT, ngaysinh;
        List<ModelSelectItem> dsDichVuDaKham = new List<ModelSelectItem>();
        string _ngaygioinphieu = "";
        bool chuyenTuCongBHYT = false;

        public static SerialPort COM_Printer = new SerialPort();
        public static List<PrintTicketModel> printTemplates;

        public Frmain_ver2()
        {
            InitializeComponent();
        }

        #region Form event
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Đóng ứng dụng");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (FrmConfirmbox.ShowDialog(this, "Có", "Không", "Bạn có thật sự muốn đóng chương trình không ?") == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            WindowState = FormWindowState.Maximized;
            btnMaximize.Visible = false;
            btnNormalSize.Visible = true;
            btnNormalSize.Location = new Point(btnMaximize.Location.X, btnMaximize.Location.Y);
            // if (permis != null)
            checkPermission();
        }
        private void btnNormalSize_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            WindowState = FormWindowState.Normal;
            btnMaximize.Visible = true;
            btnNormalSize.Visible = false;
            //if (permis != null)
            checkPermission();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            //if (permis != null)
            checkPermission();
        }

        private void btnfullscreen_Click(object sender, EventArgs e)
        {
            //if (panel1.Visible == true)
            //    panel1.Visible = false;
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            //TopMost = true;
            btnMaximize_Click(sender, e);
        }
        #endregion

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

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMaximize_Click(sender, e);
        }

        private void btnSetting_MouseHover(object sender, EventArgs e)
        {
            tt = new ToolTip();
            tt.SetToolTip(this.btnSetting, "Cấu hình hệ thống");
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            var frm = new FrmConfig();
            frm.ShowDialog();
        }

        private void btSQLConnect_MouseHover(object sender, EventArgs e)
        {
            tt = new ToolTip();
            tt.SetToolTip(this.btSQLConnect, "Kết nối CSDL");
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

        private void btnTemplateEditor_MouseHover(object sender, EventArgs e)
        {
            tt = new ToolTip();
            tt.SetToolTip(this.btnTemplateEditor, "Mẫu phiếu");
        }

        private void Frmain_ver2_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode)
            {
                case Keys.F11:
                    {
                        btnMaximize_Click(sender, e);
                        //if (panel1.Visible == true)
                        //    panel1.Visible = false;

                        FormBorderStyle = FormBorderStyle.None;
                        //WindowState = FormWindowState.Maximized;
                        //TopMost = true;
                        break;
                    }
                case Keys.Escape:
                    {
                        //if (panel1.Visible == false)
                        //    panel1.Visible = true;

                        // FormBorderStyle = FormBorderStyle.Sizable;
                        WindowState = FormWindowState.Normal;
                        TopMost = false;

                        btnNormalSize_Click(sender, e);
                        break;
                    }
            }
        }

        private void Frmain_ver2_Load(object sender, EventArgs e)
        {
            lbStatus.Text = "";
            //chuyenJson_CLS();
            txtma.Focus();
            pndsdichvu.Dock = DockStyle.Fill;
            pndskhoa.Dock = DockStyle.Fill;
            pndsPK.Dock = DockStyle.Fill;
            GetConfig();
            timerReset.Enabled = true;
            try
            {
                // var str = "[{'BenhNhan_Id':284711,'MaBenhNhan':'17002904','TenBenhNhan':'Trương Thị Hiên','ThoiGianHen_BatDau':'2020-08-25T07:20:37.34','ThoiGianHen_KetThuc':'2020-08-25T07:35:37.34','PhongKham_Id':580,'MaPhongKham':'KB_KB_NOI12','TenPhongKham':'211 - Khám Nội BS. Chung','DichVu_Id':20545,'MaDichVu':'239138','TenDichVu':'Khám CK Khám  bệnh B(BS, ThS, BSCI)','BacSi_Id':42903,'TenBacSi':'Lưu Quang Chung','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':21178,'MaBenhNhan':'08009486','TenBenhNhan':'Đỗ Văn Hằng','ThoiGianHen_BatDau':'2020-08-25T07:35:18','ThoiGianHen_KetThuc':'2020-08-25T09:05:18','PhongKham_Id':889,'MaPhongKham':'T4_TTN','TenPhongKham':'212 - Khám Thận tiết niệu','DichVu_Id':4890,'MaDichVu':'04890','TenDichVu':'Khám Nội [Thận Tiết Niệu]','BacSi_Id':43033,'TenBacSi':'Ngô Thị Tuyết Nga','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':132209,'MaBenhNhan':'13006321','TenBenhNhan':'Nguyễn Tất Thắng','ThoiGianHen_BatDau':'2020-08-25T07:59:26','ThoiGianHen_KetThuc':'2020-08-25T09:29:26','PhongKham_Id':891,'MaPhongKham':'KB_NTDTD2','TenPhongKham':'ĐTĐ2 Đái Tháo Đường 2','DichVu_Id':4161,'MaDichVu':'04161','TenDichVu':'Khám Nội tiết','BacSi_Id':42558,'TenBacSi':'Trần Thị Bích Liên','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':55216,'MaBenhNhan':'09031024','TenBenhNhan':'Phạm Văn Khôi','ThoiGianHen_BatDau':'2020-08-25T08:08:03.54','ThoiGianHen_KetThuc':'2020-08-25T08:23:03.54','PhongKham_Id':539,'MaPhongKham':'KB_KA_NOI2','TenPhongKham':'AN2 - Khám Nội 2 ','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':43464,'TenBacSi':'Bùi Trung Đức','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':23982,'MaBenhNhan':'09000367','TenBenhNhan':'Hồ Tiến Nghị','ThoiGianHen_BatDau':'2020-08-25T08:08:33.677','ThoiGianHen_KetThuc':'2020-08-25T08:23:33.677','PhongKham_Id':720,'MaPhongKham':'PBVSKTW3','TenPhongKham':'T3- Phòng Khám BVSKTW3','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':43126,'TenBacSi':'Trần Phương Nghĩa','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':396658,'MaBenhNhan':'20002963','TenBenhNhan':'Dương Xuân Thủy','ThoiGianHen_BatDau':'2020-08-25T08:10:42','ThoiGianHen_KetThuc':'2020-08-25T09:40:42','PhongKham_Id':774,'MaPhongKham':'KNT','TenPhongKham':'Khoa Nội tiết - đái tháo đường','DichVu_Id':20511,'MaDichVu':'239105','TenDichVu':'Khám CK Nội tiết đái tháo đường (BS, ThS, BSCKI)','BacSi_Id':42553,'TenBacSi':'Nguyễn Thị Thanh Thủy','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':401791,'MaBenhNhan':'20007094','TenBenhNhan':'Lương Duy Chiến','ThoiGianHen_BatDau':'2020-08-25T08:18:52','ThoiGianHen_KetThuc':'2020-08-25T09:48:52','PhongKham_Id':702,'MaPhongKham':'KB_KM_9PKN','TenPhongKham':'315 - BS Nguyệt','DichVu_Id':20496,'MaDichVu':'239091','TenDichVu':'Khám Chuyên Khoa  (BS, ThS, BSCKI) [Mắt]','BacSi_Id':42684,'TenBacSi':'Lê Thị Minh Nguyệt','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':401790,'MaBenhNhan':'20007093','TenBenhNhan':'Trần Thị Nhung','ThoiGianHen_BatDau':'2020-08-25T08:19:12.803','ThoiGianHen_KetThuc':'2020-08-25T08:34:12.803','PhongKham_Id':702,'MaPhongKham':'KB_KM_9PKN','TenPhongKham':'315 - BS Nguyệt','DichVu_Id':20496,'MaDichVu':'239091','TenDichVu':'Khám Chuyên Khoa  (BS, ThS, BSCKI) [Mắt]','BacSi_Id':42684,'TenBacSi':'Lê Thị Minh Nguyệt','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':27118,'MaBenhNhan':'09003464','TenBenhNhan':'Nguyễn Công Ích','ThoiGianHen_BatDau':'2020-08-25T08:33:28.687','ThoiGianHen_KetThuc':'2020-08-25T08:48:28.687','PhongKham_Id':577,'MaPhongKham':'KB_KB_CKTM11','TenPhongKham':'11B - CK Tim Mạch ','DichVu_Id':4944,'MaDichVu':'04944','TenDichVu':'Khám Nội [Tim Mạch - lần 6 trở lên]','BacSi_Id':43447,'TenBacSi':'Đỗ Thị Hải Linh','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':153702,'MaBenhNhan':'14009537','TenBenhNhan':'Nguyễn Văn Thinh','ThoiGianHen_BatDau':'2020-08-25T08:38:47','ThoiGianHen_KetThuc':'2020-08-25T10:08:47','PhongKham_Id':577,'MaPhongKham':'KB_KB_CKTM11','TenPhongKham':'11B - CK Tim Mạch ','DichVu_Id':4944,'MaDichVu':'04944','TenDichVu':'Khám Nội [Tim Mạch - lần 6 trở lên]','BacSi_Id':43447,'TenBacSi':'Đỗ Thị Hải Linh','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':401801,'MaBenhNhan':'20007104','TenBenhNhan':'Dương Thị Hồng Yến','ThoiGianHen_BatDau':'2020-08-25T08:49:14.973','ThoiGianHen_KetThuc':'2020-08-25T09:04:14.973','PhongKham_Id':574,'MaPhongKham':'KB_KB_CKTK13','TenPhongKham':'13B - CK Thần kinh ','DichVu_Id':20532,'MaDichVu':'239126','TenDichVu':'Khám TS, BSCKII [Thần kinh]','BacSi_Id':42749,'TenBacSi':'Nguyễn Đức Trung','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':155604,'MaBenhNhan':'14011438','TenBenhNhan':'Phạm Văn Khiên','ThoiGianHen_BatDau':'2020-08-25T08:54:42','ThoiGianHen_KetThuc':'2020-08-25T10:24:42','PhongKham_Id':576,'MaPhongKham':'KB_KB_NOI8','TenPhongKham':'204 - Khám Nội BS. Hồng','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':43317,'TenBacSi':'Nguyễn Thị Minh Hồng','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':75588,'MaBenhNhan':'10012919','TenBenhNhan':'Lê Thị Oanh','ThoiGianHen_BatDau':'2020-08-25T09:16:53.91','ThoiGianHen_KetThuc':'2020-08-25T09:31:53.91','PhongKham_Id':578,'MaPhongKham':'KB_KB_NOI5','TenPhongKham':'203 - Khám Nội BS. Thành','DichVu_Id':4900,'MaDichVu':'04900','TenDichVu':'Khám Nội [lần 2]','BacSi_Id':42618,'TenBacSi':'Đào Trọng Thành','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':24578,'MaBenhNhan':'09000956','TenBenhNhan':'Vũ T Mai Lương','ThoiGianHen_BatDau':'2020-08-25T09:26:09','ThoiGianHen_KetThuc':'2020-08-25T10:56:09','PhongKham_Id':786,'MaPhongKham':'KB_NTDTD1','TenPhongKham':'ĐTĐ - Đái Tháo Đường 1','DichVu_Id':4161,'MaDichVu':'04161','TenDichVu':'Khám Nội tiết','BacSi_Id':43331,'TenBacSi':'Nguyễn Thị Phương Trang','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':34439,'MaBenhNhan':'09010697','TenBenhNhan':'Nguyễn Văn Minh','ThoiGianHen_BatDau':'2020-08-25T09:58:43.41','ThoiGianHen_KetThuc':'2020-08-25T10:13:43.41','PhongKham_Id':549,'MaPhongKham':'KB_KB_NOI7','TenPhongKham':'201 - Khám Nội BS. Loan','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42902,'TenBacSi':'Lê Thị Hồng Loan','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':401816,'MaBenhNhan':'20007119','TenBenhNhan':'Lê Thanh Hòa','ThoiGianHen_BatDau':'2020-08-25T10:20:49','ThoiGianHen_KetThuc':'2020-08-25T11:50:49','PhongKham_Id':774,'MaPhongKham':'KNT','TenPhongKham':'Khoa Nội tiết - đái tháo đường','DichVu_Id':20511,'MaDichVu':'239105','TenDichVu':'Khám CK Nội tiết đái tháo đường (BS, ThS, BSCKI)','BacSi_Id':42553,'TenBacSi':'Nguyễn Thị Thanh Thủy','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':26191,'MaBenhNhan':'09002557','TenBenhNhan':'Nguyễn Văn Mão','ThoiGianHen_BatDau':'2020-08-25T10:22:18.613','ThoiGianHen_KetThuc':'2020-08-25T10:37:18.613','PhongKham_Id':889,'MaPhongKham':'T4_TTN','TenPhongKham':'212 - Khám Thận tiết niệu','DichVu_Id':4890,'MaDichVu':'04890','TenDichVu':'Khám Nội [Thận Tiết Niệu]','BacSi_Id':43033,'TenBacSi':'Ngô Thị Tuyết Nga','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':99439,'MaBenhNhan':'11013871','TenBenhNhan':'Vũ Quốc Khánh','ThoiGianHen_BatDau':'2020-08-25T10:23:32.137','ThoiGianHen_KetThuc':'2020-08-25T10:38:32.137','PhongKham_Id':786,'MaPhongKham':'KB_NTDTD1','TenPhongKham':'ĐTĐ - Đái Tháo Đường 1','DichVu_Id':4161,'MaDichVu':'04161','TenDichVu':'Khám Nội tiết','BacSi_Id':44527,'TenBacSi':'Vũ Ngọc Bích','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':401822,'MaBenhNhan':'20007125','TenBenhNhan':'Lưu Quang Hiếu','ThoiGianHen_BatDau':'2020-08-25T11:06:31.723','ThoiGianHen_KetThuc':'2020-08-25T11:21:31.723','PhongKham_Id':580,'MaPhongKham':'KB_KB_NOI12','TenPhongKham':'211 - Khám Nội BS. Chung','DichVu_Id':20545,'MaDichVu':'239138','TenDichVu':'Khám CK Khám  bệnh B(BS, ThS, BSCI)','BacSi_Id':42903,'TenBacSi':'Lưu Quang Chung','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':17257,'MaBenhNhan':'08005597','TenBenhNhan':'Phạm Thanh Hà','ThoiGianHen_BatDau':'2020-08-25T11:50:34.98','ThoiGianHen_KetThuc':'2020-08-25T12:05:34.98','PhongKham_Id':786,'MaPhongKham':'KB_NTDTD1','TenPhongKham':'ĐTĐ - Đái Tháo Đường 1','DichVu_Id':4161,'MaDichVu':'04161','TenDichVu':'Khám Nội tiết','BacSi_Id':44527,'TenBacSi':'Vũ Ngọc Bích','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':59745,'MaBenhNhan':'09035535','TenBenhNhan':'Bùi Ngọc Hải','ThoiGianHen_BatDau':'2020-08-25T13:40:36.793','ThoiGianHen_KetThuc':'2020-08-25T13:55:36.793','PhongKham_Id':577,'MaPhongKham':'KB_KB_CKTM11','TenPhongKham':'11B - CK Tim Mạch ','DichVu_Id':2383,'MaDichVu':'02383','TenDichVu':'Khám Nội [Tim Mạch]','BacSi_Id':42763,'TenBacSi':'Trần Thị Hải Hà','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':147467,'MaBenhNhan':'14003310','TenBenhNhan':'Đinh Hồng Thái','ThoiGianHen_BatDau':'2020-08-25T13:48:56','ThoiGianHen_KetThuc':'2020-08-25T15:18:56','PhongKham_Id':563,'MaPhongKham':'KB_KA_NOI3','TenPhongKham':'AN3 - Khám Nội 3','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42884,'TenBacSi':'Tường Thị Vân Anh','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':33733,'MaBenhNhan':'09010002','TenBenhNhan':'Lê Phúc Thành','ThoiGianHen_BatDau':'2020-08-25T14:26:54.613','ThoiGianHen_KetThuc':'2020-08-25T14:41:54.613','PhongKham_Id':578,'MaPhongKham':'KB_KB_NOI5','TenPhongKham':'203 - Khám Nội BS. Thành','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42618,'TenBacSi':'Đào Trọng Thành','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':17849,'MaBenhNhan':'08006187','TenBenhNhan':'Nguyễn Ngọc Cảnh','ThoiGianHen_BatDau':'2020-08-25T14:28:41','ThoiGianHen_KetThuc':'2020-08-25T15:58:41','PhongKham_Id':580,'MaPhongKham':'KB_KB_NOI12','TenPhongKham':'211 - Khám Nội BS. Chung','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42903,'TenBacSi':'Lưu Quang Chung','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':62125,'MaBenhNhan':'09037915','TenBenhNhan':'Dương Văn Bình','ThoiGianHen_BatDau':'2020-08-25T14:28:58','ThoiGianHen_KetThuc':'2020-08-25T15:58:58','PhongKham_Id':549,'MaPhongKham':'KB_KB_NOI7','TenPhongKham':'201 - Khám Nội BS. Loan','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42902,'TenBacSi':'Lê Thị Hồng Loan','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':26763,'MaBenhNhan':'09003117','TenBenhNhan':'Nguyễn Văn Bắc','ThoiGianHen_BatDau':'2020-08-25T14:32:21','ThoiGianHen_KetThuc':'2020-08-25T14:47:21','PhongKham_Id':580,'MaPhongKham':'KB_KB_NOI12','TenPhongKham':'211 - Khám Nội BS. Chung','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42903,'TenBacSi':'Lưu Quang Chung','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':30739,'MaBenhNhan':'09007045','TenBenhNhan':'Lê Trần Mạnh','ThoiGianHen_BatDau':'2020-08-25T14:46:31.19','ThoiGianHen_KetThuc':'2020-08-25T15:01:31.19','PhongKham_Id':580,'MaPhongKham':'KB_KB_NOI12','TenPhongKham':'211 - Khám Nội BS. Chung','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42903,'TenBacSi':'Lưu Quang Chung','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':41098,'MaBenhNhan':'09017208','TenBenhNhan':'Nguyễn Thị Ngọc','ThoiGianHen_BatDau':'2020-08-25T14:54:41','ThoiGianHen_KetThuc':'2020-08-25T16:24:41','PhongKham_Id':576,'MaPhongKham':'KB_KB_NOI8','TenPhongKham':'204 - Khám Nội BS. Hồng','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':43317,'TenBacSi':'Nguyễn Thị Minh Hồng','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':16134,'MaBenhNhan':'08004481','TenBenhNhan':'Đỗ Tràng Thiện','ThoiGianHen_BatDau':'2020-08-25T15:03:43.57','ThoiGianHen_KetThuc':'2020-08-25T15:18:43.57','PhongKham_Id':549,'MaPhongKham':'KB_KB_NOI7','TenPhongKham':'201 - Khám Nội BS. Loan','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':42902,'TenBacSi':'Lê Thị Hồng Loan','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':401830,'MaBenhNhan':'20007133','TenBenhNhan':'Trương Quang Hoài Nam','ThoiGianHen_BatDau':'2020-08-25T15:37:59.28','ThoiGianHen_KetThuc':'2020-08-25T15:52:59.28','PhongKham_Id':929,'MaPhongKham':'CBCC','TenPhongKham':'Phòng Khám Cán Bộ Cao Cấp','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':43276,'TenBacSi':'Đào Văn Ninh','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':401831,'MaBenhNhan':'20007134','TenBenhNhan':'Nguyễn Thế Phương','ThoiGianHen_BatDau':'2020-08-25T15:40:10.597','ThoiGianHen_KetThuc':'2020-08-25T15:55:10.597','PhongKham_Id':929,'MaPhongKham':'CBCC','TenPhongKham':'Phòng Khám Cán Bộ Cao Cấp','DichVu_Id':1307,'MaDichVu':'01307','TenDichVu':'Khám Nội','BacSi_Id':43276,'TenBacSi':'Đào Văn Ninh','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':47115,'MaBenhNhan':'09023103','TenBenhNhan':'Nguyễn Thị Hợi','ThoiGianHen_BatDau':'2020-08-25T16:01:06.153','ThoiGianHen_KetThuc':'2020-08-25T16:16:06.153','PhongKham_Id':891,'MaPhongKham':'KB_NTDTD2','TenPhongKham':'ĐTĐ2 Đái Tháo Đường 2','DichVu_Id':4990,'MaDichVu':'04990','TenDichVu':'Khám Nội Tiết [lần 2]','BacSi_Id':43331,'TenBacSi':'Nguyễn Thị Phương Trang','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':133974,'MaBenhNhan':'13008086','TenBenhNhan':'Nguyễn Thị Dung(kim Dung)','ThoiGianHen_BatDau':'2020-08-25T16:17:29.17','ThoiGianHen_KetThuc':'2020-08-25T16:32:29.17','PhongKham_Id':891,'MaPhongKham':'KB_NTDTD2','TenPhongKham':'ĐTĐ2 Đái Tháo Đường 2','DichVu_Id':4161,'MaDichVu':'04161','TenDichVu':'Khám Nội tiết','BacSi_Id':43331,'TenBacSi':'Nguyễn Thị Phương Trang','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'DaXacNhan'},{'BenhNhan_Id':15450,'MaBenhNhan':'08003805','TenBenhNhan':'Lữ Thị Cẩm Vân','ThoiGianHen_BatDau':'2020-08-25T16:24:53.117','ThoiGianHen_KetThuc':'2020-08-25T16:39:53.117','PhongKham_Id':891,'MaPhongKham':'KB_NTDTD2','TenPhongKham':'ĐTĐ2 Đái Tháo Đường 2','DichVu_Id':4990,'MaDichVu':'04990','TenDichVu':'Khám Nội Tiết [lần 2]','BacSi_Id':43331,'TenBacSi':'Nguyễn Thị Phương Trang','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'},{'BenhNhan_Id':401828,'MaBenhNhan':'20007131','TenBenhNhan':'Phạm Thị Xuân Thu','ThoiGianHen_BatDau':'2020-08-25T16:48:17.027','ThoiGianHen_KetThuc':'2020-08-25T17:03:17.027','PhongKham_Id':786,'MaPhongKham':'KB_NTDTD1','TenPhongKham':'ĐTĐ - Đái Tháo Đường 1','DichVu_Id':20511,'MaDichVu':'239105','TenDichVu':'Khám CK Nội tiết đái tháo đường (BS, ThS, BSCKI)','BacSi_Id':42558,'TenBacSi':'Trần Thị Bích Liên','MaLoaiCuocHen':'HenTaiKham','TenLoaiCuocHen':'Hẹn tái khám','TrangThai':'ChoXacNhan'}]";
                // dsDatHens = JsonConvert.DeserializeObject<List<DatHenModel>>(str);

                //TODO
                //dsDatHens = JsonConvert.DeserializeObject<List<DatHenModel>>(serviceClient.LayDanhSachDatHenTrongNgay());
                // MessageBox.Show(dsDatHens.Count.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "loi lay ds dat hen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnMaximize_Click(sender, e);
        }

        private void GetConfig()
        {
            try
            {
                string filePath = Application.StartupPath + "\\Config.XML";
                cfObj = QMS_BenhVien.Helper.Helper.Instance.GetAppConfig(filePath);
                solien = cfObj.solien;
                if (!string.IsNullOrEmpty(cfObj.button_style))
                    btStyle = JsonConvert.DeserializeObject<ButtonStyleModel>(cfObj.button_style);

                configs = BLLConfig.Instance.Gets(connectString, true);
                lib_Services = BLLService.Instance.GetsForMain(connectString);
                int.TryParse(GetConfigByCode(eConfigCode.PrintType), out printType);
                int.TryParse(GetConfigByCode(eConfigCode.CheckTimeBeforePrintTicket), out CheckTimeBeforePrintTicket);
                int.TryParse(GetConfigByCode(eConfigCode.PrintTicketReturnCurrentNumberOrServiceCode), out printTicketReturnCurrentNumberOrServiceCode);
                int.TryParse(GetConfigByCode(eConfigCode.StartNumber), out startNumber);
                int.TryParse(GetConfigByCode(eConfigCode.UseWithThirdPattern), out UseWithThirdPattern);

                printTemplates = BLLPrintTemplate.Instance.Gets(connectString).Where(x => x.IsActive).ToList();
                
                vienphiId = cfObj.vienphi;
                laymauId = cfObj.laymau;
                nhanKqId = cfObj.ketqua;
                xquangId = cfObj.xquang;
                sieuamId = cfObj.sieuam;
                phatthuocId = cfObj.phatthuoc;
                tieptanId = cfObj.tieptan;
                ctRoomId = cfObj.CTRoom;
                string path = "";
                switch (cfObj.appType)
                {
                    case 0: //phòng khám
                        label2.Text = "KIOSK PHÒNG KHÁM | GPRO Gọi số - Hệ thống lấy số tự động";
                        path = Application.StartupPath + "\\JSON.XML";
                        pnKioskPK.Visible = true;
                        pnKioskPK.Dock = DockStyle.Fill;

                        if (!File.Exists(path))
                        {
                            _showMessage(4, "Không lấy được thông tin Khoa từ HIS.Vui lòng kiểm tra lại cấu hình hoặc đồng bộ lại.");
                            //chuyenJson();
                        }
                        else
                        {
                            // Open the file to read from.
                            string jsonText = File.ReadAllText(path);
                            khoas = JsonConvert.DeserializeObject<List<KhoaModel>>(jsonText);

                            // string json = "[{ Id : 1 , MaKhoa:'Khoa1', TenKhoa:'Khoa Hô hấp', DichVus :[{ Id:1, MaDichVu:'lần 1', TenDichVu:'lần 1', PhongKhams:[{ Id:1, MaPK:'PK1', TenPK:'Phòng khám 1' }] }] }, { Id : 2 , MaKhoa:'Khoa2', TenKhoa:'Khoa Gan', DichVus :[{ Id:1, MaDichVu:'lần 1', TenDichVu:'lần 1', PhongKhams:[{ Id:1, MaPK:'PK1', TenPK:'Phòng khám Gan 1' }] }] }]";
                            // khoas = JsonConvert.DeserializeObject<List<KhoaModel>>(json);

                            if (!string.IsNullOrEmpty(cfObj.permissions))
                            {
                                permis = cfObj.permissions.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

                                var _khoa = new List<KhoaModel>();
                                for (int i = 0; i < permis.Length; i++)
                                {
                                    _khoa.Add(khoas[permis[i]]);
                                }
                                khoas = _khoa;
                                checkPermission();
                            }
                            if (!string.IsNullOrEmpty(cfObj.services))
                                serviceIds = cfObj.services.Split(',').Select(x => Convert.ToInt32(x)).ToList();

                            TaoDanhSachKhoa();
                        }

                        path = Application.StartupPath + "\\DM_BV.XML";
                        if (!File.Exists(path))
                        {
                            _showMessage(4, "Không lấy được thông tin DMBenhVien từ HIS.Vui lòng kiểm tra lại cấu hình hoặc đồng bộ lại.");
                            //chuyenJson();
                        }
                        else
                        {
                            string jsonText = File.ReadAllText(path);
                            dMBenhViens = JsonConvert.DeserializeObject<List<DMBenhVien>>(jsonText);
                        }
                        break;
                    case 1: // CLS
                        label2.Text = "KIOSK CẬN LÂM SÀN | GPRO Gọi số - Hệ thống lấy số tự động";
                        pnCLS.Visible = true;
                        pnCLS.Dock = DockStyle.Fill;
                        path = Application.StartupPath + "\\JSON_CLS.XML";
                        lbTieuDeBHYT.Visible = false;
                        lbCLS.Visible = true;
                        txtBHYT.Focus();
                        btCLSClear.Location = new Point(pnCLS.Width - 310, pnCLS.Height - 130);
                        CLSClear();
                        break;
                    case 2: // viện phí
                        label2.Text = "KIOSK VIỆN PHÍ | GPRO Gọi số - Hệ thống lấy số tự động";
                        pnVP_PT.Visible = true;
                        pnVP_PT.Dock = DockStyle.Fill;
                        lbTieuDeBHYT.Enabled = false;
                        txtBHYT.Enabled = false;
                        lbCLS.Visible = false;
                        txtma.Focus();

                        btclear_VpPt.Location = new Point(pnVP_PT.Width - 310, pnVP_PT.Height - 130);
                        clear_VpPt();
                        break;
                    case 3: // phát thuốc
                        label2.Text = "KIOSK PHÁT THUỐC | GPRO Gọi số - Hệ thống lấy số tự động";
                        pnVP_PT.Visible = true;
                        pnVP_PT.Dock = DockStyle.Fill;
                        lbTieuDeBHYT.Enabled = false;
                        txtBHYT.Enabled = false;
                        lbCLS.Visible = false;
                        txtma.Focus();

                        btclear_VpPt.Location = new Point(pnVP_PT.Width - 310, pnVP_PT.Height - 130);
                        clear_VpPt();

                        //btPhatThuoc.Visible = true;
                        break;
                    case 4: // lấy máu XN
                        label2.Text = "KIOSK LẤY MẪU XÉT NGHIỆM | GPRO Gọi số - Hệ thống lấy số tự động";
                        pnVP_PT.Visible = true;
                        pnVP_PT.Dock = DockStyle.Fill;
                        lbTieuDeBHYT.Enabled = false;
                        txtBHYT.Enabled = false;
                        lbCLS.Visible = false;
                        txtma.Focus();

                        btclear_VpPt.Location = new Point(pnVP_PT.Width - 310, pnVP_PT.Height - 130);
                        clear_VpPt();
                        break;
                }

                giayHeight = cfObj.giayHeight;
                giayWidth = cfObj.giayWidth;
                if (!string.IsNullOrEmpty(cfObj.anhnen))
                    if (File.Exists(cfObj.anhnen))
                        panel2.BackgroundImage = Image.FromFile(cfObj.anhnen);
                    else
                    {
                        _showMessage(4, "Ảnh nền hiện không tồn tại. Xin vui lòng chọn lại ảnh nền.");
                    }

                //  timerReset.Interval = (cfObj.timeResetForm * 1000);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The underlying provider failed on Open.")
                {
                    frmSQLConnect f = new frmSQLConnect();
                    f.ShowDialog();
                }
            }
        }


        private void checkPermission()
        {
            switch (cfObj.appType)
            {
                case 1: // CLS
                    txtBHYT.Focus();
                    btCLSClear.Location = new Point(pnCLS.Width - 310, pnCLS.Height - 130);
                    break;
                case 2: // viện phí
                case 3: // phát thuốc
                case 4: // lấy máu XN    
                    txtma.Focus();
                    btclear_VpPt.Location = new Point(pnVP_PT.Width - 310, pnVP_PT.Height - 130);
                    break;
            }

            if (pndsPK.Visible)
                TaoNutPhongKham();
            else if (pndsdichvu.Visible)
                TaoNutDichVu();
            else
                TaoDanhSachKhoa();

            int _width = groupBox1.Width / 2 - 5;
            if (_width > 0)
                pn01.Width = _width;
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

        private void buttonControl_MouseDown(object sender, EventArgs e)
        {
            ButtonControl btn = (ButtonControl)sender;
            ButtonEffect_MouseDown(btn);
        }

        private void buttonControl_MouseUp(object sender, EventArgs e)
        {
            ButtonControl btn = (ButtonControl)sender;
            ButtonEffect_MouseUp(btn, btn.BackColor, btn.ForeColor, Color.Silver);
        }

        private void txtBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (cfObj.appType == 0 && txtBHYT.Text.Length > 15)
                    CheckThongTuyenBHYT();
                else
                    CheckPhieuChiDinhCLS();

        }
        private void txtma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CheckBenhNhanTrongHeThong(true);
        }
        private void label1_DoubleClick(object sender, EventArgs e)
        {
            btnNormalSize_Click(sender, e);
        }

        #region siêu âm - x-quang
        private void TaoNutSieuAm_X_Quang()
        {
            pndsPK.Controls.Clear();
            int pointX = pndsPK.Width / 2, pointY = pndsPK.Height / 2;
            Button btn;
            FontConverter converter = new FontConverter();
            btn = new Button();
            btn.Name = "btsieuam";
            btn.Text = ("in số thứ tự SIÊU ÂM").ToUpper();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Width = 305;
            btn.Height = 105;
            btn.Location = new Point(pointX - 360, pointY - 52);
            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
            btn.Anchor = AnchorStyles.Left;

            btn.Click += new System.EventHandler(this.btSieuAm_Click);
            this.pndsPK.Controls.Add(btn);

            btn = new Button();
            btn.Name = "btxquang";
            btn.Text = ("in số thứ tự chụp X-QUANG").ToUpper();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Width = 305;
            btn.Height = 105;
            btn.Location = new Point(pointX + 10, pointY - 52);
            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
            btn.Anchor = AnchorStyles.Left;

            btn.Click += new System.EventHandler(this.btXQuang_Click);
            this.pndsPK.Controls.Add(btn);
        }

        private void btXQuang_Click(object sender, EventArgs e)
        {
            PrintTicket(xquangId, DateTime.Now, "STT Chụp X-Quang", (int)eDailyRequireType.KhamBenh);
        }

        private void btSieuAm_Click(object sender, EventArgs e)
        {
            PrintTicket(sieuamId, DateTime.Now, "STT Siêu Âm", (int)eDailyRequireType.KhamBenh);
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            if (pndsdichvu.Visible)
            {
                pndsdichvu.Visible = false;
                pndskhoa.Visible = true;
                btBack.Enabled = false;
            }
            else if (pndsPK.Visible)
            {
                pndsPK.Visible = false;
                pndsdichvu.Visible = true;
            }
        }
        #endregion

        #region  Viện phí
        private void TaoVienPhi()
        {
            pndsPK.Controls.Clear();
            int pointX = pndsPK.Width / 2, pointY = pndsPK.Height / 2;
            Button btn;
            FontConverter converter = new FontConverter();
            btn = new Button();
            btn.Name = "btvienphi";
            btn.Text = ("in số thứ tự Thanh toán viện phí").ToUpper();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Width = 305;
            btn.Height = 105;
            btn.Location = new Point(pointX - 152, pointY - 52);
            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
            btn.Anchor = AnchorStyles.Left;

            btn.Click += new System.EventHandler(this.btVienPhi_Click);
            this.pndsPK.Controls.Add(btn);
        }

        private void btVienPhi_Click(object sender, EventArgs e)
        {
            PrintTicket(vienphiId, DateTime.Now, "STT Thanh Toán Viện Phí", (int)eDailyRequireType.KhamBenh);
        }
        #endregion

        #region  Phát thuốc
        private void TaoPhatThuoc()
        {
            pndsPK.Controls.Clear();
            int pointX = pndsPK.Width / 2, pointY = pndsPK.Height / 2;
            Button btn;
            FontConverter converter = new FontConverter();
            btn = new Button();
            btn.Name = "btphatthuoc";
            btn.Text = ("in số thứ tự cấp phát thuốc").ToUpper();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Width = 305;
            btn.Height = 105;
            btn.Location = new Point(pointX - 152, pointY - 52);
            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
            btn.Anchor = AnchorStyles.Left;

            btn.Click += new System.EventHandler(this.btPhatThuoc_Click);
            this.pndsPK.Controls.Add(btn);
        }

        private void btPhatThuoc_Click(object sender, EventArgs e)
        {
            PrintTicket(phatthuocId, DateTime.Now, "STT NHẬN THUỐC", (int)eDailyRequireType.KhamBenh);
        }
        #endregion

        #region ĐK lấy KQ XN 
        private void TaoLayKQ()
        {
            pndsPK.Controls.Clear();
            int pointX = pndsPK.Width / 2, pointY = pndsPK.Height / 2;
            Button btn;
            FontConverter converter = new FontConverter();
            btn = new Button();
            btn.Name = "btkq";
            btn.Text = ("in số thứ tự lấy kết quả XN").ToUpper();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Width = 305;
            btn.Height = 105;
            btn.Location = new Point(pointX - 152, pointY - 52);
            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
            btn.Anchor = AnchorStyles.Left;

            btn.Click += new System.EventHandler(this.btLayKQ_Click);
            this.pndsPK.Controls.Add(btn);
        }

        private void btLayKQ_Click(object sender, EventArgs e)
        {
            PrintTicket(nhanKqId, DateTime.Now, "STT NHẬN KQ CLS", (int)eDailyRequireType.KhamBenh);
        }
        #endregion

        #region ĐK lấy mẫu XN
        private void TaoLayMau()
        {
            pndsPK.Controls.Clear();
            int pointX = pndsPK.Width / 2, pointY = pndsPK.Height / 2;
            Button btn;
            FontConverter converter = new FontConverter();
            btn = new Button();
            btn.Name = "btdkxn";
            btn.Text = ("in số thứ tự đăng ký lấy mẫu XN").ToUpper();
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Width = 305;
            btn.Height = 105;
            btn.Location = new Point(pointX - 152, pointY - 52);
            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
            btn.Anchor = AnchorStyles.Left;

            btn.Click += new System.EventHandler(this.btLayMauXN_Click);
            this.pndsPK.Controls.Add(btn);
        }

        private void btLayMauXN_Click(object sender, EventArgs e)
        {
            PrintTicket(laymauId, DateTime.Now, "STT Lấy Mẫu Xét Nghiệm", (int)eDailyRequireType.KhamBenh);
        }
        #endregion

        private void btHelp_Click(object sender, EventArgs e)
        {
            PrintTicket(tieptanId, DateTime.Now, "STT giao dịch", (int)eDailyRequireType.KhamBenh);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtma.Text = "";
            txtDOB.Value = 1900;
            txtname.Text = "";
            txtBHYT.Text = "";
            txtAdd.Text = "";
            txtDKKCB.Text = "";
            txtngaysinh.Text = "--";
            txtthangsinh.Text = "--";
            pndskhoa.Visible = false;
            pndsdichvu.Visible = false;
            pndsPK.Visible = false;
            selectedDichVu = null;
            selectedKhoa = null;
            txtBHYT.Enabled = true;
            txtma.Enabled = true;
            btDatKhamTongDai.Enabled = false;
            btKetLuan.Enabled = true;
            txtma.Focus();
            dsDichVuDaKham = new List<ModelSelectItem>();
            lbbhFrom.Text = "---";
            lbbhTo.Text = "---";
            chuyenTuCongBHYT = false;
        }

        private void btnCheckUseInHIS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtma.Text))
            {
                _showMessage(4, "Vui lòng quẹt mã sổ hoặc nhập mã bệnh nhân");
                txtma.Focus();
            }
            else
            {
                CheckBenhNhanTrongHeThong(true);
            }
        }

        private void CheckBenhNhanTrongHeThong(bool pndskhoaVisible)
        {
            txtma.Enabled = false;
            mabenhnhan = txtma.Text;
            var result = serviceClient.GetBenhNhanInfo_ByMaBenhNhan(txtma.Text);
            if (result != null)
            {
                if (!string.IsNullOrEmpty(mabenhnhan))
                {
                    string ktra = serviceClient.Check_ChuaRaVien_ByMaBenhNhan(mabenhnhan);
                    // MessageBox.Show("Check_ChuaRaVien_ByMaBenhNhan : " + ktra);
                    if (ktra == "0")
                    {
                        try
                        {
                            txtname.Text = "";
                            txtAdd.Text = "";

                            string gender = result.GioiTinh == "T" ? "Nam" : "Nữ";
                            // MessageBox.Show(JsonConvert.SerializeObject(result));
                            if (result.NgaySinh.HasValue)
                            {
                                ngaysinh = result.NgaySinh.Value;
                            }
                            else
                            {
                                ngaysinh = DateTime.ParseExact(("01/01/" + result.NamSinh.Value + " 00:00:00"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            }

                            bhFrom = DateTime.ParseExact(result.BHYTTuNgay.Split(' ')[0], "M/d/yyyy", CultureInfo.InvariantCulture);
                            bhTo = DateTime.ParseExact(result.BHYTDenNgay.Split(' ')[0], "M/d/yyyy", CultureInfo.InvariantCulture);

                            txtname.Text = result.TenBenhNhan;
                            txtma.Text = mabenhnhan;
                            txtAdd.Text = result.DiaChi;
                            txtBHYT.Text = result.SoBHYT;

                            if (chuyenTuCongBHYT)
                            {
                                if (gender != cbGioitinh.Text ||
                                    ngaysinh != ngaysinh_congBHYT ||
                                    bhFrom != bhFrom_congBHYT ||
                                    bhTo != bhTo_congBHYT)
                                {
                                    _showMessage(4, "Thông tin thẻ BHYT trên cổng https://gdbhyt.baohiemxahoi.gov.vn và phần mềm HIS không trùng khớp, sẽ được cập nhập sau khi cấp Số thứ tự.");

                                    string ds = (lbbhFrom.Text + " - " + bhFrom.ToString("dd/MM/yyyy")) + " -> "
                                                                          + (lbbhTo.Text + " - " + bhTo.ToString("dd/MM/yyyy")) + " -> "
                                                                          + (ngaysinh.ToString("dd/MM/yyyy HH:mm:ss") + " - " + ngaysinh_congBHYT.ToString("dd/MM/yyyy HH:mm:ss")) + " -> "
                                                                          + gender + " - " + cbGioitinh.Text;
                                    MessageBox.Show(ds);

                                    ngaysinh = ngaysinh_congBHYT;
                                    bhFrom = bhFrom_congBHYT;
                                    bhTo = bhTo_congBHYT;
                                }
                            }
                            else
                            {
                                txtDKKCB.Text = "";

                                txtDOB.Value = result.NamSinh.Value;
                                txtngaysinh.Text = ngaysinh.Day.ToString();
                                txtthangsinh.Text = ngaysinh.Month.ToString();

                                cbGioitinh.Text = gender;

                                // lbStatus.Text = result.BHYTTuNgay.Split(' ')[0] + " - " + result.BHYTDenNgay.Split(' ')[0];
                                bhFrom = DateTime.ParseExact(result.BHYTTuNgay.Split(' ')[0], "M/d/yyyy", CultureInfo.InvariantCulture);
                                bhTo = DateTime.ParseExact(result.BHYTDenNgay.Split(' ')[0], "M/d/yyyy", CultureInfo.InvariantCulture);

                                lbbhFrom.Text = bhFrom.ToString("dd/MM/yyyy");
                                lbbhTo.Text = bhTo.ToString("dd/MM/yyyy");

                                txtDKKCB.Text = result.SoBHYT.Substring(result.SoBHYT.Length - 5);
                                var foundBV = dMBenhViens.FirstOrDefault(x => x.MaBenhVien.Trim() == txtDKKCB.Text.Trim());
                                txtDKKCB.Text = (txtDKKCB.Text + " - " + (foundBV != null ? foundBV.TenBenhVien : ""));
                            }

                            switch (cfObj.appType)
                            {
                                case 0:
                                    pndskhoa.Visible = pndskhoaVisible;
                                    btKetLuan.Enabled = true;
                                    btDatKhamTongDai.Enabled = true;

                                    LayThongTinKhamTrongNgay();
                                    break;
                                case 2:
                                    btVienPhi.Visible = true;
                                    txtma.Focus();
                                    break;
                                case 3:
                                    btPhatThuoc.Visible = true;
                                    txtma.Focus();
                                    break;
                                case 4:
                                    btLayMauXN.Visible = true;
                                    txtma.Focus();
                                    break;
                            }

                        }
                        catch (Exception ex)
                        {
                            _showMessage(4, "Kết nối máy chủ thất bại. Vui lòng liên hệ Bộ phận tiếp tân để được tư vấn. Xin cảm ơn!.");
                        }
                    }
                    else
                    {
                        _showMessage(4, "Bệnh nhân đang điều trị trong khoa " + ktra + " nên không thể lấy số thứ tự ở KIOS được. Vui lòng liên hệ Bộ phận tiếp tân để được tư vấn. Xin cảm ơn!.");
                    }
                }
            }
            else
            {
                _showMessage(4, "Không tìm thấy thông tin bệnh trong Hệ Thống. Vui lòng liên hệ Bộ phận tiếp tân để được tư vấn. Xin cảm ơn!.");
            }
        }

        private void LayThongTinKhamTrongNgay()
        {
            DataSet ds = serviceClient.LayThongTinKhamTrongNgay(mabenhnhan);
            DataTable table = ds.Tables[0];
            string alert = "";
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    alert += (dr["TenDichVu"].ToString());
                    alert += Environment.NewLine;
                    dsDichVuDaKham.Add(new ModelSelectItem()
                    {
                        Name = (dr["TenDichVu"].ToString()),
                        Code = (dr["MaDichVu"].ToString())
                    });
                }
            }
            if (!string.IsNullOrEmpty(alert))
            {
                _showMessage(3, String.Concat("Các dịch vụ đã đăng ký khám: ", Environment.NewLine, alert));
            }
        }

        private void btKTraBHYT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBHYT.Text))
            {
                _showMessage(4, "Vui lòng quét mã thẻ hoặc nhập số BHYT");
                txtma.Focus();
            }
            else if (string.IsNullOrEmpty(txtname.Text))
            {
                _showMessage(4, "Vui lòng nhập tên bệnh nhân");
                txtname.Focus();
            }
            else if (string.IsNullOrEmpty(txtDOB.Value.ToString()))
            {
                _showMessage(4, "Vui lòng nhập năm sinh bệnh nhân");
                txtDOB.Focus();
            }
            else
            {
                CheckThongTuyenBHYT();
            }
        }

        HISReference.KQNhanLichSuKCBBS bnInfo = null;
        private void CheckThongTuyenBHYT()
        {
            txtname.Text = "";
            txtAdd.Text = "";
            txtDKKCB.Text = "";

            bnInfo = null;
            mabenhnhan = txtma.Text;
            int ns = 0;
            if (!string.IsNullOrEmpty(txtDOB.Text))
                int.TryParse(txtDOB.Text, out ns);
            string sms = "", kq = "", _BHYT = "";
            _BHYT = txtBHYT.Text;
            bnInfo = serviceClient.NhanLichSuKCB(_BHYT, txtname.Text, txtDOB.Value.ToString(), ref sms, ref kq);
            bhFrom_congBHYT = DateTime.ParseExact(bnInfo.gtTheTuMoi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            bhTo_congBHYT = DateTime.ParseExact(bnInfo.gtTheDenMoi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //lbStatus.Text = "Gtri tu: " + bnInfo.gtTheTuMoi + " đến " + bnInfo.gtTheDenMoi;
            lbbhFrom.Text = bnInfo.gtTheTuMoi;
            lbbhTo.Text = bnInfo.gtTheDenMoi;
            LayThongTinKhamTrongNgay();
            txtname.Text = bnInfo.hoTen;
            string _strNS = "";
            if (bnInfo.ngaySinh.Length > 4)
                _strNS = bnInfo.ngaySinh + " 00:00:00";
            else
                _strNS = "01/01/" + bnInfo.ngaySinh + " 00:00:00";
            try
            {
                lbStatus.Text = _strNS;
                ngaysinh_congBHYT = DateTime.ParseExact(_strNS, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                txtDOB.Value = ngaysinh_congBHYT.Year;
                txtngaysinh.Text = ngaysinh_congBHYT.Day.ToString();
                txtthangsinh.Text = ngaysinh_congBHYT.Month.ToString();
            }
            catch (Exception)
            {
                _showMessage(4, _strNS);
            }
            cbGioitinh.Text = bnInfo.gioiTinh;
            txtAdd.Text = bnInfo.diaChi;
            txtBHYT.Enabled = false;

            var foundBV = dMBenhViens.FirstOrDefault(x => x.MaBenhVien.Trim() == bnInfo.maDKBD.Trim());
            txtDKKCB.Text = (bnInfo.maDKBD + " - " + (foundBV != null ? foundBV.TenBenhVien : ""));

            mabenhnhan = string.Empty;
            var checkKB = serviceClient.Check_KhamBenhLanDau((bnInfo.maThe + bnInfo.maDKBD), txtname.Text, (int)txtDOB.Value, (bnInfo.gioiTinh == "Nam" ? "T" : "G"), ref mabenhnhan);
            // MessageBox.Show(mabenhnhan);
            if (!string.IsNullOrEmpty(mabenhnhan))
            {
                chuyenTuCongBHYT = true;
                txtma.Text = mabenhnhan;
                txtma.Enabled = false;
                CheckBenhNhanTrongHeThong(true);
            }
            _showMessage(1, sms);
        }

        private void txtBHYT_Enter(object sender, EventArgs e)
        {

        }

        private void btKTraBHYT_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btKTraBHYT);
        }

        private void btKTraBHYT_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKTraBHYT, Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(114))))), Color.Yellow, Color.Silver);
        }

        private void btnCheckUseInHIS_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btnCheckUseInHIS);
        }

        private void btnCheckUseInHIS_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btnCheckUseInHIS, Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(114))))), Color.Yellow, Color.Silver);
        }

        private void btHelp_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btHelp);
        }

        private void btHelp_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btHelp, Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(114))))), Color.Yellow, Color.Silver);
        }

        private void btClear_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btClear);
        }

        private void btClear_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btClear, Color.White, Color.DimGray, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))));
        }

        private void btBack_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btBack);
        }

        private void btBack_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btBack, Color.White, Color.DimGray, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))));
        }

        #region Phòng khám
        private void TaoDanhSachKhoa()
        {
            ClearForm();
            pndsPK.Dock = DockStyle.None;
            pndsdichvu.Dock = DockStyle.None;
            pndskhoa.Dock = DockStyle.Fill;
            pndskhoa.Width = panel7.Width;
            pndskhoa.Visible = true;
            if (khoas.Count > 0)
            {
                pndskhoa.Controls.Clear();

                //  var services = BLLService.Instance.Gets_BenhVien(connectString).Where(x => serviceIds.Contains(x.Id) && x.isKetLuan == !isDangKy).ToList();
                // MessageBox.Show();
                // lbStatus.Text = btStyle.fontStyle + " - " + pndskhoa.Width + " - " + panel7.Width;

                if (khoas.Count > 0)
                {
                    int socot = btStyle.ButtonInRow;
                    if (khoas.Count <= socot)
                        socot = khoas.Count;
                    int sodong = (int)Math.Ceiling(khoas.Count / (double)socot),
                    sizeCot = (pndskhoa.Width - ((socot + 1) * btStyle.Margin)) / socot,
                    sizeDong = 100 / sodong;
                    FontConverter converter = new FontConverter();
                    ButtonControl btn;
                    int pointX = btStyle.Margin, pointY = btStyle.Margin;
                    int index = 0;

                    for (int i = 0; i < sodong; i++)
                    {
                        for (int ii = 0; ii < socot; ii++)
                        {
                            if (index < khoas.Count)
                            {
                                if (ii == 0)
                                    pointX = btStyle.Margin;
                                var found = khoas[index];
                                btn = new ButtonControl();
                                btn.Name = found.Id.ToString();
                                //btn.Text = "Khoa: " + found.Khoa + Environment.NewLine + "Phòng: " + found.PhongKham + Environment.NewLine + "BS: " + found.BacSi;
                                btn.ButtonText = (found.TenKhoa).ToUpper();
                                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                                btn.Width = sizeCot; // btStyle.Width;
                                btn.Height = btStyle.Height;
                                btn.Location = new Point(pointX, pointY);
                                try
                                {
                                    btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
                                }
                                catch (Exception) { }

                                btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
                                btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);

                                btn.Click += new System.EventHandler(this.btKhoa_Click);
                                btn.MouseDown += new MouseEventHandler(this.buttonControl_MouseDown);
                                btn.MouseUp += new MouseEventHandler(this.buttonControl_MouseUp);
                                pointX += sizeCot + btStyle.Margin;
                                this.pndskhoa.Controls.Add(btn);
                                index++;
                            }
                        }
                        pointY += btStyle.Height + btStyle.Margin;
                    }
                }
            }
        }

        private void btKhoa_Click(object sender, EventArgs e)
        {
            pndskhoa.Visible = false;
            pndsdichvu.Visible = true;
            int khoaId = Convert.ToInt32(((System.Windows.Forms.Control)sender).Name);
            selectedKhoa = khoas.FirstOrDefault(x => x.Id == khoaId);
            TaoNutDichVu();
        }

        private void TaoNutDichVu()
        {
            pndsPK.Dock = DockStyle.None;
            pndskhoa.Dock = DockStyle.None;
            pndsdichvu.Dock = DockStyle.Fill;
            if (selectedKhoa != null)
            {
                btBack.Enabled = true;
                pndsdichvu.Controls.Clear();
                if (selectedKhoa.DichVus.Count > 0)
                {

                    int socot = btStyle.ButtonInRow;
                    if (selectedKhoa.DichVus.Count <= socot)
                        socot = selectedKhoa.DichVus.Count;
                    int sodong = (int)Math.Ceiling(selectedKhoa.DichVus.Count / (double)socot),
                    sizeCot = (pndsdichvu.Width - ((socot + 1) * btStyle.Margin)) / socot,
                    sizeDong = 100 / sodong;

                    FontConverter converter = new FontConverter();
                    ButtonControl btn;
                    int pointX = btStyle.Margin, pointY = btStyle.Margin;
                    int index = 0;
                    for (int i = 0; i < sodong; i++)
                    {
                        for (int ii = 0; ii < socot; ii++)
                        {
                            if (index < selectedKhoa.DichVus.Count)
                            {
                                if (ii == 0)
                                    pointX = btStyle.Margin;
                                var found = selectedKhoa.DichVus[index];
                                btn = new ButtonControl();
                                btn.Name = found.Id.ToString();
                                //btn.Text = "Khoa: " + found.Khoa + Environment.NewLine + "Phòng: " + found.PhongKham + Environment.NewLine + "BS: " + found.BacSi;
                                btn.ButtonText = (found.TenDichVu).ToUpper();
                                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                                btn.Width = sizeCot;// btStyle.Width;
                                btn.Height = btStyle.Height;
                                btn.Location = new Point(pointX, pointY);
                                try
                                {
                                    // MessageBox.Show(btStyle.fontStyle);
                                    btn.Font = converter.ConvertFromString(btStyle.fontStyle) as Font;
                                }
                                catch (Exception)
                                {
                                }
                                btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
                                btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);
                                btn.MouseDown += new MouseEventHandler(this.buttonControl_MouseDown);
                                btn.MouseUp += new MouseEventHandler(this.buttonControl_MouseUp);

                                btn.Click += new System.EventHandler(this.btDichVu_Click);
                                pointX += sizeCot + btStyle.Margin;
                                this.pndsdichvu.Controls.Add(btn);
                                index++;
                            }
                        }
                        pointY += btStyle.Height + btStyle.Margin;
                    }
                }
            }
        }

        private void btDichVu_Click(object sender, EventArgs e)
        {
            int dichvuId = Convert.ToInt32(((System.Windows.Forms.Control)sender).Name);
            selectedDichVu = selectedKhoa.DichVus.FirstOrDefault(x => x.Id == dichvuId);
            if (dsDichVuDaKham.FirstOrDefault(x => x.Code == selectedDichVu.MaDichVu) != null)
                _showMessage(4, "Bạn đã sử dụng dịch vụ này rồi.Vui lòng chọn lại dịch vụ khác.");
            else
            {
                pndsdichvu.Visible = false;
                pndsPK.Visible = true;
                TaoNutPhongKham();
            }
        }

        private void TaoNutPhongKham()
        {
            try
            {
                pndskhoa.Dock = DockStyle.None;
                pndsdichvu.Dock = DockStyle.None;
                pndsPK.Dock = DockStyle.Fill;
                if (selectedDichVu != null)
                {
                    pndsPK.Controls.Clear();
                    if (selectedDichVu.PhongKhams.Count > 0)
                    {
                        int[] pkChecked = new int[] { };
                        if (!string.IsNullOrEmpty(cfObj.services))
                            pkChecked = cfObj.services.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

                        var _pkObjs = selectedDichVu.PhongKhams.Where(x => pkChecked.Contains(x.Id)).ToList();
                        var pkInfos = BLLHuuNghi.Instance.GetCurrentPKInfo(connectString, _pkObjs.Select(x => x.MaPK).ToList());
                        int socot = btStyle.ButtonInRow;
                        if (_pkObjs.Count <= socot)
                            socot = _pkObjs.Count;
                        int sodong = (int)Math.Ceiling(_pkObjs.Count / (double)socot),
                        sizeCot = (pndsPK.Width - ((socot + 1) * btStyle.Margin)) / socot,
                        sizeDong = 100 / sodong;
                        FontConverter converter = new FontConverter();
                        ButtonControl btn;
                        int pointX = btStyle.Margin, pointY = btStyle.Margin;
                        int index = 0;
                        for (int i = 0; i < sodong; i++)
                        {
                            for (int ii = 0; ii < socot; ii++)
                            {
                                if (index < _pkObjs.Count)
                                {
                                    if (ii == 0)
                                        pointX = btStyle.Margin;
                                    var found = _pkObjs[index];
                                    var _founfInfo = pkInfos.FirstOrDefault(x => x.MaPK == found.MaPK);
                                    //MessageBox.Show(found.Id.ToString());
                                    btn = new ButtonControl();
                                    btn.Name = found.Id.ToString();
                                    //btn.Text = "Khoa: " + found.Khoa + Environment.NewLine + "Phòng: " + found.PhongKham + Environment.NewLine + "BS: " + found.BacSi;
                                    btn.ButtonText = (found.TenPK.ToUpper()) + Environment.NewLine + _founfInfo.TenPK;
                                    btn.Cursor = System.Windows.Forms.Cursors.Hand;
                                    btn.Width = sizeCot;//btStyle.Width;
                                    btn.Height = btStyle.Height;
                                    btn.Location = new Point(pointX, pointY);
                                    try
                                    {
                                        // MessageBox.Show(btStyle.fontStyle);
                                        btn.Font = converter.ConvertFromString(btStyle.fontStyle) as Font;
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
                                    btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);

                                    btn.Click += new System.EventHandler(this.btPhongKham_Click);
                                    btn.MouseDown += new MouseEventHandler(this.buttonControl_MouseDown);
                                    btn.MouseUp += new MouseEventHandler(this.buttonControl_MouseUp);
                                    pointX += sizeCot + btStyle.Margin;
                                    this.pndsPK.Controls.Add(btn);
                                    index++;
                                }
                            }
                            pointY += btStyle.Height + btStyle.Margin;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btPhongKham_Click(object sender, EventArgs e)
        {
            int pkId = Convert.ToInt32(((System.Windows.Forms.Control)sender).Name);
            selectedPhongKham = selectedDichVu.PhongKhams.FirstOrDefault(x => x.Id == pkId);
            if (selectedPhongKham != null)
            {
                //MessageBox.Show(pk.TenPK);
                bool print = false;
                if (!isDK_KetLuan)
                {
                    //dky kham thi phai confirm truoc khi in
                    if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        print = true;
                    }
                }
                else
                    print = true;

                if (print)
                {
                    //TODO: 
                    //1. in phiếu
                    //2. gui thong tin cho FPT
                    // MessageBox.Show(selectedPhongKham.MaPK);
                    var qmsSer = BLLService.Instance.Get(connectString, selectedPhongKham.MaPK);
                    if (qmsSer != null)
                    {
                        //ktra hen
                        //var foundHen = dsDatHens.FirstOrDefault(x => x.MaBenhNhan == txtma.Text && x.MaPhongKham == selectedPhongKham.MaPK);
                        //if (foundHen != null)
                        //{
                        //    //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                        //    PhongKham_PrintTicket(qmsSer.Id, DateTime.Now, (!isDK_KetLuan ? "STT Kết Luận Bệnh" : "STT Khám Bệnh"), foundHen.ThoiGianHen_BatDau);

                        //}
                        //else
                        //{
                        //lbStatus.Text = selectedPhongKham.MaPK + "-" + qmsSer.Id;
                        PhongKham_PrintTicket(qmsSer.Id, DateTime.Now, "STT Khám Bệnh", null);
                        // }
                    }
                }
            }
        }

        #endregion

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

        #region CLS new 

        private void btXQ_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btXQ);
        }

        private void btXQ_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btXQ, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btXQ_Click(object sender, EventArgs e)
        {
            bool print = false;
            //dky kham thi phai confirm truoc khi in
            if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
            {
                print = true;
            }

            if (print)
            {
                //TODO: 
                //1. in phiếu
                //2. gui thong tin cho FPT
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                var qmsSer = BLLService.Instance.Get(connectString, "XQ");
                if (qmsSer != null)
                {
                    _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                    //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                    PrintTicket(qmsSer.Id, DateTime.Now, "STT CHỤP X-QUANG", (int)eDailyRequireType.KhamBenh);
                }

            }
        }


        private void btSA_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btSA);
        }

        private void btSA_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btSA, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btSA_Click(object sender, EventArgs e)
        {
            bool print = false;
            //dky kham thi phai confirm truoc khi in
            if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
            {
                print = true;
            }

            if (print)
            {
                //TODO: 
                //1. in phiếu
                //2. gui thong tin cho FPT
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                var qmsSer = BLLService.Instance.Get(connectString, "SA");
                if (qmsSer != null)
                {
                    _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                    //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                    PrintTicket(qmsSer.Id, DateTime.Now, "STT SIÊU ÂM", (int)eDailyRequireType.KhamBenh);
                }
            }
        }


        private void btCT_Click(object sender, EventArgs e)
        {
            bool print = false;
            //dky kham thi phai confirm truoc khi in
            if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
            {
                print = true;
            }

            if (print)
            {
                //TODO: 
                //1. in phiếu
                //2. gui thong tin cho FPT
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                var qmsSer = BLLService.Instance.Get(connectString, "XQ");
                if (qmsSer != null)
                {
                    _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                    //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                    PrintTicket(qmsSer.Id, DateTime.Now, "STT CT", (int)eDailyRequireType.KhamBenh);
                }

            }
        }

        private void btCT_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btCT);
        }

        private void btCT_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btCT, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }


        private void btCLSClear_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btCLSClear);
        }

        private void btCLSClear_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btCLSClear, Color.White, Color.DimGray, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))));
        }

        private void btCLSClear_Click(object sender, EventArgs e)
        {
            CLSClear();
        }

        private void CLSClear()
        {
            setEnable(true);
            btSA.Visible = false;
            btXQ.Visible = false;
            btCT.Visible = false;
            txtBHYT.Focus();
            lbbhFrom.Text = "---";
            lbbhTo.Text = "---";
            chuyenTuCongBHYT = false;
        }

        private void CheckPhieuChiDinhCLS()
        {
            if (!string.IsNullOrEmpty(txtBHYT.Text))
            {
                // lbStatus.Text = txtBHYT.Text;
                var datas = serviceClient.GetAllChiDinhCLS(txtBHYT.Text);
                if (datas.Tables[0] != null && datas.Tables[0].Rows.Count > 0)
                {
                    var table = datas.Tables[0];
                    txtma.Text = table.Rows[0][1].ToString();
                    CheckBenhNhanTrongHeThong(false);
                    setEnable(false);

                    btSA.Visible = false;
                    btXQ.Visible = false;
                    btCT.Visible = false;
                    string[] saRooms = new string[] { "0205", "0213", "0808" };
                    string[] xqRooms = new string[] { "0202", "0207", "0208", "0209", "0210", "0211", "0212" };
                    string[] ctRooms = new string[] { "0203" };
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i][5].ToString() == "ChuaThucHien")
                        {
                            string sophieu = table.Rows[i][2].ToString();
                            sophieu = sophieu.Split('.').ToArray()[1];
                            //  lbStatus.Text = sophieu;
                            if (saRooms.Contains(sophieu))
                            {
                                btSA.Visible = true;
                            }
                            else if (xqRooms.Contains(sophieu))
                            {
                                btXQ.Visible = true;
                            }
                            else if (ctRooms.Contains(sophieu))
                            {
                                btCT.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    _showMessage(4, "Không tìm thấy thông tin phiếu. Vui lòng quét lại số phiếu chỉ định thực hiện CLS.!");
                    txtBHYT.Focus();
                }
            }
            else
            {
                _showMessage(4, "Vui lòng quét số phiếu chỉ định thực hiện CLS.!");
                txtBHYT.Focus();
            }
        }

        private void setEnable(bool _enable)
        {
            txtBHYT.Enabled = _enable;
            txtma.Enabled = _enable;
            txtname.Enabled = _enable;
            txtAdd.Enabled = _enable;
            txtDKKCB.Enabled = _enable;
            txtDOB.Enabled = _enable;
            cbGioitinh.Enabled = _enable;
            if (_enable)
            {
                txtBHYT.Text = "";
                txtma.Text = "";
                txtname.Text = "";
                txtAdd.Text = "";
                txtDKKCB.Text = "";
                txtDOB.Value = 1990;
            }
        }
        #endregion

        #region Viện phí - phát thuốc - lấy máu xét nghiệm

        private void btclear_VpPt_Click(object sender, EventArgs e)
        {
            clear_VpPt();
        }

        private void clear_VpPt()
        {
            setEnable(true);
            txtBHYT.Enabled = false;
            txtma.Focus();
            btPhatThuoc.Visible = false;
            btVienPhi.Visible = false;
            btLayMauXN.Visible = false;
            lbbhFrom.Text = "---";
            lbbhTo.Text = "---";
            chuyenTuCongBHYT = false;
        }

        private void btclear_VpPt_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btclear_VpPt);
        }

        private void btclear_VpPt_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btclear_VpPt, Color.White, Color.DimGray, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))));
        }

        private void btVienPhi_Click_1(object sender, EventArgs e)
        {
            bool print = false;
            //dky kham thi phai confirm truoc khi in
            if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
            {
                print = true;
            }

            if (print)
            {
                _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                //TODO: 
                //1. in phiếu
                //2. gui thong tin cho FPT
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                // var qmsSer = BLLService.Instance.Get(connectString, selectedPhongKham.MaPK);
                // if (qmsSer != null)
                //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                PrintTicket(vienphiId, DateTime.Now, "STT THANH TOÁN VIỆN PHÍ", (int)eDailyRequireType.KhamBenh);
            }
        }
        private void btVienPhi_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btVienPhi);
        }
        private void btVienPhi_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btVienPhi, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        private void btPhatThuoc_Click_1(object sender, EventArgs e)
        {
            bool print = false;
            if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
            {
                print = true;
            }

            if (print)
            {
                _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                //TODO: 
                //1. in phiếu
                //2. gui thong tin cho FPT
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                // var qmsSer = BLLService.Instance.Get(connectString, selectedPhongKham.MaPK);
                // if (qmsSer != null)
                //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                PrintTicket(phatthuocId, DateTime.Now, "STT NHẬN THUỐC", (int)eDailyRequireType.KhamBenh);
            }
        }
        private void btPhatThuoc_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btPhatThuoc);
        }
        private void btPhatThuoc_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btPhatThuoc, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
        }

        #endregion

        #region lấy máu xét nghiệm
        private void btLayMauXN_Click_1(object sender, EventArgs e)
        {
            bool print = false;
            if (FrmConfirmbox.ShowDialog(this, "In phiếu", "Không", "Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?") == System.Windows.Forms.DialogResult.Yes)
            {
                print = true;
            }

            if (print)
            {
                _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                //TODO: 
                //1. in phiếu
                //2. gui thong tin cho FPT
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                // var qmsSer = BLLService.Instance.Get(connectString, selectedPhongKham.MaPK);
                // if (qmsSer != null)
                //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                PrintTicket(laymauId, DateTime.Now, "STT LẤY MẪU XÉT NGHIỆM", (int)eDailyRequireType.KhamBenh);
            }
        }

        private void btLayMauXN_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btLayMauXN);
        }

        private void btLayMauXN_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btLayMauXN, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);

        }
        #endregion

        #region kết luận  
        private void btKetLuan_Click(object sender, EventArgs e)
        {
            //pndskhoa.Visible = true;
            //ktra co dky kham chua
            var found = BLLHuuNghi.Instance.KiemTraDKKham(connectString, txtma.Text);
            if (found.IsSuccess)
            {
                PhongKhamKetLuan_PrintTicket("STT KẾT LUẬN BỆNH");
            }
            else
            {
                _showMessage(4, "Bạn cần phải đăng ký khám trước khi lấy STT kết luận bệnh án.");
            }
        }

        private void btKetLuan_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btKetLuan);
        }

        private void btKetLuan_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btKetLuan, Color.Pink, Color.Red, Color.Blue);
        }

        #endregion

        #region  đặt khám
        private void btDatKhamTongDai_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("DS dat hen: " + JsonConvert.SerializeObject(dsDatHens));
            //ktra hen
            var foundHen = dsDatHens.FirstOrDefault(x => x.MaBenhNhan == txtma.Text);
            if (foundHen != null)
            {
                if (foundHen.ThoiGianHen_KetThuc < DateTime.Now)
                {
                    _showMessage(4, "Thời gian hẹn của bạn đã kết thúc lúc :" + foundHen.ThoiGianHen_KetThuc.ToString("dd/MM/yyyy HH:mm") + ". Vui lòng chọn khoa phía dưới để chọn phòng khám.");
                }
                else
                {
                    selectedDichVu = new DichVuModel() { Id = foundHen.DichVu_Id, MaDichVu = foundHen.MaDichVu, TenDichVu = foundHen.TenDichVu };
                    selectedPhongKham = new PhongKhamModel() { Id = foundHen.PhongKham_Id, MaPK = foundHen.MaPhongKham, TenPK = foundHen.TenPhongKham };

                    var qmsSer = BLLService.Instance.Get(connectString, foundHen.MaPhongKham);
                    if (qmsSer != null)
                        //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                        PhongKham_PrintTicket(qmsSer.Id, DateTime.Now, "STT Khám Bệnh", foundHen.ThoiGianHen_BatDau);
                }
            }
            else
            {
                _showMessage(4, "Bạn chưa đặt khám. Vui lòng chọn khoa phía dưới để chọn phòng khám.");
            }
        }

        private void btDatKhamTongDai_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btDatKhamTongDai);
        }

        private void btDatKhamTongDai_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btDatKhamTongDai, Color.Pink, Color.Blue, Color.Blue);
        }
        #endregion

        private void _showMessage(int type, string message)
        {
            messagebox = new FrmMessagebox(type, message);
            messagebox.ShowDialog();
        }

        #region IN PHIEU
        private void PrintTicket(int serviceId, DateTime ServeTime, string tieude, int requireType)
        {
            int lastTicket = 0,
                newNumber = -1,
            nghiepVu = 0;
            string printStr = string.Empty,
                tenquay = string.Empty;
            bool err = false;
            ServiceDayModel serObj = null;
            DateTime now = DateTime.Now;

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
                            //  var rs = BLLDailyRequire.Instance.PrintNewTicket(connectString, serviceId, serObj.StartNumber, 0, now, printType, ServeTime.TimeOfDay, txtname.Text, txtAdd.Text, (!string.IsNullOrEmpty(txtDOB.Text) ? Convert.ToInt32(txtDOB.Text) : 0), txtma.Text, "", "", "");
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, serObj.StartNumber, 0, now, printType, ServeTime.TimeOfDay, txtname.Text, txtAdd.Text, (!string.IsNullOrEmpty(txtDOB.Text) ? Convert.ToInt32(txtDOB.Text) : 0), (string.IsNullOrEmpty(mabenhnhan) ? txtma.Text : mabenhnhan), "", "", "", "", "", requireType,"");
                            if (rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data_3);
                                tenquay = rs.Data_2;
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
                            var rs = BLLHuuNghi.Instance.PrintNewTicket(connectString, serviceId, startNumber, 0, now, printType, (ServeTime != null ? ServeTime.TimeOfDay : serObj.TimeProcess.TimeOfDay), txtname.Text, txtAdd.Text, Convert.ToInt32(txtDOB.Text), txtma.Text, "", "", "", "", "", requireType,"");
                            if (rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data_3);
                                tenquay = rs.Data_2;
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
                    InPhieuDungDriver(newNumber, lastTicket, tenquay, serObj.Name, txtname.Text, tieude, _ngaygioinphieu);
                }
                catch (Exception)
                {
                }

                bool kq = false;
                switch (cfObj.appType)
                {
                    case 0: //phong khám
                        if (requireType == (int)eDailyRequireType.KhamBenh && serviceId != cfObj.tieptan)
                        {
                            lbStatus.Text = "Bắt đầu gửi tiếp nhận lên HIS";
                            kq = serviceClient.Luu_TiepNhan(mabenhnhan, ngaysinh, (cbGioitinh.Text == "Nam" ? "T" : "G"), txtBHYT.Text, bhFrom, bhTo, selectedDichVu.Id, selectedPhongKham.Id, 0, newNumber.ToString());
                            string abc = String.Join("Luu_TiepNhan({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", mabenhnhan, ngaysinh.ToString("dd/MM/yyyy"), (cbGioitinh.Text == "Nam" ? "T" : "G"), txtBHYT.Text, bhFrom.ToString("dd/MM/yyyy"), bhTo.ToString("dd/MM/yyyy"), selectedDichVu.Id.ToString(), selectedPhongKham.Id.ToString(), "0", newNumber.ToString());
                            MessageBox.Show(abc);
                            if (!kq)
                            {
                                //_showMessage(4, "Lưu tiếp nhận lên HIS bị lỗi");
                            }
                            else
                            {
                                lbStatus.Text = "Gửi tiếp nhận lên HIS thành công.!";
                                ClearForm();
                            }
                        }
                        else
                            ClearForm();
                        break;
                    case 1: //cls
                        var ser = BLLService.Instance.Get(connectString, serviceId);
                        kq = serviceClient.SendSoThuTuToHIS(mabenhnhan, ser.Code, newNumber.ToString());
                        if (!kq)
                        {
                            //_showMessage(4, "Lưu tiếp nhận lên HIS bị lỗi");
                        }
                        else
                            CLSClear();
                        break;
                    case 2:
                    case 3:
                    case 4:
                        clear_VpPt(); break;
                }
            }
        }

        private void PhongKhamKetLuan_PrintTicket(string tieude)
        {
            try
            {
                int lastTicket = 0,
                             newNumber = -1,
                         nghiepVu = 0;
                string printStr = string.Empty,
                    tenquay = string.Empty;
                ServiceDayModel serObj = null;
                DateTime now = DateTime.Now;

                //MessageBox.Show(dsDatHens.Count.ToString());
                ResponseBase rs;
                rs = BLLHuuNghi.Instance.CapSoKetLuan(connectString, txtma.Text, DateTime.Now);

                if (rs.IsSuccess)
                {
                    lastTicket = (int)rs.Data;
                    nghiepVu = rs.Data_1;
                    newNumber = ((int)rs.Data_3);
                    tenquay = rs.Data_2;
                }
                else
                    errorsms = rs.Errors[0].Message;

                if (newNumber >= 0)
                {
                    try
                    {
                        _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                        InPhieuDungDriver(newNumber, lastTicket, tenquay, rs.str1, txtname.Text, tieude, _ngaygioinphieu);
                    }
                    catch (Exception ex)
                    {
                        _showMessage(4, "in phiếu bị lỗi" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _showMessage(4, "Cấp STT bị lỗi. Vui lòng thử lại.");
            }
        }

        private void PhongKham_PrintTicket(int serviceId, DateTime ServeTime, string tieude, DateTime? giohen)
        {
            try
            {
                int lastTicket = 0,
                             newNumber = -1,
                         nghiepVu = 0;
                string printStr = string.Empty,
                    tenquay = string.Empty;
                ServiceDayModel serObj = null;
                DateTime now = DateTime.Now;
                if (string.IsNullOrEmpty(mabenhnhan))
                    mabenhnhan = txtma.Text;
                serObj = lib_Services.FirstOrDefault(x => x.Id == serviceId);
                if (serObj == null)
                    errorsms = "Dịch vụ số " + serviceId + " không tồn tại. Xin quý khách vui lòng chọn dịch vụ khác.";
                else
                {
                    //MessageBox.Show(dsDatHens.Count.ToString());
                    ResponseBase rs;
                    if (giohen != null)
                    {
                        _ngaygioinphieu = String.Format("Hẹn khám qua điện thoại ngày {0} Giờ {1}", giohen.Value.ToString("dd/MM/yyyy"), giohen.Value.ToString("HH:mm"));
                        rs = BLLHuuNghi.Instance.CapSoPhongKhamCoHen(connectString, serviceId, txtname.Text, txtAdd.Text, (!string.IsNullOrEmpty(txtDOB.Text) ? Convert.ToInt32(txtDOB.Text) : 0), mabenhnhan, selectedPhongKham.MaPK, "", "", DateTime.Now, giohen.Value);
                        //MessageBox.Show("co hen");
                    }
                    else
                    {
                        // MessageBox.Show("ko hen");
                        try
                        {
                            _ngaygioinphieu = String.Format("Ngày {0} Giờ {1}", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
                            lbStatus.Text = "Bắt đầu gửi tiếp nhận lên HIS : dichvuid =" + serviceId + " ; phongkhamid=" + selectedPhongKham.MaPK + ";dsdat=" + dsDatHens.Count;
                            rs = BLLHuuNghi.Instance.CapSoPhongKhamKhongHen(connectString, serviceId, txtname.Text, txtAdd.Text, (!string.IsNullOrEmpty(txtDOB.Text) ? Convert.ToInt32(txtDOB.Text) : 0), mabenhnhan, selectedPhongKham.MaPK, "", "", DateTime.Now, dsDatHens.Select(x => x.ThoiGianHen_BatDau).ToList());
                            if (rs != null && rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data_3);
                                tenquay = rs.Data_2;
                            }
                            else
                                errorsms = rs.Errors[0].Message;
                        }
                        catch (Exception ex)
                        {
                            _showMessage(4, "in phiếu bị lỗi" + ex.Message);
                        }
                    }
                }

                if (newNumber >= 0)
                {
                    errorsms = printStr.ToString();
                    try
                    {
                        InPhieuDungDriver(newNumber, lastTicket, tenquay, serObj.Name, txtname.Text, tieude, _ngaygioinphieu);
                    }
                    catch (Exception ex)
                    {
                        _showMessage(4, "in phiếu bị lỗi" + ex.Message);
                    }
                    // _showMessage(1, "Bắt đầu gửi tiếp nhận lên HIS : dichvuid =" + selectedDichVu.Id + " ; phongkhamid=" + selectedPhongKham.Id);
                    lbStatus.Text = "Bắt đầu gửi tiếp nhận lên HIS : dichvuid =" + selectedDichVu.Id + " ; phongkhamid=" + selectedPhongKham.Id + ";bhFrom=" + bhFrom.ToString("dd/MM/yyyy") + ";bhTo=" + bhFrom.ToString("dd/MM/yyyy") + ";newNumber=" + newNumber;
                    var kq = serviceClient.Luu_TiepNhan(mabenhnhan, ngaysinh, (cbGioitinh.Text == "Nam" ? "T" : "G"), txtBHYT.Text, bhFrom, bhTo, selectedDichVu.Id, selectedPhongKham.Id, 0, newNumber.ToString());
                    if (!kq)
                    {
                        //_showMessage(4, "Lưu tiếp nhận lên HIS bị lỗi");
                    }
                    else
                    {
                        ClearForm();
                        lbStatus.Text = "Gửi tiếp nhận lên HIS thành công.!";
                    }
                }
            }
            catch (Exception ex)
            {
                _showMessage(4, "Cấp STT bị lỗi. Vui lòng thử lại. \n" + ex.Message);
            }
        }

        private void InPhieuDungDriver(int newNum, int oldNum, string tenquay, string tendichvu, string hoten, string tieude, string ngaygio)
        {
            LocalReport localReport = new LocalReport();
            try
            {
                //link cài report viewer cho máy client
                //https://www.microsoft.com/en-us/download/details.aspx?id=6442

                string fullPath = Application.StartupPath + "\\RDLC_Template\\Mau1.rdlc";
                // MessageBox.Show(fullPath);
                localReport.ReportPath = fullPath;
                ReportParameter[] reportParameters = new ReportParameter[5];
                reportParameters[0] = new ReportParameter("TenDV", tendichvu.ToUpper());
                reportParameters[1] = new ReportParameter("TenBN", hoten.ToUpper());
                reportParameters[2] = new ReportParameter("Stt", newNum.ToString());
                reportParameters[3] = new ReportParameter("TieuDe", tieude.ToUpper());
                reportParameters[4] = new ReportParameter("NgayGio", ngaygio.ToUpper());

                localReport.SetParameters(reportParameters);
                for (int i = 0; i < FrmMain.solien; i++)
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
                <PageWidth>{giayHeight}in</PageWidth>
                <PageHeight>{giayWidth}in</PageHeight>
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


        private void PrintWithNoBorad(PrintModel printModel)
        {
            var now = DateTime.Now;

            checkCOM:
            if (!COM_Printer.IsOpen)
            {
                COM_Printer.Open();
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
        }

        private string getStringValue(string value)
        {
            return !string.IsNullOrEmpty(value) ? (value + "\n") : "";
        }

        private string getIntValue(int value)
        {
            return value != null && value > 0 ? (value.ToString() + "\n") : "";
        }

        private void InitCOM_Printer()
        {
            try
            {
                COM_Printer.PortName = GetConfigByCode(eConfigCode.COM_Print);
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
        #endregion

        private void timerReset_Tick(object sender, EventArgs e)
        {
            switch (cfObj.appType)
            {
                case 0: //phòng khám
                    ClearForm();
                    break;
                case 1: // CLS 
                    setEnable(true);
                    txtBHYT.Focus();
                    break;
                case 2: // viện phí 
                case 3: // phát thuốc 
                case 4: // lấy máu XN
                    setEnable(true);
                    txtBHYT.Enabled = false;
                    txtma.Focus();
                    break;
            }
            timerReset.Enabled = false;
        }

    }

}
