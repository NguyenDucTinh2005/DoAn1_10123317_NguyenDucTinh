using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAL;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace DoAn1
{
    public partial class frmThongKe : Form
    {
        private ThongKeBUS thongKeBUS = new ThongKeBUS();
        private DataTable dtDoanhThu; // Lưu trữ dữ liệu để tái sử dụng khi xuất Excel
        private double tongDoanhThu; // Lưu trữ tổng doanh thu để xuất Excel

        public frmThongKe()
        {
            // Đặt license context cho EPPlus
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            InitializeComponent();
            LoadReport();
            Load += new EventHandler(frmThongKe_Load);
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            // Đặt ngày mặc định (7 ngày gần nhất)
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);
            dtpDenNgay.Value = DateTime.Now;

            // Đặt định dạng cho DateTimePicker
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";

            SetupChart();
            lblTongDoanhThu.Text = "0"; // Khởi tạo giá trị mặc định là 0
            
            this.reportViewer2.RefreshReport();
        }

        private void SetupChart()
        {
            chartDoanhThu.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chartDoanhThu.ChartAreas.Add(chartArea);

            chartDoanhThu.Series.Clear();
            Series series = new Series
            {
                Name = "DoanhThu",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            chartDoanhThu.Series.Add(series);

            chartDoanhThu.ChartAreas[0].AxisX.Title = "Ngày";
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh Thu (VNĐ)";
            chartDoanhThu.ChartAreas[0].AxisX.Interval = 1; // Hiển thị tất cả các ngày
            chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Xoay nhãn trục X để dễ đọc
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Thống kê doanh thu theo ngày
                dtDoanhThu = thongKeBUS.GetDoanhThuTheoThang(tuNgay, denNgay);
                chartDoanhThu.Series["DoanhThu"].Points.Clear();
                if (dtDoanhThu.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu doanh thu trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTongDoanhThu.Text = "0";
                    tongDoanhThu = 0;
                    return;
                }

                tongDoanhThu = 0;
                foreach (DataRow row in dtDoanhThu.Rows)
                {
                    string ngayFormatted = row["NgayFormatted"].ToString(); // "Thứ 2, 03/04/2025"
                    double doanhThu = Convert.ToDouble(row["DoanhThu"]);
                    chartDoanhThu.Series["DoanhThu"].Points.AddXY(ngayFormatted, doanhThu);
                    tongDoanhThu += doanhThu;
                }

                // Hiển thị tổng doanh thu chỉ với số
                lblTongDoanhThu.Text = tongDoanhThu.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thống kê doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTongDoanhThu.Text = "0";
                tongDoanhThu = 0;
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtDoanhThu == null || dtDoanhThu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất! Vui lòng thực hiện thống kê trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Chọn nơi lưu file Excel",
                FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Báo Cáo Doanh Thu");

                // ========== HEADER ==========
                worksheet.Cells["A1:B1"].Merge = true;
                worksheet.Cells["A1"].Value = "BÁO CÁO DOANH THU";
                worksheet.Cells["A1"].Style.Font.Size = 16;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["A2:B2"].Merge = true;
                worksheet.Cells["A2"].Value = $"Thời gian: từ ngày {dtpTuNgay.Value:dd/MM/yyyy} đến {dtpDenNgay.Value:dd/MM/yyyy}";
                worksheet.Cells["A2"].Style.Font.Italic = true;
                worksheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // ========== TABLE HEADER ==========
                worksheet.Cells[4, 1].Value = "Ngày";
                worksheet.Cells[4, 2].Value = "Doanh Thu (VNĐ)";
                using (var header = worksheet.Cells[4, 1, 4, 2])
                {
                    header.Style.Font.Bold = true;
                    header.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    header.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    header.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    header.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }

                // ========== DỮ LIỆU ==========
                int rowStart = 5;
                for (int i = 0; i < dtDoanhThu.Rows.Count; i++)
                {
                    worksheet.Cells[rowStart + i, 1].Value = dtDoanhThu.Rows[i]["NgayFormatted"].ToString();
                    worksheet.Cells[rowStart + i, 2].Value = Convert.ToDouble(dtDoanhThu.Rows[i]["DoanhThu"]);
                    worksheet.Cells[rowStart + i, 2].Style.Numberformat.Format = "#,##0 VNĐ";
                }

                // ========== TỔNG DOANH THU ==========
                int rowTotal = rowStart + dtDoanhThu.Rows.Count + 1;
                worksheet.Cells[rowTotal, 1].Value = "TỔNG DOANH THU:";
                worksheet.Cells[rowTotal, 2].Value = tongDoanhThu;
                worksheet.Cells[rowTotal, 2].Style.Numberformat.Format = "#,##0 VNĐ";

                using (var range = worksheet.Cells[rowTotal, 1, rowTotal, 2])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }

                // ========== CĂN CỘT ==========
                worksheet.Cells.AutoFitColumns();

                // ========== LƯU FILE ==========
                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                {
                    package.SaveAs(stream);
                }

                MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ----------------------------- MÓN BÁN CHAY -----------------------------//
        private void LoadReport()
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", GetData()); // "DataSet1" là tên Dataset trong rdlc
            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.DataSources.Add(rds);
            reportViewer2.LocalReport.ReportPath = "rptMonBanChay.rdlc";
            reportViewer2.RefreshReport();
        }

        private System.Data.DataTable GetData()
        {
            ThongKeBUS thongKeBUS = new ThongKeBUS();
            return thongKeBUS.GetMonBanChay();
        }

        private void btnThongKeNguyenLieuTon_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }
        private void LoadThongKe()
        {
            DateTime selectedDate = dtpNgayThongKe.Value.Date;
            DataTable dt = thongKeBUS.GetNguyenLieuTonTheoNgay(selectedDate);
            dtgvNguyenLieuTon.DataSource = dt;
        }
    }
}