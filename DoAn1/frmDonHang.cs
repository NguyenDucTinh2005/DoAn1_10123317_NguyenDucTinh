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
            SetupListView();
        }

        private void LoadDonHangList()
        {
            dtgvDanhSachDonHang.DataSource = donHangBUS.getDonHang();
            isShowingDonHangList = true; // Đang hiển thị danh sách đơn hàng
        }
        private void SetupListView()
        {
            lsvThongTinDonHang.View = View.Details;
            lsvThongTinDonHang.FullRowSelect = true;
            lsvThongTinDonHang.Columns.Clear();
            lsvThongTinDonHang.Columns.Add("Tên Món Ăn", 80);
            lsvThongTinDonHang.Columns.Add("Giá Bán", 80);
            lsvThongTinDonHang.Columns.Add("Số Lượng", 60);
            lsvThongTinDonHang.Columns.Add("Thành Tiền", 70);
        }
        private void HienThiChiTietDonHang(string maDonHang)
        {

            // Xóa nội dung cũ trong ListView
            lsvThongTinDonHang.Items.Clear();

            // Gọi ChiTietDonHangBUS để lấy dữ liệu
            ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();
            DataTable dt = chiTietDonHangBUS.getChiTietDonHangByMaDonHang(maDonHang);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy chi tiết đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            double tongTien = 0;
            // Duyệt qua từng hàng trong DataTable và thêm vào ListView
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["TenMonAn"].ToString());
                item.SubItems.Add(Convert.ToDouble(row["GiaBan"]).ToString("N0")); // Định dạng giá bán
                item.SubItems.Add(row["SoLuong"].ToString());
                item.SubItems.Add(Convert.ToDouble(row["ThanhTien"]).ToString("N0")); // Định dạng thành tiền
                lsvThongTinDonHang.Items.Add(item);
                tongTien += Convert.ToDouble(row["ThanhTien"]);
            }
            lblTongSoTien.Text = tongTien.ToString("N0") + " VNĐ";

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (donHangBUS.updateTrangThaiDonHang(txtMaDonHang.Text, "Đã xác nhận"))
            {
                MessageBox.Show("Xác nhận đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDonHangList();
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
                    txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                    txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                    HienThiChiTietDonHang(txtMaDonHang.Text);
                }
            }
        }


    }
}