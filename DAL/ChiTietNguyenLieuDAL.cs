
using DTO.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class ChiTietNguyenLieuDAL : DBconnect
    {
        public List<ChiTietNguyenLieuDTO> GetNguyenLieuTheoMonAn(string maMonAn)
        {
            List<ChiTietNguyenLieuDTO> ds = new List<ChiTietNguyenLieuDTO>();
            string query = string.Format("SELECT * FROM ChiTietNguyenLieu WHERE MaMonAn = '{0}'", maMonAn);

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(new ChiTietNguyenLieuDTO
                {
                    MaMonAn = reader["MaMonAn"].ToString(),
                    MaNguyenLieu = reader["MaNguyenLieu"].ToString(),
                    SoLuongCan = float.Parse(reader["SoLuongCan"].ToString())
                });
            }
            con.Close();
            return ds;
        }

        public bool InsertChiTietNguyenLieu(ChiTietNguyenLieuDTO ct)
        {
            string query = string.Format("INSERT INTO ChiTietNguyenLieu VALUES ('{0}', '{1}', {2})",
            ct.MaMonAn, ct.MaNguyenLieu, ct.SoLuongCan);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();
            return rows > 0;
        }
        public bool Update(ChiTietNguyenLieuDTO ct)
        {
            string query = string.Format("UPDATE ChiTietNguyenLieu SET SoLuongCan = {0} WHERE MaMonAn = '{1}' AND MaNguyenLieu = '{2}'",
                                         ct.SoLuongCan, ct.MaMonAn, ct.MaNguyenLieu);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();
            return rows > 0;
        }

        public bool Delete(string maMonAn, string maNguyenLieu)
        {
            string query = string.Format("DELETE FROM ChiTietNguyenLieu WHERE MaMonAn = '{0}' AND MaNguyenLieu = '{1}'",
                                         maMonAn, maNguyenLieu);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();
            return rows > 0;
        }

        public bool DeleteAllByMonAn(string maMonAn)
        {
            string query = string.Format("DELETE FROM ChiTietNguyenLieu WHERE MaMonAn = '{0}'", maMonAn);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();
            return rows > 0;
        }
      

    }

}

