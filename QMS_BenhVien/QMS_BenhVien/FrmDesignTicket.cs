using GPRO.Core.Hai;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QMS_BenhVien
{
    public partial class FrmDesignTicket : Form
    {
        public FrmDesignTicket()
        {
            InitializeComponent();
        }

        private void FrmDesignTicket_Load(object sender, EventArgs e)
        {
            txtContent.Text = FrmMain.ticketTemplate;
            txtsolien.Value = FrmMain.solien;
        }

        #region button event        

        private void btLeft_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[canh-trai]";
            txtContent.Text = content;
        }

        private void btCenter_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[canh-giua]";
            txtContent.Text = content;
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[canh-phai]";
            txtContent.Text = content;
        }

        private void bt11_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[1x1]";
            txtContent.Text = content;
        }

        private void bt21_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[2x1]";
            txtContent.Text = content;
        }

        private void bt31_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[3x1]";
            txtContent.Text = content;
        }

        private void bt22_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[2x2]";
            txtContent.Text = content;
        }

        private void bt33_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[3x3]";
            txtContent.Text = content;
        }

        private void btenter_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "\n";
            txtContent.Text = content;
        }

        private void btcut_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[cat-giay]";
            txtContent.Text = content;
        }

        private void btnngay_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[ngay]";
            txtContent.Text = content;
        }

        private void btgio_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[gio]";
            txtContent.Text = content;
        }

        private void btsave_Click(object sender, EventArgs e)
        { 
            FrmMain.ticketTemplate = txtContent.Text;
            FrmMain.solien = (int)txtsolien.Value;

            string filePath = Application.StartupPath + "\\Config.XML";
            XDocument testXML = XDocument.Load(filePath);
            XElement cStudent = testXML.Descendants("View").Where(c => c.Attribute("ID").Value.Equals("1")).FirstOrDefault();
            if (cStudent != null)
                cStudent.Element("Value").Value = txtContent.Text;
            cStudent = testXML.Descendants("View").Where(c => c.Attribute("ID").Value.Equals("2")).FirstOrDefault();
            if (cStudent != null)
                cStudent.Element("Value").Value = txtsolien.Value.ToString();

            testXML.Save(filePath);
            this.Close();
        }

        private void btDangGoi_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[dang-goi]";
            txtContent.Text = content;
        }

        private void btSTT_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[STT]";
            txtContent.Text = content;
        }

        private void btTenQuay_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[ten-quay]";
            txtContent.Text = content;
        }
        private void btTest_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            string content = txtContent.Text;
            content = content.Replace("[canh-giua]", "\x1b\x61\x01|+|");
            content = content.Replace("[canh-trai]", "\x1b\x61\x00|+|");
            content = content.Replace("[1x1]", "\x1d\x21\x00|+|");
            content = content.Replace("[2x1]", "\x1d\x21\x01|+|");
            content = content.Replace("[3x1]", "\x1d\x21\x02|+|");
            content = content.Replace("[2x2]", "\x1d\x21\x11|+|");
            content = content.Replace("[3x3]", "\x1d\x21\x22|+|");

            content = content.Replace("[STT]", "1001");
            content = content.Replace("[ten-quay]", "quay 1");
            content = content.Replace("[ten-dich-vu]", "dich vu 1");
            content = content.Replace("[ho-ten]", "Nguyen van C");
            content = content.Replace("[ngay]", ("ngay: " + now.ToString("dd/MM/yyyy")));
            content = content.Replace("[gio]", (" gio: " + now.ToString("HH/mm")));
            content = content.Replace("[dang-goi]", " dang goi 1000");
            content = content.Replace("[cat-giay]", "\x1b\x69|+|");

            var arr = content.Split(new string[] { "|+|" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            for (int ii = 0; ii < txtsolien.Value; ii++)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                   //BaseCore.Instance.PrintTicketTCVN3(FrmMain.COMPrint,arr[i]);
                }
            }
        }

        #endregion

        private void btTenDichVu_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[ten-dich-vu]";
            txtContent.Text = content;
        }

        private void btnHovaTen_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            content += "[ho-ten]";
            txtContent.Text = content;
        }
    }
}
