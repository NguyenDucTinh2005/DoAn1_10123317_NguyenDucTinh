create DATABASE DoAn1
go
use DoAn1

create table TaiKhoan (
	MaTaiKhoan int identity(1,1),
    TenDangNhap NVARCHAR(50),
    MatKhau NVARCHAR(255) NOT NULL,
    SoDienThoai VARCHAR(15),
    Quyen NVARCHAR(50) default N'khách hàng'
);
ALTER TABLE TaiKhoan
ADD MaKhachHang CHAR(10) NULL,
FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang) ON DELETE SET NULL ON UPDATE CASCADE;



create TABLE KhachHang (
    MaKhachHang CHAR(10) PRIMARY KEY,
    TenKhachHang NVARCHAR(100) NOT NULL,
    SoDienThoai CHAR(10)Check  (SoDienThoai LIKE '[0][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    DiaChi NVARCHAR(100)
);





CREATE TABLE LoaiMonAn (
    MaLoaiMon CHAR(10) PRIMARY KEY,
    TenLoaiMon NVARCHAR(100) NOT NULL
);

CREATE TABLE MonAn (
    MaMonAn CHAR(10) PRIMARY KEY,
    TenMonAn NVARCHAR(100) NOT NULL,
    GiaBan FLOAT NOT NULL,
    MoTa NVARCHAR(300),
    MaLoaiMon CHAR(10),
    FOREIGN KEY (MaLoaiMon) REFERENCES LoaiMonAn(MaLoaiMon) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE NguyenLieu (
    MaNguyenLieu CHAR(10) PRIMARY KEY,
    TenNguyenLieu NVARCHAR(100) NOT NULL,
    DonViTinh NVARCHAR(20),           -- ví dụ: gram, ml, cái, lát
    SoLuongTon FLOAT NOT NULL         -- số lượng còn lại trong kho
);

CREATE TABLE ChiTietNguyenLieu (
    MaMonAn CHAR(10),
    MaNguyenLieu CHAR(10),
    SoLuongCan FLOAT NOT NULL,        -- mỗi phần cần bao nhiêu

    PRIMARY KEY (MaMonAn, MaNguyenLieu),
    FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn) ON DELETE CASCADE,
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu) ON DELETE CASCADE
);
CREATE TABLE DonHang (
    MaDonHang CHAR(10) PRIMARY KEY,
    NgayDat DATETIME NOT NULL,
    MaKhachHang CHAR(10),
	TrangThai Nvarchar(50),default N'Chưa xác nhận'
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE ChiTietDonHang (
    MaDonHang CHAR(10),
    MaMonAn CHAR(10),
    SoLuong INT NOT NULL,
    ThanhTien FLOAT NOT NULL,
    ThoiGianDat DATETIME NOT NULL,
    PRIMARY KEY (MaDonHang, MaMonAn),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE LichSuDatHang (
    MaLichSu CHAR(10) PRIMARY KEY,
    MaDonHang CHAR(10),
    NgayDat DATETIME,
    ThoiGianDat DATETIME,
    MaMonAn CHAR(10),
    TenMonAn NVARCHAR(100),
    SoLuong INT,
    ThanhTien FLOAT
);

ALTER TABLE LichSuDatHang
ADD CONSTRAINT FK_LichSuDatHang_DonHang
FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang);



CREATE TABLE ThongTinNhanHang (
    MaDonHang CHAR(10) PRIMARY KEY,
    TenNguoiNhan NVARCHAR(100) NOT NULL,
    SoDienThoai CHAR(10)Check  (SoDienThoai LIKE '[0][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    DiaChi NVARCHAR(100) NOT NULL,
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang) ON DELETE CASCADE
);


--Chen duwx lieeuj
--Bang Tai khoan
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, SoDienThoai, Quyen) 
VALUES 
('admin', 'adim', '0987654321', N'Quản trị viên'),
('Khachhang', 'Khachhang', '0912345678', N'Khách hàng'),
('nhanvien', 'nhanvien', '0977889900', N'Nhân viên');

Select*from TaiKhoan
delete from TaiKhoan
-- Chèn dữ liệu vào bảng LoaiMonAn
INSERT INTO LoaiMonAn (MaLoaiMon, TenLoaiMon) 
VALUES 
('LMA001', N'Đồ ăn nhanh'),
('LMA002', N'Đồ uống'),
('LMA003', N'Tráng miệng'),
('LMA004', N'Cơm & Mì'),
('LMA005', N'Gà rán');

select*from LoaiMonAn


