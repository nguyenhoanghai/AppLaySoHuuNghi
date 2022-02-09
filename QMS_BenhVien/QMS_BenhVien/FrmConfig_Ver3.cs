﻿using Microsoft.Win32;
using QMS_System.Data.BLL;
using QMS_System.Data.Model;
using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Xml;

namespace QMS_BenhVien
{
    public partial class FrmConfig_Ver3 : Form
    {
        string _path = "", COMName = "";
        QMS_BenhVien.Helper.ConfigModel cfObj = null;
        int cbVP = 0, cbPT = 0, cbPS;
        public FrmConfig_Ver3()
        {
            InitializeComponent();
        }

        private void FrmConfig_Ver3_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\Config.XML";
            cfObj = Helper.Helper.Instance.GetAppConfig(filePath);
            _path = cfObj.anhnen;
            COMName = cfObj.COMName;

            cbCOMPrint.DisplayMember = "Name";
            cbCOMPrint.ValueMember = "Code";
            loadCOM();

            cbvienphi.DataSource = null;
            cbphatthuoc.DataSource = null;
            cbPhatso.DataSource = null;
            var services = BLLService.Instance.GetLookUp(FrmMain.connectString, false);
            for (int i = 0; i < services.Count; i++)
            {
                cbvienphi.Items.Add(services[i]);
                cbphatthuoc.Items.Add(services[i]);
                cbPhatso.Items.Add(services[i]);
                if (services[i].Id == cfObj.vienphi)
                    cbVP = i;
                if (services[i].Id == cfObj.phatthuoc)
                    cbPT = i;
                if (services[i].Id == cfObj.tieptan)
                    cbPT = i;
            }

            cbvienphi.SelectedIndex = cbVP;
            cbphatthuoc.SelectedIndex = cbPT;
            cbPhatso.SelectedIndex = cbPS;
        }

        private void loadCOM()
        {
            cbCOMPrint.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
                cbCOMPrint.Items.Add(new ModelSelectItem() { Name = s, Code = s });

            cbCOMPrint.Text = COMName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsettingnut_Click(object sender, EventArgs e)
        {
            var f = new frmButtonStyle();
            f.ShowDialog();
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                _path = openFileDialog.FileName;
        }

        private void btRefesh_Click(object sender, EventArgs e)
        {
            loadCOM();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string[] nodeArr = ("solien,button_style,permissions,services,laymau,trakq,xquang,sieuam,vienphi,phatthuoc,tieptan,_height,_width,imgsource,apptype,startwithwindow,timeResetForm,CTRoom,COMName").Split(',');
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
                        switch (i)
                        {
                            case 8: node.InnerText = ((ModelSelectItem)cbvienphi.SelectedItem).Id.ToString(); break;
                            case 9: node.InnerText = ((ModelSelectItem)cbphatthuoc.SelectedItem).Id.ToString(); break;
                            case 10: node.InnerText = ((ModelSelectItem)cbPhatso.SelectedItem).Id.ToString(); break;
                            case 13: node.InnerText = _path; break;
                            case 15: node.InnerText = (chkStartWithWindows.Checked ? "1" : "0"); break;
                            case 18: node.InnerText = ((ModelSelectItem)cbCOMPrint.SelectedItem).Name; break; 
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
                        case 0: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 1: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 2: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 3: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 4: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 5: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 6: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 7: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 8: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbvienphi.SelectedItem).Id.ToString())); break;
                        case 9: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbphatthuoc.SelectedItem).Id.ToString())); break;
                        case 10: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbPhatso.SelectedItem).Id.ToString())); break; 
                        case 11: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 12: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 13: node.AppendChild(xmlDoc.CreateTextNode(_path)); break;
                        case 14: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 15: node.AppendChild(xmlDoc.CreateTextNode((chkStartWithWindows.Checked ? "1" : "0"))); ; break;
                        case 16: node.AppendChild(xmlDoc.CreateTextNode("")); ; break;
                        case 17: node.AppendChild(xmlDoc.CreateTextNode("")); ; break;
                        case 18: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbCOMPrint.SelectedItem).Name.ToString())); break; 
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
    }
}
