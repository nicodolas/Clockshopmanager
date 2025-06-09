using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminNhapHang
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btn_QLSP_Click(object sender, EventArgs e)
        {
            frmQLSanPham frm = new frmQLSanPham();
            frm.Show();
            this.Hide();
        }

        private void btn_QLPhieuDat_Click(object sender, EventArgs e)
        {
            frmNhapHang frm = new frmNhapHang();
            frm.Show();
            this.Hide();
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
            this.Close();
        }

        private void btnQLTraHang_Click(object sender, EventArgs e)
        {
            frmTraHang frm = new frmTraHang();
            frm.Show();
            this.Hide();
        }

        private void btn_ThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe frm = new frmThongKe();
            frm.Show();
            this.Hide();
        }
    }
}
