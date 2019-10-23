namespace WmsApp
{
    partial class PrePackageForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInput = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.chk = new System.Windows.Forms.CheckBox();
            this.cbWorkGroup = new System.Windows.Forms.ComboBox();
            this.cbWorkShop = new System.Windows.Forms.ComboBox();
            this.cbProcessProduct = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.skuCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physicsUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weighed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processProductAttrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productWorkshopAttrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isStandardProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productWorkshopAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInput);
            this.groupBox1.Controls.Add(this.btnOrder);
            this.groupBox1.Controls.Add(this.chk);
            this.groupBox1.Controls.Add(this.cbWorkGroup);
            this.groupBox1.Controls.Add(this.cbWorkShop);
            this.groupBox1.Controls.Add(this.cbProcessProduct);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.dtBegin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1080, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(899, 55);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(158, 23);
            this.btnInput.TabIndex = 14;
            this.btnInput.Text = "批量打印(输入量)";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(741, 56);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(150, 23);
            this.btnOrder.TabIndex = 13;
            this.btnOrder.Text = "批量打印(订单量)";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Location = new System.Drawing.Point(616, 58);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(107, 20);
            this.chk.TabIndex = 12;
            this.chk.Text = "标准化加工";
            this.chk.UseVisualStyleBackColor = true;
            // 
            // cbWorkGroup
            // 
            this.cbWorkGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWorkGroup.FormattingEnabled = true;
            this.cbWorkGroup.Location = new System.Drawing.Point(671, 17);
            this.cbWorkGroup.Name = "cbWorkGroup";
            this.cbWorkGroup.Size = new System.Drawing.Size(141, 24);
            this.cbWorkGroup.TabIndex = 10;
            // 
            // cbWorkShop
            // 
            this.cbWorkShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWorkShop.FormattingEnabled = true;
            this.cbWorkShop.Location = new System.Drawing.Point(454, 55);
            this.cbWorkShop.Name = "cbWorkShop";
            this.cbWorkShop.Size = new System.Drawing.Size(141, 24);
            this.cbWorkShop.TabIndex = 10;
            // 
            // cbProcessProduct
            // 
            this.cbProcessProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcessProduct.FormattingEnabled = true;
            this.cbProcessProduct.Location = new System.Drawing.Point(162, 55);
            this.cbProcessProduct.Name = "cbProcessProduct";
            this.cbProcessProduct.Size = new System.Drawing.Size(141, 24);
            this.cbProcessProduct.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(586, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "加工小组:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "生产车间:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "加工工序:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(937, 18);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(454, 15);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(117, 26);
            this.dtBegin.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "发货日期:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(162, 15);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(141, 26);
            this.tbName.TabIndex = 3;
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(829, 18);
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
            this.label2.Location = new System.Drawing.Point(44, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "商品编码(名称):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pageSplit1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1080, 379);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "明细";
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageSplit1.Location = new System.Drawing.Point(3, 324);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(1074, 52);
            this.pageSplit1.TabIndex = 1;
            this.pageSplit1.PageChanged += new System.EventHandler(this.pageSplit1_PageChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk1,
            this.Column12,
            this.skuCode,
            this.goodsName,
            this.goodsModel,
            this.goodsUnit,
            this.physicsUnit,
            this.modelNum,
            this.weighed,
            this.orderNum,
            this.packageNum,
            this.processProductAttrDesc,
            this.productWorkshopAttrDesc,
            this.isStandardProcess,
            this.shortName,
            this.groupName,
            this.productWorkshopAttr});
            this.dataGridView1.Location = new System.Drawing.Point(3, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1027, 296);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // chk1
            // 
            this.chk1.HeaderText = "选择";
            this.chk1.Name = "chk1";
            this.chk1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chk1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chk1.Width = 65;
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
            // goodsModel
            // 
            this.goodsModel.DataPropertyName = "goodsModel";
            this.goodsModel.HeaderText = "规格";
            this.goodsModel.Name = "goodsModel";
            this.goodsModel.ReadOnly = true;
            this.goodsModel.Width = 65;
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
            // modelNum
            // 
            this.modelNum.DataPropertyName = "modelNum";
            this.modelNum.HeaderText = "包装规格";
            this.modelNum.Name = "modelNum";
            this.modelNum.ReadOnly = true;
            this.modelNum.Width = 97;
            // 
            // weighed
            // 
            this.weighed.DataPropertyName = "weighed";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.weighed.DefaultCellStyle = dataGridViewCellStyle2;
            this.weighed.HeaderText = "weighed";
            this.weighed.Name = "weighed";
            this.weighed.Visible = false;
            this.weighed.Width = 89;
            // 
            // orderNum
            // 
            this.orderNum.DataPropertyName = "orderNum";
            this.orderNum.HeaderText = "订单需求数量";
            this.orderNum.Name = "orderNum";
            this.orderNum.ReadOnly = true;
            this.orderNum.Width = 129;
            // 
            // packageNum
            // 
            this.packageNum.DataPropertyName = "packageNum";
            this.packageNum.HeaderText = "包装数量";
            this.packageNum.Name = "packageNum";
            this.packageNum.ReadOnly = true;
            this.packageNum.Width = 97;
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
            // isStandardProcess
            // 
            this.isStandardProcess.DataPropertyName = "isStandardProcess";
            this.isStandardProcess.HeaderText = "isStandardProcess";
            this.isStandardProcess.Name = "isStandardProcess";
            this.isStandardProcess.Visible = false;
            this.isStandardProcess.Width = 169;
            // 
            // shortName
            // 
            this.shortName.DataPropertyName = "shortName";
            this.shortName.HeaderText = "shortName";
            this.shortName.Name = "shortName";
            this.shortName.Visible = false;
            this.shortName.Width = 105;
            // 
            // groupName
            // 
            this.groupName.DataPropertyName = "groupName";
            this.groupName.HeaderText = "加工小组";
            this.groupName.Name = "groupName";
            this.groupName.ReadOnly = true;
            this.groupName.Width = 97;
            // 
            // productWorkshopAttr
            // 
            this.productWorkshopAttr.DataPropertyName = "productWorkshopAttr";
            this.productWorkshopAttr.HeaderText = "productWorkshopAttr";
            this.productWorkshopAttr.Name = "productWorkshopAttr";
            this.productWorkshopAttr.Visible = false;
            this.productWorkshopAttr.Width = 185;
            // 
            // PrePackageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 467);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PrePackageForm";
            this.Text = "商品包装";
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
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label2;
        private Wms.Controls.Pager.PageSplit pageSplit1;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cbWorkShop;
        private System.Windows.Forms.ComboBox cbProcessProduct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbWorkGroup;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk1;
        private System.Windows.Forms.DataGridViewButtonColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn skuCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn physicsUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn weighed;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn processProductAttrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn productWorkshopAttrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn isStandardProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn shortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn productWorkshopAttr;
    }
}