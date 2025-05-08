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
        private Form currenChildForm;
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
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDatHang());
        }

        private void btnLichSuDatHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLichSuDatHang());
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDanhMuc());
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDonHang());
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHeThong());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBanHangTaiQuan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBanHangTaiQuan());
        }
    }
}
