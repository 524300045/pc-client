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
    public partial class CustomerSelectForm : Form
    {

        private Dictionary<string, string> dics;

        private string defaultCode;
        public CustomerSelectForm()
        {
            InitializeComponent();
        }

        public CustomerSelectForm(Dictionary<string,string> dic,string code)
        {
            InitializeComponent();
            this.dics=dic;
            defaultCode = code;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UserInfo.CustomerCode = cbCustomer.SelectedValue.ToString();
            UserInfo.CustomerName = cbCustomer.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void CustomerSelectForm_Load(object sender, EventArgs e)
        {
            List<Cus> list = new List<Cus>();
            foreach (var item in dics)
            {
                Cus c = new Cus();
                c.code = item.Key;
                c.name = item.Value;
                list.Add(c);
            }
            cbCustomer.DataSource = list;
            cbCustomer.DisplayMember = "name";
            cbCustomer.ValueMember = "code";
            if (defaultCode!="")
            {
                for (int i = 0; i < cbCustomer.Items.Count; i++)
                {
                    Cus cus = (Cus)cbCustomer.Items[i];
                    if (cus.code == defaultCode)
                    {
                        cbCustomer.SelectedIndex = i;
                    }
                }
            }
         
        }

        public class Cus
        {
            public string code { get; set; }

            public string name { get; set; }
        }
    }
}
