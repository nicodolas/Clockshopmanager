using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AdminNhapHang.Services;
using AdminNhapHang.Model;

namespace AdminNhapHang
{
    public partial class frmNhapHang : Form
    {
        private frmDashboard frmDB = new frmDashboard();
        private FirestoreService firestoreService = new FirestoreService();
        private DB_Service db_Service = new DB_Service();

        private List<WarehouseReceiptDetail> receiptDetails = new List<WarehouseReceiptDetail>();
        private WarehouseReceipt currentReceipt = null;

        public frmNhapHang()
        {
            InitializeComponent();
            LoadSPAsync();
            loadPN();
        }
        void loadPN()
        {
            LoadReceiptsToListView(lstvPNHT, 1);
            LoadReceiptsToListView(lstvPNChoXacNhan, 0);
        }

        private async void LoadSPAsync()
        {
            try
            {
                var products = await firestoreService.GetProductsAsync();
                cboTenSP.DataSource = products;
                cboTenSP.DisplayMember = "ProductName";
                cboTenSP.ValueMember = "ProductId";

                if (cboTenSP.Items.Count > 0)
                    txtMaSP.Text = cboTenSP.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message);
            }
        }

        private void frmNhapHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmDB.Show();
        }
       async void loadColorOptions(string productID)
        {
            cbo_Color.DataSource = await firestoreService.GetColorOptionsByProductIdAsync(productID);
            cbo_Color.DisplayMember = "color";
            cbo_Color.ValueMember = "color";
        }

        private void cboTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenSP.SelectedItem is Product selectedProduct)
            {
                txtMaSP.Text = selectedProduct.ProductId;
                loadColorOptions(selectedProduct.ProductId);
            }
               

        }

        private void btnTaoPN_Click(object sender, EventArgs e)
        {
            if (currentReceipt != null)
            {
                MessageBox.Show("Phiếu nhập đã được tạo. Vui lòng lưu hoặc nhập lại trước khi tạo mới.");
                return;
            }

            currentReceipt = new WarehouseReceipt
            {
                ReceiptId = db_Service.TaoMaPhieuNhap(),
                Date = dtpNgayDat.Value,
                Status = 0,
                //GrandTotal = 0
            };

            txtMaPN.Text = currentReceipt.ReceiptId;
            txtTongDat.Text = "0";
            MessageBox.Show("Tạo phiếu nhập mới thành công!",
                  "Thành công",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            btnTaoPN.Enabled = false;
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (currentReceipt == null)
            {
                MessageBox.Show("Vui lòng tạo phiếu nhập trước.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng và đơn giá.");
                return;
            }

            try
            {
                int quantity = int.Parse(txtSoLuong.Text.Trim());
                decimal unitPrice = decimal.Parse(txtDonGia.Text.Trim());
                string productId = txtMaSP.Text.Trim();
                string color = cbo_Color.Text.Trim();
                //edit
                var existing = receiptDetails.FirstOrDefault(d => d.ProductId == productId&&d.Color.Equals(color));
                if (existing != null)
                {
                    existing.Quantity += quantity;
                }
                else
                {
                    receiptDetails.Add(new WarehouseReceiptDetail
                    {
                        ReceiptId = currentReceipt.ReceiptId,
                        ProductId = productId,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        Color = color
                    });
                }

                RefreshListViewAndTotal();
                txtSoLuong.Clear();
                txtDonGia.Clear();
                cboTenSP.SelectedIndex = 0;
                txtMaSP.Text = cboTenSP.SelectedValue.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Số lượng và đơn giá phải là số hợp lệ.");
            }
        }

        private async void RefreshListViewAndTotal()
        {
            lstvCTPN.Items.Clear();
            decimal total = 0;

            foreach (var item in receiptDetails)
            {
                string name = await firestoreService.GetProductNameByIdAsync(item.ProductId);
                ListViewItem lvItem = new ListViewItem(name);
                lvItem.SubItems.Add(item.Quantity.ToString());
                lvItem.SubItems.Add(item.Color.ToString());
                lvItem.SubItems.Add(item.UnitPrice.ToString("N0"));
                lvItem.SubItems.Add(item.Total.ToString("N0"));
                lvItem.Tag = item.ProductId;
                lstvCTPN.Items.Add(lvItem);

                total += item.Total;
            }

            //currentReceipt.GrandTotal = total;
            txtTongDat.Text = total.ToString("N0");
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (lstvCTPN.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            var selectedItem = lstvCTPN.SelectedItems[0];
            string productId = selectedItem.Tag?.ToString();

            if (!string.IsNullOrEmpty(productId))
            {
                var itemToRemove = receiptDetails.FirstOrDefault(r => r.ProductId == productId);
                if (itemToRemove != null)
                    receiptDetails.Remove(itemToRemove);
            }

            lstvCTPN.Items.Remove(selectedItem);
            RefreshListViewAndTotal();
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            currentReceipt = null;
            receiptDetails.Clear();
            txtMaPN.Clear();
            txtTongDat.Text = "0";
            txtSoLuong.Clear();
            txtDonGia.Clear();
            lstvCTPN.Items.Clear();
            btnTaoPN.Enabled = true;

            if (cboTenSP.Items.Count > 0)
            {
                cboTenSP.SelectedIndex = 0;
                txtMaSP.Text = cboTenSP.SelectedValue.ToString();
            }
        }

        private void btnLuuPN_Click(object sender, EventArgs e)
        {
            if (currentReceipt == null || receiptDetails.Count == 0)
            {
                MessageBox.Show("Vui lòng tạo phiếu nhập và thêm sản phẩm trước khi lưu!");
                return;
            }

            try
            {
                // Lưu phiếu nhập
                bool savedReceipt = db_Service.SaveWarehouseReceipt(currentReceipt);
                if (!savedReceipt)
                {
                    MessageBox.Show("Lưu phiếu nhập thất bại!");
                    return;
                }

                // Lưu chi tiết phiếu nhập
                bool savedDetails = db_Service.SaveWarehouseReceiptDetails(receiptDetails);
                if (!savedDetails)
                {
                    MessageBox.Show("Lưu chi tiết phiếu nhập thất bại!");
                   
                    return;
                }

                MessageBox.Show("Lưu phiếu nhập thành công!");
                loadPN();
                btnNhapLai.PerformClick(); // Reset giao diện
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }
        private void LoadReceiptsToListView(ListView ItemName,int? status = 0)
        {
            ItemName.Items.Clear();

            var receipts = db_Service.GetAllWarehouseReceipts(status);
            foreach (var r in receipts)
            {
                ListViewItem it = new ListViewItem(r.ReceiptId);
                it.SubItems.Add(r.Date.ToString("dd/MM/yyyy"));
                it.SubItems.Add(r.GrandTotal.ToString("N0"));
                //it.SubItems.Add(r.Status.ToString());
                it.Tag = r.ReceiptId;                 // để lấy lại khi click
                ItemName.Items.Add(it);
            }
        }

        private async void lstvPNHT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvPNHT.SelectedItems.Count == 0)
                return;

            // Lấy mã phiếu nhập được chọn
            string receiptId = lstvPNHT.SelectedItems[0].SubItems[0].Text;

            // Gọi DB để lấy chi tiết phiếu nhập
            List<WarehouseReceiptDetail> details = db_Service.GetReceiptDetails(receiptId);

            // Hiển thị lên ListView chi tiết phiếu nhập (giả sử tên là lstvCTPN)
            lstvCTPN.Items.Clear();

            foreach (var detail in details)
            {
                string name = await firestoreService.GetProductNameByIdAsync(detail.ProductId);
                ListViewItem item = new ListViewItem(name);
                item.SubItems.Add(detail.Quantity.ToString());
                item.SubItems.Add(detail.Color.ToString());
                item.SubItems.Add(detail.UnitPrice.ToString("N0"));
                item.SubItems.Add(detail.Total.ToString("N0"));

                lstvCTPN.Items.Add(item);
            }
        }

        private async void  lstvPNChoXacNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvPNChoXacNhan.SelectedItems.Count == 0)
                return;

            // Lấy mã phiếu nhập được chọn
            string receiptId = lstvPNChoXacNhan.SelectedItems[0].SubItems[0].Text;

            // Gọi DB để lấy chi tiết phiếu nhập
            List<WarehouseReceiptDetail> details = db_Service.GetReceiptDetails(receiptId);

            // Hiển thị lên ListView chi tiết phiếu nhập
            lstvCTPN.Items.Clear();

            foreach (var detail in details)
            {
                string name = await firestoreService.GetProductNameByIdAsync(detail.ProductId);
                ListViewItem item = new ListViewItem(name);
                item.SubItems.Add(detail.Quantity.ToString());
                item.SubItems.Add(detail.Color.ToString());
                item.SubItems.Add(detail.UnitPrice.ToString("N0"));
                item.SubItems.Add(detail.Total.ToString("N0"));
                lstvCTPN.Items.Add(item);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (lstvPNChoXacNhan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã phiếu nhập từ dòng đang chọn
            string receiptId = lstvPNChoXacNhan.SelectedItems[0].SubItems[0].Text;

           
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xác nhận phiếu nhập {receiptId}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool isSuccess = db_Service.UpdateWarehouseReceiptStatus(receiptId, 1); // 1 = đã xác nhận

                if (isSuccess)
                {
                    MessageBox.Show("Xác nhận phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadPN();
                    updateSPLenFirebase(receiptId);
                }
                else
                {
                    MessageBox.Show("Xác nhận thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        async void updateSPLenFirebase(string maPN)
        {
            List<ReceiptItemSummary> list = db_Service.GetReceiptItemSummaries(maPN);
            foreach (var item in list)
            {
                try
                {
                    await firestoreService.UpdateProductStockByColorAsync(item.ProductId, item.Color, item.Quantity);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void btnHuyPN_Click(object sender, EventArgs e)
        {
            if (lstvPNChoXacNhan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã phiếu nhập từ dòng đang chọn
            string receiptId = lstvPNChoXacNhan.SelectedItems[0].SubItems[0].Text;


            DialogResult result = MessageBox.Show($"Bạn có chắc muốn huỷ phiếu nhập {receiptId}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool isSuccess = db_Service.UpdateWarehouseReceiptStatus(receiptId,-1); // -1 = từ chối

                if (isSuccess)
                {
                    MessageBox.Show("Huỷ phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadPN();
                }
                else
                {
                    MessageBox.Show("Huỷ thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_CapNhatLenServer_Click(object sender, EventArgs e)
        {

        }

    
    }
}