-- Chèn dữ liệu vào bảng MonAn
INSERT INTO MonAn (MaMonAn, TenMonAn, GiaBan, MoTa, MaLoaiMon) 
VALUES 
-- Đồ ăn nhanh
('MA001', N'Hamburger bò', 45000, N'Bánh mì kẹp bò với rau và sốt đặc biệt', 'LMA001'),
('MA002', N'Pizza hải sản', 120000, N'Pizza phô mai với tôm, mực và cua', 'LMA001'),
('MA003', N'Khoai tây chiên', 30000, N'Khoai tây chiên giòn', 'LMA001'),
('MA004', N'Bánh mì pate', 25000, N'Bánh mì kẹp pate, chả, dưa chuột', 'LMA001'),
('MA005', N'Hotdog', 35000, N'Bánh mì xúc xích kèm sốt cà chua', 'LMA001'),
('MA006', N'Burger gà', 40000, N'Bánh mì kẹp gà giòn với sốt đặc biệt', 'LMA001'),
('MA007', N'Bánh sandwich', 38000, N'Bánh sandwich kẹp thịt nguội, trứng', 'LMA001'),
('MA008', N'Pizza gà BBQ', 130000, N'Pizza gà nướng BBQ đậm vị', 'LMA001'),
('MA009', N'Mì Ý bò bằm', 90000, N'Mì Ý sốt bò bằm đậm đà', 'LMA001'),
('MA010', N'Taco bò', 50000, N'Vỏ bánh taco kẹp thịt bò và rau củ', 'LMA001'),

-- Đồ uống
('MA011', N'Trà sữa trân châu', 40000, N'Trà sữa với trân châu đường đen', 'LMA002'),
('MA012', N'Nước ép cam', 35000, N'Nước ép cam tươi nguyên chất', 'LMA002'),
('MA013', N'Sinh tố bơ', 45000, N'Sinh tố bơ sánh mịn', 'LMA002'),
('MA014', N'Cà phê sữa đá', 30000, N'Cà phê pha phin sữa đá thơm ngon', 'LMA002'),
('MA015', N'Nước ép dứa', 32000, N'Nước ép dứa tươi', 'LMA002'),
('MA016', N'Matcha đá xay', 50000, N'Matcha đá xay thơm béo', 'LMA002'),
('MA017', N'Sinh tố xoài', 40000, N'Sinh tố xoài ngọt mát', 'LMA002'),
('MA018', N'Nước ngọt có ga', 20000, N'Coca-Cola, Pepsi, 7Up...', 'LMA002'),
('MA019', N'Chanh dây đá xay', 45000, N'Chanh dây xay mát lạnh', 'LMA002'),
('MA020', N'Trà đào cam sả', 50000, N'Trà đào kết hợp cam sả thơm ngon', 'LMA002'),

-- Tráng miệng
('MA021', N'Kem dâu', 25000, N'Kem dâu tươi mát lạnh', 'LMA003'),
('MA022', N'Bánh flan', 20000, N'Bánh flan mềm mịn với caramel', 'LMA003'),
('MA023', N'Chè khúc bạch', 35000, N'Chè khúc bạch hạnh nhân thơm ngon', 'LMA003'),
('MA024', N'Bánh tiramisu', 50000, N'Bánh tiramisu ngọt béo', 'LMA003'),
('MA025', N'Sữa chua nếp cẩm', 30000, N'Sữa chua kết hợp nếp cẩm dẻo', 'LMA003'),
('MA026', N'Bánh donut', 28000, N'Bánh donut nhiều vị', 'LMA003'),
('MA027', N'Kem socola', 27000, N'Kem vị socola đậm đà', 'LMA003'),
('MA028', N'Chè trôi nước', 32000, N'Chè trôi nước nhân đậu xanh', 'LMA003'),
('MA029', N'Bánh mousse chanh dây', 45000, N'Bánh mousse vị chanh dây chua nhẹ', 'LMA003'),
('MA030', N'Bánh crepe sầu riêng', 55000, N'Bánh crepe kèm nhân sầu riêng', 'LMA003'),

-- Cơm & Mì
('MA031', N'Cơm tấm sườn bì chả', 60000, N'Cơm tấm ăn kèm sườn nướng, bì, chả', 'LMA004'),
('MA032', N'Cơm chiên dương châu', 50000, N'Cơm chiên trứng, lạp xưởng, tôm', 'LMA004'),
('MA033', N'Cơm gà xối mỡ', 55000, N'Cơm gà chiên giòn với nước sốt', 'LMA004'),
('MA034', N'Mì xào bò', 45000, N'Mì xào bò rau cải xanh', 'LMA004'),
('MA035', N'Hủ tiếu Nam Vang', 65000, N'Hủ tiếu nước ngọt thanh', 'LMA004'),
('MA036', N'Bún bò Huế', 70000, N'Bún bò Huế cay nồng', 'LMA004'),
('MA037', N'Bánh canh cua', 75000, N'Bánh canh nước dùng đậm đà', 'LMA004'),
('MA038', N'Phở bò', 60000, N'Phở bò tái, nạm, gầu', 'LMA004'),
('MA039', N'Cơm cá hồi sốt teriyaki', 80000, N'Cơm cá hồi nướng sốt teriyaki', 'LMA004'),
('MA040', N'Cơm rang kim chi', 55000, N'Cơm rang kim chi cay ngon', 'LMA004'),

