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
        ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();
        private string quyenNguoiDung;
        private string maKhachHang;

        public frmLichSuDatHang(string quyenNguoiDung, string maKhachHang)
        {
            InitializeComponent();

            this.quyenNguoiDung = quyenNguoiDung;
            this.maKhachHang = maKhachHang;

            if (quyenNguoiDung != "Quản trị viên" && string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Tài khoản của bạn chưa được liên kết với thông tin khách hàng. Vui lòng đặt hàng trước để xem lịch sử đặt hàng.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadLichSuTheoNguoiDung();
            SetupListView();
        }

        private void LoadLichSuTheoNguoiDung()
        {
            DataTable dt;
            if (quyenNguoiDung != "Quản trị viên" && !string.IsNullOrEmpty(maKhachHang))
            {
                dt = lichSuDatHangBUS.getLichSuDatHangByMaKhachHang(maKhachHang);
            }
            else
            {
                dt = lichSuDatHangBUS.getAllLichSuDatHang();
            }

            // Nhóm các bản ghi theo MaDonHang và lấy bản ghi đầu tiên cho mỗi MaDonHang
            var distinctRows = dt.AsEnumerable()
                .GroupBy(row => row.Field<string>("MaDonHang"))
                .Select(g => g.First());

            // Tạo DataTable mới chỉ chứa các cột cần thiết
            DataTable distinctDt = new DataTable();
            distinctDt.Columns.Add("MaDonHang", typeof(string));
            distinctDt.Columns.Add("NgayDat", typeof(DateTime));
            distinctDt.Columns.Add("ThoiGianDat", typeof(DateTime));

            foreach (var row in distinctRows)
            {
                distinctDt.Rows.Add(
                    row.Field<string>("MaDonHang"),
                    row.Field<DateTime>("NgayDat"),
                    row.Field<DateTime>("ThoiGianDat")
                );
            }

            dtgvLichSuDatHang.DataSource = distinctDt;

            // Đặt tên cột cho DataGridView
            dtgvLichSuDatHang.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
            dtgvLichSuDatHang.Columns["NgayDat"].HeaderText = "Ngày Đặt";
            dtgvLichSuDatHang.Columns["ThoiGianDat"].HeaderText = "Thời Gian Đặt";
        }

        private void SetupListView()
        {
            lsvThongTinDonHang.View = View.Details;
            lsvThongTinDonHang.FullRowSelect = true;
            lsvThongTinDonHang.Columns.Add("Tên Món Ăn", 100);
            lsvThongTinDonHang.Columns.Add("Giá Bán", 90);
            lsvThongTinDonHang.Columns.Add("Số Lượng", 70);
            lsvThongTinDonHang.Columns.Add("Thành Tiền", 90);
        }

        private void HienThiChiTietDonHang(string maDonHang)
        {
            lsvThongTinDonHang.Items.Clear();
            DataTable dt = lichSuDatHangBUS.getLichSuDatHangByMaDonHang(maDonHang);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy chi tiết đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                float thanhTien = Convert.ToSingle(row["ThanhTien"]);
                float soLuong = Convert.ToSingle(row["SoLuong"]);
                float giaBan = thanhTien / soLuong;

                ListViewItem item = new ListViewItem(row["TenMonAn"].ToString());
                item.SubItems.Add(giaBan.ToString("N0"));
                item.SubItems.Add(soLuong.ToString());
                item.SubItems.Add(thanhTien.ToString("N0"));
                lsvThongTinDonHang.Items.Add(item);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadLichSuTheoNguoiDung();
                return;
            }

            DataTable dt;
            if (quyenNguoiDung != "Quản trị viên" && !string.IsNullOrEmpty(maKhachHang))
            {
                dt = lichSuDatHangBUS.getLichSuDatHangByMaKhachHang(maKhachHang);
            }
            else
            {
                dt = lichSuDatHangBUS.getAllLichSuDatHang();
            }

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"MaDonHang LIKE '%{keyword}%' OR TenMonAn LIKE '%{keyword}%'";

            // Nhóm các bản ghi theo MaDonHang và lấy bản ghi đầu tiên khi tìm kiếm
            var distinctRows = dv.ToTable().AsEnumerable()
                .GroupBy(row => row.Field<string>("MaDonHang"))
                .Select(g => g.First());

            // Tạo DataTable mới chỉ chứa các cột cần thiết
            DataTable distinctDt = new DataTable();
            distinctDt.Columns.Add("MaDonHang", typeof(string));
            distinctDt.Columns.Add("NgayDat", typeof(DateTime));
            distinctDt.Columns.Add("ThoiGianDat", typeof(DateTime));

            foreach (var row in distinctRows)
            {
                distinctDt.Rows.Add(
                    row.Field<string>("MaDonHang"),
                    row.Field<DateTime>("NgayDat"),
                    row.Field<DateTime>("ThoiGianDat")
                );
            }

            dtgvLichSuDatHang.DataSource = distinctDt;

            if (distinctDt.Rows.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy đơn hàng với từ khóa '{keyword}'!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLichSuTheoNguoiDung();
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
                if (lichSuDatHangBUS.deleteLichSuDatHangByMaDonHang(maDonHang))
                {
                    MessageBox.Show("Xóa lịch sử đơn hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLichSuTheoNguoiDung();
                    txtMaDonHang.Clear();
                    lsvThongTinDonHang.Items.Clear();
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

            DataTable orderDetails = lichSuDatHangBUS.getLichSuDatHangByMaDonHang(maDonHang);

            if (orderDetails.Rows.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy chi tiết đơn hàng cho mã {maDonHang}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<ChiTietDonHangDTO> reOrderItems = new List<ChiTietDonHangDTO>();
            foreach (DataRow row in orderDetails.Rows)
            {
                double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                reOrderItems.Add(new ChiTietDonHangDTO
                {
                    MaDonHang = maDonHang,
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

            frmThongTinNhanHang f = new frmThongTinNhanHang(reOrderItems, maKhachHang); // Truyền maKhachHang hiện tại
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadLichSuTheoNguoiDung(); // Giữ bộ lọc theo người dùng
                txtMaDonHang.Clear();
                lsvThongTinDonHang.Items.Clear();
            }
        }

        private void dtgvLichSuDatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvLichSuDatHang.Rows[e.RowIndex];
                string maDonHang = row.Cells["MaDonHang"].Value.ToString();
                txtMaDonHang.Text = maDonHang;
                HienThiChiTietDonHang(maDonHang);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}