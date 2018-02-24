namespace WmsApp
{
    partial class ContainerPrintForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oper = new System.Windows.Forms.DataGridViewButtonColumn();
            this.categoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.twoCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threeCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skuCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barCodeStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbOne = new System.Windows.Forms.ComboBox();
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
            this.groupBox2.Location = new System.Drawing.Point(0, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1234, 405);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk,
            this.oper,
            this.categoryName,
            this.twoCategoryName,
            this.threeCategoryName,
            this.skuCode,
            this.goodsName,
            this.goodsModel,
            this.goodsGrade,
            this.barCodeStr});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1228, 351);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // chk
            // 
            this.chk.HeaderText = "选择";
            this.chk.Name = "chk";
            // 
            // oper
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            this.oper.DefaultCellStyle = dataGridViewCellStyle2;
            this.oper.HeaderText = "操作";
            this.oper.Name = "oper";
            this.oper.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.oper.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.oper.Text = "打印";
            this.oper.ToolTipText = "打印";
            this.oper.UseColumnTextForButtonValue = true;
            // 
            // categoryName
            // 
            this.categoryName.DataPropertyName = "categoryName";
            this.categoryName.HeaderText = "一级分类";
            this.categoryName.Name = "categoryName";
            this.categoryName.ReadOnly = true;
            // 
            // twoCategoryName
            // 
            this.twoCategoryName.DataPropertyName = "twoCategoryName";
            this.twoCategoryName.HeaderText = "二级分类";
            this.twoCategoryName.Name = "twoCategoryName";
            this.twoCategoryName.ReadOnly = true;
            // 
            // threeCategoryName
            // 
            this.threeCategoryName.DataPropertyName = "threeCategoryName";
            this.threeCategoryName.HeaderText = "三级分类";
            this.threeCategoryName.Name = "threeCategoryName";
            this.threeCategoryName.ReadOnly = true;
            // 
            // skuCode
            // 
            this.skuCode.DataPropertyName = "skuCode";
            this.skuCode.HeaderText = "商品编码";
            this.skuCode.Name = "skuCode";
            this.skuCode.ReadOnly = true;
            // 
            // goodsName
            // 
            this.goodsName.DataPropertyName = "goodsName";
            this.goodsName.HeaderText = "商品名称";
            this.goodsName.Name = "goodsName";
            this.goodsName.ReadOnly = true;
            // 
            // goodsModel
            // 
            this.goodsModel.DataPropertyName = "goodsModel";
            this.goodsModel.HeaderText = "规格";
            this.goodsModel.Name = "goodsModel";
            this.goodsModel.ReadOnly = true;
            // 
            // goodsGrade
            // 
            this.goodsGrade.DataPropertyName = "goodsGrade";
            this.goodsGrade.HeaderText = "等级";
            this.goodsGrade.Name = "goodsGrade";
            this.goodsGrade.ReadOnly = true;
            // 
            // barCodeStr
            // 
            this.barCodeStr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.barCodeStr.DataPropertyName = "barCodeStr";
            this.barCodeStr.HeaderText = "条码";
            this.barCodeStr.Name = "barCodeStr";
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageSplit1.Location = new System.Drawing.Point(3, 368);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(1228, 34);
            this.pageSplit1.TabIndex = 0;
            this.pageSplit1.PageChanged += new System.EventHandler(this.pageSplit1_PageChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cbOne);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1234, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(150, 65);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "打印条码";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(60, 65);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cbOne
            // 
            this.cbOne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOne.FormattingEnabled = true;
            this.cbOne.Location = new System.Drawing.Point(91, 33);
            this.cbOne.Name = "cbOne";
            this.cbOne.Size = new System.Drawing.Size(85, 20);
            this.cbOne.TabIndex = 2;
            
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(346, 34);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(158, 21);
            this.tbName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "容器:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "一级分类:";
            // 
            // ContainerPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 501);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ContainerPrintForm";
            this.Text = "容器打印";
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
        private System.Windows.Forms.ComboBox cbOne;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewButtonColumn oper;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn twoCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn threeCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn skuCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsGrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn barCodeStr;
    }
}