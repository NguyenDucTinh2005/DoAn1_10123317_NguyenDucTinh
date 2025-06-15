
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn1.DTO;
using System.Collections;
using BUS;
using System.Data.SqlClient;
using DAL;
using DTO;
using DTO.DTO;

namespace DoAn1
{
    public partial class frmDanhMuc : Form
    {
        LoaiMonAnBUS LoaiMonAnBus = new LoaiMonAnBUS();

        public frmDanhMuc()
        {
            InitializeComponent();
            LoadLoaiMonList();
            LoadMonAnList();
            LoadNguyenLieuList();
            LoadMonAnListByNguyenLieu();
            LoadMonAnList2();
            LoadNguyenLieuList2();

            // Cấu hình cột cho ListView
            lsvCongThucNau.View = View.Details; // Đặt chế độ hiển thị chi tiết
            lsvCongThucNau.Columns.Add("Tên Nguyên Liệu", 150); // Cột 1: Tên Nguyên Liệu
            lsvCongThucNau.Columns.Add("Số Lượng Cần", 140); // Cột 2: Số Lượng
            lsvCongThucNau.Columns.Add("Đơn Vị Tính", 140); // Cột 3: Đơn Vị Tính
            lsvCongThucNau.Font = new Font("Arial", lsvCongThucNau.Font.Size, FontStyle.Regular);
            dtgvMonAn2.Font = new Font("Arial", dtgvMonAn2.Font.Size, FontStyle.Regular);
            dtgvNguyenLieu2.Font = new Font("Arial", dtgvNguyenLieu2.Font.Size, FontStyle.Regular);
            cboLoaiMon2.Font = new Font("Arial", cboLoaiMon2.Font.Size, FontStyle.Regular);
        }
        #region-------------------------------- Loại món ăn 
        private void LoadLoaiMonList()
        {
            dtgvLoaiMon.DataSource = LoaiMonAnBus.getAllLoaiMonAn();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoaiMon.Text.Trim();
            string tenLoai = txtTenLoaiMon.Text.Trim();

            if (string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Vui lòng nhập mã loại món!", "Thông báo");
                return;
            }

            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại món!", "Thông báo");
                return;
            }

            if (LoaiMonAnBus.kiemTraMaTrung(maLoai) == 1)
            {
                MessageBox.Show("Mã loại món đã tồn tại!", "Thông báo");
                return;
            }

            LoaiMonAnDTO loaiMonAnDTO = new LoaiMonAnDTO(maLoai, tenLoai);

