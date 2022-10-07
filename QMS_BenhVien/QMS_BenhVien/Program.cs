using System;
using System.Configuration;
using System.Diagnostics;
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

                string[] commandLineArgs = Environment.GetCommandLineArgs();
                if (commandLineArgs.Length == 1 || commandLineArgs[1] != "/new")
                {
                    if (SingleInstanceApplication.NotifyExistingInstance(Process.GetCurrentProcess().Id))
                    {
                        return;
                    }
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SingleInstanceApplication.Initialize();
                try
                {
                    if (ConfigurationManager.AppSettings["showDongBo"].ToString() == "1")
                        Application.Run(new FrmDongBo());
                    else
                    {
                        //  if (BaseCore.Instance.CONNECT_STATUS(Application.StartupPath + "\\DATA.XML"))
                        // Application.Run(new FrmMain());
                        //  Application.Run(new Frmain_ver2());
                        // else
                        //   Application.Run(new frmSQLConnect());

                        // Application.Run(new Frmain_ver3());
                        // Application.Run(new FrmMain_TV2());

                        string appType = "0";
                        if (ConfigurationManager.AppSettings["AppType"] != null &&
                            !string.IsNullOrEmpty(ConfigurationManager.AppSettings["AppType"].ToString()))
                        {
                            appType = ConfigurationManager.AppSettings["AppType"].ToString();
                        }
                        switch (appType)
                        {
                            default: Application.Run(new Frmain_ver2()); break;
                            case "0": Application.Run(new Frmain_ver3());  break;
                            case "1": Application.Run(new FrmMain_Socket_TV1()); break;
                            case "2": Application.Run(new FrmMain_Socket_TV2()); break;
                            case "3":
                                FrmMain_Socket_TV1.appPhatThuoc = true;
                                Application.Run(new FrmMain_Socket_TV1()); break;
                        }
                    }
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
