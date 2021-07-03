using GPRO.Core.Hai;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using QMS_System.Data.BLL;
using QMS_System.Data.Enum;
using QMS_System.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        public static string ticketTemplate = string.Empty;
        public static int solien = 1;
        List<int> serviceIds = new List<int>();
        List<ServiceDayModel> lib_Services;
        List<ConfigModel> configs;
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
            tieptanId = 0;
        bool isDK_KetLuan = false;
        public static string connectString = BaseCore.Instance.GetEntityConnectString(Application.StartupPath + "\\DATA.XML"),
             comName = string.Empty,
             errorsms = string.Empty;
        ButtonStyleModel btStyle = new ButtonStyleModel() { Height = 100, Width = 100, ButtonInRow = 5, Margin = 10, fontStyle = "Arial, 36pt, style=Bold", BackColor = "#ffffff", ForeColor = "#0000ff" };
        HISReference.HISServiceClient serviceClient = new HISReference.HISServiceClient();
        string mabenhnhan = string.Empty;
        public FrmMain()
        {
            InitializeComponent();
        }

        public void FrmMain_Load(object sender, EventArgs e)
        {
            GetConfig();
            panel3.Width = this.Width - 10;
        }

        private void GetConfig()
        {
            try
            {
                string filePath = Application.StartupPath + "\\Config.XML";
                var cfObj = QMS_BenhVien.Helper.Helper.Instance.GetAppConfig(filePath);
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

                if (!string.IsNullOrEmpty(cfObj.permissions))
                {
                    int[] arr = cfObj.permissions.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                    for (int i = 0; i < arr.Length; i++)
                    {
                        switch (i)
                        {
                            case 0: btDangKyKhamBenh.Enabled = true; break;
                            case 1: btDK_KetLuan.Enabled = true; break;
                            case 2: btDKLayMauXN.Enabled = true; break;
                            case 3: btDKLayKQ_XN.Enabled = true; break;
                            case 4: btDK_CLS.Enabled = true; break;
                            case 5: btDKLichKham.Enabled = true; break;
                            case 6: btVienPhi.Enabled = true; break;
                            case 7: btCapThuoc.Enabled = true; break;
                            case 8: btHelp.Enabled = true; break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(cfObj.services))
                    serviceIds = cfObj.services.Split(',').Select(x => Convert.ToInt32(x)).ToList();
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

        private string GetConfigByCode(string code)
        {
            if (configs != null && configs.Count > 0)
            {
                var obj = configs.FirstOrDefault(x => x.Code.Trim().ToUpper().Equals(code.Trim().ToUpper()));
                return obj != null ? obj.Value : string.Empty;
            }
            return string.Empty;
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    {
                        if (menuStrip1.Visible == true)
                            menuStrip1.Visible = false;

                        FormBorderStyle = FormBorderStyle.None;
                        WindowState = FormWindowState.Maximized;
                        TopMost = true;
                        break;
                    }
                case Keys.Escape:
                    {
                        if (menuStrip1.Visible == false)
                            menuStrip1.Visible = true;

                        FormBorderStyle = FormBorderStyle.Sizable;
                        WindowState = FormWindowState.Normal;
                        TopMost = false;
                        break;
                    }
            }
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (menuStrip1.Visible == true)
                menuStrip1.Visible = false;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void btDangKyKhamBenh_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtma.Focus();
            UnVisibleAll(); 
            TaoPhongKham(true);
        }

        private void btHelp_Click(object sender, EventArgs e)
        {
            //TODO
            //1. in phieu gap Bo phan tiep don
            UnVisibleAll();
            PrintTicket(tieptanId, DateTime.Now, "STT giao dịch");
        }

        #region tạo phòng khám
        private void TaoPhongKham(bool isDangKy)
        {
            panel3.Controls.Clear();
            var services = BLLService.Instance.Gets_BenhVien(connectString).Where(x => serviceIds.Contains(x.Id) && x.isKetLuan == !isDangKy).ToList();
            if (services.Count > 0)
            {
                int sodong = (int)Math.Ceiling(services.Count / (double)btStyle.ButtonInRow),
                socot = btStyle.ButtonInRow,
                sizeCot = 100 / socot,
                sizeDong = 100 / sodong;
                FontConverter converter = new FontConverter();
                Button btn;
                int pointX = btStyle.Margin, pointY = btStyle.Margin;
                int index = 0;
                for (int i = 0; i < sodong; i++)
                {
                    for (int ii = 0; ii < socot; ii++)
                    {
                        if (index < services.Count)
                        {
                            if (ii == 0)
                                pointX = btStyle.Margin;
                            var found = services[index];
                            btn = new Button();
                            btn.Name = found.Id.ToString();
                            //btn.Text = "Khoa: " + found.Khoa + Environment.NewLine + "Phòng: " + found.PhongKham + Environment.NewLine + "BS: " + found.BacSi;
                            btn.Text = found.Code + Environment.NewLine + found.Name;
                            btn.Cursor = System.Windows.Forms.Cursors.Hand;
                            btn.Width = btStyle.Width;
                            btn.Height = btStyle.Height;
                            btn.Location = new Point(pointX, pointY);
                            btn.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
                            btn.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
                            btn.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);

                            btn.Click += new System.EventHandler(this.btService_Click);
                            pointX += btStyle.Width + btStyle.Margin;
                            this.panel3.Controls.Add(btn);
                            index++;
                        }
                    }
                    pointY += btStyle.Height + btStyle.Margin;
                }
            }
            this.panel3.Visible = true;
        }

        private void btService_Click(object sender, EventArgs e)
        {
            bool print = false;
            if (!isDK_KetLuan)
            {
                //dky kham thi phai confirm truoc khi in
                if (MessageBox.Show("Bạn đã kiểm tra đầy đủ thông tin và yêu cầu in phiếu ?", "Xác nhận thông tin",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
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
                // MessageBox.Show(((System.Windows.Forms.Control)sender).Name);
                int serId = Convert.ToInt32(((System.Windows.Forms.Control)sender).Name);
                //in phiếu với dich vu đã chon va nghiệp vu la phong kham da chon
                PrintTicket(serId, DateTime.Now, (!isDK_KetLuan ? "STT Kết Luận Bệnh" : "STT Khám Bệnh"));
            }
        }
        #endregion

        #region ktra BHYT
        private void btKTraBHYT_Click(object sender, EventArgs e)
        {
            txtBHYT.Text = "";
            txtBHYT.Focus();
            UnVisibleAll();
        }

        private void btSendBHYT_Click(object sender, EventArgs e)
        {
            tmerQuetBHYT.Enabled = true;
        }

        private void tmerQuetBHYT_Tick(object sender, EventArgs e)
        {
            countQuetBHYT++;
            if (countQuetBHYT == 15)
            {
                // sau 15 giây ko nhan dc phan hoi tu FPT báo lỗi ngung timer
                tmerQuetBHYT.Enabled = false;
                MessageBox.Show("Không lấy được thông tin BHYT xin vui lòng thử lại.!.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region vien phi
        private void btVienPhi_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtma.Focus();
            UnVisibleAll();
            btInSoVienPhi.Visible = true;
            btInSoVienPhi.Location = new Point((panel1.Width - btInSoVienPhi.Width) / 2, (panel1.Height - btInSoVienPhi.Height) / 2);
        }

        private void btInSoVienPhi_Click(object sender, EventArgs e)
        {
            btInSoVienPhi.Visible = false;
            //TODO
            //1. in so cho thanh toan
            PrintTicket(vienphiId, DateTime.Now, "STT Thanh Toán Viện Phí");
        }
        #endregion

        #region CLS
        private void btDK_CLS_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtma.Focus();
            UnVisibleAll();
            btSieuAm.Visible = true;
            btXQuang.Visible = true;
        }

        private void btSieuAm_Click(object sender, EventArgs e)
        {
            btSieuAm.Visible = false;
            btXQuang.Visible = false;
            //TODO in so dich vu sieu am
            PrintTicket(sieuamId, DateTime.Now, "STT Siêu Âm");
        }

        private void btXQuang_Click(object sender, EventArgs e)
        {
            btSieuAm.Visible = false;
            btXQuang.Visible = false;
            //TODO in so dich vu X-Quang
            PrintTicket(xquangId, DateTime.Now, "STT Chụp X-Quang");
        }
        #endregion

        #region lay mau xet nghiem
        private void btDKLayMauXN_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtma.Focus();
            UnVisibleAll();
            btInLayMauXN.Visible = true;
            panel3.Visible = false;
            btInLayMauXN.Location = new Point((panel1.Width - btInLayMauXN.Width) / 2, (panel1.Height - btInLayMauXN.Height) / 2);
        }

        private void btInLayMauXN_Click(object sender, EventArgs e)
        {
            btXQuang.Visible = false;
            //TODO in so lay mau xet nghiem
            PrintTicket(laymauId, DateTime.Now, "STT Lấy Máu Xét Nghiệm");
        }
        #endregion

        #region Lay KQ XN
        private void btDKLayKQ_XN_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtma.Focus();
            UnVisibleAll();
            btInKQ_XN.Visible = true;
            btInKQ_XN.Location = new Point((panel1.Width - btInKQ_XN.Width) / 2, (panel1.Height - btInKQ_XN.Height) / 2);
        }

        private void btInKQ_XN_Click(object sender, EventArgs e)
        {
            btInKQ_XN.Visible = false;
            //TODO in so lay kq xn
            PrintTicket(nhanKqId, DateTime.Now, "STT NHẬN KQ CLS");
        }
        #endregion

        private void btDK_KetLuan_Click(object sender, EventArgs e)
        {
            // bật cờ co sho confirm box ko
            isDK_KetLuan = true;
            UnVisibleAll();
            //show ds phong kham cho user chon phong kham
            panel3.Visible = true;
            panel3.Height = 450;
            TaoPhongKham(false);
        }

        private void mẫuNútDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmButtonStyle( );
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void txtma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //send barcode THẺ to server
            }
        }

        private void txtBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //send barcode BHYT to server
            }
        }

        #region Ktra khám lần đầu
        private void btKtraKhamLanDau_Click(object sender, EventArgs e)
        {
            txtBHYT.Visible = true;

            txtBHYT.Text = "";
            txtBHYT.Focus();
            UnVisibleAll();
            btKTraBHYT.Enabled = false;
            btDangKyKhamBenh.Enabled = false;
            btDK_KetLuan.Enabled = false;
            btDKLayMauXN.Enabled = false;
            panel1.Visible = true;
            btCheckTT.Visible = true;
            btCheckTT.Location = new Point((panel1.Width - btCheckTT.Width) / 2, (panel1.Height - btCheckTT.Height) / 2);
            mabenhnhan = string.Empty;
        }

        private void btCheckTT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBHYT.Text))
            {
                MessageBox.Show("Vui lòng nhập số BHYT", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBHYT.Focus();
            }
            else if (string.IsNullOrEmpty(txtname.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên bệnh nhân", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtname.Focus();
            }
            else if (string.IsNullOrEmpty(txtDOB.Text))
            {
                MessageBox.Show("Vui lòng nhập năm sinh bệnh nhân", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDOB.Focus();
            } 
            else
            {
                int ns = 0;
                if (!string.IsNullOrEmpty(txtDOB.Text))
                    int.TryParse(txtDOB.Text, out ns);
                bool result = serviceClient.Check_KhamBenhLanDau(txtBHYT.Text, txtname.Text, ns,textBox2.Text, ref mabenhnhan);
                if (result)
                {
                    btCheckTT.Visible = false;
                    btDangKyKhamBenh.Enabled = true;
                    btDangKyKhamBenh.PerformClick();
                }
                else
                    MessageBox.Show("Không tìm thấy thông tin bệnh trong Hệ Thống. Vui lòng liên hệ Bộ phận tiếp tân để được tư vấn. Xin cảm ơn!.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region CẤp thuốc
        private void btCapThuoc_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtma.Focus();
            UnVisibleAll();
            btInCapThuoc.Visible = true;
            btInCapThuoc.Location = new Point((panel1.Width - btInCapThuoc.Width) / 2, (panel1.Height - btInCapThuoc.Height) / 2);
        }

        private void cấuHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btInCapThuoc_Click(object sender, EventArgs e)
        {
            btInCapThuoc.Visible = false;
            //TODO in so cap thuoc
            PrintTicket(phatthuocId, DateTime.Now, "STT NHẬN THUỐC");
        }
        #endregion

        private void UnVisibleAll()
        {
            panel3.Visible = false;
            btInCapThuoc.Visible = false;
            btInKQ_XN.Visible = false;
            btXQuang.Visible = false;
            btSieuAm.Visible = false;
            btInLayMauXN.Visible = false;
            btInSoVienPhi.Visible = false;
            panel3.Visible = false;
            panel3.Visible = false;
        }

        #region IN PHIEU
        private void PrintTicket(int serviceId, DateTime ServeTime, string tieude)
        {
            int lastTicket = 0,
                newNumber = -1,
            nghiepVu = 0;
            string printStr = string.Empty,
                tenquay = string.Empty;
            bool err = false;
            ServiceDayModel serObj = null;
            DateTime now = DateTime.Now;
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
                            var rs = BLLDailyRequire.Instance.PrintNewTicket(connectString, serviceId, serObj.StartNumber, 0, now, printType, ServeTime.TimeOfDay, txtname.Text, txtAdd.Text, (!string.IsNullOrEmpty(txtDOB.Text) ? Convert.ToInt32(txtDOB.Text) : 0), mabenhnhan, "", "", "","","");
                            if (rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data + 1);
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
                            var rs = BLLDailyRequire.Instance.PrintNewTicket(connectString, serviceId, startNumber, 0, now, printType, (ServeTime != null ? ServeTime.TimeOfDay : serObj.TimeProcess.TimeOfDay), txtname.Text, txtAdd.Text, Convert.ToInt32(txtDOB.Text), txtma.Text, "", "", "","","");
                            if (rs.IsSuccess)
                            {
                                lastTicket = (int)rs.Data;
                                nghiepVu = rs.Data_1;
                                newNumber = ((int)rs.Data + 1);
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
                    #region MyRegion
                    //int slCP = BLLBusiness.Instance.GetTicketAllow(entityConnectString, businessId);
                    //int slDacap = BLLDailyRequire.Instance.CountTicket(entityConnectString, businessId);
                    //if (slDacap != null && slDacap == slCP)
                    //   errorsms = ("Doanh nghiệp của bạn đã được cấp đủ số lượng phiếu giới hạn trong ngày. Xin quý khách vui lòng quay lại sau.");
                    ////  MessageBox.Show("Doanh nghiệp của bạn đã được cấp đủ số lượng phiếu giới hạn trong ngày. Xin quý khách vui lòng quay lại sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //else
                    //{
                    //    lastTicket = BLLDailyRequire.Instance.GetLastTicketNumber(connectString, serviceId, today);
                    //    serObj = lib_Services.FirstOrDefault(x => x.Id == serviceId);
                    //    if (lastTicket == 0)
                    //    {
                    //        if (serObj != null)
                    //        {
                    //            err = true;
                    //           errorsms = ("Dịch vụ không tồn tại. Xin quý khách vui lòng chọn dịch vụ khác.");
                    //            //  MessageBox.Show("Dịch vụ không tồn tại. Xin quý khách vui lòng chọn dịch vụ khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //        else
                    //            lastTicket = serObj.StartNumber;
                    //    }
                    //    else
                    //    {
                    //        lastTicket++;
                    //        if (serObj.EndNumber < lastTicket)
                    //        {
                    //            err = true;
                    //           errorsms = ("Dịch vụ này đã cấp hết phiếu trong ngày. Xin quý khách vui lòng chọn dịch vụ khác.");
                    //            //  MessageBox.Show("Dịch vụ này đã cấp hết phiếu trong ngày. Xin quý khách vui lòng chọn dịch vụ khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //    }
                    //    if (!err)
                    //    {
                    //        var rs = BLLDailyRequire.Instance.Insert(connectString, lastTicket, serviceId, businessId, now);
                    //        if (rs.IsSuccess)
                    //        {
                    //            newNumber = rs.Data;
                    //            if (newNumber != 0 && !isProgrammer)
                    //            {
                    //                var soArr = BaseCore.Instance.ChangeNumber(lastTicket);
                    //                printStr = (soArr[0] + " " + soArr[1] + " ");
                    //                if (printTicketReturnCurrentNumberOrServiceCode == 1)
                    //                {
                    //                    soArr = BaseCore.Instance.ChangeNumber(BLLDailyRequire.Instance.GetCurrentNumber(connectString, serviceId));
                    //                }
                    //                else
                    //                {
                    //                    soArr = BaseCore.Instance.ChangeNumber(serviceId);
                    //                }

                    //                printStr += (soArr[0] + " " + soArr[1] + " " + now.ToString("dd") + " " + now.ToString("MM") + " " + now.ToString("yy") + " " + now.ToString("HH") + " " + now.ToString("mm"));
                    //            }
                    //            else if (newNumber != 0 && isProgrammer)
                    //                lbRecieve.Caption = serviceId + "," + "1," + lastTicket;
                    //            nghiepVu = rs.Data_1;
                    //        }
                    //    }
                    //}
                    #endregion
                    break;
            }

            if (newNumber >= 0)
            {
                errorsms = printStr.ToString();
                InPhieu(newNumber, lastTicket, tenquay, serObj.Name, txtname.Text, tieude);
            }
        }

        private void InPhieu(int newNum, int oldNum, string tenquay, string tendichvu, string hoten, string tieude)
        {
            LocalReport localReport = new LocalReport();
            try
            {
                //link cài report viewer cho máy client
                //https://www.microsoft.com/en-us/download/details.aspx?id=6442

                string fullPath = Application.StartupPath + "\\RDLC_Template\\Mau1.rdlc";
                // MessageBox.Show(fullPath);
                localReport.ReportPath = fullPath;
                ReportParameter[] reportParameters = new ReportParameter[4];
                reportParameters[0] = new ReportParameter("TenDV", tenquay.ToUpper());
                reportParameters[1] = new ReportParameter("TenBN", hoten.ToUpper());
                reportParameters[2] = new ReportParameter("Stt", newNum.ToString());
                reportParameters[3] = new ReportParameter("TieuDe", tieude.ToUpper());

                localReport.SetParameters(reportParameters);
                for (int i = 0; i < FrmMain.solien; i++)
                {
                    PrintToPrinter(localReport);
                }
            }
            catch (Exception ex)
            {
                localReport.Dispose();
                throw ex;
            }
        }
        #endregion

        private void kếtNốiCSDLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmSQLConnect();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void mẫuPhiếuInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDesignTicket frm = new FrmDesignTicket();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FrmConfig();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
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
                <PageWidth>5.27in</PageWidth>
                <PageHeight>22.69in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>5in</MarginBottom>
            </DeviceInfo>";
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
    }
}
