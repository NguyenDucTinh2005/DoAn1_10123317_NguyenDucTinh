using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using System.Data.SqlClient;


namespace BUS
{
    public class ThongKeBUS
    {
        private ThongKeDAL thongKeDAL = new ThongKeDAL();

        public DataTable GetDoanhThuTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            return thongKeDAL.GetDoanhThuTheoNgay(tuNgay, denNgay);
        }
        public DataTable GetMonBanChay()
        {
            return thongKeDAL.GetMonBanChay();
        }

        public DataTable GetNguyenLieuTonTheoNgay(DateTime ngay)
        {
            return thongKeDAL.GetNguyenLieuTonTheoNgay(ngay);
        }
    }
}
