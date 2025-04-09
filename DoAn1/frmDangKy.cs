using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn1.DTO;
using System.Collections;
using BUS;
using System.Data.SqlClient;
using DAL;

namespace DoAn1
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
                txtNhapLaiMatKhau.UseSystemPasswordChar= false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
                txtNhapLaiMatKhau.UseSystemPasswordChar = false;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            isValid &= ValidateControl(txtTenDangNhap, "Tên đăng nhập không được để trống.");
            isValid &= ValidateControl(txtMatKhau, "Mật khẩu không được để trống.");
            isValid &= ValidateControl(txtNhapLaiMatKhau, "Nhập lại mật khẩu không được để trống.");
            isValid &= ValidateControl(txtSoDienThoai, "Số điện thoại không được để trống.");

            // Kiểm tra mật khẩu nhập lại
            if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                errorProvider.SetError(txtNhapLaiMatKhau, "Mật khẩu nhập lại không khớp!");
                isValid = false;
            }

            if (!isValid) return;

            // Đăng ký
            TaiKhoanDTO tk = new TaiKhoanDTO(txtTenDangNhap.Text, txtMatKhau.Text, txtSoDienThoai.Text, "user");
            if (new TaiKhoanBus().DangKyTaiKhoan(tk))
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại!");
            }
        }

        private bool ValidateControl(Control control, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                errorProvider.SetError(control, errorMessage);
                return false;
            }
            else
            {
                errorProvider.SetError(control, string.Empty);
                return true;
            }
        }
    }
}
