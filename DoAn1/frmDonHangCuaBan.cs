using BUS;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn1
{
    public partial class frmDonHangCuaBan : Form
    {
        DonHangBUS donHangBUS = new DonHangBUS();
        ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();
        MonAnBUS monAnBUS = new MonAnBUS(); // Thêm để lấy danh sách món ăn

        private string maKhachHang;

        private string quyenNguoiDung;
        private List<ChiTietDonHangDTO> chiTietDonHang = new List<ChiTietDonHangDTO>(); // Lưu danh sách chi tiết đơn hàng tạm thời

        public frmDonHangCuaBan(string quyenNguoiDung, string maKhachHang)
        {
            InitializeComponent();

            this.quyenNguoiDung = quyenNguoiDung;
            this.maKhachHang = maKhachHang; // Lưu maKhachHang

            SetupDataGridView();
            SetupListView();
            LoadMonAn();
            LoadLoaiMon();
            LoadDonHangTheoNguoiDung();
        }

        private void LoadDonHangTheoNguoiDung()
        {
            DataTable dt = donHangBUS.getDonHang();
            DataView dv = dt.DefaultView;

            if (quyenNguoiDung != "Quản trị viên" && !string.IsNullOrEmpty(maKhachHang))
            {
                dv.RowFilter = $"MaKhachHang = '{maKhachHang}'";
            }

            dtgvDonHang.DataSource = dv.ToTable();

            // Đặt tên cột cho DataGridView
            dtgvDonHang.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
            dtgvDonHang.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            dtgvDonHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dtgvDonHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            
            dtgvDonHang.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dtgvDonHang.Columns.Contains("MaKhachHang"))
                dtgvDonHang.Columns["MaKhachHang"].Visible = false;
        }

        private void LoadDonHang()
        {
            DataTable dt = donHangBUS.getDonHang();
            if (!string.IsNullOrEmpty(maKhachHang))
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"MaKhachHang = '{maKhachHang}'";
                dtgvDonHang.DataSource = dv.ToTable();
            }
            else
            {
                dtgvDonHang.DataSource = dt;
            }

            dtgvDonHang.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
            dtgvDonHang.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dtgvDonHang.Columns.Contains("MaKhachHang"))
                dtgvDonHang.Columns["MaKhachHang"].Visible = false;
        }

        private void SetupDataGridView()
        {
            dtgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvDonHang.MultiSelect = false;
        }

        private void LoadMonAn()
        {
            dtgvMonAn.DataSource = monAnBUS.getAllMonAn();
        }

        private void LoadLoaiMon()
        {
            cboLoaiMon.DataSource = monAnBUS.getMonAnByMaLoai();
            cboLoaiMon.DisplayMember = "TenLoaiMon";
            cboLoaiMon.ValueMember = "MaLoaiMon";
            DataTable dtLoaiMon = monAnBUS.getMonAnByMaLoai();
            DataRow newRow = dtLoaiMon.NewRow();
            newRow["MaLoaiMon"] = DBNull.Value;
            newRow["TenLoaiMon"] = "--Tất cả--";
            dtLoaiMon.Rows.InsertAt(newRow, 0);
            cboLoaiMon.DataSource = dtLoaiMon;
            dtgvMonAn.Columns["MoTa"].Width = 210;
        }

        private void SetupListView()
        {
            lsvDonHang.View = View.Details;
            lsvDonHang.FullRowSelect = true;
            lsvDonHang.Columns.Clear();
            lsvDonHang.Columns.Add("Tên Món Ăn", 150);
            lsvDonHang.Columns.Add("Giá Bán", 100);
            lsvDonHang.Columns.Add("Số Lượng", 80);
            lsvDonHang.Columns.Add("Thành Tiền", 100);
        }

        private void HienThiChiTietDonHang(string maDonHang)
        {
            lsvDonHang.Items.Clear();
            chiTietDonHang.Clear();

            DataTable dt = chiTietDonHangBUS.getChiTietDonHangByMaDonHang(maDonHang);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Đơn hàng này chưa có món ăn nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblTongTien.Text = "0 VNĐ";
                return;
            }

            double tongTien = 0;

            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["TenMonAn"].ToString());
                item.SubItems.Add(Convert.ToDouble(row["GiaBan"]).ToString("N0"));
                item.SubItems.Add(row["SoLuong"].ToString());
                item.SubItems.Add(Convert.ToDouble(row["ThanhTien"]).ToString("N0"));
                lsvDonHang.Items.Add(item);

                ChiTietDonHangDTO chiTiet = new ChiTietDonHangDTO
                {
                    MaDonHang = maDonHang,
                    MaMonAn = row["MaMonAn"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    ThanhTien = (float)Convert.ToDouble(row["ThanhTien"]),
                    ThoiGianDat = Convert.ToDateTime(row["ThoiGianDat"])
                };
                chiTietDonHang.Add(chiTiet);

                tongTien += Convert.ToDouble(row["ThanhTien"]);
            }

            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void dtgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dtgvDonHang.Rows[e.RowIndex].Selected = true; // Đảm bảo hàng được chọn
                DataGridViewRow row = dtgvDonHang.Rows[e.RowIndex];
                string maDonHang = row.Cells["MaDonHang"].Value.ToString();
                txtMaDonHang.Text = maDonHang;
                HienThiChiTietDonHang(maDonHang);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Xác nhận nhận hàng
        {
            if (dtgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xác nhận nhận hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dtgvDonHang.SelectedRows[0];
            string maDonHang = row.Cells["MaDonHang"].Value.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Chưa xác nhận";

            if (trangThai == "Chưa xác nhận")
            {
                MessageBox.Show("Đơn hàng của bạn chưa được xác nhận, không thể chuyển sang trạng thái đã giao!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThai == "Đã giao")
            {
                MessageBox.Show("Đơn hàng đã được xác nhận giao thành công trước đó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc đã nhận được đơn hàng {maDonHang}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (donHangBUS.updateTrangThaiDonHang(maDonHang, "Đã giao"))
                {
                    MessageBox.Show("Xác nhận nhận hàng thành công! Trạng thái đơn hàng đã được cập nhật thành 'Đã giao'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDonHangTheoNguoiDung();
                }
                else
                {
                    MessageBox.Show("Cập nhật trạng thái đơn hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            if (dtgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lsvDonHang.Items.Count == 0)
            {
                MessageBox.Show("Danh sách món ăn trống, không thể chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDonHang = txtMaDonHang.Text;
            string trangThai = dtgvDonHang.SelectedRows[0].Cells["TrangThai"].Value?.ToString() ?? "Chưa xác nhận";

            if (trangThai == "Đã xác nhận" || trangThai == "Đã giao")
            {
                MessageBox.Show("Đơn hàng của bạn đã được xác nhận và không thể thay đổi. Nếu muốn thay đổi, hãy liên hệ với quán qua số điện thoại 0377284513.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Xóa chi tiết đơn hàng cũ
                if (!chiTietDonHangBUS.deleteChiTietDonHangByMaDonHang(maDonHang))
                {
                    MessageBox.Show("Lỗi khi xóa chi tiết đơn hàng cũ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thêm lại chi tiết đơn hàng mới
                foreach (var chiTiet in chiTietDonHang)
                {
                    if (string.IsNullOrEmpty(chiTiet.MaMonAn))
                    {
                        MessageBox.Show("Mã món ăn không hợp lệ! Vui lòng kiểm tra lại dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool thanhCong = chiTietDonHangBUS.ThemChiTietDonHang(chiTiet);
                    if (!thanhCong)
                    {
                        MessageBox.Show($"Lỗi khi lưu chi tiết đơn hàng cho món {chiTiet.MaMonAn}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Chỉnh sửa đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại dữ liệu giao diện
                LoadDonHangTheoNguoiDung();
                lsvDonHang.Items.Clear();
                chiTietDonHang.Clear();
                lblTongTien.Text = "0 VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chỉnh sửa đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dtgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dtgvDonHang.SelectedRows[0];
            string maDonHang = row.Cells["MaDonHang"].Value.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Chưa xác nhận";

            if (trangThai == "Đã xác nhận" || trangThai == "Đã giao")
            {
                MessageBox.Show("Đơn hàng của bạn đã được xác nhận không thể thay đổi nếu vẫn muốn thay đổi thì hãy liên hệ với quán qua số điện thoại 0377284513",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn hủy đơn hàng {maDonHang}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (donHangBUS.deleteDonHang(maDonHang))
                {
                    MessageBox.Show("Hủy đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDonHangTheoNguoiDung();
                    lsvDonHang.Items.Clear();
                    lblTongTien.Text = "0 VNĐ";
                }
                else
                {
                    MessageBox.Show("Hủy đơn hàng thất bại! Vui lòng kiểm tra lại dữ liệu hoặc liên hệ quản trị viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyworld = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(keyworld))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = monAnBUS.searchMonAn(keyworld);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy món ăn nào với tên đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtgvMonAn.DataSource = null;
            }
            else
            {
                dtgvMonAn.DataSource = dt;
            }
        }

        private void cboLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiMon.SelectedValue != null)
            {
                string maLoaiMon = cboLoaiMon.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(maLoaiMon) && maLoaiMon != "System.DBNull")
                {
                    dtgvMonAn.DataSource = monAnBUS.getMonAnByLoai(maLoaiMon);
                }
                else
                {
                    LoadMonAn();
                }
            }
        }

        private void frmDonHangCuaBan_Load(object sender, EventArgs e)
        {
            LoadMonAn();
        }

        private void dtgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvMonAn.Rows[e.RowIndex];
                lblTenMon.Text = row.Cells["TenMonAn"].Value?.ToString() ?? "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (dtgvDonHang.SelectedRows.Count == 0 || string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtgvMonAn.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một món ăn để thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDonHang = txtMaDonHang.Text;
            DataGridViewRow row = dtgvMonAn.CurrentRow;
            string tenMon = row.Cells["TenMonAn"].Value?.ToString() ?? "";
            string maMonAn = row.Cells["MaMonAn"].Value?.ToString() ?? "";
            float giaBan = Convert.ToSingle(row.Cells["GiaBan"].Value);
            int soLuong = (int)nmSoLuong.Value;

            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool daCoTrongGio = false;
            foreach (ListViewItem item in lsvDonHang.Items)
            {
                if (item.SubItems[0].Text == tenMon)
                {
                    int soLuongCu = int.Parse(item.SubItems[2].Text);
                    int soLuongMoi = soLuongCu + soLuong;
                    item.SubItems[2].Text = soLuongMoi.ToString();
                    item.SubItems[3].Text = (giaBan * soLuongMoi).ToString("N0");

                    var chiTiet = chiTietDonHang.FirstOrDefault(x => x.MaMonAn == maMonAn);
                    if (chiTiet != null)
                    {
                        chiTiet.SoLuong = soLuongMoi;
                        chiTiet.ThanhTien = giaBan * soLuongMoi;
                    }

                    daCoTrongGio = true;
                    break;
                }
            }

            if (!daCoTrongGio)
            {
                ListViewItem item = new ListViewItem(tenMon);
                item.SubItems.Add(giaBan.ToString("N0"));
                item.SubItems.Add(soLuong.ToString());
                item.SubItems.Add((giaBan * soLuong).ToString("N0"));
                lsvDonHang.Items.Add(item);

                ChiTietDonHangDTO chiTiet = new ChiTietDonHangDTO
                {
                    MaDonHang = maDonHang,
                    MaMonAn = maMonAn,
                    SoLuong = soLuong,
                    ThanhTien = giaBan * soLuong,
                    ThoiGianDat = DateTime.Now
                };
                chiTietDonHang.Add(chiTiet);
                if (!chiTietDonHangBUS.ThemChiTietDonHang(chiTiet))
                {
                    MessageBox.Show($"Lỗi khi lưu chi tiết đơn hàng cho món {maMonAn}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Cập nhật tổng tiền
            double tongTien = 0;
            foreach (ListViewItem item in lsvDonHang.Items)
            {
                double thanhTien = double.Parse(item.SubItems[3].Text.Replace(",", ""));
                tongTien += thanhTien;
            }
            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";

            MessageBox.Show("Thêm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (lsvDonHang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một món ăn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = lsvDonHang.SelectedItems[0];
            string tenMon = selectedItem.SubItems[0].Text;
            int soLuongHienTai = int.Parse(selectedItem.SubItems[2].Text);
            float giaBan = float.Parse(selectedItem.SubItems[1].Text.Replace(",", ""));

            // Hỏi số lượng muốn xóa
            int soLuongXoa = (int)nmSoLuong.Value;
            if (soLuongXoa <= 0)
            {
                MessageBox.Show("Số lượng xóa phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (soLuongXoa > soLuongHienTai)
            {
                MessageBox.Show("Số lượng xóa không được lớn hơn số lượng hiện tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa {soLuongXoa} món {tenMon}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int soLuongMoi = soLuongHienTai - soLuongXoa;

                // Lấy MaMonAn để cập nhật chiTietDonHang
                string maMonAn = monAnBUS.GetMaMonAnByTen(tenMon);
                var chiTiet = chiTietDonHang.FirstOrDefault(x => x.MaMonAn == maMonAn);

                if (soLuongMoi <= 0)
                {
                    // Xóa hoàn toàn món nếu số lượng về 0
                    lsvDonHang.Items.Remove(selectedItem);
                    if (chiTiet != null)
                    {
                        chiTietDonHang.Remove(chiTiet);
                    }
                }
                else
                {
                    // Cập nhật số lượng và thành tiền
                    selectedItem.SubItems[2].Text = soLuongMoi.ToString();
                    selectedItem.SubItems[3].Text = (giaBan * soLuongMoi).ToString("N0");
                    if (chiTiet != null)
                    {
                        chiTiet.SoLuong = soLuongMoi;
                        chiTiet.ThanhTien = giaBan * soLuongMoi;
                    }
                }

                // Cập nhật tổng tiền
                double tongTien = 0;
                foreach (ListViewItem item in lsvDonHang.Items)
                {
                    double thanhTien = double.Parse(item.SubItems[3].Text.Replace(",", ""));
                    tongTien += thanhTien;
                }
                lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";

                MessageBox.Show("Xóa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}