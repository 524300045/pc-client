namespace WmsApp
{
    partial class PackageTaskForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtBiaoQian = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.ccbWave = new Wms.Controls.ComCheckBoxList();
            this.label8 = new System.Windows.Forms.Label();
            this.cbWorkGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbWorkShop = new System.Windows.Forms.ComboBox();
            this.cbProcessProduct = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackTaskCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusdes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skuCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physicsUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finishNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processProductAttrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productWorkshopAttrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtBiaoQian);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ccbWave);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbWorkGroup);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbWorkShop);
            this.groupBox1.Controls.Add(this.cbProcessProduct);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.dtBegin);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1155, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // dtBiaoQian
            // 
            this.dtBiaoQian.Location = new System.Drawing.Point(796, 52);
            this.dtBiaoQian.Name = "dtBiaoQian";
            this.dtBiaoQian.Size = new System.Drawing.Size(140, 26);
            this.dtBiaoQian.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(710, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "标签日期:";
            // 
            // ccbWave
            // 
            this.ccbWave.DataSource = null;
            this.ccbWave.Location = new System.Drawing.Point(726, 17);
            this.ccbWave.Margin = new System.Windows.Forms.Padding(4);
            this.ccbWave.Name = "ccbWave";
            this.ccbWave.Size = new System.Drawing.Size(153, 25);
            this.ccbWave.TabIndex = 19;
            this.ccbWave.ItemClick += new Wms.Controls.ComCheckBoxList.CheckBoxListItemClick(this.ccbWave_ItemClick);
            this.ccbWave.AllClick += new Wms.Controls.ComCheckBoxList.CheckBoxAllClick(this.ccbWave_AllClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(681, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "批次:";
            // 
            // cbWorkGroup
            // 
            this.cbWorkGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWorkGroup.FormattingEnabled = true;
            this.cbWorkGroup.Location = new System.Drawing.Point(563, 52);
            this.cbWorkGroup.Name = "cbWorkGroup";
            this.cbWorkGroup.Size = new System.Drawing.Size(141, 24);
            this.cbWorkGroup.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(478, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "加工小组:";
            // 
            // cbWorkShop
            // 
            this.cbWorkShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWorkShop.FormattingEnabled = true;
            this.cbWorkShop.Location = new System.Drawing.Point(326, 52);
            this.cbWorkShop.Name = "cbWorkShop";
            this.cbWorkShop.Size = new System.Drawing.Size(141, 24);
            this.cbWorkShop.TabIndex = 7;
            // 
            // cbProcessProduct
            // 
            this.cbProcessProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcessProduct.FormattingEnabled = true;
            this.cbProcessProduct.Location = new System.Drawing.Point(105, 48);
            this.cbProcessProduct.Name = "cbProcessProduct";
            this.cbProcessProduct.Size = new System.Drawing.Size(121, 24);
            this.cbProcessProduct.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "生产车间:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "加工工序:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(967, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "请选择",
            "新建",
            "包装中",
            "完成",
            "关闭"});
            this.cbStatus.Location = new System.Drawing.Point(577, 18);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(98, 24);
            this.cbStatus.TabIndex = 4;
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
            this.dtBegin.Location = new System.Drawing.Point(109, 14);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(117, 26);
            this.dtBegin.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(886, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(523, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "状态:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "商品编码(名称):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "发货日期:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pageSplit1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1155, 385);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "明细";
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageSplit1.Location = new System.Drawing.Point(3, 334);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(1149, 48);
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
            this.Column12,
            this.id,
            this.status,
            this.PackTaskCode,
            this.statusdes,
            this.progressDes,
            this.skuCode,
            this.goodsName,
            this.modelNum,
            this.goodsUnit,
            this.physicsUnit,
            this.orderCount,
            this.orderNum,
            this.finishNum,
            this.StandNum,
            this.warehouseName,
            this.processProductAttrDesc,
            this.productWorkshopAttrDesc,
            this.groupName,
            this.waveName});
            this.dataGridView1.Location = new System.Drawing.Point(3, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1140, 306);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // Column12
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column12.HeaderText = "操作";
            this.Column12.Name = "Column12";
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column12.Text = "包装";
            this.Column12.UseColumnTextForButtonValue = true;
            this.Column12.Width = 65;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 49;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.Visible = false;
            this.status.Width = 81;
            // 
            // PackTaskCode
            // 
            this.PackTaskCode.DataPropertyName = "PackTaskCode";
            this.PackTaskCode.HeaderText = "任务单号";
            this.PackTaskCode.Name = "PackTaskCode";
            this.PackTaskCode.ReadOnly = true;
            this.PackTaskCode.Width = 97;
            // 
            // statusdes
            // 
            this.statusdes.DataPropertyName = "statusdes";
            this.statusdes.HeaderText = "状态";
            this.statusdes.Name = "statusdes";
            this.statusdes.ReadOnly = true;
            this.statusdes.Width = 65;
            // 
            // progressDes
            // 
            this.progressDes.DataPropertyName = "progressDes";
            this.progressDes.HeaderText = "总进度";
            this.progressDes.Name = "progressDes";
            this.progressDes.ReadOnly = true;
            this.progressDes.Width = 81;
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
            this.goodsUnit.HeaderText = "单位";
            this.goodsUnit.Name = "goodsUnit";
            this.goodsUnit.ReadOnly = true;
            this.goodsUnit.Width = 65;
            // 
            // physicsUnit
            // 
            this.physicsUnit.DataPropertyName = "physicsUnit";
            this.physicsUnit.HeaderText = "物理单位";
            this.physicsUnit.Name = "physicsUnit";
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
            // orderNum
            // 
            this.orderNum.DataPropertyName = "orderNum";
            this.orderNum.HeaderText = "订单数";
            this.orderNum.Name = "orderNum";
            this.orderNum.ReadOnly = true;
            this.orderNum.Width = 81;
            // 
            // finishNum
            // 
            this.finishNum.DataPropertyName = "finishNum";
            this.finishNum.HeaderText = "已完成";
            this.finishNum.Name = "finishNum";
            this.finishNum.ReadOnly = true;
            this.finishNum.Width = 81;
            // 
            // StandNum
            // 
            this.StandNum.DataPropertyName = "StandNum";
            this.StandNum.HeaderText = "标准包数";
            this.StandNum.Name = "StandNum";
            this.StandNum.ReadOnly = true;
            this.StandNum.Width = 97;
            // 
            // warehouseName
            // 
            this.warehouseName.DataPropertyName = "warehouseName";
            this.warehouseName.HeaderText = "仓库名称";
            this.warehouseName.Name = "warehouseName";
            this.warehouseName.ReadOnly = true;
            this.warehouseName.Visible = false;
            this.warehouseName.Width = 97;
            // 
            // processProductAttrDesc
            // 
            this.processProductAttrDesc.DataPropertyName = "processProductAttrDesc";
            this.processProductAttrDesc.HeaderText = "加工工序";
            this.processProductAttrDesc.Name = "processProductAttrDesc";
            this.processProductAttrDesc.ReadOnly = true;
            this.processProductAttrDesc.Width = 97;
            // 
            // productWorkshopAttrDesc
            // 
            this.productWorkshopAttrDesc.DataPropertyName = "productWorkshopAttrDesc";
            this.productWorkshopAttrDesc.HeaderText = "生产车间";
            this.productWorkshopAttrDesc.Name = "productWorkshopAttrDesc";
            this.productWorkshopAttrDesc.ReadOnly = true;
            this.productWorkshopAttrDesc.Width = 97;
            // 
            // groupName
            // 
            this.groupName.DataPropertyName = "groupName";
            this.groupName.HeaderText = "加工小组";
            this.groupName.Name = "groupName";
            this.groupName.Width = 97;
            // 
            // waveName
            // 
            this.waveName.DataPropertyName = "waveName";
            this.waveName.HeaderText = "批次";
            this.waveName.Name = "waveName";
            this.waveName.ReadOnly = true;
            this.waveName.Width = 65;
            // 
            // PackageTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 467);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PackageTaskForm";
            this.Text = "订单包装";
            this.Load += new System.EventHandler(this.PackageTaskForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Wms.Controls.Pager.PageSplit pageSplit1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbProcessProduct;
        private System.Windows.Forms.ComboBox cbWorkShop;
        private System.Windows.Forms.ComboBox cbWorkGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewButtonColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackTaskCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusdes;
        private System.Windows.Forms.DataGridViewTextBoxColumn progressDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn skuCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn physicsUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn processProductAttrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn productWorkshopAttrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn waveName;
        private Wms.Controls.ComCheckBoxList ccbWave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtBiaoQian;
    }
}