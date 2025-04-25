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
        KhachHangBUS khachHangBUS = new KhachHangBUS();
        DonHangBUS DonHangBUS = new DonHangBUS();
        LichSuDatHangBUS LichSuDatHangBUS = new LichSuDatHangBUS();
        public frmThongTinNhanHang()
        {
            InitializeComponent();
        }

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
                MessageBox.Show("Đặt hàng thất bại!. Vui lòng kiểm tra lại thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo mã khách hàng và đơn hàng
            string maKhachHang = GenerateID();
            string maDonHang = GenerateID();
            DateTime ngayDat = DateTime.Now;

            // Tạo đối tượng và lưu vào DB
            var khachHangDTO = new KhachHangDTO(maKhachHang, tenNguoiNhan, diaChi, soDienThoai);
            var donHangDTO = new DonHangDTO(maDonHang, maKhachHang, ngayDat);

            khachHangBUS.insertKhachHang(khachHangDTO);
            DonHangBUS.insertDonHang(donHangDTO);

            MessageBox.Show("Đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
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
