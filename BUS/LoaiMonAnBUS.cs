using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DoAn1.DTO;
using DAL;

namespace BUS
{
    public class LoaiMonAnBUS
    {
        LoaiMonAnDAL loaiMonAnDAL = new LoaiMonAnDAL();

        public DataTable getAllLoaiMonAn()
        {
            return loaiMonAnDAL.getAllLoaiMonAn();
        }
        public bool insertLoaiMonAn(LoaiMonAnDTO loaiMon)
        {
            return loaiMonAnDAL.insertLoaiMonAn(loaiMon);
        }
        public int kiemTraMaTrung(string maLoaiMon)
        {
            return loaiMonAnDAL.kiemTraMaTrung(maLoaiMon);
        }
        public bool deleteLoaiMonAn(string maLoaiMon)
        {
            return loaiMonAnDAL.deleteLoaiMonAn(maLoaiMon);
        }
        public bool updateLoaiMonAn(LoaiMonAnDTO loaiMon)
        {
            return loaiMonAnDAL.updateLoaiMonAn(loaiMon);
        }
        public DataTable searchLoaiMonAn(string keyword)
        {
            return loaiMonAnDAL.searchLoaiMonAn(keyword);
        }

    }
}
