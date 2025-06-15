using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.DTO;
using DAL;

namespace BUS
{
    public class ChiTietNguyenLieuBUS
    {
        ChiTietNguyenLieuDAL chiTietNguyenLieuDAL = new ChiTietNguyenLieuDAL();

        public List<ChiTietNguyenLieuDTO> GetNguyenLieuTheoMonAn(string maMonAn)
        {
            return chiTietNguyenLieuDAL.GetNguyenLieuTheoMonAn(maMonAn);
        }
        public bool InsertChiTietNguyenLieu(ChiTietNguyenLieuDTO ct)
        {
            return chiTietNguyenLieuDAL.InsertChiTietNguyenLieu(ct);
        }
        public bool Update(ChiTietNguyenLieuDTO ct)
        {
            return chiTietNguyenLieuDAL.Update(ct);
        }
        public bool Delete(string maMonAn, string maNguyenLieu)
        {
            return chiTietNguyenLieuDAL.Delete(maMonAn, maNguyenLieu);
        }
    }
}
