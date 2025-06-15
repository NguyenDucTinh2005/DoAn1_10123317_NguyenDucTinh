using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DoAn1
{
    public partial class frmTrangChu : Form
    {
        private string tenDangNhap;
        private Form currenChildForm;
        private string quyenNguoiDung;
        private Button[] menuButtons;
        private Button currentButton = null; // Nút đang được chọn
    
        private string maKhachHang;
       
        private void OpenChildForm(Form childForm)
        {
            if (currenChildForm != null)
            {
                currenChildForm.Close();
            }
            currenChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public frmTrangChu(string quyen, string tenDangNhap, string maKhachHang)
        {
            InitializeComponent();
            this.quyenNguoiDung = quyen;
            this.tenDangNhap = tenDangNhap;
            this.maKhachHang = maKhachHang;
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            if (quyenNguoiDung == "Khách hàng")
            {
                btnDonHang.Visible = false;
                btnHeThong.Visible = false;
                btnDanhMuc.Visible = false;
                btnThongKe.Visible = false;
                btnBanHangTaiQuan.Visible = false;
            }
            if (quyenNguoiDung == "Nhân viên")
            {
                
                btnHeThong.Visible = false;
                
                btnThongKe.Visible = false;
            }
        }

        // Đổi màu nút đang chọn
        private void ActivateButton(Button senderBtn)
        {
            if (senderBtn != null)
            {
                ResetMenuButtonColors();
                currentButton = senderBtn;
                currentButton.BackColor = Color.Orange;
                currentButton.ForeColor = Color.White;
            }
        }

        // Reset màu các nút menu
        private void ResetMenuButtonColors()
        {
            foreach (var btn in menuButtons)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            menuButtons = new Button[] {
                btnDanhMuc, btnDatHang, btnDonHang, btnDonHangCuaBan,
                btnLichSuDatHang, btnBanHangTaiQuan, btnThongKe, btnHeThong, btnDangXuat
            };
        }

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDatHang);
            OpenChildForm(new frmDatHang(maKhachHang)); // Truyền maKhachHang
        }
        private void btnLichSuDatHang_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLichSuDatHang);
            OpenChildForm(new frmLichSuDatHang(quyenNguoiDung, maKhachHang));
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDanhMuc);
            OpenChildForm(new frmDanhMuc());
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDonHang);
            OpenChildForm(new frmDonHang());
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            ActivateButton(btnHeThong);
            OpenChildForm(new frmHeThong());
        }

        private void btnBanHangTaiQuan_Click(object sender, EventArgs e)
        {
            ActivateButton(btnBanHangTaiQuan);
            OpenChildForm(new frmBanHangTaiQuan());
        }

        private void btnDonHangCuaBan_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDonHangCuaBan);
            OpenChildForm(new frmDonHangCuaBan(quyenNguoiDung, maKhachHang));
        }




        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ActivateButton(btnThongKe);
            OpenChildForm(new frmThongKe());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                frmDangNhap frm = new frmDangNhap();
                frm.ShowDialog();
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
