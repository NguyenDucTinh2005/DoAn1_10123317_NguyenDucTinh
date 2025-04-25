using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class LichSuDatHangDAL : DBconnect
    {
        DBconnect DBconnect = new DBconnect();
        public DataTable getAllLichSuDatHang()
        {
            string query = "SELECT * FROM LichSuDatHang";
            return DBconnect.getAll(query);
        }
        public bool insertLichSuDatHang(LichSuDatHangDTO lichSuDatHang)
        {
            string query = string.Format("INSERT INTO LichSuDatHang (MaDonHang, NgayDat, MaKhachHang, MaMonAn, TenMonAn, SoLuong, ThanhTien, ThoiGianDat) " +
                                         "VALUES ('{0}', '{1}', '{2}', '{3}', N'{4}', {5}, {6}, GETDATE())",
                                         lichSuDatHang.MaDonHang,
                                         lichSuDatHang.NgayDat.ToString("yyyy-MM-dd"),
                                         lichSuDatHang.MaKhachHang,
                                         lichSuDatHang.MaMonAn,
                                         lichSuDatHang.TenMonAn,
                                         lichSuDatHang.SoLuong,
                                         lichSuDatHang.ThanhTien);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public bool deleteLichSuDatHang(string maDonHang)
        {
            string query = string.Format("DELETE FROM LichSuDatHang WHERE MaDonHang = '{0}'", maDonHang);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }
    }
}
