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
        DonHangBUS donHangBUS = new DonHangBUS();
        ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();
        MonAnBUS monAnBUS = new MonAnBUS();
        private List<ListViewItem> cartItems;
        private List<ChiTietDonHangDTO> reOrderItems; // Danh sách chi tiết đơn hàng khi đặt lại

        // Constructor mặc định
        public frmThongTinNhanHang()
        {
            InitializeComponent();
            this.cartItems = null;
            this.reOrderItems = null;
        }

        // Constructor cho đặt hàng từ frmDatHang
        public frmThongTinNhanHang(List<ListViewItem> cartItems) : this()
        {
            this.cartItems = cartItems;
        }

        // Constructor cho đặt lại đơn hàng từ frmLichSuDatHang
        public frmThongTinNhanHang(List<ChiTietDonHangDTO> reOrderItems) : this()
        {
            this.reOrderItems = reOrderItems;
        }

        // Thuộc tính công khai để lấy thông tin khách hàng
        public string TenNguoiNhan => txtTenNguoiNhan.Text.Trim();
        public string SoDienThoai => txtSoDienThoai.Text.Trim();
        public string DiaChi => txtDiaChiNhanHang.Text.Trim();

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

            string maKhachHang = GenerateID();
            string maDonHang = GenerateID();
            DateTime ngayDat = DateTime.Now;

            var khachHangDTO = new KhachHangDTO(maKhachHang, tenNguoiNhan, diaChi, soDienThoai);
            var donHangDTO = new DonHangDTO(maDonHang, maKhachHang, ngayDat);

            bool khachHangInserted = khachHangBUS.insertKhachHang(khachHangDTO);
            bool donHangInserted = donHangBUS.insertDonHang(donHangDTO);

            if (khachHangInserted && donHangInserted)
            {
                bool allDetailsInserted = true;

                // Xử lý đặt hàng từ frmDatHang (cartItems)
                if (cartItems != null && cartItems.Any())
                {
                    foreach (var item in cartItems)
                    {
                        string tenMonAn = item.SubItems[0].Text;
                        float giaBan;
                        if (!float.TryParse(item.SubItems[1].Text, out giaBan))
                        {
                            MessageBox.Show($"Giá bán không hợp lệ cho món {tenMonAn}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            allDetailsInserted = false;
                            break;
                        }
                        int soLuong;
                        if (!int.TryParse(item.SubItems[2].Text, out soLuong))
                        {
                            MessageBox.Show($"Số lượng không hợp lệ cho món {tenMonAn}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            allDetailsInserted = false;
                            break;
                        }

                        string maMonAn = monAnBUS.GetMaMonAnByTen(tenMonAn);
                        if (string.IsNullOrEmpty(maMonAn))
                        {
                            MessageBox.Show($"Không tìm thấy mã món ăn cho {tenMonAn}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            allDetailsInserted = false;
                            break;
                        }

                        ChiTietDonHangDTO ctdh = new ChiTietDonHangDTO(
                            maDonHang,
                            maMonAn,
                            soLuong,
                            giaBan * soLuong,
                            ngayDat
                        );

                        if (!chiTietDonHangBUS.ThemChiTietDonHang(ctdh))
                        {
                            allDetailsInserted = false;
                            break;
                        }
                    }
                }
                // Xử lý đặt lại đơn hàng từ frmLichSuDatHang (reOrderItems)
                else if (reOrderItems != null && reOrderItems.Any())
                {
                    foreach (var ctdh in reOrderItems)
                    {
                        ctdh.MaDonHang = maDonHang; // Cập nhật MaDonHang mới
                        ctdh.ThoiGianDat = ngayDat; // Cập nhật thời gian đặt mới
                        if (!chiTietDonHangBUS.ThemChiTietDonHang(ctdh))
                        {
                            allDetailsInserted = false;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có chi tiết đơn hàng để lưu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    allDetailsInserted = false;
                }

                if (allDetailsInserted)
                {
                    MessageBox.Show("Đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt DialogResult để báo hiệu thành công
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu chi tiết đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lưu khách hàng hoặc đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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