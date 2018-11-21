using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WmsApp
{
    public partial class InputNumForm : Form
    {
        public InputNumForm()
        {
            InitializeComponent();
        }
        public int num = 0;
             

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbNum.Text.Trim()))
                {
                    MessageBox.Show("请录入数量");
                    return;
                }
                int.TryParse(tbNum.Text.Trim(), out num);
                if (num<=0)
                {
                    MessageBox.Show("数量必须大于0");
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
