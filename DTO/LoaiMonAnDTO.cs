using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class LoaiMonAnDTO
    {
        string MaLoaiMon;
        string TenLoaiMon;

        public string maLoaiMon { get => MaLoaiMon; set => MaLoaiMon = value; }
        public string tenLoaiMon { get => TenLoaiMon; set => TenLoaiMon = value; }
        public LoaiMonAnDTO(string maLoaiMon, string tenLoaiMon)
        {
            this.maLoaiMon = maLoaiMon;
            this.tenLoaiMon = tenLoaiMon;
        }
        public LoaiMonAnDTO(DataRow row)
        {
            this.maLoaiMon = row["MaLoaiMon"].ToString();
            this.tenLoaiMon = row["TenLoaiMon"].ToString();
        }
    }
}
