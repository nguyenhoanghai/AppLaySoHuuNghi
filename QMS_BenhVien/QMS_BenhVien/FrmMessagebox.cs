using System;
using System.Drawing;
using System.Windows.Forms;

namespace QMS_BenhVien
{
    public partial class FrmMessagebox : Form
    {
        int type = 1;
        string sms = "";

        public FrmMessagebox(int _type, string _sms)
        { 
            type = _type;
            sms = _sms;
            InitializeComponent();

        }

        private void btOK_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(btOK);
        }

        private void btOK_MouseUp(object sender, MouseEventArgs e)
        { 
            ButtonEffect_MouseUp(btOK, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);

        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMessagebox_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case 1: // success
                    this.picIcon.Image = global::QMS_BenhVien.Properties.Resources.success;
                    pnStatus.BackColor = Color.Green;
                    break;
                case 2: // infor
                    this.picIcon.Image = global::QMS_BenhVien.Properties.Resources.info;
                    pnStatus.BackColor = Color.Blue;
                    break;
                case 3: // warning
                    this.picIcon.Image = global::QMS_BenhVien.Properties.Resources.warning;
                    pnStatus.BackColor = Color.Orange;
                    break;
                case 4: // error
                    this.picIcon.Image = global::QMS_BenhVien.Properties.Resources.error;
                    pnStatus.BackColor = Color.Red;
                    break;
            }
            lbsms.Text = sms;
        }

        private void ButtonEffect_MouseDown(ButtonControl button)
        {
            button.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            button.BorderColor = Color.Red;
            button.ForeColor = Color.White;
        }

        private void ButtonEffect_MouseUp(ButtonControl button, Color _backgroundColor, Color _foreColor, Color _boderColor)
        {
            button.BackgroundColor = _backgroundColor;
            button.BorderColor = _boderColor;
            button.ForeColor = _foreColor;
        }
    }
}
