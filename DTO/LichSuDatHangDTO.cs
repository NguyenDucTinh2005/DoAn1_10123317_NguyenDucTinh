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
        // 1. Khai báo biến private trước
        private string maDonHang;
        private DateTime ngayDat;
        private string maKhachHang;
        private string maMonAn;
        private string tenMonAn;
        private int soLuong;
        private double thanhTien;
        private DateTime thoiGianDat;

        // 2. Thuộc tính public (Get, Set)
        public string MaDonHang
        {
            get { return maDonHang; }
            set { maDonHang = value; }
        }
        public DateTime NgayDat
        {
            get { return ngayDat; }
            set { ngayDat = value; }
        }
        public string MaKhachHang
        {
            get { return maKhachHang; }
            set { maKhachHang = value; }
        }
        public string MaMonAn
        {
            get { return maMonAn; }
            set { maMonAn = value; }
        }
        public string TenMonAn
        {
            get { return tenMonAn; }
            set { tenMonAn = value; }
        }
        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        public double ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
        }
        public DateTime ThoiGianDat
        {
            get { return thoiGianDat; }
            set { thoiGianDat = value; }
        }

        // 3. Constructor đầy đủ tham số
        public LichSuDatHangDTO(string maDonHang, DateTime ngayDat, string maKhachHang, string maMonAn, string tenMonAn, int soLuong, double thanhTien, DateTime thoiGianDat)
        {
            this.maDonHang = maDonHang;
            this.ngayDat = ngayDat;
            this.maKhachHang = maKhachHang;
            this.maMonAn = maMonAn;
            this.tenMonAn = tenMonAn;
            this.soLuong = soLuong;
            this.thanhTien = thanhTien;
            this.thoiGianDat = thoiGianDat;
        }

        // 4. Constructor nhận DataRow
        public LichSuDatHangDTO(DataRow row)
        {
            this.maDonHang = row["MaDonHang"].ToString();
            this.ngayDat = Convert.ToDateTime(row["NgayDat"]);
            this.maKhachHang = row["MaKhachHang"].ToString();
            this.maMonAn = row["MaMonAn"].ToString();
            this.tenMonAn = row["TenMonAn"].ToString();
            this.soLuong = Convert.ToInt32(row["SoLuong"]);
            this.thanhTien = Convert.ToDouble(row["ThanhTien"]);
            this.thoiGianDat = Convert.ToDateTime(row["ThoiGianDat"]);
        }
    }
}
