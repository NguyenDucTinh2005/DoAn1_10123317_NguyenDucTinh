using DoAn1.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;
using System.Data.SqlClient;
using DTO;

namespace DoAn1
{
    public partial class frmDatHang : Form
    {
        
        private string selectedMonAn;
        private float selectedGia;
        private ListViewItem selectedItem;
        MonAnBUS gioHangBUS = new MonAnBUS();
        private string maKhachHang; // Thay tenDangNhap bằng maKhachHang
        public frmDatHang(string maKhachHang)
        {
            InitializeComponent();
            this.maKhachHang = maKhachHang;
            supperListDonHang();
            LoadMonAn();
            LoadLoaiMon();
            this.Load += frmDatHang_Load;
        }

        private void supperListDonHang()
        {
            lsvHoaDon.Clear();
            lsvHoaDon.View = View.Details;
            lsvHoaDon.FullRowSelect = true;
            lsvHoaDon.Columns.Add("Tên Món", 150);
            lsvHoaDon.Columns.Add("Giá", 100);
            lsvHoaDon.Columns.Add("Số Lượng", 100);
        }

        private void LoadMonAn()
        {
            dtgvDanhSachMon.DataSource = gioHangBUS.getAllMonAn();
        }

        private void LoadLoaiMon()
        {
            cboLoaiMon.DataSource = gioHangBUS.getMonAnByMaLoai();
            cboLoaiMon.DisplayMember = "TenLoaiMon";
            cboLoaiMon.ValueMember = "MaLoaiMon";
            DataTable dtLoaiMon = gioHangBUS.getMonAnByMaLoai();
            DataRow newRow = dtLoaiMon.NewRow();
            newRow["MaLoaiMon"] = DBNull.Value;
            newRow["TenLoaiMon"] = "--Tất cả--";
            dtLoaiMon.Rows.InsertAt(newRow, 0);
            cboLoaiMon.DataSource = dtLoaiMon;
            dtgvDanhSachMon.Columns["MoTa"].Width = 210;
        }

        private void btnDatMon_Click(object sender, EventArgs e)
        {
            if (lsvHoaDon.Items.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm món ăn vào giỏ hàng trước khi đặt hàng.", "Thông báo");
                return;
            }

            List<ListViewItem> cartItems = lsvHoaDon.Items.Cast<ListViewItem>().ToList();
            frmThongTinNhanHang f = new frmThongTinNhanHang(cartItems, maKhachHang); // Truyền maKhachHang
            f.ShowDialog();
        }

        private void AddMonAnToHoaDon(string tenMon, string gia, int soLuong)
        {
            bool daCoTrongGio = false;

            // Kiểm tra xem món ăn đã có trong giỏ hàng chưa
            foreach (ListViewItem item in lsvHoaDon.Items)
            {
                if (item.SubItems[0].Text == tenMon)
                {
                    // Nếu có, cộng dồn số lượng
                    int soLuongCu = int.Parse(item.SubItems[2].Text);
                    int soLuongMoi = soLuongCu + soLuong;
                    item.SubItems[2].Text = soLuongMoi.ToString();
                    daCoTrongGio = true;
                    break;
                }
            }

            // Nếu chưa có, thêm mới
            if (!daCoTrongGio)
            {
                ListViewItem item = new ListViewItem(tenMon);
                item.SubItems.Add(gia);
                item.SubItems.Add(soLuong.ToString());
                lsvHoaDon.Items.Add(item);
            }

            UpdateTongTien();
        }


        private void btnThemMon_Click(object sender, EventArgs e)
        {
            string tenMon = lblTenMon.Text;

            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Vui lòng chọn món ăn để thêm vào giỏ hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = (int)nmSoLuong.Value;

            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dtgvDanhSachMon.Rows)
            {
                if (row.Cells["TenMonAn"].Value.ToString() == tenMon)
                {
                    string gia = row.Cells["GiaBan"].Value.ToString();
                    AddMonAnToHoaDon(tenMon, gia, soLuong);
                    UpdateTongTien();  // Di chuyển vào đây để chỉ cập nhật khi thêm thành công
                    return;
                }
            }

            MessageBox.Show("Không tìm thấy món ăn trong danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyworld = txtTimKiem.Text;
            DataTable dataTable = gioHangBUS.searchMonAn(keyworld);
            if (string.IsNullOrEmpty(keyworld))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn cần tìm kiếm");
            }
            else
            {
                dtgvDanhSachMon.DataSource = dataTable;
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("Không tìm thấy loại món '{0}'!", keyworld), "Thông báo");
                }
                else
                {
                    dtgvDanhSachMon.DataSource = dataTable;
                }
            }
        }

        private void dtgvDanhSachMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvDanhSachMon.Rows[e.RowIndex];
                lblTenMon.Text = row.Cells["TenMonAn"].Value.ToString();
                selectedMonAn = row.Cells["TenMonAn"].Value.ToString();
                selectedGia = Convert.ToInt64(row.Cells["GiaBan"].Value);
            }
        }

        private void cboLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiMon.SelectedValue != null)
            {
                string maLoaiMon = cboLoaiMon.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(maLoaiMon))
                {
                    dtgvDanhSachMon.DataSource = gioHangBUS.getMonAnByLoai(maLoaiMon);
                }
                else
                {
                    LoadMonAn();
                }
            }
        }

        private void frmDatHang_Load(object sender, EventArgs e)
        {
            LoadMonAn();
        }

        private void lsvHoaDon_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                selectedItem = e.Item;
                nmSoLuong.Value = int.Parse(selectedItem.SubItems[2].Text);
                lblTenMon.Text = selectedItem.SubItems[0].Text;
            }
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            UpdateTongTien();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa.");
                return;
            }

            int soLuongHienTai = int.Parse(selectedItem.SubItems[2].Text);
            int soLuongGiam = Math.Abs((int)nmSoLuong.Value);

            if (soLuongHienTai > soLuongGiam)
            {
                int soLuongMoi = soLuongHienTai - soLuongGiam;
                selectedItem.SubItems[2].Text = soLuongMoi.ToString();
                float donGia = selectedGia;
                selectedItem.SubItems[1].Text = (soLuongMoi * donGia).ToString("N0");
            }
            else
            {
                lsvHoaDon.Items.Remove(selectedItem);
                selectedItem = null;
            }
            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            double tongTien = 0;
            foreach (ListViewItem item in lsvHoaDon.Items)
            {
                double gia = double.Parse(item.SubItems[1].Text);
                int soLuong = int.Parse(item.SubItems[2].Text);
                tongTien += gia * soLuong;
            }
            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
        }


       
    }
}
