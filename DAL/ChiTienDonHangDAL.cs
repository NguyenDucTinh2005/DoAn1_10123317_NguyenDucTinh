using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DoAn1.DTO;
using DTO;
using System.Collections;

namespace DAL
{
    public class ChiTienDonHangDAL : DBconnect
    {
        
        public bool ThemChiTietDonHang(ChiTietDonHangDTO ctdh)
        {
            string query = string.Format(
                "INSERT INTO ChiTietDonHang (MaDonHang, MaMonAn, SoLuong, ThanhTien, ThoiGianDat) " +
                "VALUES ('{0}', '{1}', {2}, {3}, '{4}')",
                ctdh.MaDonHang, ctdh.MaMonAn, ctdh.SoLuong, ctdh.ThanhTien, ctdh.ThoiGianDat.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                kennoi();
                SqlCommand cmd = new SqlCommand(query, con);
                int rowsAffected = cmd.ExecuteNonQuery();
                dongketnoi();
                return rowsAffected > 0;
            }
            catch
            {
                dongketnoi();
                return false;
            }
        }

        public bool DeleteChiTietDonHang(string maDonHang, string maMonAn)
        {
            string query = string.Format("DELETE FROM ChiTietDonHang WHERE MaDonHang = '{0}' AND MaMonAn = '{1}'", maDonHang, maMonAn);
            kennoi();
            SqlCommand cmd = new SqlCommand(query, con);
            int rowsAffected = cmd.ExecuteNonQuery();
            dongketnoi();
            return rowsAffected > 0;
        }
        public DataTable getChiTietDonHangByMaDonHang(string maDonHang)
        {
            string query = "SELECT ctdh.MaDonHang, ctdh.MaMonAn, ma.TenMonAn, ctdh.SoLuong, ma.GiaBan, ctdh.ThanhTien, ctdh.ThoiGianDat, " +
                  "(SELECT SUM(ThanhTien) FROM ChiTietDonHang WHERE MaDonHang = ctdh.MaDonHang) AS TongTien " +
                  "FROM ChiTietDonHang ctdh " +
                  "JOIN MonAn ma ON ctdh.MaMonAn = ma.MaMonAn " +
                  "WHERE ctdh.MaDonHang = @MaDonHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaDonHang", maDonHang)
            };
            return getAll(query, parameters);
        }
        public bool deleteChiTietDonHangByMaDonHang(string maDonHang)
        {
            string query = "DELETE FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaDonHang", maDonHang) };
            kennoi();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddRange(parameters);
                int rowsAffected = cmd.ExecuteNonQuery();
                dongketnoi();
                return rowsAffected > 0;
            }

        }

    }
}
