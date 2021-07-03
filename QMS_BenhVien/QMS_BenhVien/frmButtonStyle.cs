using Newtonsoft.Json;
using QMS_System.Data.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace QMS_BenhVien
{

    public partial class frmButtonStyle : Form
    {
        FontConverter converter = new FontConverter();
        ButtonStyleModel btStyle = new ButtonStyleModel() { Height = 100, Width = 100, ButtonInRow = 5, Margin = 10, fontStyle = "Arial, 36pt, style=Bold", BackColor = "#ffffff", ForeColor = "#0000ff" };
        string backcolor = "";
        string forecolor = "";
        string fontstr = "";
        string width = "";
        string heigth = "";
        string space = ""; 
        public frmButtonStyle( )
        {
            InitializeComponent(); 
        }

        private void frmButtonStyle_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\Config.XML";
            var cfObj = Helper.Helper.Instance.GetAppConfig(filePath);
            if (!string.IsNullOrEmpty(cfObj.button_style))
                btStyle = JsonConvert.DeserializeObject<ButtonStyleModel>(cfObj.button_style);

            UpDownButtonWidth.Value = btStyle.Width;
            UpDownButtonHeight.Value = btStyle.Height;
            UpDownButtonSpace.Value = btStyle.Margin;
            numButtonInRow.Value = btStyle.ButtonInRow;

            btnSampleButton.Size = new Size(int.Parse(UpDownButtonWidth.Value.ToString()), int.Parse(UpDownButtonHeight.Value.ToString()));
            btnSampleButton.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            btnSampleButton.BackColor = ColorTranslator.FromHtml(btStyle.BackColor);
            btnSampleButton.ForeColor = ColorTranslator.FromHtml(btStyle.ForeColor);

            fontstr = btStyle.fontStyle;
            backcolor = btStyle.BackColor;
            forecolor = btStyle.ForeColor;
        }

        private void btnFontStyle_Click(object sender, EventArgs e)
        {
            FontConverter converter = new FontConverter();
            FontDialog fontdlg = new FontDialog();
            fontdlg.Font = (Font)converter.ConvertFromString(btStyle.fontStyle);
            if (fontdlg.ShowDialog() == DialogResult.OK)
            {
                Font font = fontdlg.Font;
                fontstr = converter.ConvertToString(font);
                btnSampleButton.Font = fontdlg.Font;
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colordlg = new ColorDialog();
            colordlg.Color = ColorTranslator.FromHtml(btStyle.ForeColor);
            if (colordlg.ShowDialog() == DialogResult.OK)
            {
                forecolor = colordlg.Color.ToArgb().ToString("x");
                forecolor = forecolor.Substring(2, 6);
                forecolor = "#" + forecolor; // dạng #ffffff hex
                btnSampleButton.ForeColor = colordlg.Color;
            }
        }

        private void btnButtonBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog colordlg = new ColorDialog();
            colordlg.Color = ColorTranslator.FromHtml(btStyle.BackColor);
            if (colordlg.ShowDialog() == DialogResult.OK)
            {
                backcolor = colordlg.Color.ToArgb().ToString("x");  // chuyển màu sang dạng hex ffffffff
                backcolor = backcolor.Substring(2, 6); // cắt chuổi lấy 6 ký tự cuối chính là mã màu
                backcolor = "#" + backcolor; // thêm # vào trước mã màu được #ffffff
                btnSampleButton.BackColor = colordlg.Color;
            }
        }

        private void UpDownButtonWidth_ValueChanged(object sender, EventArgs e)
        {
            btnSampleButton.Width = int.Parse(UpDownButtonWidth.Value.ToString());
        }

        private void UpDownButtonHeight_ValueChanged(object sender, EventArgs e)
        {
            btnSampleButton.Height = int.Parse(UpDownButtonHeight.Value.ToString());
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var style = new ButtonStyleModel()
            {
                Height = (int)UpDownButtonHeight.Value,
                Width = (int)UpDownButtonWidth.Value,
                Margin = (int)UpDownButtonSpace.Value,
                fontStyle = fontstr,
                BackColor = backcolor,
                ForeColor = forecolor,
                ButtonInRow = (int)numButtonInRow.Value
            };

            string filePath = Application.StartupPath + "\\Config.XML";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode node = xmlDoc.SelectSingleNode("Appsettings/button_style");
            node.InnerText = JsonConvert.SerializeObject(style);
            xmlDoc.Save(filePath);
            // frm.FrmMain_Load(sender, e);
            Application.Restart();
            Environment.Exit(0); 
        }

        private void btapply_Click(object sender, EventArgs e)
        {

        }
    }
}
