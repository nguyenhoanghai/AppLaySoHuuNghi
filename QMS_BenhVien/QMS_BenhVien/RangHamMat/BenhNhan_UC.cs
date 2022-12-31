using API_KetNoi.Models;
using QMS_System.Data.Model;
using System;
using System.Windows.Forms;

namespace QMS_BenhVien.RangHamMat
{
    public partial class BenhNhan_UC : UserControl
    {
        BenhNhanModel benhNhanModel;
        int serviceId = 0;
        public event EventHandler<PrintTicketEventArgs> printTicketEvent;
        public BenhNhan_UC(BenhNhanModel _benhNhanModel, int _serviceId)
        {
            benhNhanModel = _benhNhanModel;
            serviceId = _serviceId;
            InitializeComponent();
        }

        private void BenhNhan_UC_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void BenhNhan_UC_Load(object sender, EventArgs e)
        {
            lbAdd.Text = benhNhanModel.thon;
            lbMa.Text = benhNhanModel.mabn;
            lbName.Text = benhNhanModel.hoten;
            lbDOB.Text = benhNhanModel.ngaysinh;
            lbCCCD.Text = benhNhanModel.socmnd;
            lbPhone.Text = benhNhanModel.dienthoai;
        }

        private void BenhNhan_UC_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (printTicketEvent != null)
            {
                printTicketEvent(sender, new PrintTicketEventArgs(new PrinterRequireModel()
                {
                    ServiceId = serviceId,
                    ServeTime = DateTime.Now, // dpkTime.Value
                    Name = benhNhanModel.hoten,
                    DOB = Convert.ToInt32(benhNhanModel.namsinh),
                    Address = benhNhanModel.thon,
                    MaBenhNhan = benhNhanModel.mabn,
                    Phone = benhNhanModel.dienthoai
                }));
            }
            //  MessageBox.Show("print ticket " + benhNhanModel.mabn);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lbName_Click(object sender, EventArgs e)
        {

        }
    }

    public class PrintTicketEventArgs : EventArgs
    {
        private PrinterRequireModel _require;
        public PrinterRequireModel Require { get { return _require; } }
        public PrintTicketEventArgs(PrinterRequireModel require) : base()
        {
            _require = require;
        }
    }
}
