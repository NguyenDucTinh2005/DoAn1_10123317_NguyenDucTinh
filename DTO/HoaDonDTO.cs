using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDonDTO
    {

        string maDonHang;
        DateTime ngayDat;
        string tenKhachHang;
        string soDienThoai;
        string diaChi;
        string tenMonAn;
        int soLuong;
        float giaBan;
        float thanhTien;
        public string MaDonHang { get => maDonHang; set => maDonHang = value; }
        public DateTime NgayDat { get => ngayDat; set => ngayDat = value; }
        public string TenKhachHang { get => tenKhachHang; set => tenKhachHang = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string TenMonAn { get => tenMonAn; set => tenMonAn = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public float GiaBan { get => giaBan; set => giaBan = value; }
        public float ThanhTien { get => thanhTien; set => thanhTien = value; }

        public HoaDonDTO(string maDonHang, DateTime ngayDat, string tenKhachHang, string soDienThoai, string diaChi, string tenMonAn, int soLuong, float giaBan, float thanhTien)
        {
            this.MaDonHang = MaDonHang;
            this.NgayDat = NgayDat;
            this.TenKhachHang = TenKhachHang;
            this.SoDienThoai = SoDienThoai;
            this.DiaChi = DiaChi;
            this.TenMonAn = TenMonAn;
            this.SoLuong = SoLuong;
            this.GiaBan = GiaBan;
            this.ThanhTien = ThanhTien;
        }
        public HoaDonDTO() { }
        public HoaDonDTO(string tenMon, float gia, int soLuong, float thanhTien)
        {
            this.tenMonAn = tenMon;
            this.GiaBan = gia;
            this.SoLuong = soLuong;
            this.ThanhTien = thanhTien;
        }
    }

}
