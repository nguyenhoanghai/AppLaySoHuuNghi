namespace QMS_BenhVien
{
    partial class FrmDongBo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCLS = new System.Windows.Forms.Button();
            this.btPK = new System.Windows.Forms.Button();
            this.txtjson = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btGetPK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btGetPK);
            this.groupBox1.Controls.Add(this.btCLS);
            this.groupBox1.Controls.Add(this.btPK);
            this.groupBox1.Controls.Add(this.txtjson);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(800, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Đồng bộ phòng khám";
            // 
            // btCLS
            // 
            this.btCLS.Location = new System.Drawing.Point(240, 411);
            this.btCLS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCLS.Name = "btCLS";
            this.btCLS.Size = new System.Drawing.Size(229, 33);
            this.btCLS.TabIndex = 2;
            this.btCLS.Text = "Đồng bộ danh mục cận lâm sàn";
            this.btCLS.UseVisualStyleBackColor = true;
            this.btCLS.Click += new System.EventHandler(this.btCLS_Click);
            // 
            // btPK
            // 
            this.btPK.Location = new System.Drawing.Point(497, 411);
            this.btPK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btPK.Name = "btPK";
            this.btPK.Size = new System.Drawing.Size(297, 33);
            this.btPK.TabIndex = 1;
            this.btPK.Text = "Đồng bộ danh mục phòng khám suống QMS";
            this.btPK.UseVisualStyleBackColor = true;
            this.btPK.Click += new System.EventHandler(this.btPK_Click);
            // 
            // txtjson
            // 
            this.txtjson.Location = new System.Drawing.Point(5, 21);
            this.txtjson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtjson.Multiline = true;
            this.txtjson.Name = "txtjson";
            this.txtjson.Size = new System.Drawing.Size(788, 384);
            this.txtjson.TabIndex = 0;
            // 
            // btGetPK
            // 
            this.btGetPK.ForeColor = System.Drawing.Color.ForestGreen;
            this.btGetPK.Location = new System.Drawing.Point(5, 411);
            this.btGetPK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btGetPK.Name = "btGetPK";
            this.btGetPK.Size = new System.Drawing.Size(229, 33);
            this.btGetPK.TabIndex = 3;
            this.btGetPK.Text = "Lấy danh mục phòng khám từ HIS";
            this.btGetPK.UseVisualStyleBackColor = true;
            this.btGetPK.Click += new System.EventHandler(this.btGetPK_Click);
            // 
            // FrmDongBo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmDongBo";
            this.Text = "Đồng bộ danh mục";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btCLS;
        private System.Windows.Forms.Button btPK;
        private System.Windows.Forms.TextBox txtjson;
        private System.Windows.Forms.Button btGetPK;
    }
}