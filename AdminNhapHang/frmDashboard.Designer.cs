namespace AdminNhapHang
{
    partial class frmDashboard
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
            this.btn_QLSP = new System.Windows.Forms.Button();
            this.btn_QLPhieuDat = new System.Windows.Forms.Button();
            this.btn_ThongKe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_DangXuat = new System.Windows.Forms.Button();
            this.btnQLTraHang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_QLSP
            // 
            this.btn_QLSP.Location = new System.Drawing.Point(62, 131);
            this.btn_QLSP.Name = "btn_QLSP";
            this.btn_QLSP.Size = new System.Drawing.Size(133, 41);
            this.btn_QLSP.TabIndex = 0;
            this.btn_QLSP.Text = "Quản Lý Sản Phẩm";
            this.btn_QLSP.UseVisualStyleBackColor = true;
            this.btn_QLSP.Click += new System.EventHandler(this.btn_QLSP_Click);
            // 
            // btn_QLPhieuDat
            // 
            this.btn_QLPhieuDat.Location = new System.Drawing.Point(62, 199);
            this.btn_QLPhieuDat.Name = "btn_QLPhieuDat";
            this.btn_QLPhieuDat.Size = new System.Drawing.Size(133, 41);
            this.btn_QLPhieuDat.TabIndex = 1;
            this.btn_QLPhieuDat.Text = "Quản Lý Phiếu Nhập";
            this.btn_QLPhieuDat.UseVisualStyleBackColor = true;
            this.btn_QLPhieuDat.Click += new System.EventHandler(this.btn_QLPhieuDat_Click);
            // 
            // btn_ThongKe
            // 
            this.btn_ThongKe.Location = new System.Drawing.Point(62, 320);
            this.btn_ThongKe.Name = "btn_ThongKe";
            this.btn_ThongKe.Size = new System.Drawing.Size(133, 41);
            this.btn_ThongKe.TabIndex = 2;
            this.btn_ThongKe.Text = "Thống Kê Doanh Thu";
            this.btn_ThongKe.UseVisualStyleBackColor = true;
            this.btn_ThongKe.Click += new System.EventHandler(this.btn_ThongKe_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Yi Baiti", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(301, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 48);
            this.label1.TabIndex = 3;
            this.label1.Text = "DASH BOARD";
            // 
            // btn_DangXuat
            // 
            this.btn_DangXuat.Location = new System.Drawing.Point(727, 411);
            this.btn_DangXuat.Name = "btn_DangXuat";
            this.btn_DangXuat.Size = new System.Drawing.Size(91, 27);
            this.btn_DangXuat.TabIndex = 4;
            this.btn_DangXuat.Text = "Đăng xuất";
            this.btn_DangXuat.UseVisualStyleBackColor = true;
            this.btn_DangXuat.Click += new System.EventHandler(this.btn_DangXuat_Click);
            // 
            // btnQLTraHang
            // 
            this.btnQLTraHang.Location = new System.Drawing.Point(62, 258);
            this.btnQLTraHang.Name = "btnQLTraHang";
            this.btnQLTraHang.Size = new System.Drawing.Size(133, 41);
            this.btnQLTraHang.TabIndex = 5;
            this.btnQLTraHang.Text = "Quản lý trả hàng";
            this.btnQLTraHang.UseVisualStyleBackColor = true;
            this.btnQLTraHang.Click += new System.EventHandler(this.btnQLTraHang_Click);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 450);
            this.Controls.Add(this.btnQLTraHang);
            this.Controls.Add(this.btn_DangXuat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ThongKe);
            this.Controls.Add(this.btn_QLPhieuDat);
            this.Controls.Add(this.btn_QLSP);
            this.Name = "frmDashboard";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_QLSP;
        private System.Windows.Forms.Button btn_QLPhieuDat;
        private System.Windows.Forms.Button btn_ThongKe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_DangXuat;
        private System.Windows.Forms.Button btnQLTraHang;
    }
}