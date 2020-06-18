using Sdbs.Wms.Controls.Pager;
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

        //打印配置LIST
        private List<CustomerPrintConfig> printConfigList;


        public SendPrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void GetPrintConfig()
        {
            printConfigList = new List<CustomerPrintConfig>();
            CustomerPrintConfigRequest request = new CustomerPrintConfigRequest();

            CustomerPrintConfigResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    printConfigList = response.result;
                }
            }
        }

        private void bindFreshAttr()
        {
            DictRequest request = new DictRequest();
            request.type = "freshAreaAttr";

            DictResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {

                    List<Dict> list = new List<Dict>();
                    list = response.result;
                    list.Insert(0, new Dict() { code = "-1", name = "全部" });

                    this.cbFreshAttr.DataSource = list;
                    this.cbFreshAttr.ValueMember = "code";
                    this.cbFreshAttr.DisplayMember = "name";
                    cbFreshAttr.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定批次
        /// </summary>
        private void bindWave()
        {
            WaveCustomerStoreRequest request = new WaveCustomerStoreRequest();
            request.warehouseCode = UserInfo.WareHouseCode;
            request.customerCode = UserInfo.CustomerCode;

            WaveCustomerStoreResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    
                    List<WaveCustomerStoreModel> list = new List<WaveCustomerStoreModel>();
                    list = response.result;
                    //list.Insert(0, new WaveCustomerStoreModel() { waveCode = "", waveName = "全部" });

                    //this.cbWave.DataSource = list;
                    //this.cbWave.ValueMember = "waveCode";
                    //this.cbWave.DisplayMember = "waveName";
                    //cbWave.SelectedIndex = 0;

                    this.ccbWave.DataSource = list;
                    this.ccbWave.ValueMember = "waveCode";
                    this.ccbWave.DisplayMember = "waveName";
                }
            }
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
            bindWave();
            printerName = getAttributeValue("obprint");
            bindFreshAttr();
            List<String> printList = LocalPrinter.GetLocalPrinters(printerName);
            cbPrinter.SelectedIndexChanged -= cbPrinter_SelectedIndexChanged;
            //   cbPrinter.DataSource = printList;
            foreach (var item in printList)
            {
                cbPrinter.Items.Add(item);
            }
            cbPrinter.SelectedIndex = 0;
            cbPrinter.SelectedIndexChanged += cbPrinter_SelectedIndexChanged;
            GetPrintConfig();
            dtBegin.Focus();
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
            request.storedName=tbStoredName.Text.Trim();
     
            request.startTime = dtBegin.Value;
            request.endTime = Convert.ToDateTime(dtEnd.Value.ToString("yyyy-MM-dd") + " 23:59:59");

            request.isPrint = printstatus;
           // request.status = 30;
            request.page = paginator.PageNo;

            int? freshAttr = int.Parse(cbFreshAttr.SelectedValue.ToString());
            if (freshAttr == -1)
            {
                request.freshAttr = null;
            }
            else
            {
                request.freshAttr = freshAttr;
            }

           
            if (this.ccbWave.CheckedItems.Count>0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    waveCodeList.Add(wcsModel.waveCode);
                }
                request.waveCodeList = waveCodeList;
            }

            //Console.WriteLine(this.ccbWave.CheckedItems.Count);
            //if (cbWave.SelectedValue.ToString()!="")
            //{
            //    request.waveCode = cbWave.SelectedValue.ToString();
            //}

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

                    if (sortingNumCount!=null)
                    {
                        item.sortingNumCount = Convert.ToDecimal(item.sortingNumCount.ToString("f3"));
                    }
                    if ( item.actualNumCount!=null)
                    {
                        item.actualNumCount = Convert.ToDecimal(item.actualNumCount.ToString("f3"));
                    }
                 
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
                paginator.PageNo = 1;
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

                    var result = printConfigList.Where(p=>p.customerCode==UserInfo.CustomerCode);
                    if (result != null && result.FirstOrDefault() != null)
                    { 
                         //新的打印逻辑
                        PrintNewConfig(item);
                    }
                    else
                    {
                        if (UserInfo.CustomerCode == "4001" || UserInfo.CustomerCode == "12001")
                        {
                            PrintRongDa(item);
                        }
                        else if (UserInfo.CustomerCode == "10001")
                        {
                            PrintTangChen(item);
                        }
                        else if (UserInfo.CustomerCode == "7001")
                        {
                            PrintXiBei(item);
                        }
                        else if (UserInfo.CustomerCode=="7003")
                        {
                            //西贝干调
                            PrintXiBeiGanTiaoNew(item);
                        }
                        else if (UserInfo.CustomerCode=="7002")
                        {
                            //西贝外阜
                            PrintXiBeiGanTiao(item);
                        }
                        else if (UserInfo.CustomerCode=="21001")
                        {
                            //黄记煌
                            PrintHuangJiHuang(item);
                        }
                        else if (UserInfo.CustomerCode=="11001")
                        {
                            //青年餐厅
                            PrintQingNian(item);
                        }
                        else if (UserInfo.CustomerCode == "43001" || UserInfo.CustomerCode == "49001")
                        {
                            //胖哥俩
                            PrintPangGeLiang(item);
                        }
                        else if (UserInfo.CustomerCode == "45001")
                        {
                           // 春播
                            PrintChunBo(item);
                        }
                        else if (UserInfo.CustomerCode=="50005")
                        {
                            //永利达
                            PrintYongLiDa(item);
                        }
                        else
                        {
                            Print(item);
                        }
                    }
                 

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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 1, 1));
                orderPrint.RowsPerPage = 27;
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildMultiHeader();
                string[,] arr = OutBoundHelper.ToArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

               // orderPrint.PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 2400, 2800);
                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

            orderPrint.Print();

              // orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }


        private void PrintChunBo(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(3, 3, 1, 1));
                orderPrint.RowsPerPage = 27;
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildChunBoMultiHeader();
                string[,] arr = OutBoundHelper.ToChunBoArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildChunBoArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                // orderPrint.PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 2400, 2800);
                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                // orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }

        /// <summary>
        /// 西贝干调和西贝外阜
        /// </summary>
        /// <param name="taskCode"></param>
        private void PrintXiBeiGanTiao(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(2, 2, 1, 1));
                orderPrint.RowsPerPage = 27;
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.XiBeiGanTiaoBuildMultiHeader();
                string[,] arr = OutBoundHelper.ToXiBeiGanTiaoArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildXiBeiGanTiaoArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                // orderPrint.PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 2400, 2800);
                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                // orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }

     

        /// <summary>
        /// 打印胖哥俩
        /// </summary>
        /// <param name="taskCode"></param>
        private void PrintPangGeLiang(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 1, 1));
                orderPrint.RowsPerPage = 27;
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildPangGeLiangHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildPangGeLiangMultiHeader();
                string[,] arr = OutBoundHelper.ToPangGeLiangArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildPangGeLiangArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }

        private void PrintQingNian(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 1, 1));
                orderPrint.RowsPerPage = 27;
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildQingNianMultiHeader();
                string[,] arr = OutBoundHelper.ToQingNianArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildQingNianArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                // orderPrint.PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 2400, 2800);
                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                // orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }

        private void PrintHuangJiHuang(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 1, 1));
                orderPrint.RowsPerPage = 27;
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildMultiHuangJiHuangHeader();
                string[,] arr = OutBoundHelper.ToHuangJiHuangArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildHuangJiHuangArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                // orderPrint.PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 2400, 2800);
                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                // orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif


            }
        }
        // 24.13,, 27.94
