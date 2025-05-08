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
using BUS;
using System.Data.SqlClient;
using DAL;

namespace DoAn1
{
    public partial class frmDonHang : Form
    {
        KhachHangBUS khachHangBUS = new KhachHangBUS();
        DonHangBUS donHangBUS = new DonHangBUS();
        
        private bool isShowingDonHangList = true; // Theo dõi chế độ hiển thị

        public frmDonHang()
        {
            InitializeComponent();
            LoadDonHangList();
        }

        private void LoadDonHangList()
        {
            dtgvDanhSachDonHang.DataSource = donHangBUS.getDonHang();
            isShowingDonHangList = true; // Đang hiển thị danh sách đơn hàng
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmHoaDon f = new frmHoaDon(txtMaDonHang.Text);
            f.ShowDialog();
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (donHangBUS.deleteDonHang(txtMaDonHang.Text))
                {
                    khachHangBUS.DeleteKH(txtMaDonHang.Text);
                    MessageBox.Show("Hủy đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDonHangList();
                    txtMaDonHang.Clear();
                    txtKhachHang.Clear();
                }
                else
                {
                    MessageBox.Show("Hủy đơn hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hủy đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgvDanhSachDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvDanhSachDonHang.Rows[e.RowIndex];

                // Chỉ truy cập cột MaDonHang khi đang hiển thị danh sách đơn hàng
                if (isShowingDonHangList)
                {
                    txtMaDonHang.Text = row.Cells["MaDonHang"].Value.ToString();
                    txtKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                }
            }
        }

        
    }
}