-- Gà rán
('MA041', N'Gà rán truyền thống', 40000, N'Gà rán giòn cay hấp dẫn', 'LMA005'),
('MA042', N'Gà rán sốt mật ong', 45000, N'Gà rán phủ sốt mật ong ngọt', 'LMA005'),
('MA043', N'Gà viên chiên', 35000, N'Gà viên chiên giòn rụm', 'LMA005'),
('MA044', N'Gà xào cay Hàn Quốc', 50000, N'Gà xào cay vị Hàn Quốc', 'LMA005'),
('MA045', N'Gà nướng BBQ', 70000, N'Gà nướng sốt BBQ', 'LMA005'),
('MA046', N'Gà không xương lắc phô mai', 55000, N'Gà rán không xương phủ phô mai', 'LMA005'),
('MA047', N'Cánh gà chiên nước mắm', 45000, N'Cánh gà chiên nước mắm tỏi', 'LMA005'),
('MA048', N'Gà rán sốt cay', 50000, N'Gà rán giòn cay Hàn Quốc', 'LMA005'),
('MA049', N'Gà popcorn', 40000, N'Gà popcorn nhỏ nhưng giòn', 'LMA005'),
('MA050', N'Combo gà rán', 120000, N'Combo gồm 5 miếng gà rán', 'LMA005');

SELECT*FROM MonAn

Select*from khachhang
Select*from TaiKhoan

select*from  DonHang


-- Chèn dữ liệu vào bảng KhachHang
INSERT INTO KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiaChi)
VALUES
('KH001', N'Nguyễn Văn A', '0912345678', N'123 Đường Lê Lợi, Quận 1, TP.HCM'),
('KH002', N'Trần Thị B', '0987654321', N'456 Đường Trần Hưng Đạo, Quận 5, TP.HCM'),
('KH003', N'Lê Văn C', '0901234567', N'789 Đường Nguyễn Huệ, Quận 1, TP.HCM');
select*from khachhang
delete from KhachHang
-- Chèn dữ liệu vào bảng DonHang
INSERT INTO DonHang (MaDonHang, NgayDat, MaKhachHang)
VALUES
('DH004', '2025-04-25 10:00:00', 'KH001'),
('DH002', '2025-04-26 14:30:00', 'KH002'),
('DH003', '2025-04-26 19:00:00', 'KH003');
delete from DonHang
-- Chèn dữ liệu vào bảng ChiTietDonHang
INSERT INTO ChiTietDonHang (MaDonHang, MaMonAn, SoLuong, ThanhTien, ThoiGianDat)
VALUES
('DH004', 'MA001', 2, 90000, '2025-04-25 10:05:00'),  -- Hamburger bò
('DH003', 'MA011', 1, 40000, '2025-04-25 10:06:00'),  -- Trà sữa trân châu

('DH002', 'MA031', 1, 60000, '2025-04-26 14:35:00'),  -- Cơm tấm sườn bì chả
('DH002', 'MA018', 2, 40000, '2025-04-26 14:36:00'),  -- Nước ngọt có ga

('DH003', 'MA045', 1, 70000, '2025-04-26 19:05:00'),  -- Gà nướng BBQ
('DH003', 'MA024', 2, 100000, '2025-04-26 19:10:00'); -- Bánh tiramisu
delete from ChiTietDonHang
CREATE TRIGGER trg_XoaKhachHangKhiXoaDonHang
ON DonHang
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Xóa khách hàng nếu không còn đơn hàng nào khác
    DELETE FROM KhachHang
    WHERE MaKhachHang IN (
        SELECT d.MaKhachHang
        FROM deleted d
        WHERE NOT EXISTS (
            SELECT 1
            FROM DonHang dh
            WHERE dh.MaKhachHang = d.MaKhachHang
        )
    );
END;

SELECT 
    dh.MaDonHang,
    dh.NgayDat,
    kh.MaKhachHang,
    kh.TenKhachHang,
    kh.SoDienThoai,
    ma.MaMonAn,
    ma.TenMonAn,
    ctdh.SoLuong,
    ctdh.ThanhTien,
    ctdh.ThoiGianDat
FROM 
    DonHang dh
INNER JOIN 
    KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
INNER JOIN 
    ChiTietDonHang ctdh ON dh.MaDonHang = ctdh.MaDonHang
INNER JOIN 
    MonAn ma ON ctdh.MaMonAn = ma.MaMonAn
ORDER BY 
    dh.NgayDat DESC, ctdh.ThoiGianDat DESC;




CREATE PROCEDURE SP_XoaLichSuDatHang
    @MaDonHang CHAR(10)
AS
BEGIN
    DELETE FROM LichSuDatHang WHERE MaDonHang = @MaDonHang;
END;





