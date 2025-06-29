﻿using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using System.Collections;
using System.Data.SqlClient;
using DAL;
using DoAn1.DTO;

namespace DoAn1
{
    public partial class frmHeThong : Form
    {
        TaiKhoanBus TaiKhoanBus = new TaiKhoanBus();
        public frmHeThong()
        {
            InitializeComponent();
            LoadTaiKhoan();
        }

        private void LoadTaiKhoan()
        {
            dtgvTaiKhoan.DataSource = TaiKhoanBus.getAllTaiKhoan();

            // Sửa lại: Không dùng ValueMember
            List<string> quyenList = new List<string> { "Quản trị viên", "Nhân viên", "Khách hàng" };
            cboQuyen.DataSource = quyenList;
        }
        private void dtgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvTaiKhoan.Rows[e.RowIndex];
                txtMaTaiKhoan.Text = row.Cells["MaTaiKhoan"].Value.ToString();
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                cboQuyen.SelectedItem = row.Cells["Quyen"].Value.ToString();

            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO(txtTenDangNhap.Text, txtMatKhau.Text, txtSoDienThoai.Text, cboQuyen.SelectedValue.ToString());

            // Nếu quyền là "Khách hàng" hoặc "Nhân viên", tự động tạo MaKhachHang và liên kết
            if (taiKhoanDTO.quyen == "Khách hàng" || taiKhoanDTO.quyen == "Nhân viên"|| taiKhoanDTO.quyen == "Quản trị viên")
            {
                string maKhachHang = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
                KhachHangDTO khachHang = new KhachHangDTO(maKhachHang, txtTenDangNhap.Text, "", txtSoDienThoai.Text);
                KhachHangBUS khachhangBUS= new KhachHangBUS();
                if (!khachhangBUS.insertKhachHang(khachHang))
                {
                    MessageBox.Show("Lỗi khi tạo thông tin khách hàng!");
                    return;
                }
                taiKhoanDTO.MaKhachHang1 = maKhachHang;
            }

            if (TaiKhoanBus.insertTaiKhoan(taiKhoanDTO))
            {
                MessageBox.Show("Thêm thành công!");
                LoadTaiKhoan();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }




        private void btnSua_Click(object sender, EventArgs e)
        {
            int maTaiKhoan = int.Parse(txtMaTaiKhoan.Text);
            TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO(maTaiKhoan, txtTenDangNhap.Text, txtMatKhau.Text, txtSoDienThoai.Text, cboQuyen.SelectedValue.ToString());
            if (TaiKhoanBus.updateTaiKhoan(taiKhoanDTO))
            {
                MessageBox.Show("Sửa thành công!");
                LoadTaiKhoan();
                txtMaTaiKhoan.Text = "";
                txtTenDangNhap.Text = "";
                txtMatKhau.Text = "";
                txtSoDienThoai.Text = "";
               
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaTaiKhoan.Text, out int maTaiKhoan))
            {
                TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO(maTaiKhoan, "", "", "", "");
                if (TaiKhoanBus.deleteTaiKhoan(taiKhoanDTO))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadTaiKhoan();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }
    }
}
