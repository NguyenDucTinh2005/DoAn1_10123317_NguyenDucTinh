namespace DoAn1
{
    partial class frmLichSuDatHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTongSoTien = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lsvThongTinDonHang = new System.Windows.Forms.ListView();
            this.txtMaDonHang = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDatLai = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnXoaLichSu = new System.Windows.Forms.Button();
            this.dtgvLichSuDatHang = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLichSuDatHang)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblTongSoTien);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lsvThongTinDonHang);
            this.panel1.Controls.Add(this.txtMaDonHang);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnDatLai);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.btnXoaLichSu);
            this.panel1.Controls.Add(this.dtgvLichSuDatHang);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 591);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(435, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 24);
            this.label3.TabIndex = 21;
            this.label3.Text = "Mã đơn hàng:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1138, 62);
            this.label2.TabIndex = 20;
            this.label2.Text = "Thông tin đơn hàng:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongSoTien
            // 
            this.lblTongSoTien.AutoSize = true;
            this.lblTongSoTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblTongSoTien.Font = new System.Drawing.Font("Arial", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongSoTien.Location = new System.Drawing.Point(978, 521);
            this.lblTongSoTien.Name = "lblTongSoTien";
            this.lblTongSoTien.Size = new System.Drawing.Size(0, 26);
            this.lblTongSoTien.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(740, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(222, 26);
            this.label7.TabIndex = 18;
            this.label7.Text = "Thông tin đơn hàng:";
            // 
            // lsvThongTinDonHang
            // 
            this.lsvThongTinDonHang.BackColor = System.Drawing.Color.Thistle;
            this.lsvThongTinDonHang.HideSelection = false;
            this.lsvThongTinDonHang.Location = new System.Drawing.Point(635, 178);
            this.lsvThongTinDonHang.Name = "lsvThongTinDonHang";
            this.lsvThongTinDonHang.Size = new System.Drawing.Size(463, 250);
            this.lsvThongTinDonHang.TabIndex = 17;
            this.lsvThongTinDonHang.UseCompatibleStateImageBehavior = false;
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMaDonHang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaDonHang.Location = new System.Drawing.Point(439, 114);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(138, 30);
            this.txtMaDonHang.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thông tin cần tìm:";
            // 
            // btnDatLai
            // 
            this.btnDatLai.BackColor = System.Drawing.Color.GreenYellow;
            this.btnDatLai.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDatLai.Location = new System.Drawing.Point(676, 446);
            this.btnDatLai.Name = "btnDatLai";
            this.btnDatLai.Size = new System.Drawing.Size(156, 60);
            this.btnDatLai.TabIndex = 4;
            this.btnDatLai.Text = "Đặt lại";
            this.btnDatLai.UseVisualStyleBackColor = false;
            this.btnDatLai.Click += new System.EventHandler(this.btnDatLai_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtTimKiem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Location = new System.Drawing.Point(12, 114);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(206, 30);
            this.txtTimKiem.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.GreenYellow;
            this.btnTimKiem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTimKiem.Location = new System.Drawing.Point(249, 80);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(156, 66);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnXoaLichSu
            // 
            this.btnXoaLichSu.BackColor = System.Drawing.Color.GreenYellow;
            this.btnXoaLichSu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXoaLichSu.Location = new System.Drawing.Point(906, 449);
            this.btnXoaLichSu.Name = "btnXoaLichSu";
            this.btnXoaLichSu.Size = new System.Drawing.Size(156, 57);
            this.btnXoaLichSu.TabIndex = 1;
            this.btnXoaLichSu.Text = "Xóa lịch sử";
            this.btnXoaLichSu.UseVisualStyleBackColor = false;
            this.btnXoaLichSu.Click += new System.EventHandler(this.btnXoaLichSu_Click);
            // 
            // dtgvLichSuDatHang
            // 
            this.dtgvLichSuDatHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvLichSuDatHang.Location = new System.Drawing.Point(6, 165);
            this.dtgvLichSuDatHang.Name = "dtgvLichSuDatHang";
            this.dtgvLichSuDatHang.RowHeadersWidth = 51;
            this.dtgvLichSuDatHang.RowTemplate.Height = 24;
            this.dtgvLichSuDatHang.Size = new System.Drawing.Size(623, 327);
            this.dtgvLichSuDatHang.TabIndex = 0;
            this.dtgvLichSuDatHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvLichSuDatHang_CellClick);
            // 
            // frmLichSuDatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 591);
            this.Controls.Add(this.panel1);
            this.Name = "frmLichSuDatHang";
            this.Text = "frmLichSuDatHang";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLichSuDatHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtgvLichSuDatHang;
        private System.Windows.Forms.Button btnXoaLichSu;
        private System.Windows.Forms.Button btnDatLai;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.Label lblTongSoTien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lsvThongTinDonHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}