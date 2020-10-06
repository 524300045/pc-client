namespace WmsApp.Order
{
    partial class OutBoundSecondSendForm
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
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.cbStore = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.orderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outboundTaskCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliveryNumCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualNumCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sortingNumCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finishSortingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPrintDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ccbWave = new Wms.Controls.ComCheckBoxList();
            this.cbFreshAttr = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStoreName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gb = new System.Windows.Forms.GroupBox();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(520, 52);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "发运";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(416, 53);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(377, 21);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(113, 21);
            this.dtEnd.TabIndex = 2;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(238, 21);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(113, 21);
            this.dtBegin.TabIndex = 1;
            // 
            // cbStore
            // 
            this.cbStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStore.FormattingEnabled = true;
            this.cbStore.Location = new System.Drawing.Point(48, 24);
            this.cbStore.Name = "cbStore";
            this.cbStore.Size = new System.Drawing.Size(121, 20);
            this.cbStore.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "发货日期:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "门店:";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk,
            this.orderNo,
            this.outboundTaskCode,
            this.storedName,
            this.StatusDes,
            this.packageNum,
            this.deliveryNumCount,
            this.actualNumCount,
            this.sortingNumCount,
            this.finishSortingTime,
            this.IsPrintDes,
            this.deliveryDate,
            this.status,
            this.waveName});
            this.dgv.Location = new System.Drawing.Point(12, 93);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1014, 384);
            this.dgv.TabIndex = 1;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            // 
            // chk
            // 
            this.chk.HeaderText = "全选";
            this.chk.Name = "chk";
            // 
            // orderNo
            // 
            this.orderNo.DataPropertyName = "orderNo";
            this.orderNo.HeaderText = "销售单号";
            this.orderNo.Name = "orderNo";
            this.orderNo.ReadOnly = true;
            this.orderNo.Width = 150;
            // 
            // outboundTaskCode
            // 
            this.outboundTaskCode.DataPropertyName = "outboundTaskCode";
            this.outboundTaskCode.HeaderText = "发运单号";
            this.outboundTaskCode.Name = "outboundTaskCode";
            this.outboundTaskCode.ReadOnly = true;
            this.outboundTaskCode.Width = 150;
            // 
            // storedName
            // 
            this.storedName.DataPropertyName = "storedName";
            this.storedName.HeaderText = "门店";
            this.storedName.Name = "storedName";
            this.storedName.ReadOnly = true;
            // 
            // StatusDes
            // 
            this.StatusDes.DataPropertyName = "StatusDes";
            this.StatusDes.HeaderText = "状态";
            this.StatusDes.Name = "StatusDes";
            this.StatusDes.ReadOnly = true;
            // 
            // packageNum
            // 
            this.packageNum.DataPropertyName = "packageNum";
            this.packageNum.HeaderText = "箱数";
            this.packageNum.Name = "packageNum";
            this.packageNum.ReadOnly = true;
            // 
            // deliveryNumCount
            // 
            this.deliveryNumCount.DataPropertyName = "deliveryNumCount";
            this.deliveryNumCount.HeaderText = "下单量";
            this.deliveryNumCount.Name = "deliveryNumCount";
            this.deliveryNumCount.ReadOnly = true;
            // 
            // actualNumCount
            // 
            this.actualNumCount.DataPropertyName = "actualNumCount";
            this.actualNumCount.HeaderText = "实际发运数量";
            this.actualNumCount.Name = "actualNumCount";
            this.actualNumCount.ReadOnly = true;
            // 
            // sortingNumCount
            // 
            this.sortingNumCount.DataPropertyName = "sortingNumCount";
            this.sortingNumCount.HeaderText = "分拣数量";
            this.sortingNumCount.Name = "sortingNumCount";
            this.sortingNumCount.ReadOnly = true;
            // 
            // finishSortingTime
            // 
            this.finishSortingTime.DataPropertyName = "finishSortingTime";
            this.finishSortingTime.HeaderText = "分拣完成时间";
            this.finishSortingTime.Name = "finishSortingTime";
            this.finishSortingTime.ReadOnly = true;
            // 
            // IsPrintDes
            // 
            this.IsPrintDes.DataPropertyName = "IsPrintDes";
            this.IsPrintDes.HeaderText = "是否打印";
            this.IsPrintDes.Name = "IsPrintDes";
            this.IsPrintDes.ReadOnly = true;
            // 
            // deliveryDate
            // 
            this.deliveryDate.DataPropertyName = "deliveryDate";
            this.deliveryDate.HeaderText = "发货日期";
            this.deliveryDate.Name = "deliveryDate";
            this.deliveryDate.ReadOnly = true;
            this.deliveryDate.Width = 150;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.Visible = false;
            // 
            // waveName
            // 
            this.waveName.DataPropertyName = "waveName";
            this.waveName.HeaderText = "批次";
            this.waveName.Name = "waveName";
            this.waveName.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ccbWave);
            this.groupBox1.Controls.Add(this.cbFreshAttr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbStoreName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.dtEnd);
            this.groupBox1.Controls.Add(this.dtBegin);
            this.groupBox1.Controls.Add(this.cbStore);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1032, 87);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // ccbWave
            // 
            this.ccbWave.DataSource = null;
            this.ccbWave.Location = new System.Drawing.Point(220, 55);
            this.ccbWave.Name = "ccbWave";
            this.ccbWave.Size = new System.Drawing.Size(150, 20);
            this.ccbWave.TabIndex = 18;
            this.ccbWave.ItemClick += new Wms.Controls.ComCheckBoxList.CheckBoxListItemClick(this.ccbWave_ItemClick);
            this.ccbWave.AllClick += new Wms.Controls.ComCheckBoxList.CheckBoxAllClick(this.ccbWave_AllClick);
            // 
            // cbFreshAttr
            // 
            this.cbFreshAttr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFreshAttr.FormattingEnabled = true;
            this.cbFreshAttr.Location = new System.Drawing.Point(48, 58);
            this.cbFreshAttr.Name = "cbFreshAttr";
            this.cbFreshAttr.Size = new System.Drawing.Size(121, 20);
            this.cbFreshAttr.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "批次:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "温区:";
            // 
            // tbStoreName
            // 
            this.tbStoreName.Location = new System.Drawing.Point(532, 20);
            this.tbStoreName.Name = "tbStoreName";
            this.tbStoreName.Size = new System.Drawing.Size(136, 21);
            this.tbStoreName.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(496, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "门店:";
            // 
            // gb
            // 
            this.gb.Controls.Add(this.pageSplit1);
            this.gb.Controls.Add(this.dgv);
            this.gb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb.Location = new System.Drawing.Point(0, 0);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(1032, 528);
            this.gb.TabIndex = 3;
            this.gb.TabStop = false;
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageSplit1.Location = new System.Drawing.Point(3, 483);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(1026, 42);
            this.pageSplit1.TabIndex = 0;
            this.pageSplit1.PageChanged += new System.EventHandler(this.pageSplit1_PageChanged);
            // 
            // OutBoundSecondSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 528);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb);
            this.Name = "OutBoundSecondSendForm";
            this.Text = "订单二次发运";
            this.Load += new System.EventHandler(this.OutBoundSendForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.ComboBox cbStore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.TextBox tbStoreName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbFreshAttr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn outboundTaskCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn storedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn deliveryNumCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualNumCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn sortingNumCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishSortingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsPrintDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn deliveryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn waveName;
        private Wms.Controls.ComCheckBoxList ccbWave;
        private Wms.Controls.Pager.PageSplit pageSplit1;
    }
}