using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DoAn1.DTO;
using DAL;
using System.Collections;
using DTO;


namespace BUS
{
    public class ChiTietDonHangBUS
    {
        ChiTienDonHangDAL ctdhDAL = new ChiTienDonHangDAL();

        public bool ThemChiTietDonHang(ChiTietDonHangDTO ctdh)
        {
            return ctdhDAL.ThemChiTietDonHang(ctdh);
        }
        public bool DeleteChiTietDonHang(string maDonHang, string maMonAn)
        {
            return ctdhDAL.DeleteChiTietDonHang(maDonHang, maMonAn);
        }
       
       

    }
}
