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

namespace DoAn1
{
    public partial class frmDatHang : Form
    {
        private string selectedMonAn;
        private decimal selectedGia;
        private ListViewItem selectedItem;
        MonAnBUS gioHangBUS = new MonAnBUS();

        public frmDatHang()
        {
            InitializeComponent();
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

            frmThongTinNhanHang f = new frmThongTinNhanHang();
            f.ShowDialog();
        }

        private void AddMonAnToHoaDon(string tenMon, string gia, int soLuong)
        {
            ListViewItem item = new ListViewItem(tenMon);
            item.SubItems.Add(gia);
            item.SubItems.Add(soLuong.ToString());
            lsvHoaDon.Items.Add(item);
            UpdateTongTien();
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            string tenMon = lblTenMon.Text;
            if (!string.IsNullOrEmpty(tenMon))
            {
                foreach (DataGridViewRow row in dtgvDanhSachMon.Rows)
                {
                    if (row.Cells["TenMonAn"].Value.ToString() == tenMon)
                    {
                        string gia = row.Cells["GiaBan"].Value.ToString();
                        int soLuong = (int)nmSoLuong.Value; // Assuming you have a NumericUpDown control for quantity

                        AddMonAnToHoaDon(tenMon, gia, soLuong);
                        return;
                    }
                }
                MessageBox.Show("Không tìm thấy món ăn trong danh sách.");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để thêm vào giỏ hàng.");
            }
            UpdateTongTien();
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
                selectedGia = Convert.ToDecimal(row.Cells["GiaBan"].Value);
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
            // Không thay đổi số lượng trong ListView khi giá trị của NumericUpDown thay đổi
            UpdateTongTien(); // Cập nhật tổng tiền đơn hàng
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
                // Giảm số lượng theo giá trị của NumericUpDown và cập nhật lại giá tiền
                int soLuongMoi = soLuongHienTai - soLuongGiam;
                selectedItem.SubItems[2].Text = soLuongMoi.ToString();

                decimal donGia = selectedGia;
                selectedItem.SubItems[1].Text = (soLuongMoi * donGia).ToString("N0");
            }
            else
            {
                // Nếu số lượng mới <= 0, xóa món ăn khỏi ListView
                lsvHoaDon.Items.Remove(selectedItem);
                selectedItem = null;
            }

            UpdateTongTien(); // Cập nhật tổng tiền sau khi xóa
        }

        private void UpdateTongTien()
        {
            //if (ListView == null)
            //{
            //    MessageBox.Show("lblTongTien chưa được khởi tạo!", "Lỗi");
            //    return;
            //}

            decimal tongTien = 0;
            foreach (ListViewItem item in lsvHoaDon.Items)
            {
                decimal gia;
                int soLuong;

                // Kiểm tra nếu cột giá hoặc số lượng không hợp lệ
                if (!decimal.TryParse(item.SubItems[1].Text, out gia) || !int.TryParse(item.SubItems[2].Text, out soLuong))
                {
                    MessageBox.Show("Dữ liệu trong ListView không hợp lệ!", "Lỗi");
                    return;
                }

                tongTien += gia * soLuong;
            }

            lblTongTien.Text = tongTien.ToString("N0") + " VND"; // Hiển thị tổng tiền
        }
    }
}