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
                    _benhNhanUC = new BenhNhan_UC(bns[i], frmPrintTicket.serviceId);
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
        }

    }
}
