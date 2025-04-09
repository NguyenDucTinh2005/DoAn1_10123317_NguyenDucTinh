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
    public class MonAnDAL:DBconnect
    {
        DBconnect connect = new DBconnect();
        public DataTable getAllMonAn()
        {
            string query = "SELECT *FROM MonAn";
            return connect.getAll(query);
        }
        public bool insertMonAn(MonAnDTO mon)
        {
            string query = string.Format("INSERT INTO MonAn (MaMonAn,TenMonAn,MaLoaiMon,GiaBan,MoTa) VALUES ('{0}',N'{1}','{2}',{3},N'{4}')", mon.MaMonAn, mon.TenMonAn, mon.MaLoaiMon, mon.GiaBan, mon.moTa);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {   con.Close();
                return true;
            }
            con.Close();
            return false;
        }
        public bool deleteMonAn(string maMon)
        {
            string query = string.Format("DELETE FROM MonAn WHERE MaMonAn = '{0}'", maMon);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            if (cmd.ExecuteNonQuery() > 0)
            {   con.Close();
                return true;
            }
            con.Close();
            return false;
        }
        public bool updateMonAn(MonAnDTO mon)
        {
            string query = string.Format("UPDATE MonAn SET TenMonAn = N'{0}', MaLoaiMon = '{1}', GiaBan = {2}, MoTa = N'{3}' WHERE MaMonAn = '{4}'",
                                         mon.TenMonAn, mon.MaLoaiMon, mon.GiaBan, mon.moTa, mon.MaMonAn);
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
        public DataTable searchMonAn(string keyword)
        {
            string query = string.Format("SELECT * FROM MonAn WHERE TenMonAn LIKE N'%{0}%'", keyword);
            return connect.getAll(query);
        }
        public int kiemTraMaTrung(string maMon)
        {
            con.Open();
            int i;
            SqlCommand cmd = new SqlCommand("select count(*) from MonAn where MaMonAn='" + maMon + "'", con);
            i = (int)cmd.ExecuteScalar();
            con.Close();
            return i;
        }

        public DataTable getMonAnByMaLoai()
        {
            string query = string.Format("SELECT * FROM LoaiMonAn");
            return connect.getAll(query);
        }
        public DataTable getMonAn()
        {
            string query = string.Format("SELECT TenMonAn MoTa DonGia FROM MonAn");
            return connect.getAll(query);
        }
        public DataTable getMonAnByLoai(string maLoaiMon)
        {
            string query = "SELECT * FROM MonAn WHERE MaLoaiMon = @MaLoaiMon";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaLoaiMon", maLoaiMon)
            };
            return connect.getAll(query, parameters);
        }
    }
}
