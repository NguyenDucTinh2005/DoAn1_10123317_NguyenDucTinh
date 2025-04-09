
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
            LoadKhachHangList();
        }
        #region Loại món ăn 
        private void LoadLoaiMonList()
        {
            dtgvLoaiMon.DataSource = LoaiMonAnBus.getAllLoaiMonAn();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LoaiMonAnDTO loaiMonAnDTO = new LoaiMonAnDTO(txtMaLoaiMon.Text, txtTenLoaiMon.Text);
            if (LoaiMonAnBus.kiemTraMaTrung(txtMaLoaiMon.Text) == 1)
            {
                LoadLoaiMonList();
                LoadMonAnList();
                MessageBox.Show("Mã loại món đã tồn tại!", "Thông báo");
            }
            else
            {
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
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

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


        private void btnThemMon_Click(object sender, EventArgs e)
        {
            MonAnDTO monAnDTO = new MonAnDTO(txtMaMon.Text, txtTenMon.Text, cboLoaiMon.SelectedValue.ToString(), txtMoTa.Text, float.Parse(txtDonGia.Text));

            if (string.IsNullOrEmpty(cboLoaiMon.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn loại món!", "Thông báo");
            }
            else if (monan.kiemTraMaTrung(txtMaMon.Text) == 1)
            {
                MessageBox.Show("Mã món đã tồn tại!", "Thông báo");
            }
            else
            {
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
            MonAnDTO monAnDTO = new MonAnDTO(txtMaMon.Text, txtTenMon.Text, cboLoaiMon.SelectedValue.ToString(), txtMoTa.Text, float.Parse(txtDonGia.Text));
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
            if (monan.deleteMonAn(txtMaMon.Text))
            {
                MessageBox.Show("Xóa thành công!");
                LoadMonAnList();
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
            string keyword = txtTimKiemMon.Text;
            DataTable searchResult = monan.searchMonAn(keyword);
            if (searchResult.Rows.Count > 0)
            {
                dtgvMonAn.DataSource = searchResult;
            }
            else
            {
                MessageBox.Show(string.Format("Không tìm thấy món '{0}'!", keyword), "Thông báo");
            }

        }
        #endregion

        #region Khách hàng
        KhachHangBUS khachHangBUS = new KhachHangBUS();
        private void LoadKhachHangList()
        {
            dtgvDanhSachKhachHang.DataSource = khachHangBUS.getAllKhachHang();
        }
        private void btnTimKhach_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemKhach.Text;
            DataTable searchResult = khachHangBUS.searchKhachHang(keyword);
            if (searchResult.Rows.Count > 0)
            {
                dtgvDanhSachKhachHang.DataSource = searchResult;
            }
            else
            {
                MessageBox.Show(string.Format("Không tìm thấy khách hàng có tên là '{0}'!", keyword), "Thông báo");
            }
        }



        private void dtgvDanhSachKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvDanhSachKhachHang.Rows[e.RowIndex];
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            }
        }
        #endregion

        private void btnXoaKhach_Click(object sender, EventArgs e)
        {
            if(khachHangBUS.deleteKhachHang(txtMaKhachHang.Text))
            {
                MessageBox.Show("Xóa thành công!");
                LoadKhachHangList();
                txtMaKhachHang.Clear();
                txtTenKhachHang.Clear();
                txtSoDienThoai.Clear();
                txtDiaChi.Clear();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }
    }
}
