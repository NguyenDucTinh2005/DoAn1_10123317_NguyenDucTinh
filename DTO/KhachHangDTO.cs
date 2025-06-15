using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHangDTO
    {
        string maKhachHang;
        string tenKhachHang;
        string diaChi;
        string soDienThoai;
        private string tenDangnhap;
        private string v;

        public string MaKhachHang { get => maKhachHang; set => maKhachHang = value; }
        public string TenKhachHang { get => tenKhachHang; set => tenKhachHang = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }

        public KhachHangDTO(string MaKhachHang, string TenKhachHang, string DiaChi, string SoDienThoai)
        {
            this.MaKhachHang = MaKhachHang;
            this.TenKhachHang = TenKhachHang;
            this.DiaChi = DiaChi;
            this.SoDienThoai = SoDienThoai;
        }
        public KhachHangDTO(DataTable row)
        {
            this.MaKhachHang = row.Rows[0]["MaKhachHang"].ToString();
            this.TenKhachHang = row.Rows[0]["TenKhachHang"].ToString();
            this.DiaChi = row.Rows[0]["DiaChi"].ToString();
            this.SoDienThoai = row.Rows[0]["SoDienThoai"].ToString();
        }

       
    }
}
