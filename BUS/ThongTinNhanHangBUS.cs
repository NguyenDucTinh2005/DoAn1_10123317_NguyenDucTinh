using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;
using DAL;
namespace BUS
{
    public class ThongTinNhanHangBUS
    {
        private ThongTinNhanHangDAL dal = new ThongTinNhanHangDAL();

        public bool InsertThongTin(ThongTinNhanHangDTO thongTin)
        {
            return dal.InsertThongTin(thongTin);
        }
    }
}
