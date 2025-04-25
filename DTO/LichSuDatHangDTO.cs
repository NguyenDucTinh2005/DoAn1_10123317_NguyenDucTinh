using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LichSuDatHangDTO
    {
        private string maDonHang;
        private DateTime ngayDat;
        private string maKhachHang;
        private string maMonAn;
        private string tenMonAn;
        private int soLuong;
        private double thanhTien;
        private DateTime thoiGianDat;

        public string MaDonHang { get => maDonHang; set => maDonHang = value; }
        public DateTime NgayDat { get => ngayDat; set => ngayDat = value; }
        public string MaKhachHang { get => maKhachHang; set => maKhachHang = value; }
        public string MaMonAn { get => maMonAn; set => maMonAn = value; }
        public string TenMonAn { get => tenMonAn; set => tenMonAn = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public double ThanhTien { get => thanhTien; set => thanhTien = value; }
        public DateTime ThoiGianDat { get => thoiGianDat; set => thoiGianDat = value; }

        public LichSuDatHangDTO(string maDonHang, DateTime ngayDat, string maKhachHang, string maMonAn, string tenMonAn, int soLuong, double thanhTien, DateTime thoiGianDat)
        {
            this.MaDonHang = maDonHang;
            this.NgayDat = ngayDat;
            this.MaKhachHang = maKhachHang;
            this.MaMonAn = maMonAn;
            this.TenMonAn = tenMonAn;
            this.SoLuong = soLuong;
            this.ThanhTien = thanhTien;
            this.ThoiGianDat = thoiGianDat;
        }
        public LichSuDatHangDTO(DataTable rows)
        {
            this.MaDonHang = rows.Rows[0]["MaDonHang"].ToString();
            this.NgayDat = Convert.ToDateTime(rows.Rows[0]["NgayDat"]);
            this.MaKhachHang = rows.Rows[0]["MaKhachHang"].ToString();
            this.MaMonAn = rows.Rows[0]["MaMonAn"].ToString();
            this.TenMonAn = rows.Rows[0]["TenMonAn"].ToString();
            this.SoLuong = Convert.ToInt32(rows.Rows[0]["SoLuong"]);
            this.ThanhTien = Convert.ToDouble(rows.Rows[0]["ThanhTien"]);
            this.ThoiGianDat = Convert.ToDateTime(rows.Rows[0]["ThoiGianDat"]);
        }
    }
}
