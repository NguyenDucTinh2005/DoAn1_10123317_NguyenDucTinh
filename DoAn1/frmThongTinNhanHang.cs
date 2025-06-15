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
using DAL;
using System.Text.RegularExpressions;

namespace DoAn1
{
    public partial class frmThongTinNhanHang : Form
    {
        private List<ListViewItem> cartItems;
        private List<ChiTietDonHangDTO> reOrderItems;
        private string maKhachHang;
        private KhachHangBUS khachHangBUS = new KhachHangBUS();
        private DonHangBUS donHangBUS = new DonHangBUS();
        private ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();
        private MonAnBUS monAnBUS = new MonAnBUS();
        private LichSuDatHangBUS lichSuDatHangBUS = new LichSuDatHangBUS();
        private ThongTinNhanHangBUS thongTinNhanHangBUS = new ThongTinNhanHangBUS();
        private NguyenLieuBUS nguyenLieuBUS = new NguyenLieuBUS();
        private ChiTietNguyenLieuBUS chiTietNguyenLieuBUS = new ChiTietNguyenLieuBUS();

        public frmThongTinNhanHang(List<ListViewItem> cartItems, string maKhachHang)
        {
            InitializeComponent();
            this.cartItems = cartItems;
            this.maKhachHang = maKhachHang;
        }

        public frmThongTinNhanHang(List<ChiTietDonHangDTO> reOrderItems, string maKhachHang)
        {
            InitializeComponent();
            this.reOrderItems = reOrderItems;
            this.maKhachHang = maKhachHang;
        }

        public List<ListViewItem> CartItems => cartItems;
        public List<ChiTietDonHangDTO> ReOrderItems => reOrderItems;

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            string tenNguoiNhan = txtTenNguoiNhan.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChiNhanHang.Text.Trim();
            bool isValid = true;

            isValid &= ValidateField(txtTenNguoiNhan, khachHangBUS.kiemTraTenNguoiNhan(tenNguoiNhan), "Tên người nhận không được để trống.");
            isValid &= ValidateField(txtDiaChiNhanHang, khachHangBUS.kiemTraDiaChi(diaChi), "Địa chỉ không hợp lệ! Vui lòng kiểm tra lại.");
            isValid &= ValidateField(txtSoDienThoai, Regex.IsMatch(soDienThoai, @"^0\d{9}$"), "Số điện thoại không hợp lệ! Vui lòng kiểm tra lại.");

            if (!isValid)
            {
                MessageBox.Show("Đặt hàng thất bại! Vui lòng kiểm tra lại thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckNguyenLieu()) return;

            string maDonHang = GenerateID();
            DateTime ngayDat = DateTime.Now;
            var donHangDTO = new DonHangDTO(maDonHang, maKhachHang, ngayDat);
            bool isSuccess = true;

            if (!donHangBUS.insertDonHang(donHangDTO))
            {
                isSuccess = false;
            }
            else
            {
                var thongTinDTO = new ThongTinNhanHangDTO(maDonHang, tenNguoiNhan, soDienThoai, diaChi);
                if (!thongTinNhanHangBUS.InsertThongTin(thongTinDTO))
                {
                    donHangBUS.deleteDonHang(maDonHang);
                    isSuccess = false;
                }
                else
                {
                    isSuccess = InsertChiTietDonHang(maDonHang, ngayDat);
                }
            }

            if (isSuccess)
            {
                TruNguyenLieu();
                MessageBox.Show("Đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckNguyenLieu()
        {
            var items = cartItems ?? reOrderItems?.Select(ct => new ListViewItem(new[]
            {
                monAnBUS.GetTenMonAnByMa(ct.MaMonAn),
                ct.ThanhTien.ToString(),
                ct.SoLuong.ToString()
            })).ToList();

            if (items == null || !items.Any()) return false;

            foreach (var item in items)
            {
                string tenMonAn = item.SubItems[0].Text;
                int soLuong = int.Parse(item.SubItems[2].Text);
                string maMonAn = monAnBUS.GetMaMonAnByTen(tenMonAn);

                var danhSachNguyenLieu = chiTietNguyenLieuBUS.GetNguyenLieuTheoMonAn(maMonAn);
                foreach (var ngl in danhSachNguyenLieu)
                {
                    float tongCan = ngl.SoLuongCan * soLuong;
                    float tonKho = nguyenLieuBUS.LaySoLuongTon(ngl.MaNguyenLieu);
                    if (tonKho < tongCan)
                    {
                        MessageBox.Show($"Không đủ nguyên liệu: {ngl.MaNguyenLieu}. Cần: {tongCan}, tồn kho: {tonKho}", "Thiếu nguyên liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool InsertChiTietDonHang(string maDonHang, DateTime ngayDat)
        {
            bool success = true;
            var items = cartItems ?? reOrderItems?.Select(ct => new ListViewItem(new[]
            {
                monAnBUS.GetTenMonAnByMa(ct.MaMonAn),
                ct.ThanhTien.ToString(),
                ct.SoLuong.ToString()
            })).ToList();

            if (items == null || !items.Any()) return false;

            foreach (var item in items)
            {
                string tenMonAn = item.SubItems[0].Text;
                float giaBan = float.Parse(item.SubItems[1].Text);
                int soLuong = int.Parse(item.SubItems[2].Text);

                string maMonAn = monAnBUS.GetMaMonAnByTen(tenMonAn);
                if (string.IsNullOrEmpty(maMonAn)) return false;

                var ctdh = new ChiTietDonHangDTO(maDonHang, maMonAn, soLuong, giaBan, ngayDat);
                if (!chiTietDonHangBUS.ThemChiTietDonHang(ctdh)) return false;

                var lichSuDTO = new LichSuDatHangDTO(
                    GenerateID(), maDonHang, ngayDat, ngayDat,
                    maMonAn, tenMonAn, soLuong, giaBan
                );
                lichSuDatHangBUS.insertLichSuDatHang(lichSuDTO);
            }

            return success;
        }

        private void TruNguyenLieu()
        {
            var items = cartItems ?? reOrderItems?.Select(ct => new ListViewItem(new[]
            {
                monAnBUS.GetTenMonAnByMa(ct.MaMonAn),
                ct.ThanhTien.ToString(),
                ct.SoLuong.ToString()
            })).ToList();

            if (items == null) return;

            foreach (var item in items)
            {
                string tenMonAn = item.SubItems[0].Text;
                int soLuong = int.Parse(item.SubItems[2].Text);
                string maMonAn = monAnBUS.GetMaMonAnByTen(tenMonAn);

                var dsNguyenLieu = chiTietNguyenLieuBUS.GetNguyenLieuTheoMonAn(maMonAn);
                foreach (var ngl in dsNguyenLieu)
                {
                    float tongCan = ngl.SoLuongCan * soLuong;
                    nguyenLieuBUS.TruSoLuong(ngl.MaNguyenLieu, tongCan);
                }
            }
        }

        private bool ValidateField(Control control, bool condition, string errorMessage)
        {
            if (!condition)
            {
                errorProvider.SetError(control, errorMessage);
                return false;
            }
            errorProvider.SetError(control, string.Empty);
            return true;
        }

        private string GenerateID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}