using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using BUS;
using DoAn1.DTO;
using System.Linq;
namespace Test_case
{
    [TestClass]
    public class UnitTest1
    {
        private LoaiMonAnBUS loaiMonAnBUS;

        [TestInitialize]
        public void TestInitialize()
        {
            loaiMonAnBUS = new LoaiMonAnBUS();
            // Đảm bảo dữ liệu mẫu cho các test case (nếu cần)
            var loaiMon = new LoaiMonAnDTO("ML01", "Món nước");
            if (loaiMonAnBUS.kiemTraMaTrung("ML01") == 0)
            {
                loaiMonAnBUS.insertLoaiMonAn(loaiMon); // Thêm dữ liệu mẫu cho TC004, TC005, TC007
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Dọn dẹp dữ liệu sau khi test
            loaiMonAnBUS.deleteLoaiMonAn("ML02");
            loaiMonAnBUS.deleteLoaiMonAn("ML03");
            loaiMonAnBUS.deleteLoaiMonAn("ML04");
        }

        [TestMethod]
        public void TC001_ThemLoaiMonThanhCong()
        {
            // TC001: Thêm loại món thành công
            var loaiMon = new LoaiMonAnDTO("ML02", "Món nướng");
            bool result = loaiMonAnBUS.insertLoaiMonAn(loaiMon);
            Assert.IsTrue(result, "Thêm loại món thất bại");
            Assert.AreEqual(1, loaiMonAnBUS.kiemTraMaTrung("ML02"), "Mã không được thêm vào DB");
        }

        [TestMethod]
        public void TC002_KhongNhapMaLoaiMon()
        {
            // TC002: Không nhập mã loại món
            var loaiMon = new LoaiMonAnDTO("", "Món nước");
            bool result = loaiMonAnBUS.insertLoaiMonAn(loaiMon);
            Assert.IsFalse(result, "Phải báo lỗi khi mã loại món trống");
            // Giả định DAL xử lý và trả về false khi mã trống (không có Message để kiểm tra)
        }

        [TestMethod]
        public void TC003_KhongNhapTenLoaiMon()
        {
            // TC003: Không nhập tên loại món
            var loaiMon = new LoaiMonAnDTO("ML03", "");
            bool result = loaiMonAnBUS.insertLoaiMonAn(loaiMon);
            Assert.IsFalse(result, "Phải báo lỗi khi tên loại món trống");
            // Giả định DAL xử lý và trả về false khi tên trống (không có Message để kiểm tra)
        }

        [TestMethod]
        public void TC004_TrungMaLoaiMon()
        {
            // TC004: Trùng mã loại món
            var loaiMon = new LoaiMonAnDTO("ML01", "Món khác");
            bool result = loaiMonAnBUS.insertLoaiMonAn(loaiMon);
            Assert.IsFalse(result, "Phải báo lỗi khi mã loại món đã tồn tại");
            Assert.AreEqual(1, loaiMonAnBUS.kiemTraMaTrung("ML01"), "Mã đã tồn tại nhưng không báo lỗi");
        }

        [TestMethod]

        public void TC005_SuaLoaiMonThanhCong()
        {
            // TC005: Sửa loại món thành công
            var loaiMon = new LoaiMonAnDTO("ML01", "Món nước - sửa");
            bool result = loaiMonAnBUS.updateLoaiMonAn(loaiMon);
            Assert.IsTrue(result, "Cập nhật loại món thất bại");

            DataTable dt = loaiMonAnBUS.getAllLoaiMonAn();
            Assert.IsNotNull(dt, "Danh sách loại món không được null");
            
        }

        [TestMethod]
        public void TC006_SuaKhiChuaChonDong()
        {
            // TC006: Sửa khi chưa chọn dòng
            var loaiMon = new LoaiMonAnDTO("", "Món nước - sửa");
            bool result = loaiMonAnBUS.updateLoaiMonAn(loaiMon);
            Assert.IsFalse(result, "Phải báo lỗi khi chưa chọn dòng");
            // Giả định DAL xử lý và trả về false khi mã trống (không có Message để kiểm tra)
        }

        [TestMethod]
        public void TC007_XoaLoaiMonThanhCong()
        {
            // TC007: Xóa loại món thành công
            var loaiMon = new LoaiMonAnDTO("ML02", "Món nướng");
            loaiMonAnBUS.insertLoaiMonAn(loaiMon);
            bool result = loaiMonAnBUS.deleteLoaiMonAn("ML02");
            Assert.IsTrue(result, "Xóa loại món thất bại");
            Assert.AreEqual(0, loaiMonAnBUS.kiemTraMaTrung("ML02"), "Mã không được xóa khỏi DB");
        }

        [TestMethod]
        public void TC008_XoaKhiChuaChonDong()
        {
            // TC008: Xóa khi chưa chọn dòng
            bool result = loaiMonAnBUS.deleteLoaiMonAn("");
            Assert.IsFalse(result, "Phải báo lỗi khi chưa chọn dòng");
            // Giả định DAL xử lý và trả về false khi mã trống (không có Message để kiểm tra)
        }

        [TestMethod]
        public void TC009_TimKiemLoaiMonChinhXac()
        {
            // TC009: Tìm kiếm loại món chính xác
            var loaiMon = new LoaiMonAnDTO("ML03", "Món nướng");
            loaiMonAnBUS.insertLoaiMonAn(loaiMon);
            DataTable result = loaiMonAnBUS.searchLoaiMonAn("Món nướng");
            Assert.IsNotNull(result, "Kết quả tìm kiếm không được null");
            Assert.IsTrue(result.Rows.Count > 0, "Phải tìm thấy kết quả");
            Assert.AreEqual("Món nướng", result.Rows[0]["TenLoaiMon"].ToString(), "Kết quả tìm kiếm không chính xác");
        }

        [TestMethod]
        public void TC010_TimKiemLoaiMonKhongTonTai()
        {
            // TC010: Tìm kiếm loại món không tồn tại
            DataTable result = loaiMonAnBUS.searchLoaiMonAn("Pizza");
            Assert.IsNotNull(result, "Kết quả tìm kiếm không được null");
            Assert.AreEqual(0, result.Rows.Count, "Phải không tìm thấy kết quả");
            // Không có Message để kiểm tra thông báo "Không tìm thấy kết quả"
        }

        [TestMethod]
        public void TC011_TimKiemVoiOTrong()
        {
            // TC011: Tìm kiếm với ô rỗng
            DataTable result = loaiMonAnBUS.searchLoaiMonAn("");
            Assert.IsNotNull(result, "Kết quả tìm kiếm không được null");
            Assert.IsTrue(result.Rows.Count > 0, "Phải hiển thị toàn bộ danh sách");
            // Không có Message để kiểm tra thông báo (nếu có)
        }

        [TestMethod]

        public void TC012_LamMoiForm()
        {
            // TC012: Làm mới form
            // Giả định làm mới không xóa dữ liệu, kiểm tra getAllLoaiMonAn như proxy
            var loaiMon = new LoaiMonAnDTO("ML04", "Món tráng miệng");
            loaiMonAnBUS.insertLoaiMonAn(loaiMon); // Thêm dữ liệu để kiểm tra
            DataTable resultBefore = loaiMonAnBUS.getAllLoaiMonAn();
            Assert.IsTrue(resultBefore.Rows.Count > 0, "Danh sách loại món không được rỗng trước khi làm mới");

            // Giả định làm mới chỉ làm mới giao diện (không xóa dữ liệu)
            DataTable resultAfter = loaiMonAnBUS.getAllLoaiMonAn();
            Assert.IsTrue(resultAfter.Rows.Count > 0, "Danh sách loại món không được rỗng sau khi làm mới");
            Assert.AreEqual(resultBefore.Rows.Count, resultAfter.Rows.Count, "Số lượng loại món thay đổi sau khi làm mới");
        }
    }
}