namespace WmsApp.Order
{
    partial class SelectOrderExeclForm
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
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbExeclName = new System.Windows.Forms.Label();
            this.btnSumbit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbWare = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCustomerName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(182, 85);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(130, 23);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "选择Execl文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "上传文件:";
            // 
            // lbExeclName
            // 
            this.lbExeclName.AutoSize = true;
            this.lbExeclName.ForeColor = System.Drawing.Color.Red;
            this.lbExeclName.Location = new System.Drawing.Point(118, 122);
            this.lbExeclName.Name = "lbExeclName";
            this.lbExeclName.Size = new System.Drawing.Size(0, 12);
            this.lbExeclName.TabIndex = 1;
            // 
            // btnSumbit
            // 
            this.btnSumbit.Location = new System.Drawing.Point(120, 160);
            this.btnSumbit.Name = "btnSumbit";
            this.btnSumbit.Size = new System.Drawing.Size(82, 22);
            this.btnSumbit.TabIndex = 2;
            this.btnSumbit.Text = "提交";
            this.btnSumbit.UseVisualStyleBackColor = true;
            this.btnSumbit.Click += new System.EventHandler(this.btnSumbit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "仓库:";
            // 
            // lbWare
            // 
            this.lbWare.AutoSize = true;
            this.lbWare.Location = new System.Drawing.Point(180, 26);
            this.lbWare.Name = "lbWare";
            this.lbWare.Size = new System.Drawing.Size(47, 12);
            this.lbWare.TabIndex = 1;
            this.lbWare.Text = "文件名:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "货主:";
            // 
            // lbCustomerName
            // 
            this.lbCustomerName.AutoSize = true;
            this.lbCustomerName.Location = new System.Drawing.Point(180, 50);
            this.lbCustomerName.Name = "lbCustomerName";
            this.lbCustomerName.Size = new System.Drawing.Size(47, 12);
            this.lbCustomerName.TabIndex = 1;
            this.lbCustomerName.Text = "文件名:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 22);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectOrderExeclForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSumbit);
            this.Controls.Add(this.lbExeclName);
            this.Controls.Add(this.lbCustomerName);
            this.Controls.Add(this.lbWare);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "SelectOrderExeclForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择导入文件";
            this.Load += new System.EventHandler(this.SelectOrderExeclForm_Load);
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbExeclName;
        private System.Windows.Forms.Button btnSumbit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbWare;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCustomerName;
        private System.Windows.Forms.Button btnCancel;
    }
}