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
            string query = @"
        SELECT 
            dh.MaDonHang,
            FORMAT(dh.NgayDat, 'yyyy-MM-dd HH:mm:ss') AS NgayDat,
            ttnh.TenNguoiNhan AS TenKhachHang,
            ttnh.SoDienThoai,
            ttnh.DiaChi,
            ma.TenMonAn,
            ctdh.SoLuong,
            ma.GiaBan,
            ctdh.ThanhTien,
            tong.TongTien AS TongTienDonHang
        FROM DonHang dh
        INNER JOIN ThongTinNhanHang ttnh ON dh.MaDonHang = ttnh.MaDonHang
        INNER JOIN ChiTietDonHang ctdh ON dh.MaDonHang = ctdh.MaDonHang
        INNER JOIN MonAn ma ON ctdh.MaMonAn = ma.MaMonAn
        INNER JOIN (
            SELECT MaDonHang, SUM(ThanhTien) AS TongTien
            FROM ChiTietDonHang
            GROUP BY MaDonHang
        ) AS tong ON dh.MaDonHang = tong.MaDonHang
        WHERE dh.MaDonHang = @MaDonHang";

            SqlParameter[] parameters = {
        new SqlParameter("@MaDonHang", maDonHang)
    };

            return getAll(query, parameters);
        }

    }
}
