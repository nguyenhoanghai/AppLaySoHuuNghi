using GPRO.Core.Hai;
using QMS_System.Data.BLL;
using QMS_System.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    public partial class FrmSettingVP : Form
    {
        string connectString = BaseCore.Instance.GetEntityConnectString(Application.StartupPath + "\\DATA.XML");
        public FrmSettingVP()
        {
            InitializeComponent();
        }

        private void gridViewList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void FrmSettingVP_Load(object sender, EventArgs e)
        {
            List<ModelSelectItem> types = new List<ModelSelectItem>();
            types.Add(new ModelSelectItem() { Id = 3, Name = "Phát thuốc" });
            types.Add(new ModelSelectItem() { Id = 4, Name = "Viện phí" });
            cbtype.DataSource = types;
            cbtype.ValueMember = "Id";
            cbtype.DisplayMember = "Name";

            loadGrid();
        }

        private void loadGrid()
        {
            gridControl1.DataSource = null;
            gridControl1.DataSource = BLLService.Instance.Gets(Frmain_ver2.connectString);
        }

        private void repbtn_deleteMajor_Click(object sender, EventArgs e)
        {
            int a = 1;
        }

        private void repbtnEdit_Click(object sender, EventArgs e)
        {
            int a = 1;
        }

        private void repbtn_deleteMajor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int a = 1;
        }

        private void repbtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int a = 1;
        }
    }
}