//        foreach(PaperSize ps in printDoc.PrinterSettings.PaperSizes)
//{
// if(ps.PaperName=="949W300H")
// {
//  printDoc.PrinterSettings.DefaultPageSettings.PaperSize=ps;
//  printDoc.DefaultPageSettings.PaperSize=ps;
// }
//}

        private void PrintRongDa(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 10, 50));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildRongDaMultiHeader();
                string[,] arr = OutBoundHelper.ToRongDaArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildRongDaArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

              orderPrint.Print();

              //  orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif

            }
        }



        private void PrintNewConfig(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 10, 50));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildPrintConfigHeader();
                string[,] arr = OutBoundHelper.ToPrintConfigArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildPrintConfigBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                //  orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif

            }
        }


        /// <summary>
        /// 永利达
        /// </summary>
        /// <param name="taskCode"></param>
        private void PrintYongLiDa(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 10, 50));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildYongLiDaHeader();
                string[,] arr = OutBoundHelper.ToPrintYongLiDaArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildPrintYongLiDaBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                //  orderPrint.Preview();
#else
                //orderPrint.Preview(); 
                orderPrint.Print();
#endif

            }
        }




        private void PrintTangChen(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 10, 50));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                orderPrint.Header = OutBoundHelper.BuildTangChenHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildTangChenMultiHeader();
                string[,] arr = OutBoundHelper.ToTangChenArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildTangChenArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                //  orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif

            }
        }



        private void PrintXiBei(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(10, 10, 10, 50));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
              //  orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);

                if (response.result.storedCode == "C020097")
                {
                    orderPrint.BarCode = OutBoundHelper.BuildBarCode("三河市鲜洁农产品有限公司", 1, null);
                }
                else
                {
                    if (response.result.orderNo.StartsWith("CS"))
                    {
                        orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);
                    }
                    else
                    {
                        orderPrint.BarCode = OutBoundHelper.BuildBarCode("北京康安利丰农业有限公司送货单", 1, null);
                    }
                }
              
                
                orderPrint.Header = OutBoundHelper.BuildXiBeiHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildXiBeiHeader();
                string[,] arr = OutBoundHelper.ToXiBeiArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildXiBeiArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                //  orderPrint.Preview();
