using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WmsApp
{
	public partial class TabWindow : DockContent
	{
		protected bool enableAutoValidate;

		public TabWindow()
		{
			InitializeComponent();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x10 && !enableAutoValidate)
			{
				AutoValidate = AutoValidate.Disable;
			}
			base.WndProc(ref m);
		}

		private void menuCloseTab_Click(object sender, EventArgs e)
		{
			DockHandler.Close();
		}

		private void menuCloseAllTab_Click(object sender, EventArgs e)
		{
			foreach (Control control in ParentForm.Controls)
			{
				if (control is DockPanel)
				{
					DockPanel dockPanel = control as DockPanel;
					new List<IDockContent>(dockPanel.Documents).ForEach(d=>d.DockHandler.Close());
				}
			}
		}
	}
}