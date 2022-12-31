using Microsoft.Win32;
using QMS_System.Data.BLL;
using QMS_System.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QMS_BenhVien.RangHamMat
{
    public partial class FrmSetting : Form
    {
        QMS_BenhVien.Helper.ConfigModel cfObj = null;
        string _path = "";
        int cbKhamUTId = 0 ;
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\Config.XML";
            cfObj = Helper.Helper.Instance.GetAppConfig(filePath);
            _path = cfObj.anhnen; 
            numWidth.Text = cfObj.giayWidth;
            numHeight.Text = cfObj.giayHeight;
           numSolien.Value = cfObj.solien;
            txtAPI.Text = cfObj.COMName;
               
            cbKhamUT.DataSource = null; 
            var services = BLLService.Instance.GetLookUp(FrmMain.connectString, false);
            for (int i = 0; i < services.Count; i++)
            { 
                cbKhamUT.Items.Add(services[i]);  
                if (services[i].Id == cfObj.tieptan)
                    cbKhamUTId = i; 
            } 
            cbKhamUT.SelectedIndex = cbKhamUTId; 
            chkStartWithWindows.Checked = cfObj.startwithwindow;
        } 

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                _path = openFileDialog.FileName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string[] nodeArr = ("solien,button_style,permissions,services,laymau,trakq,xquang,sieuam,vienphi,phatthuoc,tieptan,_height,_width,imgsource,apptype,startwithwindow,timeResetForm,CTRoom,COMName,Phatso").Split(',');
            string filePath = Application.StartupPath + "\\Config.XML";
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = null;
            if (System.IO.File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                for (int i = 0; i < nodeArr.Length; i++)
                {
                    if (i != 1)
                    {
                        node = xmlDoc.SelectSingleNode("Appsettings/" + nodeArr[i]);
                        if (node != null)
                            switch (i)
                            {
                                case 0: node.InnerText = numSolien.Value.ToString(); break; 
                                case 10: node.InnerText = ((ModelSelectItem)cbKhamUT.SelectedItem).Id.ToString(); break;
                                case 11: node.InnerText = numHeight.Text; break;
                                case 12: node.InnerText = numWidth.Text; break;
                                case 13: node.InnerText = _path; break;
                                case 15: node.InnerText = (chkStartWithWindows.Checked ? "1" : "0"); break;
                                case 18: node.InnerText = txtAPI.Text; break;
                               // case 19: node.InnerText = ((ModelSelectItem)cbTieuDuong.SelectedItem).Id.ToString(); break;
                            }
                    }
                }
            }
            else
            {
                #region create new
                XmlNode newChild = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(newChild);
                XmlNode xmlNode = xmlDoc.CreateElement("Appsettings");
                xmlDoc.AppendChild(xmlNode);
                for (int i = 0; i < nodeArr.Length; i++)
                {
                    node = xmlDoc.CreateElement(nodeArr[i]);
                    switch (i)
                    {
                        case 0: node.AppendChild(xmlDoc.CreateTextNode(numSolien.Value.ToString())); break;
                        case 1: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 2: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 3: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 4: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 5: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 6: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 7: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 8: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 9: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 10: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbKhamUT.SelectedItem).Id.ToString())); break;
                        case 11: node.AppendChild(xmlDoc.CreateTextNode(numHeight.Text)); break;
                        case 12: node.AppendChild(xmlDoc.CreateTextNode(numWidth.Text)); break;
                        case 13: node.AppendChild(xmlDoc.CreateTextNode(_path)); break;
                        case 14: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 15: node.AppendChild(xmlDoc.CreateTextNode((chkStartWithWindows.Checked ? "1" : "0"))); ; break;
                        case 16: node.AppendChild(xmlDoc.CreateTextNode("")); ; break;
                        case 17: node.AppendChild(xmlDoc.CreateTextNode("")); ; break;
                        case 18: node.AppendChild(xmlDoc.CreateTextNode(txtAPI.Text)); break;
                        case 19: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                    }
                    xmlNode.AppendChild(node);
                }
                #endregion
            }
            xmlDoc.Save(filePath);

            //start with windows
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                           ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (chkStartWithWindows.Checked)
                {
                    registryKey.SetValue("QMS_BenhVien", Application.ExecutablePath);
                }
                else
                {
                    registryKey.DeleteValue("QMS_BenhVien");
                }
            }
            catch (Exception ex)
            {
            }

            Application.Restart();
            Environment.Exit(0);
        }

        private void txtAPI_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
