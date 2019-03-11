namespace WmsApp.Order
{
    partial class OrderImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderImportForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCommonImport = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.excelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storedCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerSkuCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCommonImport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1218, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCommonImport
            // 
            this.tsbCommonImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCommonImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbCommonImport.Image")));
            this.tsbCommonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCommonImport.Name = "tsbCommonImport";
            this.tsbCommonImport.Size = new System.Drawing.Size(81, 22);
            this.tsbCommonImport.Text = "通用订单导入";
            this.tsbCommonImport.Click += new System.EventHandler(this.tsbCommonImport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1218, 512);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.excelNo,
            this.storedCode,
            this.customerSkuCode,
            this.filename,
            this.msg});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 17);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1212, 492);
            this.dgv.TabIndex = 0;
            // 
            // excelNo
            // 
            this.excelNo.DataPropertyName = "excelNo";
            this.excelNo.HeaderText = "行号";
            this.excelNo.Name = "excelNo";
            this.excelNo.ReadOnly = true;
            // 
            // storedCode
            // 
            this.storedCode.DataPropertyName = "storedCode";
            this.storedCode.HeaderText = "门店编码";
            this.storedCode.Name = "storedCode";
            this.storedCode.ReadOnly = true;
            // 
            // customerSkuCode
            // 
            this.customerSkuCode.DataPropertyName = "customerSkuCode";
            this.customerSkuCode.HeaderText = "客户商品编码";
            this.customerSkuCode.Name = "customerSkuCode";
            this.customerSkuCode.ReadOnly = true;
            this.customerSkuCode.Width = 130;
            // 
            // filename
            // 
            this.filename.DataPropertyName = "filename";
            this.filename.HeaderText = "文明名称";
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            this.filename.Width = 300;
            // 
            // msg
            // 
            this.msg.DataPropertyName = "msg";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            this.msg.DefaultCellStyle = dataGridViewCellStyle1;
            this.msg.HeaderText = "错误信息";
            this.msg.Name = "msg";
            this.msg.ReadOnly = true;
            this.msg.Width = 400;
            // 
            // OrderImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 537);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "OrderImportForm";
            this.Text = "订单导入";
            this.Load += new System.EventHandler(this.OrderImportForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCommonImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn excelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn storedCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerSkuCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn msg;
    }
}