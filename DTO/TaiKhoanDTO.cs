using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class TaiKhoanDTO
    {
        private string TenDangNhap;
        private string MatKhau;
        private string SoDienThoai;
        private string Quyen;

        public string TenDangnhap { get => TenDangNhap; set => TenDangNhap = value; }
        public string Matkhau{ get => MatKhau; set => MatKhau = value; }
        public string SoDienthoai { get => SoDienThoai; set => SoDienThoai = value; }
        public string quyen { get => Quyen; set => Quyen = value; }

        public TaiKhoanDTO(string tenDangNhap, string matKhau, string soDienThoai,string Quyen)
        {
            this.TenDangnhap = tenDangNhap;
            this.Matkhau = matKhau;
            this.SoDienthoai = soDienThoai;
            this.Quyen = Quyen;
        }
        public TaiKhoanDTO(DataRow Row) {
            this.TenDangnhap = Row["TenDangNhap"].ToString();
            this.Matkhau = Row["MatKhau"].ToString();
            this.SoDienthoai = Row["SoDienThoai"].ToString();
            this.Quyen = Row["Quyen"].ToString();
        }
    }
}
