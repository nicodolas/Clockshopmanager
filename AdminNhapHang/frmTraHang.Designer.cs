namespace AdminNhapHang
{
    partial class frmTraHang
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_Returns = new System.Windows.Forms.DataGridView();
            this.menuChuotPhai = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnChapNhan = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTuChoi = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadTraHang = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel_QuayLai = new System.Windows.Forms.LinkLabel();
            this.dgvReturnItems = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Returns)).BeginInit();
            this.menuChuotPhai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(186, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(369, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "QUẢN LÝ TRẢ HÀNG";
            // 
            // dgv_Returns
            // 
            this.dgv_Returns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Returns.ContextMenuStrip = this.menuChuotPhai;
            this.dgv_Returns.Location = new System.Drawing.Point(63, 93);
            this.dgv_Returns.Name = "dgv_Returns";
            this.dgv_Returns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Returns.Size = new System.Drawing.Size(748, 166);
            this.dgv_Returns.TabIndex = 5;
            this.dgv_Returns.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Returns_CellClick);
            this.dgv_Returns.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_Returns_CellFormatting);
            // 
            // menuChuotPhai
            // 
            this.menuChuotPhai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnChapNhan,
            this.btnTuChoi});
            this.menuChuotPhai.Name = "menuChuotPhai";
            this.menuChuotPhai.Size = new System.Drawing.Size(133, 48);
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(132, 22);
            this.btnChapNhan.Text = "Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(132, 22);
            this.btnTuChoi.Text = "Từ chối";
            this.btnTuChoi.Click += new System.EventHandler(this.btnTuChoi_Click);
            // 
            // btnLoadTraHang
            // 
            this.btnLoadTraHang.Location = new System.Drawing.Point(63, 265);
            this.btnLoadTraHang.Name = "btnLoadTraHang";
            this.btnLoadTraHang.Size = new System.Drawing.Size(83, 39);
            this.btnLoadTraHang.TabIndex = 6;
            this.btnLoadTraHang.Text = "Tải danh sách yêu cầu";
            this.btnLoadTraHang.UseVisualStyleBackColor = true;
            this.btnLoadTraHang.Click += new System.EventHandler(this.btnLoadTraHang_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Danh sách các yêu cầu trả hàng";
            // 
            // linkLabel_QuayLai
            // 
            this.linkLabel_QuayLai.AutoSize = true;
            this.linkLabel_QuayLai.Location = new System.Drawing.Point(766, 505);
            this.linkLabel_QuayLai.Name = "linkLabel_QuayLai";
            this.linkLabel_QuayLai.Size = new System.Drawing.Size(45, 13);
            this.linkLabel_QuayLai.TabIndex = 13;
            this.linkLabel_QuayLai.TabStop = true;
            this.linkLabel_QuayLai.Text = "Quay lại";
            this.linkLabel_QuayLai.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_QuayLai_LinkClicked);
            // 
            // dgvReturnItems
            // 
            this.dgvReturnItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnItems.Location = new System.Drawing.Point(63, 332);
            this.dgvReturnItems.Name = "dgvReturnItems";
            this.dgvReturnItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReturnItems.Size = new System.Drawing.Size(748, 166);
            this.dgvReturnItems.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Chi tiết đơn hàng";
            // 
            // frmTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 527);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvReturnItems);
            this.Controls.Add(this.linkLabel_QuayLai);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadTraHang);
            this.Controls.Add(this.dgv_Returns);
            this.Controls.Add(this.label1);
            this.Name = "frmTraHang";
            this.Text = "frmTraHang";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Returns)).EndInit();
            this.menuChuotPhai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_Returns;
        private System.Windows.Forms.Button btnLoadTraHang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel_QuayLai;
        private System.Windows.Forms.DataGridView dgvReturnItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip menuChuotPhai;
        private System.Windows.Forms.ToolStripMenuItem btnChapNhan;
        private System.Windows.Forms.ToolStripMenuItem btnTuChoi;
    }
}