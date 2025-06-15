using System;
using System.Data;
using DAL;
using DTO;

namespace BUS
{
    public class LichSuDatHangBUS
    {
        private LichSuDatHangDAL lichSuDatHangDAL = new LichSuDatHangDAL();

        public DataTable getAllLichSuDatHang()
        {
            return lichSuDatHangDAL.getAllLichSuDatHang();
        }

        public DataTable getLichSuDatHangByMaDonHang(string maDonHang)
        {
            return lichSuDatHangDAL.getLichSuDatHangByMaDonHang(maDonHang);
        }

        public bool insertLichSuDatHang(LichSuDatHangDTO dto)
        {
            return lichSuDatHangDAL.insertLichSuDatHang(dto);
        }

        public bool deleteLichSuDatHangByMaDonHang(string maDonHang)
        {
            return lichSuDatHangDAL.deleteLichSuDatHangByMaDonHang(maDonHang);
        }

        public DataTable getLichSuDatHangByMaKhachHang(string maKhachHang)
        {
            return lichSuDatHangDAL.getLichSuDatHangByMaKhachHang(maKhachHang);
        }
    }
}