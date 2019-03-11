using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

namespace WmsApp.Order
{
    public partial class SelectOrderExeclForm : Form
    {

        private IWMSClient client = null;
        public List<ImportOperateInfo> infoList { get; set; }

        public List<ImportOperateInfo> returnList { get; set; }

        public  int sucCount = 0;
        public int errCount = 0;
        public SelectOrderExeclForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            

            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
          //  WaitForm waitForm = new WaitForm();



           
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show("请选择上传文件");
                    return;
                }

                if (fileName.StartsWith("销售调度通用") == false)
                {
                    MessageBox.Show("导入文件名称不合规范，文件名必须以销售调度通用开头！");
                    return;
                }
                btnSumbit.Enabled = false;
              //  waitForm.Show();
                LoadingHelper.ShowLoadingScreen();
                
                sucCount = 0;
                errCount = 0;

                DataTable dt = null;
                using (ExcelHelper excelHelper = new ExcelHelper(filePath))
                {
                    dt = excelHelper.ExcelToDataTable("MySheet", true);
                }

                if (dt == null)
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("文件导入失败，没有读取到数据");
                    return;
                }

                //判断列明是否存在
                if (!dt.Columns.Contains("下单日期"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【下单日期】列");
                    return;
                }
                if (!dt.Columns.Contains("发货日期"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【发货日期】列");
                    return;
                }
                if (!dt.Columns.Contains("供应商"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【供应商】列");
                    return;
                }

                if (!dt.Columns.Contains("门店编码"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【门店编码】列");
                    return;
                }

                if (!dt.Columns.Contains("门店名称"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【门店名称】列");
                    return;
                }

                if (!dt.Columns.Contains("客户商品编码"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【客户商品编码】列");
                    return;
                }

                if (!dt.Columns.Contains("客户商品名称"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【客户商品名称】列");
                    return;
                }

                if (!dt.Columns.Contains("规格型号"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【规格型号】列");
                    return;
                }

                if (!dt.Columns.Contains("计价单位"))
                {
                    MessageBox.Show("Execl文件中没有【计价单位】列");
                    return;
                }

                if (!dt.Columns.Contains("计划数量"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【计划数量】列");
                    return;
                }
                if (!dt.Columns.Contains("含税单价"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【含税单价】列");
                    return;
                }

                if (!dt.Columns.Contains("税率"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【税率】列");
                    return;
                }

                if (!dt.Columns.Contains("下单人"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【下单人】列");
                    return;
                }

                if (!dt.Columns.Contains("备注"))
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("Execl文件中没有【下单人】列");
                    return;
                }

                string errMsg = "";

                infoList = new List<ImportOperateInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string curErrMsg = "";

                    ImportOperateInfo operationInfo = new ImportOperateInfo();
                    operationInfo.excelNo = i + 1;
                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["下单日期"].ToString()))
                    {
                        errMsg += operationInfo.excelNo + "行，下单日期为空";
                        curErrMsg = "下单日期为空;";
                    }
                    DateTime dtOrderTime;
                    if (!DateTime.TryParse(dt.Rows[i]["下单日期"].ToString(), out dtOrderTime))
                    {
                        errMsg += operationInfo.excelNo + "行，日期格式错误";
                        curErrMsg += "日期格式错误;";
                    }
                    else
                    {

                        operationInfo.orderDate = Convert.ToDateTime(dt.Rows[i]["下单日期"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }


                    //发货日期
                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["发货日期"].ToString()))
                    {
                        errMsg += operationInfo.excelNo + "行，发货日期为空";
                        curErrMsg = "发货日期为空;";
                    }
                    DateTime dtDeliveryTime;
                    if (!DateTime.TryParse(dt.Rows[i]["发货日期"].ToString(), out dtDeliveryTime))
                    {
                        errMsg += operationInfo.excelNo + "行，日期格式错误";
                        curErrMsg += "日期格式错误;";
                    }
                    else
                    {
                        operationInfo.deliveryDate = Convert.ToDateTime(dt.Rows[i]["发货日期"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }



                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["门店编码"].ToString()))
                    {
                        errMsg += operationInfo.excelNo + "行，门店编码不能为空";
                        curErrMsg += "门店编码不能为空;";
                    }
                    else
                    {
                        operationInfo.storedCode = dt.Rows[i]["门店编码"].ToString().Trim();
                    }

                    operationInfo.storedName = dt.Rows[i]["门店名称"].ToString();

                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["客户商品编码"].ToString()))
                    {
                        errMsg += operationInfo.excelNo + "行，客户商品编码不能为空";
                        curErrMsg += "客户商品编码不能为空;";
                    }
                    else
                    {
                        operationInfo.customerSkuCode = dt.Rows[i]["客户商品编码"].ToString().Trim();
                    }
                    operationInfo.customerGoodsName = dt.Rows[i]["客户商品名称"].ToString();
                    operationInfo.goodsModel = dt.Rows[i]["规格型号"].ToString();
                    operationInfo.goodsUnit = dt.Rows[i]["计价单位"].ToString();

                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["计划数量"].ToString()))
                    {
                        errMsg += operationInfo.excelNo + "行，计划数量不能为空";
                        curErrMsg += "计划数量不能为空;";
                    }
                    else
                    {
                        decimal planNum = 0;
                        if (Decimal.TryParse(dt.Rows[i]["计划数量"].ToString(), out planNum))
                        {
                            if (planNum <= 0)
                            {
                                errMsg += operationInfo.excelNo + "行，计划数量必须大于0";
                                curErrMsg += "计划数量必须大于0;";
                            }
                            operationInfo.planNum = Convert.ToDecimal(dt.Rows[i]["计划数量"].ToString());
                        }
                        else
                        {
                            errMsg += operationInfo.excelNo + "行，计划数量非数字";
                            curErrMsg += "计划数量非数字;";
                        }

                    }

                    //含税单价
                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["含税单价"].ToString()))
                    {

                    }
                    else
                    {
                        decimal taxPrice = 0;
                        if (Decimal.TryParse(dt.Rows[i]["含税单价"].ToString(), out taxPrice))
                        {
                            if (taxPrice <= 0)
                            {
                                errMsg += operationInfo.excelNo + "行，含税单价必须大于0";
                                curErrMsg += "含税单价必须大于0;";
                            }
                            operationInfo.taxPrice = Convert.ToDecimal(dt.Rows[i]["含税单价"].ToString());
                        }
                        else
                        {
                            errMsg += operationInfo.excelNo + "行，含税单价非数字";
                            curErrMsg += "含税单价非数字;";
                        }

                    }

                    //税率

                    //含税单价
                    if (string.IsNullOrWhiteSpace(dt.Rows[i]["税率"].ToString()))
                    {

                    }
                    else
                    {
                        decimal taxPrice = 0;
                        if (Decimal.TryParse(dt.Rows[i]["税率"].ToString(), out taxPrice))
                        {
                            if (taxPrice <= 0)
                            {
                                errMsg += operationInfo.excelNo + "行，税率必须大于0";
                                curErrMsg += "税率必须大于0;";
                            }
                            operationInfo.taxRate = Convert.ToDecimal(dt.Rows[i]["税率"].ToString());
                        }
                        else
                        {
                            errMsg += operationInfo.excelNo + "行，税率非数字";
                            curErrMsg += "税率非数字;";
                        }

                    }



                    operationInfo.msg = curErrMsg;
                    operationInfo.remark = dt.Rows[i]["备注"].ToString();
                    operationInfo.orderUser = dt.Rows[i]["下单人"].ToString();
                    operationInfo.customerCode = UserInfo.CustomerCode;
                    operationInfo.customerName = UserInfo.CustomerName;
                    operationInfo.warehouseCode = UserInfo.WareHouseCode;
                    operationInfo.warehouseName = UserInfo.WareHouseName;
                    operationInfo.partnerName = dt.Rows[i]["供应商"].ToString();
                    operationInfo.filename = fileName;
                    infoList.Add(operationInfo);
                }

                //if (!string.IsNullOrWhiteSpace(errMsg))
                //{
                //    MessageBox.Show("导入失败，请查看错误信息!");
                //    this.DialogResult = DialogResult.OK;
                //    return;
                //}

                if (infoList == null || infoList.Count == 0)
                {
                    LoadingHelper.CloseForm();
                    MessageBox.Show("没有导入信息!");
                    return;
                }


       

                List<ImportOperateInfo> errList = new List<ImportOperateInfo>();

                List<ImportOperateInfo> addList = new List<ImportOperateInfo>();

                returnList = new List<ImportOperateInfo>();

                foreach (ImportOperateInfo item in infoList)
                {
                    if (!string.IsNullOrWhiteSpace(item.msg))
                    {
                        errList.Add(item);
                    }
                    else
                    {
                        addList.Add(item);
                    }
                }

                if (addList.Count == 0)
                {
                    returnList.AddRange(errList);
                    LoadingHelper.CloseForm();
                    MessageBox.Show("导入失败");
                    this.DialogResult = DialogResult.OK;
                    return;
                }

                   int pagesize=300;
                   int pagecount=(int) Math.Ceiling((decimal) addList.Count/pagesize);
                     for (int i =1; i <= pagecount; i++)
			          {
                        
                          List<ImportOperateInfo> curList = addList.Skip((i - 1) * pagesize).Take(pagesize).ToList();
                          ImportOrderExeclRequest request = new ImportOrderExeclRequest();
                          request.request = curList;
                          ImportOrderExeclResponse response = client.Execute(request);
                          if (!response.IsError)
                          {
                              returnList.AddRange(response.result);
                              foreach (var item in response.result)
                              {
                                  if (item.msg == "成功")
                                  {
                                      sucCount += 1;
                                  }
                                  else
                                  {
                                      errCount += 1;
                                  }
                              }
                          }

                         // Application.DoEvents();
                   
                     }

                     LoadingHelper.CloseForm();
                        string infoMsg = "共" + returnList.Count + "记录,导入成功:" + sucCount + "条,失败:" + errCount + "条";
                        MessageBox.Show(infoMsg);
                   

                //ImportOrderExeclRequest request = new ImportOrderExeclRequest();
                //request.request = addList;
                //ImportOrderExeclResponse response = client.Execute(request);
                //if (!response.IsError)
                //{
                //    returnList.AddRange(response.result);

                 
                //    foreach (var item in returnList)
                //    {
                //        if (item.msg == "成功")
                //        {
                //            sucCount += 1;
                //        }
                //        else
                //        {
                //            errCount += 1;
                //        }
                //    }
                //    LoadingHelper.CloseForm();
                //    string infoMsg = "共" + returnList.Count + "记录,导入成功:" + sucCount + "条,失败:" + errCount + "条";
                //    MessageBox.Show(infoMsg);
                //}

                //using (ExcelHelper excelHelper = new ExcelHelper(file))
                //{
                //    DataTable data = GenerateData();
                //    int count = excelHelper.DataTableToExcel(data, "MySheet", true);
                //    if (count > 0)
                //        Console.WriteLine("Number of imported data is {0} ", count);
                //}
               
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                LoadingHelper.CloseForm();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnSumbit.Enabled = true;
               // waitForm.Close();
            
            }
           
        }

        private void SelectOrderExeclForm_Load(object sender, EventArgs e)
        {
            lbCustomerName.Text = UserInfo.CustomerName;
            lbWare.Text = UserInfo.WareHouseName;
        }

        string filePath = "";
        string fileName = "";

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "选择导入的文件";
            of.Filter = "Execl   Files(*.xls;*.xlsx)|*.XLS;*.XLSX";
            of.RestoreDirectory = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                filePath = "";
                lbExeclName.Text = "";
                lbExeclName.Text = System.IO.Path.GetFileName(of.FileName);
                fileName = System.IO.Path.GetFileName(of.FileName);
                filePath = of.FileName;
            }
        }
    }
}
