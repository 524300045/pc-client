namespace WmsApp
{
    partial class BinForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.areaCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbCol = new System.Windows.Forms.TextBox();
            this.tbLayerno = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbCol111 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.pageSplit1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1223, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk,
            this.areaCode,
            this.locationCode,
            this.locationName,
            this.path,
            this.layerNo,
            this.colNo});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1217, 396);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // chk
            // 
            this.chk.HeaderText = "选择";
            this.chk.Name = "chk";
            // 
            // areaCode
            // 
            this.areaCode.DataPropertyName = "areaCode";
            this.areaCode.HeaderText = "库区";
            this.areaCode.Name = "areaCode";
            this.areaCode.ReadOnly = true;
            // 
            // locationCode
            // 
            this.locationCode.DataPropertyName = "locationCode";
            this.locationCode.HeaderText = "货位编码";
            this.locationCode.Name = "locationCode";
            this.locationCode.ReadOnly = true;
            // 
            // locationName
            // 
            this.locationName.DataPropertyName = "locationName";
            this.locationName.HeaderText = "货位名称";
            this.locationName.Name = "locationName";
            this.locationName.ReadOnly = true;
            // 
            // path
            // 
            this.path.DataPropertyName = "path";
            this.path.HeaderText = "排";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            // 
            // layerNo
            // 
            this.layerNo.DataPropertyName = "layerNo";
            this.layerNo.HeaderText = "层";
            this.layerNo.Name = "layerNo";
            this.layerNo.ReadOnly = true;
            // 
            // colNo
            // 
            this.colNo.DataPropertyName = "colNo";
            this.colNo.HeaderText = "列";
            this.colNo.Name = "colNo";
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageSplit1.Location = new System.Drawing.Point(3, 413);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(1217, 34);
            this.pageSplit1.TabIndex = 0;
            this.pageSplit1.PageChanged += new System.EventHandler(this.pageSplit1_PageChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbCol);
            this.groupBox1.Controls.Add(this.tbLayerno);
            this.groupBox1.Controls.Add(this.tbPath);
            this.groupBox1.Controls.Add(this.tbCol111);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cbArea);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1223, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // tbCol
            // 
            this.tbCol.Location = new System.Drawing.Point(759, 12);
            this.tbCol.Name = "tbCol";
            this.tbCol.Size = new System.Drawing.Size(118, 21);
            this.tbCol.TabIndex = 13;
            // 
            // tbLayerno
            // 
            this.tbLayerno.Location = new System.Drawing.Point(628, 14);
            this.tbLayerno.Name = "tbLayerno";
            this.tbLayerno.Size = new System.Drawing.Size(100, 21);
            this.tbLayerno.TabIndex = 12;
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(442, 16);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(118, 21);
            this.tbPath.TabIndex = 11;
            // 
            // tbCol111
            // 
            this.tbCol111.AutoSize = true;
            this.tbCol111.Location = new System.Drawing.Point(736, 18);
            this.tbCol111.Name = "tbCol111";
            this.tbCol111.Size = new System.Drawing.Size(17, 12);
            this.tbCol111.TabIndex = 8;
            this.tbCol111.Text = "列";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(595, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "层";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "排";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1126, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1034, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(935, 13);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cbArea
            // 
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(69, 14);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(85, 20);
            this.cbArea.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(228, 18);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(158, 21);
            this.tbName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "库位:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "库区:";
            // 
            // BinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 501);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BinForm";
            this.Text = "货位管理";
            this.Load += new System.EventHandler(this.GoodsBarCodePrintForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Wms.Controls.Pager.PageSplit pageSplit1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewTextBoxColumn layerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.TextBox tbCol;
        private System.Windows.Forms.TextBox tbLayerno;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label tbCol111;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}