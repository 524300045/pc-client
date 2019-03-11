using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WmsApp.Order
{
    public partial class OrderImportForm : TabWindow
    {
        public OrderImportForm()
        {
            InitializeComponent();
        }

        private void tsbCommonImport_Click(object sender, EventArgs e)
        {
            SelectOrderExeclForm form = new SelectOrderExeclForm();
            this.dgv.DataSource = null;
            if (form.ShowDialog()==DialogResult.OK)
            {
                string infoMsg = "共" + form.returnList.Count + "记录,导入成功:" + form.sucCount + "条,失败:" + form.errCount + "条";
                this.groupBox1.Text = infoMsg;

                this.dgv.DataSource = form.returnList;
               
            }
        }

        private void OrderImportForm_Load(object sender, EventArgs e)
        {
            this.dgv.AutoGenerateColumns = false;
        }
    }
}
