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
    public partial class FrmConfirmbox : Form
    {
        string yesText = "", noText = "", question = "";
        public FrmConfirmbox(string _yesText , string _noText  , string _question)
        {
            yesText = _yesText;
            noText = _noText;
            question = _question;
            InitializeComponent();
        }

        // This static method is the equivalent of MessageBox.Show
        public static DialogResult ShowDialog(IWin32Window owner, string yesText, string noText, string question)
        {
            // Setting the DialogResult does not close the form, it just hides it. 
            // This is why I'm disposing it. see the link at the end of my answer for details.
            using (var customDialog = new FrmConfirmbox(yesText ,noText, question))
            {
                return customDialog.ShowDialog(owner);
            }
        }

        private void FrmConfirmbox_Load(object sender, EventArgs e)
        {
            btYES.ButtonText = yesText;
            btNO.ButtonText = noText;
            lbsms.Text = question;
        }

        private void btNO_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btNO);
        }

        private void btNO_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btNO, Color.White, Color.DimGray, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))));
        }

        private void btNO_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btYES_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btYES_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseDown(this.btYES);
        }

        private void btYES_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonEffect_MouseUp(btYES, Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))), Color.Yellow, Color.Silver);
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
