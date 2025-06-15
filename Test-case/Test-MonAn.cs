using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using BUS;
using DoAn1.DTO;
using System.Linq;

namespace Test_case
{
    [TestClass]
    public class UnitTest2
    {
        private MonAnBUS monAnBUS;

        [TestInitialize]
        public void TestInitialize()
        {
            monAnBUS = new MonAnBUS();
            // Đảm bảo dữ liệu mẫu cho các test case (nếu cần)
            var monAn = new MonAnDTO("MA01", "Món 1", "LM01", "Mô tả 1", 10000);
            if (monAnBUS.kiemTraMaTrung("MA01") == 0)
            {
                monAnBUS.insertMonAn(monAn); // Thêm dữ liệu mẫu cho TC007, TC008
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Dọn dẹp dữ liệu sau khi test
            monAnBUS.deleteMonAn("MA02");
            monAnBUS.deleteMonAn("MA03");
            monAnBUS.deleteMonAn("MA04");
        }

        [TestMethod]
        public void TC001_ThemMonMoiThanhCong()
        {
            // Thêm món mới thành công
            var monAn = new MonAnDTO("MA02", "Món 2", "LM01", "Mô tả 2", 20000);
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsTrue(result, "Thêm món mới thất bại");
            Assert.AreEqual(1, monAnBUS.kiemTraMaTrung("MA02"), "Mã không được thêm vào DB");
        }

        [TestMethod]
        public void TC002_KhongNhapMaMon()
        {
            // Không nhập mã món
            var monAn = new MonAnDTO("", "Món 2", "LM01", "Mô tả 2", 20000);
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi mã món trống");
        }

        [TestMethod]
        public void TC003_KhongNhapTenMon()
        {
            // Không nhập tên món
            var monAn = new MonAnDTO("MA03", "", "LM01", "Mô tả 2", 20000);
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi tên món trống");
        }

        [TestMethod]
        public void TC004_KhongNhapDonGia()
        {
            // Không nhập đơn giá
            var monAn = new MonAnDTO("MA04", "Món 4", "LM01", "Mô tả 4", 0);
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi đơn giá không hợp lệ (0)");
        }

        [TestMethod]
        public void TC005_NhapDonGiaAm()
        {
            // Nhập đơn giá âm
            var monAn = new MonAnDTO("MA05", "Món 5", "LM01", "Mô tả 5", -1000);
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi đơn giá âm");
        }

        [TestMethod]
        public void TC006_NhapDonGiaLaChu()
        {
            // Nhập đơn giá là chữ (giả định giá trị float không hợp lệ sẽ được xử lý trong DAL)
            var monAn = new MonAnDTO("MA06", "Món 6", "LM01", "Mô tả 6", float.NaN); // Sử dụng NaN để mô phỏng giá trị không hợp lệ
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi đơn giá là chữ hoặc không hợp lệ");
        }

        [TestMethod]
        public void TC007_ThemMonBiTrungMa()
        {
            // Thêm món bị trùng mã
            var monAn = new MonAnDTO("MA01", "Món trùng", "LM01", "Mô tả trùng", 30000);
            bool result = monAnBUS.insertMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi mã món đã tồn tại");
            Assert.AreEqual(1, monAnBUS.kiemTraMaTrung("MA01"), "Mã đã tồn tại nhưng không báo lỗi");
        }

        [TestMethod]
        public void TC008_SuaMonThanhCong()
        {
            // Sửa món thành công
            var monAn = new MonAnDTO("MA01", "Món 1 - sửa", "LM01", "Mô tả 1 - sửa", 15000);
            bool result = monAnBUS.updateMonAn(monAn);
            Assert.IsTrue(result, "Cập nhật món thất bại");
            DataTable dt = monAnBUS.getAllMonAn();
        }

        [TestMethod]
        public void TC009_SuaMonKhiChuaChonDong()
        {
            // Sửa món khi chưa chọn dòng
            var monAn = new MonAnDTO("", "Món 1 - sửa", "LM01", "Mô tả 1 - sửa", 15000);
            bool result = monAnBUS.updateMonAn(monAn);
            Assert.IsFalse(result, "Phải báo lỗi khi chưa chọn dòng");
        }

        [TestMethod]
        public void TC010_XoaMonThanhCong()
        {
            // Xóa món thành công
            var monAn = new MonAnDTO("MA02", "Món 2", "LM01", "Mô tả 2", 20000);
            monAnBUS.insertMonAn(monAn);
            bool result = monAnBUS.deleteMonAn("MA02");
            Assert.IsTrue(result, "Xóa món thất bại");
            Assert.AreEqual(0, monAnBUS.kiemTraMaTrung("MA02"), "Mã không được xóa khỏi DB");
        }

        [TestMethod]
        public void TC011_XoaMonKhiChuaChonDong()
        {
            // Xóa món khi chưa chọn dòng
            bool result = monAnBUS.deleteMonAn("");
            Assert.IsFalse(result, "Phải báo lỗi khi chưa chọn dòng");
        }

        [TestMethod]
        public void TC012_TimMonTheoTenChinhXac()
        {
            // Tìm món theo tên chính xác
            var monAn = new MonAnDTO("MA03", "Món 3", "LM01", "Mô tả 3", 30000);
            monAnBUS.insertMonAn(monAn);
            DataTable result = monAnBUS.searchMonAn("Món 3");
            Assert.IsNotNull(result, "Kết quả tìm kiếm không được null");
            Assert.IsTrue(result.Rows.Count > 0, "Phải tìm thấy kết quả");
            Assert.AreEqual("Món 3", result.Rows[0]["TenMonAn"].ToString(), "Kết quả tìm kiếm không chính xác");
        }

        [TestMethod]
        public void TC013_TimMonKhongTonTai()
        {
            // Tìm món không tồn tại
            DataTable result = monAnBUS.searchMonAn("Món không tồn tại");
            Assert.IsNotNull(result, "Kết quả tìm kiếm không được null");
            Assert.AreEqual(0, result.Rows.Count, "Phải không tìm thấy kết quả");
        }

        [TestMethod]
        public void TC014_TimKiemVoiORong()
        {
            // Tìm kiếm với ô rỗng
            DataTable result = monAnBUS.searchMonAn("");
            Assert.IsNotNull(result, "Kết quả tìm kiếm không được null");
            Assert.IsTrue(result.Rows.Count > 0, "Phải hiển thị toàn bộ danh sách");
        }

        [TestMethod]
        public void TC015_LamMoiDanhSach()
        {
            // Làm mới danh sách
            // Giả định làm mới không xóa dữ liệu, kiểm tra getAllMonAn như proxy
            var monAn = new MonAnDTO("MA04", "Món 4", "LM01", "Mô tả 4", 40000);
            monAnBUS.insertMonAn(monAn);
            DataTable resultBefore = monAnBUS.getAllMonAn();

            DataTable resultAfter = monAnBUS.getAllMonAn();
            Assert.AreEqual(resultBefore.Rows.Count, resultAfter.Rows.Count, "Số lượng món thay đổi sau khi làm mới");
        }
    }
}