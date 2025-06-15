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
    public class ThongTinNhanHangDAL : DBconnect
    {


        public bool InsertThongTin(ThongTinNhanHangDTO thongTin)
        {
            string query = "INSERT INTO ThongTinNhanHang (MaDonHang, TenNguoiNhan, SoDienThoai, DiaChi) " +
                           "VALUES (@MaDonHang, @TenNguoiNhan, @SoDienThoai, @DiaChi)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaDonHang", thongTin.MaDonHang);
            cmd.Parameters.AddWithValue("@TenNguoiNhan", thongTin.TenNguoiNhan);
            cmd.Parameters.AddWithValue("@SoDienThoai", thongTin.SoDienThoai);
            cmd.Parameters.AddWithValue("@DiaChi", thongTin.DiaChi);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0;
        }
    }
}



