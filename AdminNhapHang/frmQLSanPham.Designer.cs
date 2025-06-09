namespace AdminNhapHang
{
    partial class frmQLSanPham
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
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_luuThayDoi = new System.Windows.Forms.ToolStripMenuItem();
            this.gr_AddProduct = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel_QuayLai = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lst_Coloroptions = new System.Windows.Forms.ListBox();
            this.txtProductImageUrl = new System.Windows.Forms.TextBox();
            this.cbo_brands = new System.Windows.Forms.ComboBox();
            this.cbo_cate = new System.Windows.Forms.ComboBox();
            this.txtProductDescription = new System.Windows.Forms.RichTextBox();
            this.txtProductPrice = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.btn_LoadDSSP = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.gr_AddProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvProducts.Location = new System.Drawing.Point(32, 12);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(829, 193);
            this.dgvProducts.TabIndex = 0;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            this.dgvProducts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellEndEdit);
            this.dgvProducts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProducts_CellFormatting);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_luuThayDoi});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 26);
            // 
            // btn_luuThayDoi
            // 
            this.btn_luuThayDoi.Name = "btn_luuThayDoi";
            this.btn_luuThayDoi.Size = new System.Drawing.Size(140, 22);
            this.btn_luuThayDoi.Text = "Lưu thay đổi";
            this.btn_luuThayDoi.Click += new System.EventHandler(this.btn_luuThayDoi_Click);
            // 
            // gr_AddProduct
            // 
            this.gr_AddProduct.Controls.Add(this.label7);
            this.gr_AddProduct.Controls.Add(this.label6);
            this.gr_AddProduct.Controls.Add(this.label5);
            this.gr_AddProduct.Controls.Add(this.btnThemSP);
            this.gr_AddProduct.Controls.Add(this.label4);
            this.gr_AddProduct.Controls.Add(this.linkLabel_QuayLai);
            this.gr_AddProduct.Controls.Add(this.label3);
            this.gr_AddProduct.Controls.Add(this.label2);
            this.gr_AddProduct.Controls.Add(this.label1);
            this.gr_AddProduct.Controls.Add(this.pictureBox);
            this.gr_AddProduct.Controls.Add(this.lst_Coloroptions);
            this.gr_AddProduct.Controls.Add(this.txtProductImageUrl);
            this.gr_AddProduct.Controls.Add(this.cbo_brands);
            this.gr_AddProduct.Controls.Add(this.cbo_cate);
            this.gr_AddProduct.Controls.Add(this.txtProductDescription);
            this.gr_AddProduct.Controls.Add(this.txtProductPrice);
            this.gr_AddProduct.Controls.Add(this.txtProductName);
            this.gr_AddProduct.Controls.Add(this.txtProductId);
            this.gr_AddProduct.Location = new System.Drawing.Point(32, 238);
            this.gr_AddProduct.Name = "gr_AddProduct";
            this.gr_AddProduct.Size = new System.Drawing.Size(882, 297);
            this.gr_AddProduct.TabIndex = 1;
            this.gr_AddProduct.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(455, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Mô tả";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(247, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Loại";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Thương hiệu";
            // 
            // btnThemSP
            // 
            this.btnThemSP.Location = new System.Drawing.Point(458, 174);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(101, 32);
            this.btnThemSP.TabIndex = 14;
            this.btnThemSP.Text = "Thêm sản phẩm";
            this.btnThemSP.UseVisualStyleBackColor = true;
            this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "URL Hình Ảnh";
            // 
            // linkLabel_QuayLai
            // 
            this.linkLabel_QuayLai.AutoSize = true;
            this.linkLabel_QuayLai.Location = new System.Drawing.Point(821, 265);
            this.linkLabel_QuayLai.Name = "linkLabel_QuayLai";
            this.linkLabel_QuayLai.Size = new System.Drawing.Size(45, 13);
            this.linkLabel_QuayLai.TabIndex = 12;
            this.linkLabel_QuayLai.TabStop = true;
            this.linkLabel_QuayLai.Text = "Quay lại";
            this.linkLabel_QuayLai.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_QuayLai_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Giá";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Tên sản phẩm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(266, 127);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(175, 105);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            // 
            // lst_Coloroptions
            // 
            this.lst_Coloroptions.FormattingEnabled = true;
            this.lst_Coloroptions.Location = new System.Drawing.Point(730, 27);
            this.lst_Coloroptions.Name = "lst_Coloroptions";
            this.lst_Coloroptions.Size = new System.Drawing.Size(152, 147);
            this.lst_Coloroptions.TabIndex = 7;
            // 
            // txtProductImageUrl
            // 
            this.txtProductImageUrl.Location = new System.Drawing.Point(10, 258);
            this.txtProductImageUrl.Name = "txtProductImageUrl";
            this.txtProductImageUrl.ReadOnly = true;
            this.txtProductImageUrl.Size = new System.Drawing.Size(454, 20);
            this.txtProductImageUrl.TabIndex = 6;
            // 
            // cbo_brands
            // 
            this.cbo_brands.Enabled = false;
            this.cbo_brands.FormattingEnabled = true;
            this.cbo_brands.Location = new System.Drawing.Point(320, 37);
            this.cbo_brands.Name = "cbo_brands";
            this.cbo_brands.Size = new System.Drawing.Size(121, 21);
            this.cbo_brands.TabIndex = 5;
            // 
            // cbo_cate
            // 
            this.cbo_cate.Enabled = false;
            this.cbo_cate.FormattingEnabled = true;
            this.cbo_cate.Location = new System.Drawing.Point(320, 80);
            this.cbo_cate.Name = "cbo_cate";
            this.cbo_cate.Size = new System.Drawing.Size(121, 21);
            this.cbo_cate.TabIndex = 4;
            // 
            // txtProductDescription
            // 
            this.txtProductDescription.Location = new System.Drawing.Point(458, 27);
            this.txtProductDescription.Name = "txtProductDescription";
            this.txtProductDescription.Size = new System.Drawing.Size(266, 141);
            this.txtProductDescription.TabIndex = 3;
            this.txtProductDescription.Text = "";
            // 
            // txtProductPrice
            // 
            this.txtProductPrice.Location = new System.Drawing.Point(84, 126);
            this.txtProductPrice.Name = "txtProductPrice";
            this.txtProductPrice.ReadOnly = true;
            this.txtProductPrice.Size = new System.Drawing.Size(145, 20);
            this.txtProductPrice.TabIndex = 2;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(84, 80);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(145, 20);
            this.txtProductName.TabIndex = 1;
            // 
            // txtProductId
            // 
            this.txtProductId.Location = new System.Drawing.Point(84, 36);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.ReadOnly = true;
            this.txtProductId.Size = new System.Drawing.Size(145, 20);
            this.txtProductId.TabIndex = 0;
            // 
            // btn_LoadDSSP
            // 
            this.btn_LoadDSSP.Location = new System.Drawing.Point(298, 211);
            this.btn_LoadDSSP.Name = "btn_LoadDSSP";
            this.btn_LoadDSSP.Size = new System.Drawing.Size(134, 30);
            this.btn_LoadDSSP.TabIndex = 2;
            this.btn_LoadDSSP.Text = "Tải DS sản phẩm";
            this.btn_LoadDSSP.UseVisualStyleBackColor = true;
            this.btn_LoadDSSP.Click += new System.EventHandler(this.btn_LoadDSSP_Click);
            // 
            // frmQLSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 547);
            this.Controls.Add(this.btn_LoadDSSP);
            this.Controls.Add(this.gr_AddProduct);
            this.Controls.Add(this.dgvProducts);
            this.Name = "frmQLSanPham";
            this.Text = "Sản Phẩm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.gr_AddProduct.ResumeLayout(false);
            this.gr_AddProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.GroupBox gr_AddProduct;
        private System.Windows.Forms.Button btn_LoadDSSP;
        private System.Windows.Forms.RichTextBox txtProductDescription;
        private System.Windows.Forms.TextBox txtProductPrice;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.TextBox txtProductImageUrl;
        private System.Windows.Forms.ComboBox cbo_brands;
        private System.Windows.Forms.ComboBox cbo_cate;
        private System.Windows.Forms.ListBox lst_Coloroptions;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btn_luuThayDoi;
        private System.Windows.Forms.LinkLabel linkLabel_QuayLai;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnThemSP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}