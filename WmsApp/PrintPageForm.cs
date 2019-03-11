using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WmsApp
{
    public partial class PrintPageForm : Form
    {
        public PrintPageForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 获取AppSettings中某一节点值
        /// </summary>
        /// <param name="key"></param>
        public static string GetConfigValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
                return config.AppSettings.Settings[key].Value;
            else

                return string.Empty;
        }    

        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbLeft.Text.Trim()))
                {
                    MessageBox.Show("请输入边距");
                    return;
                }
                SetConfigValue("printleft", tbLeft.Text.Trim());
                SystemInfo.PrintMarginLeft = int.Parse(tbLeft.Text.Trim());
                MessageBox.Show("设置成功");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
         /// 修改AppSettings中配置
         /// </summary>
         /// <param name="key">key值</param>
         /// <param name="value">相应值</param>
         public static bool SetConfigValue(string key, string value)
         {
             try
             {
                 Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                 if (config.AppSettings.Settings[key] != null)
                     config.AppSettings.Settings[key].Value = value;
                 else
                     config.AppSettings.Settings.Add(key, value);
                 config.Save(ConfigurationSaveMode.Modified);
                 ConfigurationManager.RefreshSection("appSettings");
                 return true;
             }
             catch
             {
                 return false;
             }
         }

         private void PrintPageForm_Load(object sender, EventArgs e)
         {
             string leftMargin = GetConfigValue("printleft");
             if (string.IsNullOrWhiteSpace(leftMargin.Trim()))
             {
                 tbLeft.Text = SystemInfo.PrintMarginLeft.ToString();
             }
             else
             {
                 tbLeft.Text = leftMargin;
             }
             
         }


    }
}
