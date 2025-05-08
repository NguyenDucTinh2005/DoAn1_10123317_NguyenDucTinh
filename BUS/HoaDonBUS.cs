using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DoAn1.DTO;
using DAL;
using System.Data.SqlClient;

namespace BUS
{
    public class HoaDonBUS
    {
        HoaDonDAL hoaDonDAL = new HoaDonDAL();

        public DataTable GetHoaDon(string maDonHang)
        {
            return hoaDonDAL.GetHoaDonByMaDonHang(maDonHang);
        }
    }
}
