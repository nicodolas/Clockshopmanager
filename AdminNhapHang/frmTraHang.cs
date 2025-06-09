using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminNhapHang.Model;
using AdminNhapHang.Services;

namespace AdminNhapHang
{
    public partial class frmTraHang : Form
    {
        private readonly FirestoreService service = new FirestoreService();
        private BindingList<ReturnRequest> returnRequestList = new BindingList<ReturnRequest>();

        private readonly frmDashboard frmDashboard;

        public frmTraHang()
        {
            InitializeComponent();

            frmDashboard = new frmDashboard();

            // Thiết lập DataGridView
            dgv_Returns.AutoGenerateColumns = true;
            dgv_Returns.DataSource = returnRequestList;
            dgv_Returns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Returns.MultiSelect = false;

           
            dgv_Returns.CellFormatting += dgv_Returns_CellFormatting;
            dgv_Returns.CellClick += dgv_Returns_CellClick;

         
        
        }

     
        private async Task LoadReturnRequests()
        {
            try
            {
                // Ngắt sự kiện để tránh lặp trong lúc cập nhật data
                dgv_Returns.CellFormatting -= dgv_Returns_CellFormatting;

                var list = await service.GetReturnRequestsAsync();

                // Sắp xếp theo trạng thái mong muốn
                var order = new[] { "Chờ xác nhận", "Chấp nhận", "Từ chối" };
                var sortedList = list.OrderBy(r =>
                {
                    int idx = Array.IndexOf(order, r.Status);
                    return idx == -1 ? int.MaxValue : idx;
                }).ToList();

               
                returnRequestList.Clear();
                foreach (var item in sortedList)
                    returnRequestList.Add(item);

                SetColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải phiếu trả hàng: " + ex.Message);
            }
            finally
            {
                // Đăng ký lại sự kiện sau khi load xong
                dgv_Returns.CellFormatting += dgv_Returns_CellFormatting;
            }
        }

        
        private void SetColumnHeaders()
        {
            if (dgv_Returns.Columns.Contains("ReturnId"))
                dgv_Returns.Columns["ReturnId"].HeaderText = "Mã trả hàng";
            if (dgv_Returns.Columns.Contains("userId"))
                dgv_Returns.Columns["userId"].HeaderText = "Mã khách hàng";
            if (dgv_Returns.Columns.Contains("OrderId"))
                dgv_Returns.Columns["OrderId"].HeaderText = "Mã đơn hàng";
            if (dgv_Returns.Columns.Contains("Status"))
                dgv_Returns.Columns["Status"].HeaderText = "Trạng thái";
            if (dgv_Returns.Columns.Contains("Reason"))
                dgv_Returns.Columns["Reason"].HeaderText = "Lý do";
            if (dgv_Returns.Columns.Contains("RequestDate"))
                dgv_Returns.Columns["RequestDate"].HeaderText = "Ngày yêu cầu";
            if (dgv_Returns.Columns.Contains("Amount"))
                dgv_Returns.Columns["Amount"].HeaderText = "Tổng tiền";
        }

        // Định dạng dòng theo trạng thái
        private void dgv_Returns_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_Returns.Rows.Count)
                return;

            var row = dgv_Returns.Rows[e.RowIndex];
            if (row.DataBoundItem is ReturnRequest rr)
            {
                var defaultFont = dgv_Returns.Font;

                switch (rr.Status)
                {
                    case "Từ chối":
                        row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                        row.DefaultCellStyle.Font = new Font(defaultFont, FontStyle.Bold);
                        break;
                    case "Chấp nhận":
                        row.DefaultCellStyle.ForeColor = Color.Green;
                        row.DefaultCellStyle.Font = new Font(defaultFont, FontStyle.Bold);
                        break;
                    case "Chờ xác nhận":
                        row.DefaultCellStyle.ForeColor = Color.DarkBlue;
                        row.DefaultCellStyle.Font = new Font(defaultFont, FontStyle.Underline);
                        break;
                    default:
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.Font = defaultFont;
                        break;
                }
            }
            else
            {
                row.DefaultCellStyle.ForeColor = Color.Black;
                row.DefaultCellStyle.Font = dgv_Returns.Font;
            }
        }

        // Khi click một dòng, show chi tiết sản phẩm trả
        private void dgv_Returns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_Returns.Rows.Count)
                return;

            var selectedRequest = dgv_Returns.Rows[e.RowIndex].DataBoundItem as ReturnRequest;

            if (selectedRequest?.ReturnItems != null)
            {
                dgvReturnItems.DataSource = selectedRequest.ReturnItems.Select(item => new
                {
                    MãSP = item.ProductId,
                    TênSP = item.ProductName,
                    Màu = item.SelectedColor,
                    Giá = item.Price,
                    SốLượng = item.Quantity,
                    ThànhTiền = item.Amount,
                    HìnhẢnh = item.SelectedImageUrl
                }).ToList();
            }
        }

      
        private async void btnLoadTraHang_Click(object sender, EventArgs e)
        {
            await LoadReturnRequests();
        }

       
        private async void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (dgv_Returns.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu trả hàng để chấp nhận.");
                return;
            }

            var returnId = dgv_Returns.SelectedRows[0].Cells["ReturnId"].Value?.ToString();

            if (string.IsNullOrEmpty(returnId))
            {
                MessageBox.Show("Không lấy được mã phiếu trả hàng.");
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn chấp nhận phiếu trả hàng này không?",
                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await service.UpdateReturnRequestStatusAsync(returnId, "Chấp nhận");
                MessageBox.Show("Đã chấp nhận phiếu trả hàng.");
                await LoadReturnRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }

        
        private async void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dgv_Returns.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu trả hàng để từ chối.");
                return;
            }

            var returnId = dgv_Returns.SelectedRows[0].Cells["ReturnId"].Value?.ToString();

            if (string.IsNullOrEmpty(returnId))
            {
                MessageBox.Show("Không lấy được mã phiếu trả hàng.");
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn từ chối phiếu trả hàng này không?",
                                          "Từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await service.UpdateReturnRequestStatusAsync(returnId, "Từ chối");
                MessageBox.Show("Đã từ chối phiếu trả hàng.");
                await LoadReturnRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }

        private void linkLabel_QuayLai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            frmDashboard.Show();
        }
    }
}
