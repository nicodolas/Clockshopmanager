using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminNhapHang.Model;
using AdminNhapHang.Services;
namespace AdminNhapHang
{
    public partial class frmThemSanPham : Form
    {
        FirestoreService firestoreService = new FirestoreService();
        CloudinaryService cloudinaryService = new CloudinaryService();
        public frmThemSanPham()
        {
            InitializeComponent();
            load_brand_cate();
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

        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(txtProductImageUrl.Text) ||
                string.IsNullOrWhiteSpace(txtProductDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!!");
                return;
            }

            // Parse giá sản phẩm
            if (!double.TryParse(txtProductPrice.Text, out double price))
            {
                MessageBox.Show("Giá sản phẩm không hợp lệ!");
                return;
            }

            // Tạo danh sách màu từ DataGridView
            List<ColorOption> colorOptions = new List<ColorOption>();
            foreach (DataGridViewRow row in dgvColorOptions.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua dòng trống cuối cùng

                string color = row.Cells["Color"].Value?.ToString();
                string imageUrl = row.Cells["ImageUrl"].Value?.ToString();
                int stock = 0;

                if (!string.IsNullOrWhiteSpace(color) && !string.IsNullOrWhiteSpace(imageUrl))
                {
                    colorOptions.Add(new ColorOption
                    {
                        Color = color,
                        ImageUrl = await cloudinaryService.UploadAndGetUrlImageAsync(imageUrl),
                        Stock = stock
                    });
                }
            }

            // Tạo đối tượng Product
            var product = new Product
            {
                BrandId = cbo_brands.SelectedValue.ToString(),
                CategoryId = cbo_cate.SelectedValue.ToString(),
                ProductName = txtProductName.Text.Trim(),
                ProductPrice = price,
                ProductImageUrl = await cloudinaryService.UploadAndGetUrlImageAsync(txtProductImageUrl.Text.Trim()),
                ProductDescription = txtProductDescription.Text.Trim(),
                ProductStatus = "Hết hàng",
                ColorOptions = colorOptions
            };

            // Gọi hàm thêm sản phẩm
            bool success = await firestoreService.AddProductAsync(product);
            if (success)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProductDescription.Clear();
                txtProductImageUrl.Clear();
                txtProductName.Clear();
                txtProductPrice.Clear();
                dgvColorOptions.Rows.Clear();
                txtProductName.Focus();
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
