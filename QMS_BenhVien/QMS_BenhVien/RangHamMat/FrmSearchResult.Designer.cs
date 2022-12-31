namespace QMS_BenhVien.RangHamMat
{
    partial class FrmSearchResult
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnDSBN = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnDSBN
            // 
            this.pnDSBN.AutoScroll = true;
            this.pnDSBN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDSBN.Location = new System.Drawing.Point(0, 0);
            this.pnDSBN.Name = "pnDSBN";
            this.pnDSBN.Size = new System.Drawing.Size(868, 547);
            this.pnDSBN.TabIndex = 0;
            // 
            // FrmSearchResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 547);
            this.Controls.Add(this.pnDSBN);
            this.MinimumSize = new System.Drawing.Size(860, 594);
            this.Name = "FrmSearchResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kết quả tìm kiếm";
            this.Load += new System.EventHandler(this.FrmSearchResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnDSBN;
    }
}