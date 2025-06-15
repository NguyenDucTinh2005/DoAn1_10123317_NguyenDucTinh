using BUS;
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

namespace DoAn1
{
    public partial class frmHoaDonTaiQuan : Form
    {
        private string maDonHang;
        private ChiTietDonHangBUS chiTietDonHangBUS = new ChiTietDonHangBUS();

        public frmHoaDonTaiQuan(string maDonHang)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
        }

        private void frmHoaDonTaiQuan_Load(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu chi tiết đơn hàng
                DataTable dt = chiTietDonHangBUS.getChiTietDonHangByMaDonHang(maDonHang);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Kiểm tra và xử lý dữ liệu
                if (dt.Columns.Contains("TongTien") && dt.Rows[0]["TongTien"] != DBNull.Value)
                {
                    double tongTien = Convert.ToDouble(dt.Rows[0]["TongTien"]);
                    dt.Columns["TongTien"].ReadOnly = false; // Đảm bảo cột có thể chỉnh sửa nếu cần
                    foreach (DataRow row in dt.Rows)
                    {
                        row["TongTien"] = tongTien; // Gán tổng tiền cho mỗi hàng (nếu cần)
                    }
                }
                else
                {
                    double tongTien = dt.AsEnumerable().Sum(row => row.Field<double>("ThanhTien"));
                    dt.Columns.Add("TongTien", typeof(double)).SetOrdinal(dt.Columns.Count - 1);
                    foreach (DataRow row in dt.Rows)
                    {
                        row["TongTien"] = tongTien;
                    }
                }

                // Thiết lập báo cáo
                reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo ReportDataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Thêm các tham số cho báo cáo
                string tongTienStr = dt.Rows[0]["TongTien"] != DBNull.Value ? Convert.ToDouble(dt.Rows[0]["TongTien"]).ToString("N0") + " VNĐ" : "0 VNĐ";
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("MaDonHang", maDonHang),
                    new ReportParameter("NgayDat", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")),
                    new ReportParameter("TongTien", tongTienStr)
                };
               

                // Làm mới báo cáo
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải hóa đơn: {ex.Message}\nChi tiết: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
