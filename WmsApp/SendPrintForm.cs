﻿using Sdbs.Wms.Controls.Pager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Wms.Controls.Pager;
using Wms.Print;
using Wms.Print.BarCode;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

namespace WmsApp
{
    public partial class SendPrintForm : TabWindow
    {

        private IWMSClient client = null;

        int totalPage;

        private PaginatorDTO paginator;

        private SortableBindingList<ShipmentModel> sortList = null;

        //加载所有打印机


        String printerName = ConfigurationManager.AppSettings["obprint"];

        public SendPrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void bindStore()
        {
            StoreInfoAllRequest request = new StoreInfoAllRequest();
            //request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            StoreInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<StoreInfo> storeList = new List<StoreInfo>();
                    storeList = response.result;
                    storeList.Insert(0, new StoreInfo { storedCode = "0", storedName = "全部" });
                    this.cbStore.DataSource = storeList;
                    this.cbStore.DisplayMember = "storedName";
                    this.cbStore.ValueMember = "storedCode";
                    cbStore.SelectedIndex = 0;
                }
            }
        }

        private void SendPrintForm_Load(object sender, EventArgs e)
        {
            dtBegin.Value = DateTime.Today.AddDays(1);
            dtEnd.Value = DateTime.Today.AddDays(1);
            this.cbPrintStatus.SelectedIndex = 0;
            this.dgv.AutoGenerateColumns = false;
            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();

            cbHeader.Value = string.Empty;

            cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 50 };
            dgv.Columns[0].HeaderCell = cbHeader;
            bindStore();

            List<String> printList = LocalPrinter.GetLocalPrinters(printerName);
            cbPrinter.SelectedIndexChanged -= cbPrinter_SelectedIndexChanged;
            //   cbPrinter.DataSource = printList;
            foreach (var item in printList)
            {
                cbPrinter.Items.Add(item);
            }
            cbPrinter.SelectedIndex = 0;
            cbPrinter.SelectedIndexChanged += cbPrinter_SelectedIndexChanged;
        }


        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            //这一句很重要结束编辑状态
            dgv.EndEdit();
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells[0].Value = state;
                }
            }
        }

        private void BindDgv()
        {

            string storeCode = cbStore.SelectedValue.ToString();
            int? printstatus = null;//打印状态

            if (cbPrintStatus.SelectedIndex == 1)
            {
                printstatus = 0;//未打印
            }

            if (cbPrintStatus.SelectedIndex == 2)
            {
                printstatus = 1;//已打印
            }

            OutBoundPrintQueryRequest request = new OutBoundPrintQueryRequest();
            request.warehouseCode = UserInfo.WareHouseCode;
            request.customerCode = UserInfo.CustomerCode;
            if (cbStore.SelectedIndex != 0)
            {
                request.storedCode = cbStore.SelectedValue.ToString();
            }

            request.startTime = dtBegin.Value;
            request.endTime =Convert.ToDateTime(dtEnd.Value.ToString("yyyy-MM-dd")+" 23:59:59");

            request.isPrint = printstatus;

            OutBoundPrintPageResponse response = client.Execute(request);

            if (!response.IsError)
            {

                if (response.result == null)
                {
                    this.dgv.DataSource = null;
                    MessageBox.Show("没有打印任务!");
                    return;
                }

                foreach (var item in response.result)
                {

                    if (item.status == 0)
                    {
                        item.StatusDes = "新建";
                    }
                    if (item.status == 10)
                    {
                        item.StatusDes = "已包装";

                    }
                    if (item.status == 20)
                    {
                        item.StatusDes = "已分拣";
                    }
                    if (item.status == 30)
                    {
                        item.StatusDes = "已发运";
                    }
                    if (item.status == 40)
                    {
                        item.StatusDes = "关闭";
                    }

                    if (item.isPrint == 0)
                    {
                        item.IsPrintDes = "否";
                    }
                    if (item.isPrint == 1)
                    {
                        item.IsPrintDes = "是";
                    }
                }

                int recordCount = response.pageUtil.totalRow;

                if (recordCount % paginator.PageSize == 0)
                {
                    totalPage = recordCount / paginator.PageSize;
                }
                else
                {
                    totalPage = recordCount / paginator.PageSize + 1;
                }

                IPagedList<ShipmentModel> pageList = new PagedList<ShipmentModel>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<ShipmentModel>(pageList.ContentList);
                this.dgv.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();
            }

        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Text = "正在查询";
                btnSearch.Enabled = false;

                BindDgv();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnSearch.Enabled = true;
                btnSearch.Text = "查询";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                int m = 0;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if ((bool)dgv.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        m++;
                    }
                }
                if (m == 0)
                {
                    MessageBox.Show("请选择要打印的单据");
                    return;
                }

                btnPrint.Text = "正在打印...";
                btnPrint.Enabled = false;

                List<string> list = new List<string>();

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if ((bool)dgv.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        String taskCode = this.dgv.Rows[i].Cells["outboundTaskCode"].Value == null ? "" : this.dgv.Rows[i].Cells["outboundTaskCode"].Value.ToString();
                        list.Add(taskCode);
                    }
                }

                foreach (string item in list)
                {
                    Print(item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("打印异常");
            }
            finally
            {
                btnPrint.Enabled = true;
                btnPrint.Text = "打印";
            }
        }

        private void Print(string taskCode)
        {


            OutBoundDetailPrintRequest request = new OutBoundDetailPrintRequest();
            request.outboundTaskCode = taskCode;
            request.printMan = UserInfo.UserName;
            request.updateUser = UserInfo.UserName;
            OutBoundPrintDetailResponse response = client.Execute(request);
            if (!response.IsError)
            {
                List<ShipMentDetailVo> detaiList = response.result.detailList;

                OutBoundPrintModel outBoundPrint = response.result;

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 10, 140));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "出库单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildMultiHeader();
                string[,] arr = OutBoundHelper.ToArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                //  orderPrint.Print();

                orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }

        private void cbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // cfa.AppSettings.Settings["obprint"].Value = cbPrinter.Text.ToString();
            //cfa.Save();
            //SaveConfig("", "obprint");
            SaveConfig(cbPrinter.Text.ToString(), "obprint");

        }


        //第一个参数是xml文件中的add节点的value，第二个参数是add节点的key  
        private void SaveConfig(string ConnenctionString, string strKey)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径  
            string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            // string  strFileName= AppDomain.CurrentDomain.BaseDirectory + "\\exe.config";  
            doc.Load(strFileName);
            //找出名称为“add”的所有元素  
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性  
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素  
                if (att.Value == strKey)
                {
                    //对目标元素中的第二个属性赋值  
                    att = nodes[i].Attributes["value"];
                    att.Value = ConnenctionString;
                    break;
                }
            }
            //保存上面的修改  
            doc.Save(strFileName);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }




    }

    class LocalPrinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();
        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
            return fPrintDocument.PrinterSettings.PrinterName;
        }


        public static List<String> GetLocalPrinters(string printerName)
        {
            List<String> fPrinters = new List<String>();
            if (printerName == "")
            {
                fPrinters.Add(DefaultPrinter()); //默认打印机始终出现在列表的第一项
            }
            else
            {
                fPrinters.Add(printerName);
            }

            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }
    }
}
