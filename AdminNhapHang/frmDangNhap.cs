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
namespace AdminNhapHang
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (txt_MK.Text.Length == 0 || txt_TK.Text.Length == 0)
                return;
            string tk = txt_TK.Text.Trim();
            string mk = txt_MK.Text.Trim();
            TaiKhoan account = new TaiKhoan();
            if (account.Login(tk,mk))
            {
                frmDashboard frm = new frmDashboard();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }
    }
}
