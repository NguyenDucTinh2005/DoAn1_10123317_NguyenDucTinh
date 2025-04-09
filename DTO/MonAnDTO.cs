using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class MonAnDTO
    {
        string maMonAn;
        string tenMonAn;
        string maLoaiMon;
        /// <summary>
        /// string maLoaiMon;
        /// </summary>
        float giaBan;
        string MoTa;
        
        public MonAnDTO() { }
        public MonAnDTO(string maMonAn, string tenMonAn, string maLoaiMon, string moTa, float giaBan)
        {
            this.MaMonAn = maMonAn;
            this.TenMonAn = tenMonAn;
            this.MaLoaiMon = maLoaiMon;
            //   this.MaLoaiMon = maLoaiMon;
            this.moTa = moTa;
            this.GiaBan = giaBan;
           
        }

        public MonAnDTO(DataRow row)
        {
            this.MaMonAn = row["MaMonAn"].ToString();
            this.TenMonAn = row["TenMonAn"].ToString();
            this.MaLoaiMon = row["TenLoaiMon"].ToString();
            //this.MaLoaiMon = row["MaLoaiMon"].ToString();
            this.moTa = row["MoTa"].ToString();
            this.GiaBan = Convert.ToSingle(row["GiaBan"]);
           
        }
        public string MaMonAn { get => maMonAn; set => maMonAn = value; }
       // public string MaLoaiMon { get => maLoaiMon; set => maLoaiMon = value; }
        public string TenMonAn { get => tenMonAn; set => tenMonAn = value; }
        public string MaLoaiMon { get => maLoaiMon; set => maLoaiMon = value; }
        public float GiaBan { get => giaBan; set => giaBan = value; }
        public string moTa { get => MoTa; set => MoTa = value; }
        
    }
}
