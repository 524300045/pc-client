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
    public partial class SuitDateForm : Form
    {

        public String productionDate = "";

        public DateTime lastDateTime ;
        public SuitDateForm()
        {
            InitializeComponent();
        }

        public SuitDateForm(DateTime dt)
        {
            InitializeComponent();
            this.dateTimePicker1.Value = dt;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            productionDate = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
            lastDateTime = dateTimePicker1.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
