using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;
using DAL;
namespace BUS
{
    public class LichSuDatHangBUS
    {
        LichSuDatHangDAL lichSuDatHangDAL = new LichSuDatHangDAL();
        public DataTable getAllLichSuDatHang()
        {
            return lichSuDatHangDAL.getAllLichSuDatHang();
        }
       public bool deleteLichSuDatHang(string maDonHang)
        {
            return lichSuDatHangDAL.deleteLichSuDatHang(maDonHang);
        }
    }
}
