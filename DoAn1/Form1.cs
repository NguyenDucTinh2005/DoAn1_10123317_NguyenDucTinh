
using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn1
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        TaiKhoanBus taiKhoanBus = new TaiKhoanBus();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy f = new frmDangKy();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if(chkHienMatKhau.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (taiKhoanBus.KiemTraDangNhap(tenDangNhap, matKhau))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                // Lấy quyền người dùng và MaKhachHang
                DataTable dt = taiKhoanBus.getAllTaiKhoan();
                string quyen = "Khách hàng";
                string maKhachHang = null;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["TenDangNhap"].ToString() == tenDangNhap)
                    {
                        quyen = row["Quyen"].ToString();
                        maKhachHang = row["MaKhachHang"]?.ToString(); // Lấy MaKhachHang
                        break;
                    }
                }

                this.Hide();
                frmTrangChu frm = new frmTrangChu(quyen, tenDangNhap, maKhachHang); // Truyền cả ba tham số
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi");
            }
        }


        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
}
