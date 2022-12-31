using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();
        }

        public FrmLoading(Form parent)
        {
            InitializeComponent();
        }

        public void CloseForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
