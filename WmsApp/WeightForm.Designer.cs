namespace WmsApp
{
    partial class WeightForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeightForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnEsc = new System.Windows.Forms.Button();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.lbSkuInfo = new System.Windows.Forms.Label();
            this.lbProcess = new System.Windows.Forms.Label();
            this.lbStore = new System.Windows.Forms.Label();
            this.lbOrderWeight = new System.Windows.Forms.Label();
            this.lbUpDown = new System.Windows.Forms.Label();
            this.lbPackNUM = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbStoreWeight = new System.Windows.Forms.Label();
            this.lbStorePlanNumKe = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(45, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(663, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "总进度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(45, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "门店:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(698, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "订单量:";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(662, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "上下限:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(666, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "标准包数:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(45, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "称重:";
            // 
            // btnEsc
            // 
            this.btnEsc.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEsc.Location = new System.Drawing.Point(618, 359);
            this.btnEsc.Name = "btnEsc";
            this.btnEsc.Size = new System.Drawing.Size(147, 56);
            this.btnEsc.TabIndex = 1;
            this.btnEsc.Text = "返回(ESC)";
            this.btnEsc.UseVisualStyleBackColor = true;
            this.btnEsc.Click += new System.EventHandler(this.btnEsc_Click);
            // 
            // tbWeight
            // 
            this.tbWeight.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWeight.ForeColor = System.Drawing.Color.Red;
            this.tbWeight.Location = new System.Drawing.Point(127, 214);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(221, 61);
            this.tbWeight.TabIndex = 0;
            this.tbWeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbWeight_KeyDown);
            // 
            // lbSkuInfo
            // 
            this.lbSkuInfo.AutoSize = true;
            this.lbSkuInfo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSkuInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbSkuInfo.Location = new System.Drawing.Point(123, 38);
            this.lbSkuInfo.Name = "lbSkuInfo";
            this.lbSkuInfo.Size = new System.Drawing.Size(259, 20);
            this.lbSkuInfo.TabIndex = 0;
            this.lbSkuInfo.Text = "8927  菠菜|水洗|   5斤/包";
            // 
            // lbProcess
            // 
            this.lbProcess.AutoSize = true;
            this.lbProcess.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbProcess.Location = new System.Drawing.Point(739, 38);
            this.lbProcess.Name = "lbProcess";
            this.lbProcess.Size = new System.Drawing.Size(129, 20);
            this.lbProcess.TabIndex = 0;
            this.lbProcess.Text = "5 / 10   50%";
            // 
            // lbStore
            // 
            this.lbStore.AutoSize = true;
            this.lbStore.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbStore.Location = new System.Drawing.Point(110, 113);
            this.lbStore.Name = "lbStore";
            this.lbStore.Size = new System.Drawing.Size(109, 20);
            this.lbStore.TabIndex = 0;
            this.lbStore.Text = "绿茶朝阳店";
            // 
            // lbOrderWeight
            // 
            this.lbOrderWeight.AutoSize = true;
            this.lbOrderWeight.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbOrderWeight.Location = new System.Drawing.Point(772, 279);
            this.lbOrderWeight.Name = "lbOrderWeight";
            this.lbOrderWeight.Size = new System.Drawing.Size(69, 20);
            this.lbOrderWeight.TabIndex = 0;
            this.lbOrderWeight.Text = "15公斤";
            this.lbOrderWeight.Visible = false;
            // 
            // lbUpDown
            // 
            this.lbUpDown.AutoSize = true;
            this.lbUpDown.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbUpDown.Location = new System.Drawing.Point(747, 170);
            this.lbUpDown.Name = "lbUpDown";
            this.lbUpDown.Size = new System.Drawing.Size(119, 20);
            this.lbUpDown.TabIndex = 0;
            this.lbUpDown.Text = "14斤 ~ 17斤";
            // 
            // lbPackNUM
            // 
            this.lbPackNUM.AutoSize = true;
            this.lbPackNUM.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPackNUM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbPackNUM.Location = new System.Drawing.Point(778, 234);
            this.lbPackNUM.Name = "lbPackNUM";
            this.lbPackNUM.Size = new System.Drawing.Size(19, 20);
            this.lbPackNUM.TabIndex = 0;
            this.lbPackNUM.Text = "2";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(375, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "克";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(457, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "门店量:";
            // 
            // lbStoreWeight
            // 
            this.lbStoreWeight.AutoSize = true;
            this.lbStoreWeight.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStoreWeight.ForeColor = System.Drawing.Color.Red;
            this.lbStoreWeight.Location = new System.Drawing.Point(543, 113);
            this.lbStoreWeight.Name = "lbStoreWeight";
            this.lbStoreWeight.Size = new System.Drawing.Size(112, 23);
            this.lbStoreWeight.TabIndex = 0;
            this.lbStoreWeight.Text = "10/100斤";
            // 
            // lbStorePlanNumKe
            // 
            this.lbStorePlanNumKe.AutoSize = true;
            this.lbStorePlanNumKe.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStorePlanNumKe.ForeColor = System.Drawing.Color.Red;
            this.lbStorePlanNumKe.Location = new System.Drawing.Point(259, 113);
            this.lbStorePlanNumKe.Name = "lbStorePlanNumKe";
            this.lbStorePlanNumKe.Size = new System.Drawing.Size(0, 23);
            this.lbStorePlanNumKe.TabIndex = 0;
            // 
            // WeightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 470);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.btnEsc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbPackNUM);
            this.Controls.Add(this.lbUpDown);
            this.Controls.Add(this.lbOrderWeight);
            this.Controls.Add(this.lbStore);
            this.Controls.Add(this.lbProcess);
            this.Controls.Add(this.lbSkuInfo);
            this.Controls.Add(this.lbStorePlanNumKe);
            this.Controls.Add(this.lbStoreWeight);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WeightForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "称重打包";
            this.Load += new System.EventHandler(this.WeightForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WeightForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnEsc;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label lbSkuInfo;
        private System.Windows.Forms.Label lbProcess;
        private System.Windows.Forms.Label lbStore;
        private System.Windows.Forms.Label lbOrderWeight;
        private System.Windows.Forms.Label lbUpDown;
        private System.Windows.Forms.Label lbPackNUM;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbStoreWeight;
        private System.Windows.Forms.Label lbStorePlanNumKe;
    }
}