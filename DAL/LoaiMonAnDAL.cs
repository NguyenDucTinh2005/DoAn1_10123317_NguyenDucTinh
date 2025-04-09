using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DoAn1.DTO;


namespace DAL
{
    public class LoaiMonAnDAL:DBconnect
    {
        DBconnect connect = new DBconnect();

        public DataTable getAllLoaiMonAn()
        {
            string query = "SELECT * FROM LoaiMonAn";
            return connect.getAll(query);
        }
        public bool insertLoaiMonAn(LoaiMonAnDTO loaiMon)
        {
         
            string query = string.Format("INSERT INTO LoaiMonAn (MaLoaiMon,TenLoaiMon) VALUES ('{0}',N'{1}')", loaiMon.maLoaiMon, loaiMon.tenLoaiMon);
            con.Open();
            ;            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {   con.Close();
                return true;
              
            }
            con.Close();
            return false;
           
        }

        public int kiemTraMaTrung(string maLoaiMon)
        {
            con.Open();
            int i;
            SqlCommand cmd = new SqlCommand("select count(*) from LoaiMonAN where MaLoaiMon='" + maLoaiMon+ "'", con);
            i = (int)cmd.ExecuteScalar();
            con.Close();
            return i;
        }

        public bool deleteLoaiMonAn(string maLoaiMon)
        {
            con.Open();
            string query = string.Format("DELETE FROM LoaiMonAn WHERE MaLoaiMon = '{0}'", maLoaiMon);
            
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {con.Close();
                return true;
            }
            con.Close();
            return false;
        }
        public bool updateLoaiMonAn(LoaiMonAnDTO loaiMon)
        {
            con.Open();
            string query = string.Format("UPDATE LoaiMonAn SET TenLoaiMon = N'{0}' WHERE MaLoaiMon = '{1}'", loaiMon.tenLoaiMon, loaiMon.maLoaiMon);
           
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            { con.Close();
                return true;
            }
            con.Close();
            return false;
        }
        public DataTable searchLoaiMonAn(string keyword)
        {
            string query = string.Format("SELECT * FROM LoaiMonAn WHERE MaLoaiMon LIKE '%{0}%' OR TenLoaiMon LIKE N'%{0}%'", keyword);
            return connect.getAll(query);
        }

    }
}
