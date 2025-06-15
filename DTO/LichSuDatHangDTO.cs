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
        // 1. Khai báo biến private
        private string maLichSu;
        private string maDonHang;
        private DateTime ngayDat;
        private DateTime thoiGianDat;
        private string maMonAn;
        private string tenMonAn;
        private int soLuong;
        private double thanhTien;

        // 2. Thuộc tính public (Get, Set)
        public string MaLichSu
        {
            get { return maLichSu; }
            set { maLichSu = value; }
        }
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
        public DateTime ThoiGianDat
        {
            get { return thoiGianDat; }
            set { thoiGianDat = value; }
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

        // 3. Constructor đầy đủ tham số
        public LichSuDatHangDTO(string maLichSu, string maDonHang, DateTime ngayDat, DateTime thoiGianDat, string maMonAn, string tenMonAn, int soLuong, double thanhTien)
        {
            this.maLichSu = maLichSu;
            this.maDonHang = maDonHang;
            this.ngayDat = ngayDat;
            this.thoiGianDat = thoiGianDat;
            this.maMonAn = maMonAn;
            this.tenMonAn = tenMonAn;
            this.soLuong = soLuong;
            this.thanhTien = thanhTien;
        }

        // 4. Constructor nhận DataRow
        public LichSuDatHangDTO(DataRow row)
        {
            this.maLichSu = row["MaLichSu"].ToString();
            this.maDonHang = row["MaDonHang"].ToString();
            this.ngayDat = Convert.ToDateTime(row["NgayDat"]);
            this.thoiGianDat = Convert.ToDateTime(row["ThoiGianDat"]);
            this.maMonAn = row["MaMonAn"] != DBNull.Value ? row["MaMonAn"].ToString() : null;
            this.tenMonAn = row["TenMonAn"] != DBNull.Value ? row["TenMonAn"].ToString() : null;
            this.soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
            this.thanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDouble(row["ThanhTien"]) : 0.0;
        }
    }
}