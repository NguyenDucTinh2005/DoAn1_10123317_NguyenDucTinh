using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinNhanHangDTO
    {
        string maDonHang;
        string tenNguoiNhan;
        string soDienThoai;
        string diaChi;
        public ThongTinNhanHangDTO(string maDonHang, string tenNguoiNhan, string soDienThoai, string diaChi)
        {
            MaDonHang = maDonHang;
            TenNguoiNhan = tenNguoiNhan;
            SoDienThoai = soDienThoai;
            DiaChi = diaChi;
        }

        public string MaDonHang { get => maDonHang; set => maDonHang = value; }
        public string TenNguoiNhan { get => tenNguoiNhan; set => tenNguoiNhan = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}
