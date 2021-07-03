using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace QMS_BenhVien.Helper
{
    public class Helper
    {
        #region constructor
        static object key = new object();
        private static volatile Helper _Instance;
        public static Helper Instance
        {
            get
            {
                if (_Instance == null)
                    lock (key)
                        _Instance = new Helper();

                return _Instance;
            }
        }
        private Helper() { }
        #endregion

        public ConfigModel GetAppConfig(string path)
        {
            ConfigModel cf = new ConfigModel();
            if (File.Exists(path))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Appsettings");
                cf.solien = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[0].InnerText);
                cf.button_style = elementsByTagName.Item(0).ChildNodes[1].InnerText;
                cf.permissions = elementsByTagName.Item(0).ChildNodes[2].InnerText;
                cf.services = elementsByTagName.Item(0).ChildNodes[3].InnerText;
                cf.laymau = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[4].InnerText);
                cf.ketqua = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[5].InnerText);
                cf.xquang = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[6].InnerText);
                cf.sieuam = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[7].InnerText);
                cf.vienphi = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[8].InnerText);
                cf.phatthuoc = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[9].InnerText);
                cf.tieptan = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[10].InnerText);


                cf.giayHeight = elementsByTagName.Item(0).ChildNodes[11].InnerText;
                cf.giayWidth = elementsByTagName.Item(0).ChildNodes[12].InnerText;
                cf.anhnen = elementsByTagName.Item(0).ChildNodes[13].InnerText.ToString();
                cf.appType = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[14].InnerText);
                string vl = elementsByTagName.Item(0).ChildNodes[15].InnerText;
                cf.startwithwindow = (string.IsNullOrEmpty(vl) ? false : (vl == "0" ? false : true));
                cf.timeResetForm = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[16].InnerText);
                cf.CTRoom = Convert.ToInt32(elementsByTagName.Item(0).ChildNodes[17].InnerText);
            }
            return cf;
        }
    }

    public class ConfigModel
    {
        [DefaultValue(1)]
        public int solien { get; set; }

        [DefaultValue("")]
        public string button_style { get; set; }

        [DefaultValue("")]
        public string permissions { get; set; }

        [DefaultValue("")]
        public string services { get; set; }

        [DefaultValue(0)]
        public int laymau { get; set; }

        [DefaultValue(0)]
        public int ketqua { get; set; }

        [DefaultValue(0)]
        public int sieuam { get; set; }

        [DefaultValue(0)]
        public int xquang { get; set; }

        [DefaultValue(0)]
        public int vienphi { get; set; }

        [DefaultValue(0)]
        public int phatthuoc { get; set; }

        [DefaultValue(0)]
        public int tieptan { get; set; }

        [DefaultValue("")]
        public string anhnen { get; set; }
        [DefaultValue("3,69")]
        public string giayWidth { get; set; }
        [DefaultValue("7,69")]
        public string giayHeight { get; set; }
        [DefaultValue(0)]
        public int appType { get; set; }
        [DefaultValue(0)]
        public bool startwithwindow { get; set; }

        /// <summary>
        /// tính theo giây
        /// </summary>
        [DefaultValue(1)]
        public int timeResetForm { get; set; }

        /// <summary>
        /// phòng ct
        /// </summary>
        [DefaultValue(0)]
        public int CTRoom { get; set; }
    }

    public class DMBenhVien
    {
        public int  BenhVien_Id { get; set; }
        public string MaBenhVien { get; set; }
        public string TenBenhVien { get; set; }
        public string MaTuyen { get; set; }
        public string TenTuyen { get; set; } 
    }
}
