using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace DoAn1
{
    public partial class frmLichSuDatHang : Form
    {
        LichSuDatHangBUS lichSuDatHangBUS = new LichSuDatHangBUS();
        DonHangBUS donHangBUS = new DonHangBUS();
        KhachHangBUS khachHangBUS = new KhachHangBUS();
        ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();

        public frmLichSuDatHang()
        {
            InitializeComponent();
            LoadLichSuDatHang();
        }

        private void LoadLichSuDatHang()
        {
            dtgvLichSuDatHang.DataSource = lichSuDatHangBUS.getAllLichSuDatHang();
            dtgvLichSuDatHang.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
            dtgvLichSuDatHang.Columns["NgayDat"].HeaderText = "Ngày Đặt";
            dtgvLichSuDatHang.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            dtgvLichSuDatHang.Columns["MaMonAn"].HeaderText = "Mã Món Ăn";
            dtgvLichSuDatHang.Columns["TenMonAn"].HeaderText = "Tên Món Ăn";
            dtgvLichSuDatHang.Columns["SoLuong"].HeaderText = "Số Lượng";
            dtgvLichSuDatHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dtgvLichSuDatHang.Columns["ThoiGianDat"].HeaderText = "Thời Gian Đặt";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadLichSuDatHang();
                return;
            }

            DataTable searchResult = lichSuDatHangBUS.getAllLichSuDatHang();
            DataView dv = searchResult.DefaultView;
            dv.RowFilter = $"MaDonHang LIKE '%{keyword}%' OR TenMonAn LIKE '%{keyword}%'";

            if (dv.Count > 0)
            {
                dtgvLichSuDatHang.DataSource = dv.ToTable();
            }
            else
            {
                MessageBox.Show($"Không tìm thấy đơn hàng với từ khóa '{keyword}'!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLichSuDatHang();
            }
        }

        private void btnXoaLichSu_Click(object sender, EventArgs e)
        {
            string maDonHang = txtMaDonHang.Text.Trim();
            if (string.IsNullOrEmpty(maDonHang))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa lịch sử đơn hàng {maDonHang}?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (lichSuDatHangBUS.deleteLichSuDatHang(maDonHang))
                {
                    MessageBox.Show("Xóa lịch sử đơn hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLichSuDatHang();
                    txtMaDonHang.Clear();
                }
                else
                {
                    MessageBox.Show("Xóa lịch sử đơn hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            string maDonHang = txtMaDonHang.Text.Trim();
            if (string.IsNullOrEmpty(maDonHang))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để đặt lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy chi tiết đơn hàng
            DataTable orderDetails = lichSuDatHangBUS.getAllLichSuDatHang();
            DataRow[] selectedOrder = orderDetails.Select($"MaDonHang = '{maDonHang}'");

            if (selectedOrder.Length == 0)
            {
                MessageBox.Show($"Không tìm thấy chi tiết đơn hàng cho mã {maDonHang}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo danh sách chi tiết đơn hàng để truyền vào frmThongTinNhanHang
            List<ChiTietDonHangDTO> reOrderItems = new List<ChiTietDonHangDTO>();
            foreach (DataRow row in selectedOrder)
            {
                double thanhTien;
                if (row["ThanhTien"] == DBNull.Value || string.IsNullOrEmpty(row["ThanhTien"].ToString()))
                {
                    MessageBox.Show($"Giá trị Thành Tiền không hợp lệ hoặc rỗng cho món {row["MaMonAn"]}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!double.TryParse(row["ThanhTien"].ToString(), out thanhTien))
                {
                    MessageBox.Show($"Không thể chuyển đổi giá trị Thành Tiền cho món {row["MaMonAn"]}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reOrderItems.Add(new ChiTietDonHangDTO
                {
                    MaDonHang = maDonHang, // Sẽ được cập nhật trong frmThongTinNhanHang
                    MaMonAn = row["MaMonAn"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    ThanhTien = (float)thanhTien,
                    ThoiGianDat = DateTime.Now
                });
            }

            if (reOrderItems.Count == 0)
            {
                MessageBox.Show("Không có chi tiết đơn hàng nào được chọn để đặt lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gọi frmThongTinNhanHang với danh sách chi tiết đơn hàng
            frmThongTinNhanHang f = new frmThongTinNhanHang(reOrderItems);
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy thông tin khách hàng từ thuộc tính công khai
                    string tenNguoiNhan = f.TenNguoiNhan;
                    string soDienThoai = f.SoDienThoai;
                    string diaChi = f.DiaChi;

                    // Tạo mã mới
                    string newMaKhachHang = GenerateID();
                    string newMaDonHang = GenerateID();
                    DateTime ngayDat = DateTime.Now;

                    // Thêm khách hàng mới
                    KhachHangDTO khachHangDTO = new KhachHangDTO(newMaKhachHang, tenNguoiNhan, diaChi, soDienThoai);
                    if (!khachHangBUS.insertKhachHang(khachHangDTO))
                    {
                        throw new Exception("Không thể thêm khách hàng mới.");
                    }

                    // Thêm đơn hàng mới
                    DonHangDTO donHangDTO = new DonHangDTO(newMaDonHang, newMaKhachHang, ngayDat);
                    if (!donHangBUS.insertDonHang(donHangDTO))
                    {
                        throw new Exception("Không thể thêm đơn hàng mới.");
                    }

                    // Thêm chi tiết đơn hàng (đã được xử lý trong frmThongTinNhanHang)
                    MessageBox.Show("Đặt lại đơn hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLichSuDatHang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đặt lại đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtgvLichSuDatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvLichSuDatHang.Rows[e.RowIndex];
                txtMaDonHang.Text = row.Cells["MaDonHang"].Value.ToString();
            }
        }

        private string GenerateID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}