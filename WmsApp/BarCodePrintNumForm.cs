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
    public partial class BarCodePrintNumForm : Form
    {

        public int number = 0;
        public BarCodePrintNumForm()
        {
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            int num = 0;
            int.TryParse(tbNum.Text.Trim(), out num);
            if (num <= 0)
            {
                MessageBox.Show("打印数量必须大于0");
                return;
            }
            if (num > 50)
            {
                MessageBox.Show("数量不能大于50");
                return;
            }

            number = num;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
