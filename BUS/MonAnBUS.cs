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
    public class MonAnBUS
    {
        MonAnDAL MonAnDAL = new MonAnDAL();
        public DataTable getAllMonAn()
        {
            return MonAnDAL.getAllMonAn();
        }
        public bool insertMonAn(MonAnDTO mon)
        {
            return MonAnDAL.insertMonAn(mon);
        }
        public bool deleteMonAn(string maMon)
        {
            return MonAnDAL.deleteMonAn(maMon);
        }
        public bool updateMonAn(MonAnDTO mon)
        {
            return MonAnDAL.updateMonAn(mon);
        }
        public DataTable searchMonAn(string keyword)
        {
            return MonAnDAL.searchMonAn(keyword);
        }
        public int kiemTraMaTrung(string maMon)
        {
            return MonAnDAL.kiemTraMaTrung(maMon);
        }

        public DataTable getMonAnByMaLoai()
        {
            return MonAnDAL.getMonAnByMaLoai();
        }
        public DataTable getMonAn()
        {
            return MonAnDAL.getMonAn();

        }
        public DataTable getMonAnByLoai(string maLoaiMon)
        {
            return MonAnDAL.getMonAnByLoai(maLoaiMon);
        }

        public string GetMaMonAnByTen(string tenMonAn)
        {
            return MonAnDAL.GetMaMonAnByTen(tenMonAn);
        }
        public string GetTenMonAnByMa(string maMonAn)
        {
            return MonAnDAL.GetTenMonAnByMa(maMonAn);
        }
    }
}
