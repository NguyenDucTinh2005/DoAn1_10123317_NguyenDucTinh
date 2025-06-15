using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class NguyenLieuDTO
    {
        string maNguyenLieu;
        string tenNguyenLieu;
        string donViTinh;
        float soLuongTon;
        public string MaNguyenLieu { get => maNguyenLieu; set => maNguyenLieu = value; }
        public string TenNguyenLieu { get => tenNguyenLieu; set => tenNguyenLieu = value; }
        public string DonViTinh { get => donViTinh; set => donViTinh = value; }
        public float SoLuongTon { get => soLuongTon; set => soLuongTon = value; }
        public NguyenLieuDTO(string MaNguyenLieu, string TenNguyenLieu, string DonViTinh, float SoLuongTon)
        {
            this.MaNguyenLieu = MaNguyenLieu;
            this.TenNguyenLieu = TenNguyenLieu;
            this.DonViTinh = DonViTinh;
            this.SoLuongTon = SoLuongTon;
        }
        public NguyenLieuDTO() { }
    }

  
}

