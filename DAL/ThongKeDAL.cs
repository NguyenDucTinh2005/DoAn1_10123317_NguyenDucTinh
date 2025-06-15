using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThongKeDAL : DBconnect
    {
        public DataTable GetDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string query = "SELECT CAST(dh.NgayDat AS DATE) AS Ngay, " +
                          "DATENAME(WEEKDAY, dh.NgayDat) + ', ' + CONVERT(VARCHAR, dh.NgayDat, 103) AS NgayFormatted, " +
                          "SUM(ctdh.ThanhTien) AS DoanhThu " +
                          "FROM ChiTietDonHang ctdh " +
                          "JOIN DonHang dh ON ctdh.MaDonHang = dh.MaDonHang " +
                          "WHERE dh.NgayDat BETWEEN @TuNgay AND @DenNgay " +
                          "GROUP BY CAST(dh.NgayDat AS DATE), " +
                          "DATENAME(WEEKDAY, dh.NgayDat) + ', ' + CONVERT(VARCHAR, dh.NgayDat, 103) " +
                          "ORDER BY CAST(dh.NgayDat AS DATE)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };
            return getAll(query, parameters);
        }
        public DataTable GetMonBanChay()
        {

            con.Open();
            string query = "SELECT M.TenMonAn, SUM(C.SoLuong) AS TongSoLuong, SUM(C.ThanhTien) AS TongDoanhThu " +
                           "FROM MonAn M " +
                           "INNER JOIN ChiTietDonHang C ON M.MaMonAn = C.MaMonAn " +
                           "INNER JOIN DonHang D ON C.MaDonHang = D.MaDonHang " +
                           "GROUP BY M.TenMonAn " +
                           "ORDER BY TongSoLuong DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetNguyenLieuTonTheoNgay(DateTime ngay)
        {

            con.Open();
            string query = "SELECT N.TenNguyenLieu, N.DonViTinh, (N.SoLuongTon - ISNULL(SUM(CN.SoLuongCan * CD.SoLuong), 0)) AS SoLuongTon " +
                           "FROM NguyenLieu N " +
                           "LEFT JOIN ChiTietNguyenLieu CN ON N.MaNguyenLieu = CN.MaNguyenLieu " +
                           "LEFT JOIN ChiTietDonHang CD ON CN.MaMonAn = CD.MaMonAn " +
                           "LEFT JOIN DonHang D ON CD.MaDonHang = D.MaDonHang " +
                           "WHERE CONVERT(date, D.NgayDat) = @Ngay OR D.NgayDat IS NULL " +
                           "GROUP BY N.TenNguyenLieu, N.DonViTinh, N.SoLuongTon";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }

}
