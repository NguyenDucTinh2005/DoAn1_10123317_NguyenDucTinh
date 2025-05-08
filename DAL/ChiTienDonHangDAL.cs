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
        
      
       
   
    }
}
