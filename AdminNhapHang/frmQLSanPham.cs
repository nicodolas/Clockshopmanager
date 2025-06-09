using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminNhapHang.Services;
using AdminNhapHang.Model;


using SkiaSharp;
using System.Net;
using System.IO;
using System.Drawing;
namespace AdminNhapHang
{
    public partial class frmQLSanPham : Form
    {
        private frmDashboard frmDashboard = new frmDashboard();
        private FirestoreService firestoreService;

        public frmQLSanPham()
        {
            InitializeComponent();
            firestoreService = new FirestoreService();
          


        }
        private async void load_brand_cate()
        {
            try
            {
                var brands = await firestoreService.GetBrandsAsync();
                var categories = await firestoreService.GetCategoriesAsync();
                cbo_brands.DataSource = brands;
                cbo_brands.DisplayMember = "brandName";
                cbo_brands.ValueMember = "brandId";
                cbo_cate.DataSource = categories;
                cbo_cate.DisplayMember = "categoryName";
                cbo_cate.ValueMember = "categoryId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async void LoadProducts()
        {
            try
            {
                dgvProducts.DataSource = await firestoreService.GetProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải sản phẩm: {ex.Message}");
            }
        }

       

        private void btn_LoadDSSP_Click(object sender, EventArgs e)
        {
            LoadProducts();
            load_brand_cate();
        }
       
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var row = dgvProducts.SelectedRows[0];

                // Gán dữ liệu sang control phía dưới
                txtProductId.Text = row.Cells["ProductId"].Value?.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();
                txtProductDescription.Text = row.Cells["ProductDescription"].Value?.ToString();
                txtProductPrice.Text = row.Cells["ProductPrice"].Value?.ToString();
                txtProductImageUrl.Text = row.Cells["ProductImageUrl"].Value?.ToString().Trim();
                var brandId = row.Cells["brandId"].Value?.ToString();
                var categoryId = row.Cells["categoryId"].Value?.ToString();

                
                cbo_brands.SelectedItem = cbo_brands.Items.Cast<Brand>().FirstOrDefault(b => b.brandId == brandId);
                cbo_cate.SelectedItem = cbo_cate.Items.Cast<Category>().FirstOrDefault(c => c.categoryId == categoryId);

                // Hiển thị ảnh sản phẩm
                var imageUrl = txtProductImageUrl.Text;
                try
                {
                    pictureBox.LoadAsync(imageUrl);
                }
                catch { pictureBox.Image = null; }

                // TODO: Gọi FirestoreService để lấy chi tiết ColorOptions nếu cần
                LoadColorOptions(row.Cells["ProductId"].Value?.ToString());
            }
        }
        private async void LoadColorOptions(string productId)
        {
            var firestoreService = new FirestoreService();
            var products = await firestoreService.GetProductsAsync();
            var product = products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null && product.ColorOptions != null)
            {
                lst_Coloroptions.Items.Clear();
                foreach (var option in product.ColorOptions)
                {
                    lst_Coloroptions.Items.Add($"Màu: {option.Color} | Stock: {option.Stock}");
                }
            }
        }




        //===================================== UPDATE
        private HashSet<(int rowIndex, int columnIndex)> modifiedCells = new HashSet<(int rowIndex, int columnIndex)>();
        private object originalValue;

        private void dgvProducts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            originalValue = dgvProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }
        private void dgvProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var newValue = dgvProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (!Equals(originalValue, newValue))
            {
                modifiedCells.Add((e.RowIndex, e.ColumnIndex));
            }
            else
            {
                modifiedCells.Remove((e.RowIndex, e.ColumnIndex));
            }
        }
        private async void btn_luuThayDoi_Click(object sender, EventArgs e)
        {
            int updatedCount = 0;

            var modifiedRows = modifiedCells.Select(cell => cell.rowIndex).Distinct();

            foreach (int rowIndex in modifiedRows)
            {
                var row = dgvProducts.Rows[rowIndex];
                var product = row.DataBoundItem as Product;

                if (product != null)
                {
                    var success = await firestoreService.UpdateProductAsync(product);
                    if (success)
                    {
                        updatedCount++;
                    }
                    else
                    {
                        MessageBox.Show($"Lưu thất bại ở sản phẩm: {product.ProductName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            modifiedCells.Clear();

            if (updatedCount > 0)
            {
                MessageBox.Show($"Đã lưu thành công {updatedCount} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có chỉnh sửa nào để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = dgvProducts.Rows[e.RowIndex];

            // Lấy object sản phẩm đang bind
            if (row.DataBoundItem is Product product)
            {
                if (product.ProductStatus == "Ngừng bán")
                {
                    row.DefaultCellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.MediumVioletRed;
                }
                else if (product.ProductStatus == "Hết hàng")
                {
                    row.DefaultCellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.IndianRed;
                }
                else
                {
                    // Trả về style mặc định
                    row.DefaultCellStyle.Font = dgvProducts.Font;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void linkLabel_QuayLai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            frmDashboard.Show();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            frmThemSanPham frm = new frmThemSanPham();
            frm.ShowDialog();
        }

       
    }




}
