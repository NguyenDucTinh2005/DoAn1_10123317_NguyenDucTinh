using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn1
{
    public partial class frmLichSuDatHang : Form
    {
        public frmLichSuDatHang()
        {
            InitializeComponent();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            frmThongTinNhanHang f = new frmThongTinNhanHang();
            f.ShowDialog();
        }
    }
}
