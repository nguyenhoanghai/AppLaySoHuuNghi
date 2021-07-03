using DevExpress.XtraEditors.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using QMS_System.Data.BLL;
using QMS_System.Data.Model;
using QMS_System.Data.Model.HuuNghi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace QMS_BenhVien
{
    public partial class FrmConfig : Form
    {
        string _path = "";
        string[] _dsKhoas = new string[] { };
        QMS_BenhVien.Helper.ConfigModel cfObj = null;
        List<KhoaModel> khoas = new List<KhoaModel>();
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            string dsKhoaDaChon = "0";
            string filePath = Application.StartupPath + "\\Config.XML";
            cfObj = Helper.Helper.Instance.GetAppConfig(filePath);
            var objs = BLLService.Instance.GetLookUp(FrmMain.connectString, false);
            cblaymau.DataSource = null;
            cbketqua.DataSource = null;

            cbketqua.DisplayMember = "Name";
            cbketqua.ValueMember = "Id";

            cbxquang.DataSource = null;
            cbsieuam.DataSource = null;
            cbvienphi.DataSource = null;
            cbtieptan.DataSource = null;
            cbphatthuoc.DataSource = null;
            cbCTRoom.DataSource = null;
            int cbLM = 0, cbKQ = 0, cbXQ = 0, cbSA = 0, cbVP = 0, cbTT = 0, cbPT = 0,cbCT = 0;

            for (int i = 0; i < objs.Count; i++)
            {
                cblaymau.Items.Add(objs[i]);
                cbketqua.Items.Add(objs[i]);
                cbxquang.Items.Add(objs[i]);
                cbsieuam.Items.Add(objs[i]);
                cbvienphi.Items.Add(objs[i]);
                cbtieptan.Items.Add(objs[i]);
                cbphatthuoc.Items.Add(objs[i]);
                cbCTRoom.Items.Add(objs[i]);
                if (objs[i].Id == cfObj.laymau)
                    cbLM = i;
                if (objs[i].Id == cfObj.ketqua)
                    cbKQ = i;
                if (objs[i].Id == cfObj.xquang)
                    cbXQ = i;
                if (objs[i].Id == cfObj.sieuam)
                    cbSA = i;
                if (objs[i].Id == cfObj.vienphi)
                    cbVP = i;
                if (objs[i].Id == cfObj.tieptan)
                    cbTT = i;
                if (objs[i].Id == cfObj.phatthuoc)
                    cbPT = i;
                if (objs[i].Id == cfObj.CTRoom)
                    cbCT = i;
            }
            cbketqua.SelectedIndex = cbKQ;
            cblaymau.SelectedIndex = cbLM;
            cbxquang.SelectedIndex = cbXQ;
            cbsieuam.SelectedIndex = cbSA;
            cbvienphi.SelectedIndex = cbVP;
            cbtieptan.SelectedIndex = cbTT;
            cbphatthuoc.SelectedIndex = cbPT;
            cbCTRoom.SelectedIndex = cbCT;

            numWidth.Text = cfObj.giayWidth;
            numHeight.Text = cfObj.giayHeight;

            chkStartWithWindows.Checked = cfObj.startwithwindow;
            txtsolien.Value = cfObj.solien;
            dsKhoaDaChon = cfObj.permissions;


            string path = "";
            //if (cfObj.appType == 0)  // phong kham
            path = Application.StartupPath + "\\JSON.XML";
            // else //can lam san
            //   path = Application.StartupPath + "\\JSON_CLS.XML";


            if (!File.Exists(path))
            {
                MessageBox.Show("Không lấy được thông tin Khoa từ HIS.Vui lòng kiểm tra lại cấu hình hoặc đồng bộ lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // chuyenJson();
            }
            else
            {
                // Open the file to read from.
                string jsonText = File.ReadAllText(path);
                khoas = JsonConvert.DeserializeObject<List<KhoaModel>>(jsonText);//  
                _dsKhoas = khoas.Select(x => x.TenKhoa).ToArray();
            }

            refreshPK(cfObj.permissions, cfObj.services);

            CheckedListBoxItem cbItem2;
            chkPermissions.Items.Clear();
            int[] selectedPermiss = dsKhoaDaChon.Split(',').Select(x => Convert.ToInt32(x)).ToArray(); ;
            for (int i = 0; i < _dsKhoas.Length; i++)
            {
                cbItem2 = new CheckedListBoxItem() { Value = i, Description = _dsKhoas[i].ToString() };
                chkPermissions.Items.Add(cbItem2);
            }
            setPermisionValue(selectedPermiss);

            _path = cfObj.anhnen;

            List<ModelSelectItem> apps = new List<ModelSelectItem>();
            apps.Add(new ModelSelectItem() { Id = 0, Name = "Phòng khám" });
            apps.Add(new ModelSelectItem() { Id = 1, Name = "Cận lâm sàn" });
            apps.Add(new ModelSelectItem() { Id = 2, Name = "Viện phí" });
            apps.Add(new ModelSelectItem() { Id = 3, Name = "Cấp phát thuốc" });
            apps.Add(new ModelSelectItem() { Id = 4, Name = "Lấy máu xét nghiệm" });
            cbAppType.DataSource = apps;
            cbAppType.SelectedIndex = cfObj.appType;

            numResetForm.Value = cfObj.timeResetForm;
        }

        private void refreshPK(string dsKhoaDangChon, string dsPKhamDangChon)
        {
            int[] pkChecked = new int[] { };
            int[] khoaIndexArr = new int[] { };
            var _khoaSelects = new List<KhoaModel>();

            if (!string.IsNullOrEmpty(dsKhoaDangChon))
                khoaIndexArr = dsKhoaDangChon.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            if (!string.IsNullOrEmpty(dsPKhamDangChon))
                pkChecked = dsPKhamDangChon.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            for (int i = 0; i < khoaIndexArr.Length; i++)
                _khoaSelects.Add(khoas[khoaIndexArr[i]]);

            CheckedListBoxItem cbItem;
            cbPhongkham.Properties.Items.Clear();
            var _pks = new List<PhongKhamModel>();
            foreach (var item in _khoaSelects)
            {
                foreach (var dv in item.DichVus)
                {
                    foreach (var pk in dv.PhongKhams)
                    {
                        _pks.Add(pk);
                    }
                }
            }
            var idDistincts = _pks.Select(x => x.Id).Distinct().ToArray();
            _pks = _pks.Where(x => idDistincts.Contains(x.Id)).ToList();
            for (int ii = 0; ii < idDistincts.Length; ii++)
            {
                var _found = _pks.FirstOrDefault(x => x.Id == idDistincts[ii]);

                cbItem = new CheckedListBoxItem() { Value = _found.Id, Description = _found.TenPK };
                if (pkChecked.Contains(_found.Id))
                    cbItem.CheckState = CheckState.Checked;
                else
                    cbItem.CheckState = CheckState.Unchecked;
                cbPhongkham.Properties.Items.Add(cbItem);
            }
        }

        private void setPermisionValue(int[] selectedPermis)
        {
            if (selectedPermis != null && selectedPermis.Count() > 0)
                if (this.chkPermissions.Items != null && this.chkPermissions.Items.Count > 0)
                    foreach (CheckedListBoxItem item in this.chkPermissions.Items)
                        if (selectedPermis.Contains((int)item.Value))
                            item.CheckState = CheckState.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string dsKhoaDangChon = "", dsPKhamDangChon = "";
            if (this.chkPermissions.Items != null && this.chkPermissions.Items.Count > 0)
            {
                foreach (CheckedListBoxItem item in this.chkPermissions.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        dsKhoaDangChon += item.Value + ",";
                }
                dsKhoaDangChon = dsKhoaDangChon.Substring(0, dsKhoaDangChon.Length - 1);
            }

            if (this.cbPhongkham.Properties.Items != null && this.cbPhongkham.Properties.Items.Count > 0)
            {
                foreach (CheckedListBoxItem item in this.cbPhongkham.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        dsPKhamDangChon += item.Value + ",";
                }
                dsPKhamDangChon = dsPKhamDangChon.Substring(0, dsPKhamDangChon.Length - 1);
            }
            string[] nodeArr = ("solien,button_style,permissions,services,laymau,trakq,xquang,sieuam,vienphi,phatthuoc,tieptan,_height,_width,imgsource,apptype,startwithwindow,CTRoom").Split(',');
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
                            case 0: node.InnerText = txtsolien.Value.ToString(); break;
                            case 2: node.InnerText = dsKhoaDangChon; break;
                            case 3: node.InnerText = dsPKhamDangChon; break;
                            case 4: node.InnerText = ((ModelSelectItem)cblaymau.SelectedItem).Id.ToString(); break;
                            case 5: node.InnerText = ((ModelSelectItem)cbketqua.SelectedItem).Id.ToString(); break;
                            case 6: node.InnerText = ((ModelSelectItem)cbxquang.SelectedItem).Id.ToString(); break;
                            case 7: node.InnerText = ((ModelSelectItem)cbsieuam.SelectedItem).Id.ToString(); break;
                            case 8: node.InnerText = ((ModelSelectItem)cbvienphi.SelectedItem).Id.ToString(); break;
                            case 9: node.InnerText = ((ModelSelectItem)cbphatthuoc.SelectedItem).Id.ToString(); break;
                            case 10: node.InnerText = ((ModelSelectItem)cbtieptan.SelectedItem).Id.ToString(); break;
                            case 11: node.InnerText = numHeight.Text; break;
                            case 12: node.InnerText = numWidth.Text; break;
                            case 13: node.InnerText = _path; break;
                            case 14: node.InnerText = ((ModelSelectItem)cbAppType.SelectedItem).Id.ToString(); ; break;
                            case 15: node.InnerText = (chkStartWithWindows.Checked ? "1" : "0"); break;
                            case 16: node.InnerText = numResetForm.Value.ToString(); ; break;
                            case 17: node.InnerText = ((ModelSelectItem)cbCTRoom.SelectedItem).Id.ToString(); break;
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
                        case 0: node.AppendChild(xmlDoc.CreateTextNode(txtsolien.Value.ToString())); break;
                        case 1: node.AppendChild(xmlDoc.CreateTextNode("")); break;
                        case 2: node.AppendChild(xmlDoc.CreateTextNode(dsKhoaDangChon)); break;
                        case 3: node.AppendChild(xmlDoc.CreateTextNode(dsPKhamDangChon)); break;
                        case 4: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cblaymau.SelectedItem).Id.ToString())); break;
                        case 5: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbketqua.SelectedItem).Id.ToString())); break;
                        case 6: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbxquang.SelectedItem).Id.ToString())); break;
                        case 7: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbsieuam.SelectedItem).Id.ToString())); break;
                        case 8: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbvienphi.SelectedItem).Id.ToString())); break;
                        case 9: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbphatthuoc.SelectedItem).Id.ToString())); break;
                        case 10: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbtieptan.SelectedItem).Id.ToString())); break;
                        case 11: node.AppendChild(xmlDoc.CreateTextNode(numHeight.Text)); break;
                        case 12: node.AppendChild(xmlDoc.CreateTextNode(numWidth.Text)); break;
                        case 13: node.AppendChild(xmlDoc.CreateTextNode(_path)); break;
                        case 14: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbAppType.SelectedItem).Id.ToString())); break;
                        case 15: node.AppendChild(xmlDoc.CreateTextNode((chkStartWithWindows.Checked ? "1" : "0"))); ; break;
                        case 16: node.AppendChild(xmlDoc.CreateTextNode(numResetForm.Value.ToString())); ; break;
                        case 17: node.AppendChild(xmlDoc.CreateTextNode(((ModelSelectItem)cbCTRoom.SelectedItem).Id.ToString())); break;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cblaymau_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cblaymau.SelectedText
            // cbketqua.SelectedItem
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                _path = openFileDialog.FileName;
        }

        private void btnsettingnut_Click(object sender, EventArgs e)
        {
            var f = new frmButtonStyle();
            f.ShowDialog();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            string dsKhoaDangChon = "", dsPKhamDangChon = "";
            if (this.chkPermissions.Items != null && this.chkPermissions.Items.Count > 0)
            {
                foreach (CheckedListBoxItem item in this.chkPermissions.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        dsKhoaDangChon += item.Value + ",";
                }
                if (!string.IsNullOrEmpty(dsKhoaDangChon))
                    dsKhoaDangChon = dsKhoaDangChon.Substring(0, dsKhoaDangChon.Length - 1);
            }

            if (this.cbPhongkham.Properties.Items != null && this.cbPhongkham.Properties.Items.Count > 0)
            {
                foreach (CheckedListBoxItem item in this.cbPhongkham.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        dsPKhamDangChon += item.Value + ",";
                }
                if (!string.IsNullOrEmpty(dsPKhamDangChon))
                    dsPKhamDangChon = dsPKhamDangChon.Substring(0, dsPKhamDangChon.Length - 1);
            }
            refreshPK(dsKhoaDangChon, dsPKhamDangChon);
        }
    }
}
