using BUS;
using DoAn1.HoaDonTableAdapters;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace DoAn1
{
    public partial class frmHoaDon : Form
    {
        private string maDonHang;
        HoaDonBUS hoaDonBUS = new HoaDonBUS();
        

        public frmHoaDon(string maDonHang)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            DataTable1TableAdapter adapter = new DataTable1TableAdapter();
            DataTable dt = adapter.GetData(maDonHang);
         


            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            reportViewer1.LocalReport.ReportPath = "rptHoaDon.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

          
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void btnIN_Click(object sender, EventArgs e)
        {
            reportViewer1.PrintDialog();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
