namespace Wms.Controls.Pager
{
    partial class PageSplit
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDesp = new System.Windows.Forms.Label();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPage = new System.Windows.Forms.TextBox();
            this.btend = new System.Windows.Forms.Button();
            this.btnext = new System.Windows.Forms.Button();
            this.btPrev = new System.Windows.Forms.Button();
            this.btfirst = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDesp
            // 
            this.lblDesp.AutoSize = true;
            this.lblDesp.Location = new System.Drawing.Point(426, 13);
            this.lblDesp.Name = "lblDesp";
            this.lblDesp.Size = new System.Drawing.Size(41, 12);
            this.lblDesp.TabIndex = 5;
            this.lblDesp.Text = "label1";
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(339, 14);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(41, 12);
            this.lblTotalPage.TabIndex = 5;
            this.lblTotalPage.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "/";
            // 
            // tbPage
            // 
            this.tbPage.Location = new System.Drawing.Point(272, 9);
            this.tbPage.Name = "tbPage";
            this.tbPage.Size = new System.Drawing.Size(31, 21);
            this.tbPage.TabIndex = 6;
            // 
            // btend
            // 
            this.btend.Location = new System.Drawing.Point(207, 7);
            this.btend.Name = "btend";
            this.btend.Size = new System.Drawing.Size(47, 23);
            this.btend.TabIndex = 7;
            this.btend.Text = "末页";
            this.btend.UseVisualStyleBackColor = true;
            this.btend.Click += new System.EventHandler(this.btend_Click);
            // 
            // btnext
            // 
            this.btnext.Location = new System.Drawing.Point(143, 7);
            this.btnext.Name = "btnext";
            this.btnext.Size = new System.Drawing.Size(58, 23);
            this.btnext.TabIndex = 7;
            this.btnext.Text = "下一页";
            this.btnext.UseVisualStyleBackColor = true;
            this.btnext.Click += new System.EventHandler(this.btnext_Click);
            // 
            // btPrev
            // 
            this.btPrev.Location = new System.Drawing.Point(79, 7);
            this.btPrev.Name = "btPrev";
            this.btPrev.Size = new System.Drawing.Size(58, 23);
            this.btPrev.TabIndex = 7;
            this.btPrev.Text = "上一页";
            this.btPrev.UseVisualStyleBackColor = true;
            this.btPrev.Click += new System.EventHandler(this.btPrev_Click);
            // 
            // btfirst
            // 
            this.btfirst.Location = new System.Drawing.Point(15, 7);
            this.btfirst.Name = "btfirst";
            this.btfirst.Size = new System.Drawing.Size(58, 23);
            this.btfirst.TabIndex = 7;
            this.btfirst.Text = "首页";
            this.btfirst.UseVisualStyleBackColor = true;
            this.btfirst.Click += new System.EventHandler(this.btfirst_Click);
            // 
            // PageSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.btfirst);
            this.Controls.Add(this.btPrev);
            this.Controls.Add(this.btnext);
            this.Controls.Add(this.btend);
            this.Controls.Add(this.tbPage);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDesp);
            this.Name = "PageSplit";
            this.Size = new System.Drawing.Size(533, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDesp;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPage;
        private System.Windows.Forms.Button btend;
        private System.Windows.Forms.Button btnext;
        private System.Windows.Forms.Button btPrev;
        private System.Windows.Forms.Button btfirst;
    }
}
