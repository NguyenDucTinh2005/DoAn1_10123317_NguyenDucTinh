namespace DoAn1
{
    partial class frmDonHang
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
            this.dtgvDanhSachDonHang = new System.Windows.Forms.DataGridView();
            this.btnHuyDon = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaDonHang = new System.Windows.Forms.TextBox();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDanhSachDonHang)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvDanhSachDonHang
            // 
            this.dtgvDanhSachDonHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvDanhSachDonHang.Location = new System.Drawing.Point(123, 163);
            this.dtgvDanhSachDonHang.Name = "dtgvDanhSachDonHang";
            this.dtgvDanhSachDonHang.RowHeadersWidth = 51;
            this.dtgvDanhSachDonHang.Size = new System.Drawing.Size(796, 234);
            this.dtgvDanhSachDonHang.TabIndex = 0;
            this.dtgvDanhSachDonHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvDanhSachDonHang_CellClick);
            // 
            // btnHuyDon
            // 
            this.btnHuyDon.Location = new System.Drawing.Point(513, 444);
            this.btnHuyDon.Name = "btnHuyDon";
            this.btnHuyDon.Size = new System.Drawing.Size(88, 53);
            this.btnHuyDon.TabIndex = 1;
            this.btnHuyDon.Text = "Hủy đơn";
            this.btnHuyDon.UseVisualStyleBackColor = true;
            this.btnHuyDon.Click += new System.EventHandler(this.btnHuyDon_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(278, 444);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(88, 53);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận ";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã đơn hàng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Khách hàng:";
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Location = new System.Drawing.Point(312, 56);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(149, 22);
            this.txtMaDonHang.TabIndex = 5;
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Location = new System.Drawing.Point(312, 108);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new System.Drawing.Size(149, 22);
            this.txtKhachHang.TabIndex = 6;
            // 
            // frmDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 624);
            this.Controls.Add(this.txtKhachHang);
            this.Controls.Add(this.txtMaDonHang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.btnHuyDon);
            this.Controls.Add(this.dtgvDanhSachDonHang);
            this.Name = "frmDonHang";
            this.Text = "frmDonHang";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDanhSachDonHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvDanhSachDonHang;
        private System.Windows.Forms.Button btnHuyDon;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.TextBox txtKhachHang;
    }
}