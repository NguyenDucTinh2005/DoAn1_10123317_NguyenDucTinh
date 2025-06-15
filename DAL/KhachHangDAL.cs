using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Text.RegularExpressions;

namespace DAL
{
    public class KhachHangDAL:DBconnect
    {
        DBconnect DBconnect = new DBconnect();
        public DataTable getAllKhachHang()
        {
            string query = "SELECT * FROM KhachHang";
            return DBconnect.getAll(query);
        }
        public bool insertKhachHang(KhachHangDTO khachHang)
        {
            string query = string.Format("INSERT INTO KhachHang (MaKhachHang,TenKhachHang,DiaChi,SoDienThoai) VALUES ('{0}',N'{1}',N'{2}','{3}')", khachHang.MaKhachHang, khachHang.TenKhachHang, khachHang.DiaChi, khachHang.SoDienThoai);
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
        public bool deleteKhachHang(string maKhachHang)
        {
            string query = string.Format("DELETE FROM KhachHang WHERE MaKhachHang = '{0}'", maKhachHang);
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

        public DataTable searchKhachHang(string keyword)
        {
            string query = "SELECT * FROM KhachHang WHERE MaKhachHang LIKE @keyword OR TenKhachHang LIKE @keyword";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@keyword", "%" + keyword + "%")
            };
            return DBconnect.getAll(query, parameters);
        }

        public int kiemTraSDT(string SDT)
        {
            // Validate the phone number format
            if (!Regex.IsMatch(SDT, @"^0\d{9}$"))
            {
                throw new ArgumentException("Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0.");
            }

            con.Open();
            int i;
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM KhachHang WHERE SDT = @SDT", con);
            cmd.Parameters.AddWithValue("@SDT", SDT);
            i = (int)cmd.ExecuteScalar();
            con.Close();
            return i;
        }

        public bool kiemtraTenNguoiNhan(string tenNguoiNhan)
        {
            return !string.IsNullOrWhiteSpace(tenNguoiNhan);
        }

        public bool kiemtraDiaChi(string diaChi)
        {
            return !string.IsNullOrWhiteSpace(diaChi);
        }

        public bool DeleteKH(string maDonHang)
        {
            try
            {
                string query = "DELETE FROM DonHang WHERE MaDonHang = @maDonHang";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@maDonHang", maDonHang);

                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception("Lỗi khi xóa đơn hàng: " + ex.Message);
            }
        }

        public bool UpdateKhachHang(KhachHangDTO khachHang)
        {
            string query = string.Format(
                "UPDATE KhachHang SET TenKhachHang = N'{0}', DiaChi = N'{1}', SoDienThoai = '{2}' WHERE MaKhachHang = '{3}'",
                khachHang.TenKhachHang, khachHang.DiaChi, khachHang.SoDienThoai, khachHang.MaKhachHang);

            con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result > 0;
            
        }

    }
}
