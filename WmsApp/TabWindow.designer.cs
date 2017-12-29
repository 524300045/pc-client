namespace WmsApp
{
	partial class TabWindow
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseAllTab = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCloseTab,
            this.menuCloseAllTab});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // menuCloseTab
            // 
            this.menuCloseTab.Name = "menuCloseTab";
            this.menuCloseTab.Size = new System.Drawing.Size(118, 22);
            this.menuCloseTab.Text = "关闭";
            this.menuCloseTab.Click += new System.EventHandler(this.menuCloseTab_Click);
            // 
            // menuCloseAllTab
            // 
            this.menuCloseAllTab.Name = "menuCloseAllTab";
            this.menuCloseAllTab.Size = new System.Drawing.Size(118, 22);
            this.menuCloseAllTab.Text = "关闭全部";
            this.menuCloseAllTab.Click += new System.EventHandler(this.menuCloseAllTab_Click);
            // 
            // TabWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 377);
            this.Name = "TabWindow";
            this.TabPageContextMenuStrip = this.contextMenuStrip1;
            this.TabText = "TabWindow";
            this.Text = "TabWindow";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuCloseTab;
		private System.Windows.Forms.ToolStripMenuItem menuCloseAllTab;
	}
}