using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using System.Data.SqlClient;
using System.Collections;
using DAL;

namespace DoAn1
{
    public partial class frmHoaDonTQ : Form
    {
        public frmHoaDonTQ()
        {
            InitializeComponent();
        }
        private List<HoaDonDTO>hoaDon;
        private float tongTien;

        public frmHoaDonTQ(List<HoaDonDTO> hoaDon, float tongTien)
        {
            InitializeComponent();
            this.hoaDon = hoaDon;
            this.tongTien = tongTien;
        }

        private void frmHoaDonTQ_Load(object sender, EventArgs e)
        {

        }
    }
}
