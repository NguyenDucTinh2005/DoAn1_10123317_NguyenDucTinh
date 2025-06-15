using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDonHangDTO
    {
        string maDonHang;
        string maMonAn;
        int soLuong;
        float thanhTien;
        DateTime thoiGianDat;
        public float  ThanhTien { get => thanhTien; set => thanhTien = value; }
        public string MaDonHang { get => maDonHang; set => maDonHang = value; }
        public string MaMonAn { get => maMonAn; set => maMonAn = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public DateTime ThoiGianDat { get => thoiGianDat; set => thoiGianDat = value; }

        public ChiTietDonHangDTO() { }

        public ChiTietDonHangDTO(string maDonHang, string maMonAn, int soLuong, float thanhTien, DateTime thoiGianDat)
        {
            this.MaDonHang = maDonHang;
            this.MaMonAn = maMonAn;
            this.SoLuong = soLuong;
            this.ThanhTien = thanhTien;
            this.ThoiGianDat = thoiGianDat;
        }

        public ChiTietDonHangDTO(DataRow row)
        {
            MaDonHang = row["MaDonHang"].ToString();
            MaMonAn = row["MaMonAn"].ToString();
            SoLuong = Convert.ToInt32(row["SoLuong"]);
            ThanhTien = Convert.ToSingle(row["ThanhTien"]);
            ThoiGianDat = Convert.ToDateTime(row["ThoiGianDat"]);
        }
      
    }
}
