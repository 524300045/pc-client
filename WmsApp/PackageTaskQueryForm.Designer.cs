namespace WmsApp
{
    partial class PackageTaskQueryForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.packTaskCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusdes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skuCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physicsUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.realWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbWave = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.tbTaskCode = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1147, 563);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1141, 488);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "明细";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageSplit1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 438);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1135, 47);
            this.panel1.TabIndex = 1;
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Location = new System.Drawing.Point(0, 0);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(983, 47);
            this.pageSplit1.TabIndex = 1;
            this.pageSplit1.PageChanged += new System.EventHandler(this.pageSplit1_PageChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.packTaskCode,
            this.statusdes,
            this.warehouseCode,
            this.skuCode,
            this.goodsName,
            this.weight,
            this.storedName,
            this.packageCode,
            this.boxCode,
            this.modelNum,
            this.goodsUnit,
            this.physicsUnit,
            this.orderCount,
            this.realWeight,
            this.packageNum,
            this.deliveryDate,
            this.waveName});
            this.dataGridView1.Location = new System.Drawing.Point(3, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1129, 410);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // packTaskCode
            // 
            this.packTaskCode.DataPropertyName = "packTaskCode";
            this.packTaskCode.HeaderText = "任务单号";
            this.packTaskCode.Name = "packTaskCode";
            this.packTaskCode.ReadOnly = true;
            this.packTaskCode.Width = 97;
            // 
            // statusdes
            // 
            this.statusdes.DataPropertyName = "statusdes";
            this.statusdes.HeaderText = "状态";
            this.statusdes.Name = "statusdes";
            this.statusdes.ReadOnly = true;
            this.statusdes.Width = 65;
            // 
            // warehouseCode
            // 
            this.warehouseCode.DataPropertyName = "warehouseCode";
            this.warehouseCode.HeaderText = "仓库";
            this.warehouseCode.Name = "warehouseCode";
            this.warehouseCode.ReadOnly = true;
            this.warehouseCode.Width = 65;
            // 
            // skuCode
            // 
            this.skuCode.DataPropertyName = "skuCode";
            this.skuCode.HeaderText = "商品编码";
            this.skuCode.Name = "skuCode";
            this.skuCode.ReadOnly = true;
            this.skuCode.Width = 97;
            // 
            // goodsName
            // 
            this.goodsName.DataPropertyName = "goodsName";
            this.goodsName.HeaderText = "商品名称";
            this.goodsName.Name = "goodsName";
            this.goodsName.ReadOnly = true;
            this.goodsName.Width = 97;
            // 
            // weight
            // 
            this.weight.DataPropertyName = "weight";
            this.weight.HeaderText = "重量";
            this.weight.Name = "weight";
            this.weight.Width = 65;
            // 
            // storedName
            // 
            this.storedName.DataPropertyName = "storedName";
            this.storedName.HeaderText = "门店";
            this.storedName.Name = "storedName";
            this.storedName.Width = 65;
            // 
            // packageCode
            // 
            this.packageCode.DataPropertyName = "packageCode";
            this.packageCode.HeaderText = "包裹号";
            this.packageCode.Name = "packageCode";
            this.packageCode.Width = 81;
            // 
            // boxCode
            // 
            this.boxCode.DataPropertyName = "boxCode";
            this.boxCode.HeaderText = "箱号";
            this.boxCode.Name = "boxCode";
            this.boxCode.Width = 65;
            // 
            // modelNum
            // 
            this.modelNum.DataPropertyName = "modelNum";
            this.modelNum.HeaderText = "规格";
            this.modelNum.Name = "modelNum";
            this.modelNum.ReadOnly = true;
            this.modelNum.Width = 65;
            // 
            // goodsUnit
            // 
            this.goodsUnit.DataPropertyName = "goodsUnit";
            this.goodsUnit.HeaderText = "计价单位";
            this.goodsUnit.Name = "goodsUnit";
            this.goodsUnit.ReadOnly = true;
            this.goodsUnit.Width = 97;
            // 
            // physicsUnit
            // 
            this.physicsUnit.DataPropertyName = "physicsUnit";
            this.physicsUnit.HeaderText = "物理单位";
            this.physicsUnit.Name = "physicsUnit";
            this.physicsUnit.ReadOnly = true;
            this.physicsUnit.Width = 97;
            // 
            // orderCount
            // 
            this.orderCount.DataPropertyName = "orderCount";
            this.orderCount.HeaderText = "订单总量";
            this.orderCount.Name = "orderCount";
            this.orderCount.ReadOnly = true;
            this.orderCount.Width = 97;
            // 
            // realWeight
            // 
            this.realWeight.DataPropertyName = "realWeight";
            this.realWeight.HeaderText = "实出量";
            this.realWeight.Name = "realWeight";
            this.realWeight.ReadOnly = true;
            this.realWeight.Width = 81;
            // 
            // packageNum
            // 
            this.packageNum.DataPropertyName = "packageNum";
            this.packageNum.HeaderText = "包裹数";
            this.packageNum.Name = "packageNum";
            this.packageNum.ReadOnly = true;
            this.packageNum.Width = 81;
            // 
            // deliveryDate
            // 
            this.deliveryDate.DataPropertyName = "deliveryDate";
            this.deliveryDate.HeaderText = "配送日期";
            this.deliveryDate.Name = "deliveryDate";
            this.deliveryDate.ReadOnly = true;
            this.deliveryDate.Width = 97;
            // 
            // waveName
            // 
            this.waveName.DataPropertyName = "waveName";
            this.waveName.HeaderText = "批次";
            this.waveName.Name = "waveName";
            this.waveName.ReadOnly = true;
            this.waveName.Width = 65;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbWave);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.tbTaskCode);
            this.groupBox3.Controls.Add(this.tbName);
            this.groupBox3.Controls.Add(this.dtBegin);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnQuery);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(3, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1141, 55);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // cbWave
            // 
            this.cbWave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWave.FormattingEnabled = true;
            this.cbWave.Location = new System.Drawing.Point(828, 17);
            this.cbWave.Name = "cbWave";
            this.cbWave.Size = new System.Drawing.Size(129, 24);
            this.cbWave.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(774, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "批次:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1047, 19);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tbTaskCode
            // 
            this.tbTaskCode.Location = new System.Drawing.Point(627, 17);
            this.tbTaskCode.Name = "tbTaskCode";
            this.tbTaskCode.Size = new System.Drawing.Size(141, 26);
            this.tbTaskCode.TabIndex = 3;
            this.tbTaskCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTaskCode_KeyDown);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(369, 17);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(141, 26);
            this.tbName.TabIndex = 3;
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(95, 18);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(139, 26);
            this.dtBegin.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(517, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "加工任务单号:";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(963, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "商品编码(名称):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "发货日期:";
            // 
            // PackageTaskQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 563);
            this.Controls.Add(this.groupBox1);
            this.Name = "PackageTaskQueryForm";
            this.Text = "任务单查询跟踪";
            this.Load += new System.EventHandler(this.PackageTaskForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private Wms.Controls.Pager.PageSplit pageSplit1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox tbTaskCode;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbWave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn packTaskCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusdes;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn skuCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn storedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn boxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn physicsUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn realWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn deliveryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn waveName;

    }
}