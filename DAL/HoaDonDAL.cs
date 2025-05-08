using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DoAn1.DTO;

namespace DAL
{
    public class HoaDonDAL : DBconnect
    {
        public DataTable GetHoaDonByMaDonHang(string maDonHang)
        {
            string query = @"SELECT 
                                dh.MaDonHang,
                                dh.NgayDat,
                                kh.TenKhachHang,
                                kh.SoDienThoai,
                                kh.DiaChi,
                                ma.TenMonAn,
                                ctdh.SoLuong,
                                ma.GiaBan,
                                ctdh.ThanhTien
                             FROM DonHang dh
                             INNER JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
                             INNER JOIN ChiTietDonHang ctdh ON dh.MaDonHang = ctdh.MaDonHang
                             INNER JOIN MonAn ma ON ctdh.MaMonAn = ma.MaMonAn
                             WHERE dh.MaDonHang = @MaDonHang";

            SqlParameter[] parameters = {
                new SqlParameter("@MaDonHang", maDonHang)
            };

            return getAll(query, parameters);
        }
    }
}
