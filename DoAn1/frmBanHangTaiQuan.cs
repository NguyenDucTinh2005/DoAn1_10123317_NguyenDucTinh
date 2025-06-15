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
    public partial class frmBanHangTaiQuan : Form
    {
        private string selectedMonAn;
        private float selectedGia;
        private ListViewItem selectedItem;
        MonAnBUS gioHangBUS = new MonAnBUS();
        NguyenLieuBUS NguyenLieuBUS = new NguyenLieuBUS();

        public frmBanHangTaiQuan()
        {
            InitializeComponent();
            supperListDonHang();
            LoadMonAn();
            LoadLoaiMon();
            this.Load += frmBanHangTaiQuan_Load;
        }

        private void supperListDonHang()
        {
            lsvHoaDon.Clear();
            lsvHoaDon.View = View.Details;
            lsvHoaDon.FullRowSelect = true;
            lsvHoaDon.Columns.Add("Tên Món", 150);
            lsvHoaDon.Columns.Add("Giá", 100);
            lsvHoaDon.Columns.Add("Số Lượng", 100);
        }

        private void LoadMonAn()
        {
            dtgvDanhSachMon.DataSource = gioHangBUS.getAllMonAn();
        }

        private void LoadLoaiMon()
        {
            cboLoaiMon.DataSource = gioHangBUS.getMonAnByMaLoai();
            cboLoaiMon.DisplayMember = "TenLoaiMon";
            cboLoaiMon.ValueMember = "MaLoaiMon";
            DataTable dtLoaiMon = gioHangBUS.getMonAnByMaLoai();
            DataRow newRow = dtLoaiMon.NewRow();
            newRow["MaLoaiMon"] = DBNull.Value;
            newRow["TenLoaiMon"] = "--Tất cả--";
            dtLoaiMon.Rows.InsertAt(newRow, 0);
            cboLoaiMon.DataSource = dtLoaiMon;
            dtgvDanhSachMon.Columns["MoTa"].Width = 210;
        }

        private void AddMonAnToHoaDon(string tenMon, string gia, int soLuong)
        {
            bool daCoTrongGio = false;

            // Kiểm tra xem món ăn đã có trong giỏ hàng chưa
            foreach (ListViewItem item in lsvHoaDon.Items)
            {
                if (item.SubItems[0].Text == tenMon)
                {
                    // Nếu có, cộng dồn số lượng
                    int soLuongCu = int.Parse(item.SubItems[2].Text);
                    int soLuongMoi = soLuongCu + soLuong;
                    item.SubItems[2].Text = soLuongMoi.ToString();
                    item.SubItems[1].Text = (float.Parse(gia) * soLuongMoi).ToString("N0"); // Cập nhật giá
                    daCoTrongGio = true;
                    break;
                }
            }

            // Nếu chưa có, thêm mới
            if (!daCoTrongGio)
            {
                ListViewItem item = new ListViewItem(tenMon);
                item.SubItems.Add(float.Parse(gia).ToString("N0"));
                item.SubItems.Add(soLuong.ToString());
                lsvHoaDon.Items.Add(item);
            }

            UpdateTongTien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyworld = txtTimKiem.Text;
            DataTable dataTable = gioHangBUS.searchMonAn(keyworld);
            if (string.IsNullOrEmpty(keyworld))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn cần tìm kiếm");
            }
            else
            {
                dtgvDanhSachMon.DataSource = dataTable;
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("Không tìm thấy loại món '{0}'!", keyworld), "Thông báo");
                }
                else
                {
                    dtgvDanhSachMon.DataSource = dataTable;
                }
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            string tenMon = lblTenMon.Text;

            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Vui lòng chọn món ăn để thêm vào giỏ hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = (int)nmSoLuong.Value;

            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dtgvDanhSachMon.Rows)
            {
                if (row.Cells["TenMonAn"].Value.ToString() == tenMon)
                {
                    string gia = row.Cells["GiaBan"].Value.ToString();
                    AddMonAnToHoaDon(tenMon, gia, soLuong);
                    return;
                }
            }

            MessageBox.Show("Không tìm thấy món ăn trong danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dtgvDanhSachMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvDanhSachMon.Rows[e.RowIndex];
                lblTenMon.Text = row.Cells["TenMonAn"].Value.ToString();
                selectedMonAn = row.Cells["TenMonAn"].Value.ToString();
                selectedGia = Convert.ToSingle(row.Cells["GiaBan"].Value);
            }
        }

        private void cboLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiMon.SelectedValue != null)
            {
                string maLoaiMon = cboLoaiMon.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(maLoaiMon) && maLoaiMon != "System.DBNull")
                {
                    dtgvDanhSachMon.DataSource = gioHangBUS.getMonAnByLoai(maLoaiMon);
                }
                else
                {
                    LoadMonAn();
                }
            }
        }

        private void frmBanHangTaiQuan_Load(object sender, EventArgs e)
        {
            LoadMonAn();
        }

        private void lsvHoaDon_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                selectedItem = e.Item;
                nmSoLuong.Value = int.Parse(selectedItem.SubItems[2].Text);
                lblTenMon.Text = selectedItem.SubItems[0].Text;
            }
        }

        private void nmSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                int soLuongMoi = (int)nmSoLuong.Value;
                if (soLuongMoi > 0)
                {
                    selectedItem.SubItems[2].Text = soLuongMoi.ToString();
                    selectedItem.SubItems[1].Text = (selectedGia * soLuongMoi).ToString("N0");
                }
                else
                {
                    lsvHoaDon.Items.Remove(selectedItem);
                    selectedItem = null;
                }
                UpdateTongTien();
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lsvHoaDon.Items.Remove(selectedItem);
            selectedItem = null;
            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            double tongTien = 0;
            foreach (ListViewItem item in lsvHoaDon.Items)
            {
                if (!double.TryParse(item.SubItems[1].Text.Replace(",", ""), out double gia) ||
                    !int.TryParse(item.SubItems[2].Text, out int soLuong))
                {
                    MessageBox.Show("Dữ liệu trong giỏ hàng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tongTien += gia * soLuong;
            }
            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (lsvHoaDon.Items.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm món ăn vào giỏ hàng trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra nguyên liệu trước khi thanh toán
                ChiTietNguyenLieuBUS ctNguyenLieuBUS = new ChiTietNguyenLieuBUS();
                NguyenLieuBUS nguyenLieuBUS = new NguyenLieuBUS();

                foreach (ListViewItem item in lsvHoaDon.Items)
                {
                    string tenMon = item.SubItems[0].Text;
                    string maMonAn = gioHangBUS.GetMaMonAnByTen(tenMon);
                    int soLuong = int.Parse(item.SubItems[2].Text);

                    var dsNguyenLieu = ctNguyenLieuBUS.GetNguyenLieuTheoMonAn(maMonAn);
                    foreach (var ngl in dsNguyenLieu)
                    {
                        float tongCan = ngl.SoLuongCan * soLuong;
                        float tonKho = nguyenLieuBUS.LaySoLuongTon(ngl.MaNguyenLieu);
                        if (tonKho < tongCan)
                        {
                            MessageBox.Show($"Không đủ nguyên liệu: {ngl.MaNguyenLieu}. Cần: {tongCan}, tồn: {tonKho}", "Thiếu nguyên liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // Tạo mã đơn hàng
                string maDonHang = GenerateID();
                DateTime ngayDat = DateTime.Now;

                // Lưu đơn hàng vào bảng DonHang
                DonHangDTO donHangDTO = new DonHangDTO(maDonHang, null, ngayDat, "Đã xác nhận");
                DonHangBUS donHangBUS = new DonHangBUS();
                if (!donHangBUS.insertDonHang2(donHangDTO))
                {
                    MessageBox.Show("Lỗi khi lưu đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lưu chi tiết đơn hàng vào bảng ChiTietDonHang
                ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();
                foreach (ListViewItem item in lsvHoaDon.Items)
                {
                    string tenMon = item.SubItems[0].Text;
                    string maMonAn = gioHangBUS.GetMaMonAnByTen(tenMon);
                    if (string.IsNullOrEmpty(maMonAn))
                    {
                        MessageBox.Show($"Không tìm thấy mã món ăn cho món {tenMon}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!float.TryParse(item.SubItems[1].Text.Replace(",", ""), out float gia) ||
                        !int.TryParse(item.SubItems[2].Text, out int soLuong))
                    {
                        MessageBox.Show($"Dữ liệu không hợp lệ cho món {tenMon}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    float thanhTien = gia * soLuong;
                    DateTime thoiGianDat = DateTime.Now;

                    ChiTietDonHangDTO chiTietDTO = new ChiTietDonHangDTO
                    {
                        MaDonHang = maDonHang,
                        MaMonAn = maMonAn,
                        SoLuong = soLuong,
                        ThanhTien = thanhTien,
                        ThoiGianDat = thoiGianDat
                    };

                    if (!chiTietDonHangBUS.ThemChiTietDonHang(chiTietDTO))
                    {
                        MessageBox.Show($"Lỗi khi lưu chi tiết đơn hàng cho món {tenMon}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Trừ kho nguyên liệu
                foreach (ListViewItem item in lsvHoaDon.Items)
                {
                    string tenMon = item.SubItems[0].Text;
                    string maMonAn = gioHangBUS.GetMaMonAnByTen(tenMon);
                    int soLuong = int.Parse(item.SubItems[2].Text);

                    var dsNguyenLieu = ctNguyenLieuBUS.GetNguyenLieuTheoMonAn(maMonAn);
                    foreach (var ngl in dsNguyenLieu)
                    {
                        float soLuongTru = ngl.SoLuongCan * soLuong;
                        nguyenLieuBUS.TruSoLuong(ngl.MaNguyenLieu, soLuongTru);
                    }
                }

                // Hiển thị hóa đơn
                frmHoaDonTaiQuan frmHoaDonTQ = new frmHoaDonTaiQuan(maDonHang);
                frmHoaDonTQ.ShowDialog();

                // Xóa giỏ hàng
                lsvHoaDon.Items.Clear();
                UpdateTongTien();
                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string GenerateID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }

     
    }
}