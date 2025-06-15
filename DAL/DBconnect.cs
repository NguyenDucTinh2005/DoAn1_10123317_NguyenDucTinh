using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBconnect
    {
        protected SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=DoAn1;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public void kennoi()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void dongketnoi()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        // Ham lay du lieu

        public DataTable getAll(string sql, SqlParameter[] parameters = null)
        {
            kennoi();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dongketnoi();
                    return dt;
                }
            }
        }


        // Ham thuc thi cau lenh insert, update, delete

        public void thuthisql(string sql)
        {
            kennoi();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongketnoi();
        }
        public DataTable getAllWithParam(string query, SqlParameter parameter)
        {
           
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(parameter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            
           
        }
    }
}
