using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using WmsApp.Order;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

namespace WmsApp
{
    public partial class MainForm : Form, IFrame
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private static MainForm instance;

        private IWMSClient client = null;
        internal static MainForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainForm();
                }
                return instance;
            }
        }

        public void AddToFrame(IDockContent fx)
        {
            DockContent f = (DockContent)fx;
            f.TabText = f.Text;
            if (dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                f.Show(dockPanel1);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CloseAllTab();
        }

        public void CloseCurrentTab()
        {
            if (dockPanel1.ActiveDocument != null)
            {

                dockPanel1.ActiveDocument.DockHandler.Close();
            }
        }

        public void CloseAllTab()
        {
            while (dockPanel1.DocumentsCount > 0)
            {
                dockPanel1.ActiveDocument.DockHandler.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DockContent fx = FindCurrentForm("PackageTaskForm");
            if (fx == null)
            {
                AddToFrame(new PackageTaskForm());
            }
            else
            {
                fx.Activate();
            }

        }

        private DockContent FindCurrentForm(string name)
        {
            if (dockPanel1.DocumentsCount > 0)
            {
                foreach (IDockContent fx in dockPanel1.Documents)
                {
                    DockContent f = (DockContent)fx;
                    if (f.Name == name)
                    {
                        return f;
                    }
                }
            }

            return null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("PackageDetailForm");
            if (fx == null)
            {
                AddToFrame(new PackageDetailForm());
            }
            else
            {
                fx.Activate();
            }

        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }

        }

        private void tsbPrePack_Click(object sender, EventArgs e)
        {

            DockContent fx = FindCurrentForm("PrePackageForm");
            if (fx == null)
            {
                AddToFrame(new PrePackageForm());
            }
            else
            {
                fx.Activate();
            }

        }

        private void tsbBox_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("BoxPrintForm");
            if (fx == null)
            {
                AddToFrame(new BoxPrintForm());
            }
            else
            {
                fx.Activate();
            }

        }

        private void tsbTaskQuery_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("PackageTaskQueryForm");
            if (fx == null)
            {
                AddToFrame(new PackageTaskQueryForm());
            }
            else
            {
                fx.Activate();
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("PreprocessForm");
            if (fx == null)
            {
                AddToFrame(new PreprocessForm());
            }
            else
            {
                fx.Activate();
            }

        }



        private void MainForm_Load(object sender, EventArgs e)
        {

            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text = this.Text + "--" + UserInfo.PartnerName + "(" + UserInfo.PartnerCode + ")";
            //lbUserName.Text =UserInfo.RealName+"("+ UserInfo.UserName+")";
            this.Text += "----货主:" + UserInfo.CustomerName + "----仓库:" + UserInfo.WareHouseName + "---" + version;
#if(DEBUG)

          this.Text = this.Text + "测试版";
#endif

#if(!DEBUG)



            List<SubMenu> subMenu = null;
            var result = UserInfo.menuDtos.Where(p => p.menuCode == "RE00052");
            if (result != null && result.FirstOrDefault() != null)
            {
                subMenu = result.FirstOrDefault().subMenus;
            }

            if (subMenu == null || subMenu.Count == 0)
            {
                foreach (ToolStripItem item in toolStrip1.Items)
                {
                    if (item.Tag != null)
                    {
                        string tag = item.Tag.ToString();
                        item.Enabled = false;
                    }

                }
            }
            else
            {
                foreach (ToolStripItem item in toolStrip1.Items)
                {
                    if (item.Tag != null)
                    {
                        string tag = item.Tag.ToString();
                        Console.WriteLine(item.Text);
                        var curResult = subMenu.Where(p => p.subMenuCode == tag);
                        if (curResult == null || curResult.FirstOrDefault()==null)
                        {
                            item.Enabled = false;
                        }
                    }

                }
            }
#endif

            //创建节点



            //添加导航窗体

            DockContent fx = FindCurrentForm("NavigationForm");
            if (fx == null)
            {
                AddToFrame(new NavigationForm());
            }
            else
            {
                fx.Activate();
            }


            client = new DefalutWMSClient();
            PartnerRequest request = new PartnerRequest();
            request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            PartnerResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    UserInfo.labelName = response.result.labelName;
                    UserInfo.areaName = response.result.areaName;
                    UserInfo.foodLicenseNo = response.result.foodLicenseNo;
                }
            }

           // UserInfo.labelName = "三河鲜洁农产品有限公司";
            //if (UserInfo.CustomerCode=="7001")
            //{
            //    LoadXiBeiSkuCode();
                
            //}
         
        }




        private void LoadXiBeiSkuCode()
        {
            try
            {
                List<string> skuCodeList = new List<string>();
                string path = Application.StartupPath + "/xibei.txt";
                if (File.Exists(path))
                {
                    StreamReader sr = new StreamReader(path, Encoding.Default);
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line.ToString());
                        skuCodeList.Add(line.Trim().TrimEnd(new char[] {','}));
                    }
                }
                SystemInfo.skuCodeXiBeiList = skuCodeList;
                LogHelper.Log(skuCodeList.Count+"个SKU");
            }
            catch (Exception ex)
            {

                LogHelper.Log(ex.Message);
            }
        
        
        }


        /// <summary>
        /// 供应商包装任务查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            DockContent fx = FindCurrentForm("PartnerPackageTaskQueryForm");
            if (fx == null)
            {
                AddToFrame(new PartnerPackageTaskQueryForm());
            }
            else
            {
                fx.Activate();
            }
        }

        private void tsbGoodsPrint_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("GoodsBarCodePrintForm");
            if (fx == null)
            {
                AddToFrame(new GoodsBarCodePrintForm());
            }
            else
            {
                fx.Activate();
            }
        }

        private void tsbParnterPrint_Click(object sender, EventArgs e)
        {

            DockContent fx = FindCurrentForm("PartnerGoodsBarCodePrintForm");
            if (fx == null)
            {
                AddToFrame(new PartnerGoodsBarCodePrintForm());
            }
            else
            {
                fx.Activate();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {


            DockContent fx = FindCurrentForm("ContainerPrintForm");
            if (fx == null)
            {
                AddToFrame(new ContainerPrintForm());
            }
            else
            {
                fx.Activate();
            }

        }

        private void tsbSendPrint_Click(object sender, EventArgs e)
        {
            LogHelper.Log("tsbSendPrint_Click");
            try
            {
               
                if (!File.Exists(Application.StartupPath + "\\config.xml"))
                {
                    LogHelper.Log(Application.StartupPath);
                    CreateConfigXml();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("tsbSendPrint_Click" + ex.Message);
            }
          
          //  SaveConfig("", "obprint");

            DockContent fx = FindCurrentForm("SendPrintForm");
            if (fx == null)
            {
                AddToFrame(new SendPrintForm());
            }
            else
            {
                fx.Activate();
            }
        }


        private void CreateConfigXml()
        {

            try
            {

           
            //创建XmlDocument对象
            XmlDocument xmlDoc = new XmlDocument();
            //XML的声明<?xml version="1.0" encoding="gb2312"?> 
            XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            //追加xmldecl位置
            xmlDoc.AppendChild(xmlSM);
            //添加一个名为Gen的根节点
            XmlElement xml = xmlDoc.CreateElement("", "root", "");
            //追加Gen的根节点位置
            xmlDoc.AppendChild(xml);
            //添加另一个节点,与Gen所匹配，查找<Gen>
            XmlNode gen = xmlDoc.SelectSingleNode("root");
            //添加一个名为<Zi>的节点   
            XmlElement zi = xmlDoc.CreateElement("config");
            //为<Zi>节点的属性
            zi.SetAttribute("key", "obprint");
            zi.SetAttribute("value", "");
            gen.AppendChild(zi);//添加到<Gen>节点中   
            //保存好创建的XML文档

            xmlDoc.Save(Application.StartupPath + "\\config.xml");

            }
            catch (Exception ex)
            {

                LogHelper.Log(ex.Message);
            }

        }

        private void tsbOrderImport_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("OrderImportForm");
            if (fx == null)
            {
                AddToFrame(new OrderImportForm());
            }
            else
            {
                fx.Activate();
            }
        }

        private void tsbSend_Click(object sender, EventArgs e)
        {
            DockContent fx = FindCurrentForm("OutBoundSendForm");
            if (fx == null)
            {
                AddToFrame(new OutBoundSendForm());
            }
            else
            {
                fx.Activate();
            }
        }


    }
}
