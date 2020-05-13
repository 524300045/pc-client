using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WmsApp.Order;
using WmsSDK.Model;

namespace WmsApp
{
    public partial class NavigationForm : TabWindow
    {
        public NavigationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 订单包装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbOrderPackage_Click(object sender, EventArgs e)
        {
            OpenForm("toolStripButton1");
        }


        private void OpenForm(string toolButtonName)
        {

            List<SubMenu> subMenu = null;
            var result = UserInfo.menuDtos.Where(p => p.menuCode == "RE00052");
            if (result != null && result.FirstOrDefault() != null)
            {
                subMenu = result.FirstOrDefault().subMenus;
            }
            else
            {
                MessageBox.Show("您无权限操作当前菜单!");
                return;
            }

            Control c = this.Parent;
            foreach (Control item in c.Parent.Controls)
            {
                if (item.Name == "toolStrip1")
                {
                    ToolStrip ts = (ToolStrip)item;
                    foreach (var tsItem in ts.Items)
                    {
                        if (tsItem is ToolStripButton)
                        {
                            if (((ToolStripButton)tsItem).Name == toolButtonName)
                            {
                                var curResult = subMenu.Where(p => p.subMenuCode == ((ToolStripButton)tsItem).Tag.ToString());
                                if (curResult == null || curResult.FirstOrDefault()==null)
                                {
                                    MessageBox.Show("你无权操作当前菜单!");
                                }
                                else
                                {

                                    ((ToolStripButton)tsItem).PerformClick();
                                }
                              
                            }

                        }
                    }

                }

            }
        }

        private void pbGoodsPackage_Click(object sender, EventArgs e)
        {
            OpenForm("tsbPrePack");
        }

        private void pbPackageDetail_Click(object sender, EventArgs e)
        {
            OpenForm("toolStripButton2");
        }

        private void pbTaskQuery_Click(object sender, EventArgs e)
        {

            OpenForm("tsbTaskQuery");
        }

        private void pbPackTaskQuery_Click(object sender, EventArgs e)
        {
            OpenForm("toolStripButton5");
        }

        private void pbBoxPrint_Click(object sender, EventArgs e)
        {
            OpenForm("tsbBox");
        }

        private void pbKangAnBarCodePrint_Click(object sender, EventArgs e)
        {
            OpenForm("tsbGoodsPrint");
        }

        private void pbSupplierBarCodePrint_Click(object sender, EventArgs e)
        {
            OpenForm("tsbParnterPrint");
        }

        private void pbContaierPrint_Click(object sender, EventArgs e)
        {
            OpenForm("toolStripButton6");
        }

        private void pbPrePackQuery_Click(object sender, EventArgs e)
        {
            OpenForm("toolStripButton4");
        }

        private void pbCloseAll_Click(object sender, EventArgs e)
        {

            Control c = this.Parent;
            foreach (Control item in c.Parent.Controls)
            {
                if (item.Name == "toolStrip1")
                {
                    ToolStrip ts = (ToolStrip)item;
                    foreach (var tsItem in ts.Items)
                    {
                        if (tsItem is ToolStripButton)
                        {
                            if (((ToolStripButton)tsItem).Name == "toolStripButton3")
                            {
                                ((ToolStripButton)tsItem).PerformClick();
                            }
                        }
                    }

                }

            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
          //  OpenForm("tsbExit");
            if (MessageBox.Show("确定要退出系统吗?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void pbSendPrint_Click(object sender, EventArgs e)
        {
            OpenForm("tsbSendPrint");
          
            
        }

        private void pbPrintMargin_Click(object sender, EventArgs e)
        {
            PrintPageForm form = new PrintPageForm();

            form.ShowDialog();
        }

        private void pbOrderImport_Click(object sender, EventArgs e)
        {
            OpenForm("tsbOrderImport"); 
        }

        private void pbSend_Click(object sender, EventArgs e)
        {
            OpenForm("tsbSend"); 
        }

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重新登陆吗?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.ExitThread();
                Restart();
            }
          
        }


        private void Restart()
        {

            Thread thtmp = new Thread(new ParameterizedThreadStart(run));

            object appName = Application.ExecutablePath;

            Thread.Sleep(2000);

            thtmp.Start(appName);

        }

        private void run(Object obj)
        {

            Process ps = new Process();

            ps.StartInfo.FileName = obj.ToString();

            ps.Start();

        }

        private void pbSecondSend_Click(object sender, EventArgs e)
        {
            OpenForm("tsbSecondSend"); 
        }

        /// <summary>
        /// 门店信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbStoreInfo_Click(object sender, EventArgs e)
        {
            OpenForm("tsbStore"); 
        }
    }
}
