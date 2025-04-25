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
    public class TaiKhoanDAL : DBconnect
    {
        DBconnect connect = new DBconnect();

        public DataTable getAllTaiKhoan()
        {
            string query = "SELECT * FROM TaiKhoan";
            return connect.getAll(query);
        }
        public bool insertTaiKhoan(TaiKhoanDTO tk)
        {
            string query = string.Format("INSERT INTO TaiKhoan (TenDangnhap, Matkhau, SoDienthoai, Quyen) VALUES ('{0}', '{1}', '{2}', '{3}')",
                tk.TenDangnhap, tk.Matkhau, tk.SoDienthoai, tk.quyen);
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

        public bool updateTaiKhoan(TaiKhoanDTO tk)
        {
            string query = string.Format("UPDATE TaiKhoan SET TenDangNhap = N'{0}', MatKhau = N'{1}', SoDienThoai = '{2}', Quyen = N'{3}' WHERE MaTaiKhoan = {4}",
                tk.TenDangnhap, tk.Matkhau, tk.SoDienthoai, tk.quyen, tk.maTaiKhoan);  // Viết hoa đúng thuộc tính

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result > 0;
        }
        public bool detedeTaiKhoan(TaiKhoanDTO tk) { 
            string query = string.Format("DELETE FROM TaiKhoan WHERE MaTaiKhoan = {0}", tk.maTaiKhoan);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0;
        }
        public bool dangKyTaiKhoan(TaiKhoanDTO tk)
        {
            string query = string.Format("INSERT INTO TaiKhoan (TenDangnhap, Matkhau, SoDienthoai) VALUES ('{0}', '{1}', '{2}')", tk.TenDangnhap, tk.Matkhau, tk.SoDienthoai);

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)

                return true;


            return false;
        }

        public bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            kennoi();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", matKhau);

            int count = (int)cmd.ExecuteScalar();
            dongketnoi();

            return count > 0;
        }
    }
}
