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
    public class DonHangDAL : DBconnect
    {
        DBconnect dBconnect = new DBconnect();
        public DataTable getDonHang()
        {
            string query = "SELECT M.MaDonHang,K.MaKhachHang,K.TenKhachHang,k.SoDienThoai,k.DiaChi FROM DonHang M INNER JOIN KhachHang k on M.MaKhachHang=K.MaKhachHang";
            return dBconnect.getAll(query);
        }

        public bool insertDonHang(DonHangDTO donHang)
        {
            // Format the date to 'yyyy-MM-dd'
            string ngaydat = donHang.NgayDat.ToString("yyyy-MM-dd");
            string query = string.Format("INSERT INTO DonHang (MaDonHang, NgayDat, MaKhachHang) VALUES ('{0}', '{1}', '{2}')", donHang.MaDonHang, ngaydat, donHang.MaKhachHang);

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
        public bool deleteDonHang(string maDonHang)
        {
            string query = string.Format("DELETE FROM DonHang WHERE MaDonHang = '{0}'", maDonHang);
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

        public DataTable SearchDonHang(string keyword)
        {
            string query = "SELECT * FROM DonHang WHERE MaDonHang LIKE @keyword OR NgayDat LIKE @keyword";
            return dBconnect.getAll(query);
        }
    }
}
