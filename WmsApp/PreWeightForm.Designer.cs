namespace WmsApp
{
    partial class PreWeightForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreWeightForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.btnEsc = new System.Windows.Forms.Button();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.lbSkuInfo = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.lbUpDown = new System.Windows.Forms.Label();
            this.lbStandPackage = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbModelNum = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.tbUnit = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbOrderNum = new System.Windows.Forms.Label();
            this.lbBiaoQian = new System.Windows.Forms.Label();
            this.tbBiaoQianNum = new System.Windows.Forms.TextBox();
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
            this.label2.Location = new System.Drawing.Point(498, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "已打包数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(45, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "上下限:";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWeight.Location = new System.Drawing.Point(45, 216);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(59, 20);
            this.lbWeight.TabIndex = 0;
            this.lbWeight.Text = "称重:";
            this.lbWeight.Visible = false;
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
            this.tbWeight.Location = new System.Drawing.Point(174, 207);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(156, 61);
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
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbCount.Location = new System.Drawing.Point(603, 38);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(129, 20);
            this.lbCount.TabIndex = 0;
            this.lbCount.Text = "5 / 10   50%";
            // 
            // lbUpDown
            // 
            this.lbUpDown.AutoSize = true;
            this.lbUpDown.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbUpDown.Location = new System.Drawing.Point(130, 96);
            this.lbUpDown.Name = "lbUpDown";
            this.lbUpDown.Size = new System.Drawing.Size(119, 20);
            this.lbUpDown.TabIndex = 0;
            this.lbUpDown.Text = "14斤 ~ 17斤";
            // 
            // lbStandPackage
            // 
            this.lbStandPackage.AutoSize = true;
            this.lbStandPackage.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStandPackage.Location = new System.Drawing.Point(45, 216);
            this.lbStandPackage.Name = "lbStandPackage";
            this.lbStandPackage.Size = new System.Drawing.Size(119, 20);
            this.lbStandPackage.TabIndex = 0;
            this.lbStandPackage.Text = "标准包裹数:";
            this.lbStandPackage.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(498, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "标准包规:";
            // 
            // lbModelNum
            // 
            this.lbModelNum.AutoSize = true;
            this.lbModelNum.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbModelNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbModelNum.Location = new System.Drawing.Point(603, 96);
            this.lbModelNum.Name = "lbModelNum";
            this.lbModelNum.Size = new System.Drawing.Size(129, 20);
            this.lbModelNum.TabIndex = 0;
            this.lbModelNum.Text = "5 / 10   50%";
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
            // tbUnit
            // 
            this.tbUnit.AutoSize = true;
            this.tbUnit.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUnit.Location = new System.Drawing.Point(367, 221);
            this.tbUnit.Name = "tbUnit";
            this.tbUnit.Size = new System.Drawing.Size(29, 20);
            this.tbUnit.TabIndex = 2;
            this.tbUnit.Text = "克";
            this.tbUnit.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(498, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "订单需求:";
            // 
            // lbOrderNum
            // 
            this.lbOrderNum.AutoSize = true;
            this.lbOrderNum.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbOrderNum.Location = new System.Drawing.Point(603, 152);
            this.lbOrderNum.Name = "lbOrderNum";
            this.lbOrderNum.Size = new System.Drawing.Size(29, 20);
            this.lbOrderNum.TabIndex = 0;
            this.lbOrderNum.Text = "11";
            // 
            // lbBiaoQian
            // 
            this.lbBiaoQian.AutoSize = true;
            this.lbBiaoQian.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBiaoQian.Location = new System.Drawing.Point(498, 216);
            this.lbBiaoQian.Name = "lbBiaoQian";
            this.lbBiaoQian.Size = new System.Drawing.Size(99, 20);
            this.lbBiaoQian.TabIndex = 0;
            this.lbBiaoQian.Text = "标签数量:";
            this.lbBiaoQian.Visible = false;
            // 
            // tbBiaoQianNum
            // 
            this.tbBiaoQianNum.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbBiaoQianNum.Location = new System.Drawing.Point(607, 212);
            this.tbBiaoQianNum.Name = "tbBiaoQianNum";
            this.tbBiaoQianNum.Size = new System.Drawing.Size(100, 38);
            this.tbBiaoQianNum.TabIndex = 3;
            this.tbBiaoQianNum.Text = "1";
            this.tbBiaoQianNum.Visible = false;
            this.tbBiaoQianNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbBiaoQianNum_KeyDown);
            // 
            // PreWeightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 470);
            this.Controls.Add(this.tbBiaoQianNum);
            this.Controls.Add(this.tbUnit);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.btnEsc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbBiaoQian);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbUpDown);
            this.Controls.Add(this.lbOrderNum);
            this.Controls.Add(this.lbModelNum);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lbSkuInfo);
            this.Controls.Add(this.lbStandPackage);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PreWeightForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预加工称重打包";
            this.Load += new System.EventHandler(this.WeightForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WeightForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Button btnEsc;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label lbSkuInfo;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbUpDown;
        private System.Windows.Forms.Label lbStandPackage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbModelNum;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label tbUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbOrderNum;
        private System.Windows.Forms.Label lbBiaoQian;
        private System.Windows.Forms.TextBox tbBiaoQianNum;
    }
}