            if (LoaiMonAnBus.insertLoaiMonAn(loaiMonAnDTO))
            {
                MessageBox.Show("Thêm thành công!");
                LoadLoaiMonList();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiMon.Text))
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo");
                return;
            }

            if (LoaiMonAnBus.deleteLoaiMonAn(txtMaLoaiMon.Text))
            {
                MessageBox.Show("Xóa thành công!");
                LoadLoaiMonList();
                txtMaLoaiMon.Text = "";
                txtTenLoaiMon.Text = "";
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiMon.Text))
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo");
                return;
            }

            LoaiMonAnDTO loaiMonAnDTO = new LoaiMonAnDTO(txtMaLoaiMon.Text, txtTenLoaiMon.Text);

            if (LoaiMonAnBus.updateLoaiMonAn(loaiMonAnDTO))
            {
                MessageBox.Show("Sửa thành công!");
                LoadLoaiMonList();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string keyword = txtTimKiem.Text;
            DataTable searchResult = LoaiMonAnBus.searchLoaiMonAn(keyword);

            if (searchResult.Rows.Count > 0)
            {
                dtgvLoaiMon.DataSource = searchResult;

            }
            else
            {
                MessageBox.Show(string.Format("Không tìm thấy loại món '{0}'!", keyword), "Thông báo");
            }

        }

        private void dtgvLoaiMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvLoaiMon.Rows[e.RowIndex];
                txtMaLoaiMon.Text = row.Cells[0].Value.ToString();
                txtTenLoaiMon.Text = row.Cells[1].Value.ToString();
            }
        }

        private void btnLamMoiLoaiMon_Click(object sender, EventArgs e)
        {
            txtMaLoaiMon.Clear();
            txtTenLoaiMon.Clear();
            LoadLoaiMonList();
        }
        #endregion

        #region -----------------MÓN ĂN-----------------
        MonAnBUS monan = new MonAnBUS();
        private void LoadMonAnList()
        {
            dtgvMonAn.DataSource = monan.getAllMonAn();

            DataTable dtLoaiMon = monan.getMonAnByMaLoai();
            DataRow newRow = dtLoaiMon.NewRow();
            newRow["MaLoaiMon"] = DBNull.Value;
            newRow["TenLoaiMon"] = "--Chọn loại món--";
            dtLoaiMon.Rows.InsertAt(newRow, 0);

            cboLoaiMon.DataSource = dtLoaiMon;
            cboLoaiMon.DisplayMember = "TenLoaiMon";
            cboLoaiMon.ValueMember = "MaLoaiMon";
            cboLoaiMon.SelectedIndex = 0;

            dtgvMonAn.Columns["MoTa"].Width = 210;
        }
        private void btnLamMoiMon_Click(object sender, EventArgs e)
        {
            txtMaMon.Clear();
            txtTenMon.Clear();
            txtDonGia.Clear();
            txtMoTa.Clear();
            LoadMonAnList();
        }

        private bool ValidateForm()
        {
            errorProvider1.Clear();
            bool isValid = true;

            // Danh sách các trường cần kiểm tra rỗng
            var requiredFields = new Dictionary<Control, string>
    {
        { txtMaMon, "Vui lòng nhập mã món!" },
        { txtTenMon, "Vui lòng nhập tên món!" },
        { cboLoaiMon, "Vui lòng chọn loại món!" },
        { txtDonGia, "Vui lòng nhập đơn giá!" }
    };

            foreach (var field in requiredFields)
            {
                if (string.IsNullOrWhiteSpace(field.Key.Text))
                {
                    errorProvider1.SetError(field.Key, field.Value);
                    isValid = false;
                }
            }

            // Kiểm tra đơn giá có phải là số và > 0
            if (!float.TryParse(txtDonGia.Text, out float donGia) || donGia <= 0)
            {
                errorProvider1.SetError(txtDonGia, "Đơn giá phải là số lớn hơn 0!");
                isValid = false;
            }

            // Kiểm tra mã món trùng
            if (monan.kiemTraMaTrung(txtMaMon.Text) == 1)
            {
                errorProvider1.SetError(txtMaMon, "Mã món đã tồn tại!");
                isValid = false;
            }

            return isValid;
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            float donGia = float.Parse(txtDonGia.Text);
            MonAnDTO monAnDTO = new MonAnDTO(
                txtMaMon.Text,
                txtTenMon.Text,
                cboLoaiMon.SelectedValue.ToString(),
                txtMoTa.Text,
                donGia
            );

            if (monan.insertMonAn(monAnDTO))
            {
                MessageBox.Show("Thêm thành công!");
                LoadMonAnList();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void dtgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvMonAn.Rows[e.RowIndex];
                txtMaMon.Text = row.Cells["MaMonAn"].Value.ToString();
                txtTenMon.Text = row.Cells["TenMonAn"].Value.ToString();
                cboLoaiMon.SelectedValue = row.Cells["MaLoaiMon"].Value.ToString();
                txtDonGia.Text = row.Cells["GiaBan"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            }
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng chưa chọn món để sửa (txtMaMon rỗng hoặc không hợp lệ)
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn món cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra dữ liệu đầu vào nếu muốn dùng lại hàm ValidateForm()
            if (!ValidateForm()) return;

            float donGia = float.Parse(txtDonGia.Text);
            MonAnDTO monAnDTO = new MonAnDTO(
                txtMaMon.Text,
                txtTenMon.Text,
                cboLoaiMon.SelectedValue.ToString(),
                txtMoTa.Text,
                donGia
            );

            if (monan.updateMonAn(monAnDTO))
            {
                MessageBox.Show("Sửa thành công!");
                LoadMonAnList();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }


        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa chọn món (mã món rỗng)
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi lại người dùng để xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa món này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            // Thực hiện xóa
            if (monan.deleteMonAn(txtMaMon.Text))
            {
                MessageBox.Show("Xóa thành công!");
                LoadMonAnList();

                // Xóa trắng các trường nhập
                txtMaMon.Clear();
                txtTenMon.Clear();
                txtDonGia.Clear();
                txtMoTa.Clear();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }


        private void btnTimMon_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemMon.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable searchResult = monan.searchMonAn(keyword);
            if (searchResult.Rows.Count > 0)
            {
                dtgvMonAn.DataSource = searchResult;
            }
            else
            {
                MessageBox.Show(string.Format("Không tìm thấy món '{0}'!", keyword), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
        #region ----------------------Nguyeen lieu
        NguyenLieuBUS nguyenieuBUS = new NguyenLieuBUS();
        private void LoadNguyenLieuList()
        {
            dtgvNguyenLieu.DataSource = nguyenieuBUS.getAllNguyenLieu();

            dtgvNguyenLieu.Columns["MaNguyenLieu"].HeaderText = "Mã Nguyên Liệu";
            dtgvNguyenLieu.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
            dtgvNguyenLieu.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dtgvNguyenLieu.Columns["SoLuongTon"].HeaderText = "Số Lượng";
        }
        private void btnTimKhach_Click(object sender, EventArgs e)
        {
            string keyworld = txtTimNguyenLieu.Text.Trim();
            if (string.IsNullOrEmpty(keyworld))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable searchResult = nguyenieuBUS.searchNguyenLieu(keyworld);
            dtgvNguyenLieu.DataSource = searchResult;

        }



        private void dtgvDanhSachKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvNguyenLieu.Rows[e.RowIndex];
                txtMaNguyenLieu.Text = row.Cells["MaNguyenLieu"].Value.ToString();
                txtTenNguyenLieu.Text = row.Cells["TenNguyenLieu"].Value.ToString();
                txtDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();
            }
        }


        private void btnXoaKhach_Click(object sender, EventArgs e)
        {
            if (nguyenieuBUS.DeleteNguyenLieu(txtMaNguyenLieu.Text))
            {
                // Hỏi người dùng xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                MessageBox.Show("Xóa thành công!");
                LoadNguyenLieuList();
                txtMaNguyenLieu.Clear();
                txtTenNguyenLieu.Clear();
                txtDonViTinh.Clear();
                txtSoLuongTon.Clear();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        private void btnThemNguyenLieu_Click(object sender, EventArgs e)
        {
            NguyenLieuDTO nguyenLieuDTO = new NguyenLieuDTO(
                txtMaNguyenLieu.Text,
                txtTenNguyenLieu.Text,
                txtDonViTinh.Text,
                float.Parse(txtSoLuongTon.Text)
            );
            if (nguyenieuBUS.InsertNguyenLieu(nguyenLieuDTO))
            {
                MessageBox.Show("Thêm thành công!");
                LoadNguyenLieuList();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnSuaNguyenLieu_Click(object sender, EventArgs e)
        {
            NguyenLieuDTO nguyenLieuDTO = new NguyenLieuDTO(
                txtMaNguyenLieu.Text,
                txtTenNguyenLieu.Text,
                txtDonViTinh.Text,
                float.Parse(txtSoLuongTon.Text)
            );
            if (nguyenieuBUS.UpdateNguyenLieu(nguyenLieuDTO))
            {
                MessageBox.Show("Sửa thành công!");
                LoadNguyenLieuList();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }

        private void btnLamMoiNguyenLieu_Click(object sender, EventArgs e)
        {
            txtMaNguyenLieu.Text = "";
            txtTenNguyenLieu.Text = "";
            txtDonViTinh.Text = "";
            txtSoLuongTon.Text = "";
        }
        #endregion
        #region Cong Thức Nấu Món Ăn
        // NGUYEEN LIEEUJ
        ChiTietNguyenLieuBUS chiTietNguyenLieu = new ChiTietNguyenLieuBUS();
        private void lsvCongThucNau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvCongThucNau.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lsvCongThucNau.SelectedItems[0];
                lblNguyenLieu.Text = selectedItem.Text; // Cột 1: Tên Nguyên Liệu
                lblDonViTinh.Text = selectedItem.SubItems[2].Text; // Cột 3: Đơn Vị Tính
            }
        }
        private void LoadNguyenLieuList2()
        {
            dtgvNguyenLieu2.DataSource = nguyenieuBUS.getAllNguyenLieu();

            dtgvNguyenLieu2.Columns["MaNguyenLieu"].HeaderText = "Mã Nguyên Liệu";
            dtgvNguyenLieu2.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
            dtgvNguyenLieu2.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dtgvNguyenLieu2.Columns["SoLuongTon"].HeaderText = "Số Lượng";
        }
        private void LoadMonAnList2()
        {
            dtgvMonAn.DataSource = monan.getAllMonAn();

            DataTable dtLoaiMon = monan.getMonAnByMaLoai();
            DataRow newRow = dtLoaiMon.NewRow();
            newRow["MaLoaiMon"] = DBNull.Value;
            newRow["TenLoaiMon"] = "--Chọn loại món--";
            dtLoaiMon.Rows.InsertAt(newRow, 0);

            cboLoaiMon2.DataSource = dtLoaiMon;
            cboLoaiMon2.DisplayMember = "TenLoaiMon";
            cboLoaiMon2.ValueMember = "MaLoaiMon";
            cboLoaiMon2.SelectedIndex = 0;

            dtgvMonAn2.Columns["MoTa"].Width = 210;
        }
        private void LoadMonAnListByNguyenLieu()
        {
            dtgvMonAn2.DataSource = monan.getAllMonAn();
        }
        
        private void btnTimKiemMonAn_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemMonAn.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable searchResult = monan.searchMonAn(keyword);
            if (searchResult.Rows.Count > 0)
            {
                dtgvMonAn2.DataSource = searchResult;
            }
            else
            {
                MessageBox.Show(string.Format("Không tìm thấy món '{0}'!", keyword), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboLoaiMon2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiMon2.SelectedValue != null)
            {
                string maLoaiMon = cboLoaiMon2.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(maLoaiMon) && maLoaiMon != "System.DBNull")
                {
                    dtgvMonAn2.DataSource = monan.getMonAnByLoai(maLoaiMon);
                }
                else
                {
                    LoadMonAnListByNguyenLieu();
                }
            }
        }
        private void LoadCongThucLenListView()
        {
            lsvCongThucNau.Items.Clear();

            if (dtgvMonAn2.CurrentRow != null)
            {
                string maMonAn = dtgvMonAn2.Rows[dtgvMonAn2.CurrentRow.Index].Cells["MaMonAn"].Value?.ToString();
                if (!string.IsNullOrEmpty(maMonAn))
                {
                    List<ChiTietNguyenLieuDTO> ds = chiTietNguyenLieu.GetNguyenLieuTheoMonAn(maMonAn);

                    foreach (var ct in ds)
                    {
                        // Lấy thông tin nguyên liệu từ NguyenLieuBUS
                        NguyenLieuDTO nguyenLieu = nguyenieuBUS.getNguyenLieuByMa(ct.MaNguyenLieu);
                        string tenNguyenLieu = nguyenLieu != null ? nguyenLieu.TenNguyenLieu : "Không xác định";
                        string donViTinh = nguyenLieu != null ? nguyenLieu.DonViTinh : "";

                        ListViewItem item = new ListViewItem(tenNguyenLieu); // Cột 1: Tên Nguyên Liệu
                        item.SubItems.Add(ct.SoLuongCan.ToString()); // Cột 2: Số Lượng
                        item.SubItems.Add(donViTinh); // Cột 3: Đơn Vị Tính
                        lsvCongThucNau.Items.Add(item);
                    }
                }
            }
        }
        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            LoadMonAnListByNguyenLieu();
            if (dtgvMonAn2.Rows.Count > 0)
            {
                dtgvMonAn2.Rows[0].Selected = true; // Chọn dòng đầu tiên của dtgvMonAn2
                dtgvMonAn2_CellClick(dtgvMonAn2, new DataGridViewCellEventArgs(0, 0)); // Kích hoạt sự kiện CellClick
            }
            if (dtgvNguyenLieu2.Rows.Count > 0)
            {
                dtgvNguyenLieu2.Rows[0].Selected = true; // Chọn dòng đầu tiên của dtgvNguyenLieu2
                dtgvNguyenLieu2_CellClick(dtgvNguyenLieu2, new DataGridViewCellEventArgs(0, 0)); // Kích hoạt sự kiện CellClick
            }
            dtgvMonAn2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Điều chỉnh theo nội dung
            dtgvNguyenLieu2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            LoadCongThucLenListView(); // Nạp công thức dựa trên dòng được chọn
                                       // Tự động điều chỉnh độ rộng cột
           
        }

        private void dtgvMonAn2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvMonAn2.Rows[e.RowIndex];
                lblTenMonAn.Text = row.Cells["TenMonAn"].Value?.ToString() ?? "";
                LoadCongThucLenListView(); // Cập nhật ListView khi chọn món
            }
        }

        private void dtgvNguyenLieu2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dtgvNguyenLieu2.Rows[e.RowIndex];
        lblNguyenLieu.Text = row.Cells["TenNguyenLieu"].Value?.ToString() ?? "";
        lblDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString() ?? ""; // Hiển thị DonViTinh
    }
        }

        private void btnTimKiemNguyenLieu_Click(object sender, EventArgs e)
        {
            string keyworld = txtTimNguyenieu.Text.Trim();
            if (string.IsNullOrEmpty(keyworld))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DataTable searchResult = nguyenieuBUS.searchNguyenLieu(keyworld);
            dtgvNguyenLieu2.DataSource = searchResult;
        }


        private void btnThemCongThuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTenMonAn.Text) || string.IsNullOrEmpty(lblNguyenLieu.Text) || string.IsNullOrEmpty(nmSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn món ăn, nguyên liệu và nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChiTietNguyenLieuDTO ct = new ChiTietNguyenLieuDTO
            {
                MaMonAn = dtgvMonAn2.Rows[dtgvMonAn2.CurrentRow.Index].Cells["MaMonAn"].Value.ToString(),
                MaNguyenLieu = dtgvNguyenLieu2.Rows[dtgvNguyenLieu2.CurrentRow.Index].Cells["MaNguyenLieu"].Value.ToString(),
                SoLuongCan = float.Parse(nmSoLuong.Text)
            };


            if (chiTietNguyenLieu.InsertChiTietNguyenLieu(ct))
            {
                MessageBox.Show("Thêm công thức thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCongThucLenListView(); // Cập nhật danh sách công thức
            }
            else
            {
                MessageBox.Show("Thêm công thức thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnXoaCongThuc_Click(object sender, EventArgs e)
        {
            if (dtgvMonAn2.CurrentRow == null || dtgvNguyenLieu2.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn và nguyên liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMonAn = dtgvMonAn2.Rows[dtgvMonAn2.CurrentRow.Index].Cells["MaMonAn"].Value.ToString();
            string maNguyenLieu = dtgvNguyenLieu2.Rows[dtgvNguyenLieu2.CurrentRow.Index].Cells["MaNguyenLieu"].Value.ToString();

            // Xác nhận xóa
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa công thức này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            if (chiTietNguyenLieu.Delete(maMonAn, maNguyenLieu))
            {
                MessageBox.Show("Xóa công thức thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCongThucLenListView();
            }
            else
            {
                MessageBox.Show("Xóa công thức thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaCongThuc_Click(object sender, EventArgs e)
        {
            if (dtgvMonAn2.CurrentRow == null || dtgvNguyenLieu2.CurrentRow == null || string.IsNullOrEmpty(nmSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn món ăn, nguyên liệu và nhập số lượng mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMonAn = dtgvMonAn2.Rows[dtgvMonAn2.CurrentRow.Index].Cells["MaMonAn"].Value.ToString();
            string maNguyenLieu = dtgvNguyenLieu2.Rows[dtgvNguyenLieu2.CurrentRow.Index].Cells["MaNguyenLieu"].Value.ToString();

            if (!float.TryParse(nmSoLuong.Text, out float soLuongCan))
            {
                MessageBox.Show("Số lượng không hợp lệ! Vui lòng nhập số hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChiTietNguyenLieuDTO ct = new ChiTietNguyenLieuDTO
            {
                MaMonAn = maMonAn,
                MaNguyenLieu = maNguyenLieu,
                SoLuongCan = soLuongCan
            };

            if (chiTietNguyenLieu.Update(ct))
            {
                MessageBox.Show("Cập nhật số lượng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCongThucLenListView();
            }
            else
            {
                MessageBox.Show("Cập nhật số lượng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
          
            MessageBox.Show("Lưu công thức thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCongThucLenListView(); // Cập nhật lại giao diện
        }
        #endregion

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
