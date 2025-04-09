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
        DonHangBUS donHangBUS = new DonHangBUS();
        public frmDonHang()
        {
            InitializeComponent();
            LoadDonHangList();
        }
        private void LoadDonHangList()
        {
            dtgvDanhSachDonHang.DataSource = donHangBUS.getDonHang();
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {

        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (donHangBUS.deleteDonHang(txtMaDonHang.Text))
            {
                MessageBox.Show("Hủy đơn hàng thành công!");
                LoadDonHangList();
                txtMaDonHang.Clear();
                txtKhachHang.Clear();
            }
            else
            {
                MessageBox.Show("Hủy đơn hàng thất bại!");
            }
        }

        private void dtgvDanhSachDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvDanhSachDonHang.Rows[e.RowIndex];
                txtMaDonHang.Text = row.Cells["MaDonHang"].Value.ToString();
                txtKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
            }
        }
    }
}
