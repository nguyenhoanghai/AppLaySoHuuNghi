using API_KetNoi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QMS_BenhVien.RangHamMat
{
    public partial class FrmSearchResult : Form
    {
        List<BenhNhanModel> bns;
        FrmPrintTicket frmPrintTicket;
        FrmMessagebox messagebox;

        public FrmSearchResult(List<BenhNhanModel> _BNs, FrmPrintTicket _frmPrintTicket)
        {
            bns = _BNs;
            frmPrintTicket = _frmPrintTicket;
            InitializeComponent();
        }

        private void FrmSearchResult_Load(object sender, EventArgs e)
        {
            if (bns != null && bns.Count > 0)
            {
                BenhNhan_UC _benhNhanUC;
                int x = 5, y = 5;
                for (int i = 0; i < bns.Count; i++)
                {
                    _benhNhanUC = new BenhNhan_UC(bns[i], frmPrintTicket.serKhuCId);
                    _benhNhanUC.Location = new Point(x, y);
                    _benhNhanUC.Name = "uCtr_" + bns[i].mabn;
                    _benhNhanUC.printTicketEvent += new EventHandler<PrintTicketEventArgs>(PrintTicket);
                    pnDSBN.Controls.Add(_benhNhanUC);

                    y += 190;
                }
            }
        }

        private void PrintTicket(object sender, PrintTicketEventArgs e)
        {
            frmPrintTicket.PrintTicket(e.Require.ServiceId,  e.Require.Name.ToUpper(), e.Require.Address, e.Require.DOB??0, e.Require.MaBenhNhan, "");
            _showMessage((int)eMessageType.info, "Quý Khách vui lòng lấy phiếu STT sau đó di chuyển sang quầy tiếp tân Khu C chờ gọi số thứ tự. Xin cảm ơn.!");
        }

        private void _showMessage(int type, string message)
        {
            messagebox = new FrmMessagebox(type, message);
            messagebox.ShowDialog();
        }
    }
}
