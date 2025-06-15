using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    namespace DTO
    {
        public class ChiTietNguyenLieuDTO
        {
            string maMonAN;
            string maNguyenLieu;
            float soLuongCan;

            public string MaMonAn { get => maMonAN; set => maMonAN = value; }
            public string MaNguyenLieu { get => maNguyenLieu; set => maNguyenLieu = value; }
            public float SoLuongCan { get => soLuongCan; set => soLuongCan = value; }
            public ChiTietNguyenLieuDTO(string maMonAN, string maNguyenLieu, float soLuongCan)
            {
                this.maMonAN = maMonAN;
                this.maNguyenLieu = maNguyenLieu;
                this.soLuongCan = soLuongCan;
            }
            public ChiTietNguyenLieuDTO() { }

        }
    }

}
