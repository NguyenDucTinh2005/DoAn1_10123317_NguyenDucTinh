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
    public class TaiKhoanBus
    {
        TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
        public bool insertTaiKhoan(TaiKhoanDTO tk)
        {
          
            return taiKhoanDAL.insertTaiKhoan(tk);
        }
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            return taiKhoanDAL.KiemTraDangNhap(tenDangNhap, matKhau);
        }
        public bool DangKyTaiKhoan(TaiKhoanDTO tk)
        {
            return taiKhoanDAL.dangKyTaiKhoan(tk);
        }
    }
}
