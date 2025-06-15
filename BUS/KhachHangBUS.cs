using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
using DAL;

namespace BUS
{
    public class KhachHangBUS
    {
        KhachHangDAL khachHang = new KhachHangDAL();
        public DataTable getAllKhachHang()
        {
            return khachHang.getAllKhachHang();
        }
        public bool insertKhachHang(KhachHangDTO khachHang)
        {
            return this.khachHang.insertKhachHang(khachHang);
        }
        public bool deleteKhachHang(string maKhachHang)
        {
            return khachHang.deleteKhachHang(maKhachHang);
        }
        public DataTable searchKhachHang(string keyword)
        {
            return khachHang.searchKhachHang(keyword);
        }
        public int kiemTraSDT(string SDT)
        {
            // Validate the phone number format
            if (!System.Text.RegularExpressions.Regex.IsMatch(SDT, @"^0\d{9}$"))
            {
                throw new ArgumentException("Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0.");
            }
            return khachHang.kiemTraSDT(SDT);
        }
        public bool kiemTraTenNguoiNhan(string maKhachHang)
        {
            return khachHang.kiemtraTenNguoiNhan(maKhachHang);
        }
        public bool kiemTraDiaChi(string maKhachHang)
        {
            return khachHang.kiemtraDiaChi(maKhachHang);
        }

        public bool DeleteKH(string maKhachHang)
        {
            return khachHang.DeleteKH(maKhachHang);
        }
     
        public bool UpdateKhachHang(KhachHangDTO khachHang)
        {
            return this.khachHang.UpdateKhachHang(khachHang);
        }

    }
}
