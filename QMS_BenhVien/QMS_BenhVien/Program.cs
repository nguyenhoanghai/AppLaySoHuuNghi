using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            try
            {
                //SerialKey serialKey = new SerialKey();
                ////  ModelStatic.dateCheckActive = DateTime.Now.Date;
                //ModelCheckKey modelCheckKey = serialKey.CheckActive("GPRO_QMS_KIOS", Application.StartupPath);
                //if (modelCheckKey != null)
                //{
                //    if (!modelCheckKey.checkResult)
                //    { 
                //        MessageBox.Show("Phần mềm QMS đã hết hạn sử dụng.\nQuý khách vui lòng liên hệ theo Hotline : Võ Đại Trí 0918319714 hoặc Email : vodaitri@yahoo.com để được tư vấn và kích hoạt sử dụng.\nXin cám ơn quý khách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        Application.Exit();
                //    }
                //    else
                //    {
                //if (!string.IsNullOrEmpty(modelCheckKey.message))
                //    MessageBox.Show(modelCheckKey.message);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    if (ConfigurationManager.AppSettings["showDongBo"].ToString() == "1")
                        Application.Run(new FrmDongBo());
                    else

                        //  if (BaseCore.Instance.CONNECT_STATUS(Application.StartupPath + "\\DATA.XML"))
                        // Application.Run(new FrmMain());
                        Application.Run(new Frmain_ver2());
                    // else
                    //   Application.Run(new frmSQLConnect());
                }
                catch (Exception ex)
                {
                    string errorsms = "Kết nối máy chủ SQL thất bại. Vui lòng kiểm tra lại.";
                    MessageBox.Show(errorsms + "- " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Run(new frmSQLConnect());
                }
                Process[] processe;
                processe = Process.GetProcessesByName("QMS_BenhVien");
                foreach (Process dovi in processe)
                    dovi.Kill();
                // }
                //}
                //else
                //{
                //    MessageBox.Show("Phần mềm QMS chưa được kích hoạt sử dụng.\nQuý khách vui lòng liên hệ theo Hotline : Võ Đại Trí 0918319714 hoặc Email : vodaitri@yahoo.com để được tư vấn và kích hoạt sử dụng.\nXin cám ơn quý khách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Application.Exit();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi khác" + ex.Message);
            }

        }
    }
}
