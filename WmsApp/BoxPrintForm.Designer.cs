namespace WmsApp
{
    partial class BoxPrintForm
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
            this.cbStore = new System.Windows.Forms.ComboBox();
            this.btnBox = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pageSplit1 = new Wms.Controls.Pager.PageSplit();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.boxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storedCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printMan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbStore);
            this.groupBox1.Controls.Add(this.btnBox);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1089, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // cbStore
            // 
            this.cbStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStore.FormattingEnabled = true;
            this.cbStore.Location = new System.Drawing.Point(98, 16);
            this.cbStore.Name = "cbStore";
            this.cbStore.Size = new System.Drawing.Size(121, 24);
            this.cbStore.TabIndex = 2;
            // 
            // btnBox
            // 
            this.btnBox.Location = new System.Drawing.Point(393, 18);
            this.btnBox.Name = "btnBox";
            this.btnBox.Size = new System.Drawing.Size(75, 23);
            this.btnBox.TabIndex = 1;
            this.btnBox.Text = "打印箱码";
            this.btnBox.UseVisualStyleBackColor = true;
            this.btnBox.Click += new System.EventHandler(this.btnBox_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(271, 18);
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
            this.label2.Location = new System.Drawing.Point(29, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "门店:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1089, 412);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "明细";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pageSplit1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 330);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1083, 79);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // pageSplit1
            // 
            this.pageSplit1.BackColor = System.Drawing.Color.LightGray;
            this.pageSplit1.Description = "";
            this.pageSplit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageSplit1.Location = new System.Drawing.Point(3, 22);
            this.pageSplit1.Name = "pageSplit1";
            this.pageSplit1.PageCount = 1;
            this.pageSplit1.PageNo = 1;
            this.pageSplit1.Size = new System.Drawing.Size(1077, 54);
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.boxCode,
            this.storedCode,
            this.storedName,
            this.yn,
            this.printTime,
            this.printMan});
            this.dataGridView1.Location = new System.Drawing.Point(3, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1074, 323);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // boxCode
            // 
            this.boxCode.DataPropertyName = "boxCode";
            this.boxCode.HeaderText = "箱码";
            this.boxCode.Name = "boxCode";
            // 
            // storedCode
            // 
            this.storedCode.DataPropertyName = "storedCode";
            this.storedCode.HeaderText = "门店编码";
            this.storedCode.Name = "storedCode";
            // 
            // storedName
            // 
            this.storedName.DataPropertyName = "storedName";
            this.storedName.HeaderText = "门店名称";
            this.storedName.Name = "storedName";
            // 
            // yn
            // 
            this.yn.DataPropertyName = "yn";
            this.yn.HeaderText = "状态";
            this.yn.Name = "yn";
            this.yn.Visible = false;
            // 
            // printTime
            // 
            this.printTime.DataPropertyName = "printTime";
            this.printTime.HeaderText = "打印时间";
            this.printTime.Name = "printTime";
            // 
            // printMan
            // 
            this.printMan.DataPropertyName = "printMan";
            this.printMan.HeaderText = "打印人";
            this.printMan.Name = "printMan";
            // 
            // BoxPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 467);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BoxPrintForm";
            this.Text = "箱码标签打印";
            this.Load += new System.EventHandler(this.PackageTaskForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label2;
        private Wms.Controls.Pager.PageSplit pageSplit1;
        private System.Windows.Forms.ComboBox cbStore;
        private System.Windows.Forms.Button btnBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn boxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn storedCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn storedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn yn;
        private System.Windows.Forms.DataGridViewTextBoxColumn printTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn printMan;
    }
}