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
        public DataTable getAllLichSuDatHang()
        {
            string query = "SELECT * FROM LichSuDatHang";
            return this.getAll(query);
        }

        public DataTable getLichSuDatHangByMaDonHang(string maDonHang)
        {
            string query = "SELECT * FROM LichSuDatHang WHERE MaDonHang = @MaDonHang";
            return getAllWithParam(query, new SqlParameter("@MaDonHang", maDonHang));
        }
        public bool insertLichSuDatHang(LichSuDatHangDTO dto)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO LichSuDatHang (MaLichSu, MaDonHang, NgayDat, ThoiGianDat, MaMonAn, TenMonAn, SoLuong, ThanhTien) " +
                               "VALUES (@MaLichSu, @MaDonHang, @NgayDat, @ThoiGianDat, @MaMonAn, @TenMonAn, @SoLuong, @ThanhTien)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaLichSu", dto.MaLichSu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaDonHang", dto.MaDonHang ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDat", dto.NgayDat);
                cmd.Parameters.AddWithValue("@ThoiGianDat", dto.ThoiGianDat);
                cmd.Parameters.AddWithValue("@MaMonAn", dto.MaMonAn ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TenMonAn", dto.TenMonAn ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SoLuong", dto.SoLuong);
                cmd.Parameters.AddWithValue("@ThanhTien", dto.ThanhTien);

                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                return false;
            }
        }

        public bool deleteLichSuDatHangByMaDonHang(string maDonHang)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_XoaLichSuDatHang", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                return false;
            }
        }

        public DataTable getLichSuDatHangByMaKhachHang(string maKhachHang)
        {
            string query = "SELECT l.* FROM LichSuDatHang l " +
                           "INNER JOIN DonHang d ON l.MaDonHang = d.MaDonHang " +
                           "WHERE d.MaKhachHang = @MaKhachHang";
            return getAllWithParam(query, new SqlParameter("@MaKhachHang", maKhachHang));
        }
    }
}