
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
            bool dangNhapThanhCong = taiKhoanBus.KiemTraDangNhap(tenDangNhap, matKhau);
            if (dangNhapThanhCong)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frmTrangChu frm = new frmTrangChu(); // Màn hình chính sau khi đăng nhập
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
}
