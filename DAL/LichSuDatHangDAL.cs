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

            string query = "SELECT dh.MaDonHang, dh.NgayDat, kh.TenKhachHang, ctdh.MaMonAn, ma.TenMonAn, " +
                           "ctdh.SoLuong, ctdh.ThanhTien, ctdh.ThoiGianDat " +
                           "FROM DonHang dh " +
                           "JOIN ChiTietDonHang ctdh ON dh.MaDonHang = ctdh.MaDonHang " +
                           "JOIN MonAn ma ON ctdh.MaMonAn = ma.MaMonAn " +
                           "JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang";
            return DBconnect.getAll(query);
        }


        public bool deleteLichSuDatHang(string maDonHang)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SP_XoaLichSuDatHang", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

            int rowsAffected = cmd.ExecuteNonQuery();

            con.Close();

            return rowsAffected > 0;
        }
    }
}
