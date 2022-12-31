using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_BenhVien
{
   public class LoadingFunc
    {
        FrmLoading frmLoading;
        Thread loadThread;

        public void Show()
        {
            loadThread = new Thread(new ThreadStart(ShowLoading));
            loadThread.Start();
        }

        public void Show(Form parent)
        {
            loadThread = new Thread(new ParameterizedThreadStart(ShowLoading));
            loadThread.Start(parent);
        }

        public void Close()
        {
            if (frmLoading != null)
            {
                frmLoading.BeginInvoke(new ThreadStart(frmLoading.CloseForm));
                frmLoading = null;
                loadThread = null;
            }
        }
        public void ShowLoading()
        {
            frmLoading = new FrmLoading();
            frmLoading.ShowDialog(); 
        }

        public void ShowLoading(object parent)
        {
            Form fParent = parent as Form;
            frmLoading = new FrmLoading(fParent);
            frmLoading.ShowDialog();
        }
    }
}
