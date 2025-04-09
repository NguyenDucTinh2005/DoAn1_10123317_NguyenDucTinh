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

        public bool insertTaiKhoan(TaiKhoanDTO tk)
        {
            string query = string.Format("INSERT INTO TaiKhoan (TenDangnhap, Matkhau, SoDienthoai,Quyen) VALUES ('{0}', '{1}', '{2}',{3})", tk.TenDangnhap, tk.Matkhau, tk.SoDienthoai,tk.quyen);

            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)

                return true;


            return false;
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
