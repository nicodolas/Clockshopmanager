using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Windows.Forms;
using AdminNhapHang.Services;
namespace AdminNhapHang
{
    public partial class frmThongKe : Form
    {
        frmDashboard frmDB;
        DB_Service dB = new DB_Service();
        FirestoreService firestoreService = new FirestoreService();

        public frmThongKe()
        {
            InitializeComponent();
            frmDB = new frmDashboard();
            this.WindowState = FormWindowState.Maximized;
        }

        private async void frmThongKe_Load(object sender, EventArgs e)
        {
            decimal TienNhap = dB.TinhTongTienPhieuNhap(1); // Phiếu đã xác nhận
            decimal TienBan = await firestoreService.GetTotalInvoiceAmountAsync();
            DataTable dt = GetTongTienData(TienBan, TienNhap);

            MyReport rpt = new MyReport();
            rpt.SetDataSource(dt);

            crystalReportViewer1.ReportSource = rpt;
        }

        private DataTable GetTongTienData(decimal tongBan, decimal tongNhap)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Loai", typeof(string));
            dt.Columns.Add("SoTien", typeof(decimal));
            dt.Columns.Add("DoanhThu", typeof(decimal));  

            dt.Rows.Add("Tổng bán", tongBan);
            dt.Rows.Add("Tổng nhập", tongNhap);
            dt.Rows.Add("Lợi nhuận", tongBan-tongNhap);

            return dt;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frmDB.Show();
            this.Close();
        }
    }
}
