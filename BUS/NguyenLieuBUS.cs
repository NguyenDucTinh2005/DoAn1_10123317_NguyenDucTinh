using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BUS
{
    public class NguyenLieuBUS
    {
        NguyenLieuDAL nguyenLieuDAL = new NguyenLieuDAL();
        public bool TruSoLuong(string maNguyenLieu, float soLuongBiTru)
        {
            return nguyenLieuDAL.TruSoLuong(maNguyenLieu, soLuongBiTru);
        }
        public float LaySoLuongTon(string maNguyenLieu)
        {
            return nguyenLieuDAL.LaySoLuongTon(maNguyenLieu);
        }
        public bool InsertNguyenLieu(NguyenLieuDTO nguyenLieuDTO)
        {
            return nguyenLieuDAL.insertNguyenLieu(nguyenLieuDTO);
        }
        public bool DeleteNguyenLieu(string maNguyenLieu)
        {
            return nguyenLieuDAL.DeleteNguyenLieu(maNguyenLieu);
        }
        public DataTable getAllNguyenLieu()
        {
            return nguyenLieuDAL.getAllNguyenLieu();
        }
        public bool UpdateNguyenLieu(NguyenLieuDTO nguyenLieuDTO)
        {
            return nguyenLieuDAL.UpdateNguyenLieu(nguyenLieuDTO);

        }
        public DataTable searchNguyenLieu(string NguyenLieu)
        {
            return nguyenLieuDAL.SearchNguyenLieu(NguyenLieu);
        }
        public bool KiemTraMaNguyenLieu(string maNguyenLieu)
        {
            return nguyenLieuDAL.KiemTraMaNguyenLieu(maNguyenLieu);
        }
        public NguyenLieuDTO getNguyenLieuByMa(string maNguyenLieu)
        {
            return nguyenLieuDAL.GetNguyenLieuByMa(maNguyenLieu);
        }
    }
}
