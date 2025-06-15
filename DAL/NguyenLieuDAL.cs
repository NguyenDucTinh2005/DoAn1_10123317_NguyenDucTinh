using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.DTO;
namespace DAL
{
    public class NguyenLieuDAL : DBconnect
    {
        public bool TruSoLuong(string maNguyenLieu, float soLuongBiTru)
        {
            string query = "UPDATE NguyenLieu SET SoLuongTon = SoLuongTon - @SoLuongBiTru WHERE MaNguyenLieu = @MaNguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
            cmd.Parameters.AddWithValue("@SoLuongBiTru", soLuongBiTru);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0;
        }

        public float LaySoLuongTon(string maNguyenLieu)
        {
            string query = "SELECT SoLuongTon FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);

            con.Open();
            float soLuong = Convert.ToSingle(cmd.ExecuteScalar());
            con.Close();
            return soLuong;
        }
        public bool insertNguyenLieu(NguyenLieuDTO nguyenLieuDTO)
        {
            string query = "INSERT INTO NguyenLieu (MaNguyenLieu, TenNguyenLieu, DonViTinh, SoLuongTon) " +
                         "VALUES (@MaNguyenLieu, @TenNguyenLieu, @DonViTinh, @SoLuongTon)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", nguyenLieuDTO.MaNguyenLieu);
            cmd.Parameters.AddWithValue("@TenNguyenLieu", nguyenLieuDTO.TenNguyenLieu);
            cmd.Parameters.AddWithValue("@DonViTinh", nguyenLieuDTO.DonViTinh);
            cmd.Parameters.AddWithValue("@SoLuongTon", nguyenLieuDTO.SoLuongTon);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0;

        }
        public bool DeleteNguyenLieu(string manguyenLieu)
        {
            string query = "DELETE FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", manguyenLieu);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0;


        }

        public bool UpdateNguyenLieu(NguyenLieuDTO nguyenLieuDTO)
        {
            string query = "UPDATE NguyenLieu SET TenNguyenLieu = @TenNguyenLieu, DonViTinh = @DonViTinh, SoLuongTon = @SoLuongTon " +
                           "WHERE MaNguyenLieu = @MaNguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", nguyenLieuDTO.MaNguyenLieu);
            cmd.Parameters.AddWithValue("@TenNguyenLieu", nguyenLieuDTO.TenNguyenLieu);
            cmd.Parameters.AddWithValue("@DonViTinh", nguyenLieuDTO.DonViTinh);
            cmd.Parameters.AddWithValue("@SoLuongTon", nguyenLieuDTO.SoLuongTon);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0;
        }

        public DataTable getAllNguyenLieu()
        {
            string query = "SELECT * FROM NguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable SearchNguyenLieu(string tenNguyenLieu)
        {
            string query = "SELECT * FROM NguyenLieu WHERE TenNguyenLieu LIKE @TenNguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TenNguyenLieu", "%" + tenNguyenLieu + "%");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;

        }
        public bool KiemTraMaNguyenLieu(string maNguyenLieu)
        {
            string query = "SELECT COUNT(*) FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            return count > 0;
        }
        public NguyenLieuDTO GetNguyenLieuByMa(string maNguyenLieu)
        {
            string query = string.Format("SELECT * FROM NguyenLieu WHERE MaNguyenLieu = '{0}'", maNguyenLieu);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            NguyenLieuDTO nguyenLieu = null;
            if (reader.Read())
            {
                nguyenLieu = new NguyenLieuDTO
                {
                    MaNguyenLieu = reader["MaNguyenLieu"].ToString(),
                    TenNguyenLieu = reader["TenNguyenLieu"].ToString(),
                    DonViTinh = reader["DonViTinh"].ToString(),
                    SoLuongTon = float.Parse(reader["SoLuongTon"].ToString())
                };
            }
            con.Close();
            return nguyenLieu;
        }

    }

}