#else
            //orderPrint.Preview(); 
            orderPrint.Print();
#endif

            }
        }



        private void PrintXiBeiGanTiaoNew(string taskCode)
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

                OutBoundPrint orderPrint = new OutBoundPrint(false, new Margins(2, 2, 10, 50));
                Image barcode = Code128Rendering.GetCodeAorBImg(taskCode, 70, 1, true);
                //  orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);

                orderPrint.BarCode = OutBoundHelper.BuildBarCode(response.result.remark + "送货单", 1, null);


                orderPrint.Header = OutBoundHelper.BuildHeader(outBoundPrint);
                orderPrint.MultiHeader1 = OutBoundHelper.BuildXiBeiGanTiaoHeaderNew();
                string[,] arr = OutBoundHelper.ToXiBeiGanTiaoNewArrFromList(detaiList);
                orderPrint.Body1 = OutBoundHelper.BuildXiBeiGanTiaoNewArriveBody(arr);
                orderPrint.Bottom = OutBoundHelper.BuildBottom(response.result.companyAddress, UserInfo.RealName);
                orderPrint.Footer = OutBoundHelper.BuildFooter();

                orderPrint.PrintDocument.PrinterSettings.PrinterName = cbPrinter.Text;
#if DEBUG

                orderPrint.Print();

                //  orderPrint.Preview();
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


        private string getAttributeValue(string strKey)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径  
            string strFileName = Application.StartupPath+"\\config.xml";
            // string  strFileName= AppDomain.CurrentDomain.BaseDirectory + "\\exe.config";  
            doc.Load(strFileName);
            //找出名称为“add”的所有元素  
            XmlNodeList nodes = doc.GetElementsByTagName("config");
            if (nodes != null && nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性  
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素  
                    if (att.Value == strKey)
                    {
                        //对目标元素中的第二个属性赋值  
                        att = nodes[i].Attributes["value"];
                        string vaule = att.Value;
                        return vaule;
                        break;
                    }
                }
            }

            return "";
        }

        //第一个参数是xml文件中的add节点的value，第二个参数是add节点的key  
        private void SaveConfig(string ConnenctionString, string strKey)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径  
          //  string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            // string  strFileName= AppDomain.CurrentDomain.BaseDirectory + "\\exe.config";  

            string strFileName = Application.StartupPath + "\\config.xml";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素  
            XmlNodeList nodes = doc.GetElementsByTagName("config");
            if (nodes != null && nodes.Count > 0)
            {
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
            }
            else
            {

                //XmlNode memberlist = doc.SelectSingleNode("configuration/appSettings");
                //XmlElement memberB = doc.CreateElement("add");
                //memberB.SetAttribute("key", "obprint");
                //memberB.SetAttribute("value", ConnenctionString);
                //memberlist.AppendChild(memberB);
            }
          
            //保存上面的修改  
            doc.Save(strFileName);
           // System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            BindDgv();
        }

        private void ccbWave_AllClick(object sender, EventArgs e)
        {
            List<string> selectList = new List<string>();

            if (this.ccbWave.CheckedItems.Count > 0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    //selectText += wcsModel.waveName + ",";
                    selectList.Add(wcsModel.waveName);
                }
            }

            string selectText = "";
            foreach (var item in selectList)
            {
                selectText += item + ",";
            }
            selectText = selectText.Trim(',');
            this.ccbWave.SetTitleText(selectText);
        }

        private void ccbWave_ItemClick(object sender, ItemCheckEventArgs e)
        {
            List<string> selectList = new List<string>();

            if (this.ccbWave.CheckedItems.Count > 0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    //selectText += wcsModel.waveName + ",";
                    selectList.Add(wcsModel.waveName);
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                //  selectText+=
                WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)this.ccbWave.Items[e.Index];
                selectList.Add(wcsModel.waveName);
            }

            if (e.NewValue != CheckState.Checked)
            {
                WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)this.ccbWave.Items[e.Index];
                selectList.Remove(wcsModel.waveName);
            }

            string selectText = "";
            foreach (var item in selectList)
            {
                selectText += item + ",";
            }
            selectText = selectText.Trim(',');
            this.ccbWave.SetTitleText(selectText);
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
