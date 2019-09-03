using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Wms.Print.DocumentObject;
using Wms.Print.Service.Grid;
using WmsSDK.Model;

namespace WmsApp
{
    public  class OutBoundHelper
    {
        /// <summary>
        /// 纸张一行最多打印的汉字数 :50
        /// </summary>
        public const int RowMaxChar = 50;
        /// <summary>
        /// 打印中，表格的默认字体
        /// </summary>
        public static readonly Font bodyDefaultFont = new Font("宋体", 9, FontStyle.Regular);

        public static readonly Font bodyBodyFont2 = new Font("宋体", 20, FontStyle.Bold);
        /// <summary>
        /// 打印中，表格的加粗字体
        /// </summary>
        public static readonly Font bodyBoldFont = new Font("宋体", 11, FontStyle.Bold);
        /// <summary>
        /// 打印中，Strings对象的默认字体
        /// </summary>
        public static readonly Font stringsDefaultFont = new Font("宋体", 15, FontStyle.Bold);
        /// <summary>
        /// LOGO的字体
        /// </summary>
        public static readonly Font LogoFont2 = new Font("黑体", 18, FontStyle.Bold);


        /// <summary>
        /// 生成打印所需的LogoAndBarCode对象
        /// </summary>
        /// <param name="of">订单对象</param>
        /// <param name="index"></param>
        /// <param name="barCodeImg"></param>
        /// <returns></returns>
        public static BarCodeObject BuildBarCode(string batchNo, int index, Image barCodeImg)
        {
            BarCodeObject logo = null;
            logo = new BarCodeObject(batchNo, barCodeImg);
            logo.Font = LogoFont2;
            return logo;
        }




        /// <summary>
        /// 生成打印所需的Strings对象
        /// </summary>
        /// <param name="of">订单对象</param>
        /// <param name="index">第几个</param>
        /// <returns></returns>
        public static Strings BuildStrings()
        {
            Strings strings = null;
            return strings;
        }

     

        /// <summary>
        /// 生成打印所需的MultiHeader对象
        /// </summary>
        /// <returns></returns>
        public static MultiHeader BuildMultiHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 10);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "产品分类.名称.规格", "单位", "发货数", "单价", "#", "产品分类.名称.规格", "单位", "发货数", "单价" } };
            mhObj.ColsWidth = BuildColsWidth();
            return mhObj;
        }


        public static MultiHeader BuildRongDaMultiHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 12);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "名称", "单位", "发货数", "单价", "小计", "#", "名称", "单位", "发货数", "单价", "小计" } };
            mhObj.ColsWidth = BuildRongDaColsWidth();
            return mhObj;
        }

        public static MultiHeader BuildTangChenMultiHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 10);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "名称", "单位", "下单量", "发货数", "#", "名称", "单位", "下单量", "发货数" } };
            mhObj.ColsWidth = BuildTangChenColsWidth();
            return mhObj;
        }



        public static MultiHeader BuildPrintConfigHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 10);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "客户商品编码", "客户商品名称", "单位", "订货数量", "发货数量", "含税单价", "价税合计" } };
            mhObj.ColsWidth = BuildPrintConfigColsWidth();
            return mhObj;
        }

        public static Body BuildPrintConfigBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildPrintConfigColsWidth();
            return body;
        }


        public static int[] BuildPrintConfigColsWidth()
        {
            return new int[] { 30, 100, 250, 60, 80, 100, 100, 110 };
        }



        public static MultiHeader BuildXiBeiHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 10);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "名称", "单位", "订货量", "发货斤数", "斤单价(含税)", "金额(含税)", "斤单价(不含税)", "金额(不含税)" } };
            mhObj.ColsWidth = BuildXiBeiColsWidth();
            return mhObj;
        }

        public static int[] BuildRongDaColsWidth()
        {
            return new int[] { 30, 155, 40, 60, 60, 65, 30, 155, 40, 60, 60, 65 };
        }


        public static int[] BuildTangChenColsWidth()
        {
            return new int[] { 30, 155, 40, 60, 80, 30, 155, 40, 60,80 };
        }


        public static int[] BuildXiBeiColsWidth()
        {
            return new int[] { 30, 175, 40, 60, 80, 100, 100, 110, 100 };
        }




        public static int[] BuildColsWidth()
        {
            return new int[] { 40, 170, 40, 70, 70, 40, 170, 40, 70, 70};
        }



        /// <summary>
        /// 生成打印所需的body对象 换回商品信息
        /// </summary>
        /// <param name="purchase">订单对象</param>
        /// <returns></returns>
        public static Body BuildBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyDefaultFont;
            body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildColsWidth();
            return body;
        }

      

        public static Bottom BuildBottom(string address,string createby)
        {
            Bottom bottom = new Bottom();
            bottom.IsDrawAllPage = false;
          
            bottom.Font = bodyBoldFont;
            bottom.DataSource = new string[,] { 
                  { " 公司地址:"+address ,""},
                   { " 制单人:"+createby ,"物流:                          客户:"}
            };
            return bottom;
        }


        public static Header BuildHeader(OutBoundPrintModel orderModel)
        {
            Header headerObj = new Header();
            headerObj.Font = bodyBoldFont;
            if (orderModel.receiverPhone == null||orderModel.receiverPhone.ToString() =="null")
            {
                orderModel.receiverPhone = "";
            }
            headerObj.DataSource = new string[,] { 
                                                                         { "购货单位:"+orderModel.storedName, "送货日期:"+Convert.ToDateTime(orderModel.deliveryDate).ToString("yyyy-MM-dd"),"交货单号:"+orderModel.outboundTaskCode}, 
                                                                         { "联系人:"+orderModel.receiver, "地址:"+orderModel.address,""},
                                                                          { "客户电话:"+orderModel.receiverPhone,"业务&电话:"+orderModel.customerPhone,""},
                                                                           { "备注:"+orderModel.orderNo,"单据合计:"+orderModel.priceCount.ToString("f3"),""}
                                                                         };
            headerObj.DrawGrid.Merge = GridMergeFlag.Row;
            return headerObj;
        }


        public static Header BuildTangChenHeader(OutBoundPrintModel orderModel)
        {
            Header headerObj = new Header();
            headerObj.Font = bodyBoldFont;
            if (orderModel.receiverPhone == null || orderModel.receiverPhone.ToString() == "null")
            {
                orderModel.receiverPhone = "";
            }
            headerObj.DataSource = new string[,] { 
                                                                         { "购货单位:"+orderModel.storedName, "送货日期:"+Convert.ToDateTime(orderModel.deliveryDate).ToString("yyyy-MM-dd"),"交货单号:"+orderModel.outboundTaskCode}, 
                                                                         { "联系人:"+orderModel.receiver, "地址:"+orderModel.address,""},
                                                                          { "客户电话:"+orderModel.receiverPhone,"业务&电话:"+orderModel.customerPhone,""},
                                                                           { "备注:"+orderModel.orderNo,"",""}
                                                                         };
            headerObj.DrawGrid.Merge = GridMergeFlag.Row;
            return headerObj;
        }


        public static Header BuildXiBeiHeader(OutBoundPrintModel orderModel)
        {
            Header headerObj = new Header();
            headerObj.Font = bodyBoldFont;
            if (orderModel.receiverPhone == null || orderModel.receiverPhone.ToString() == "null")
            {
                orderModel.receiverPhone = "";
            }
            headerObj.DataSource = new string[,] { 
                                                                         { "购货单位:"+orderModel.storedName, "送货日期:"+Convert.ToDateTime(orderModel.deliveryDate).ToString("yyyy-MM-dd"),"交货单号:"+orderModel.outboundTaskCode}, 
                                                                         { "联系人:"+orderModel.receiver, "地址:"+orderModel.address,""},
                                                                          { "客户电话:"+orderModel.receiverPhone,"业务&电话:"+orderModel.customerPhone,""},
                                                                           { "备注:"+orderModel.orderNo,"",""}
                                                                         };
            headerObj.DrawGrid.Merge = GridMergeFlag.Row;
            return headerObj;
        }

        /// <summary>
        /// 生成打印所需的body对象 换回商品信息
        /// </summary>
        /// <param name="purchase">订单对象</param>
        /// <returns></returns>
        public static Body BuildArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
        //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildColsWidth();
            return body;
        }


        public static Body BuildRongDaArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildRongDaColsWidth();
            return body;
        }


        public static Body BuildTangChenArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildTangChenColsWidth();
            return body;
        }

        public static Body BuildXiBeiArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildXiBeiColsWidth();
            return body;
        }

        /// <summary>
        /// 获取图片的十六进制ascii码
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <returns>十六进制ascii码</returns>
        static string GetAsciiHexString(Bitmap bitmap)
        {
            StringBuilder sb = new StringBuilder();
            List<byte> bytes = new List<byte>();
            int bp = 0;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    byte colorValue = (color.R == 0) ? (byte)1 : (byte)0;
                    if (bp++ % 8 == 0)
                    {
                        bytes.Add((byte)(colorValue << 7));
                    }
                    else
                    {
                        bytes[bytes.Count - 1] |= ((bp % 8 == 0) ? colorValue : (byte)(colorValue << (8 - bp % 8)));
                    }
                }
            }
            foreach (byte b in bytes.ToArray())
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string[,] ToArrFromList(List<ShipMentDetailVo> list)
        {

            if (list == null)
            {
                return new string[0, 0];
            }

            int mRows, mCols;
            string[,] arrGridText;

            if (list.Count % 2 == 0)
            {
                mRows = list.Count / 2;
            }
            else
            {
                mRows = list.Count / 2+1;
            }
           
            mCols =10;

            arrGridText = new string[mRows, mCols];

            if (list.Count % 2 == 0)
            {
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {
             
                    arrGridText[i, 0] = (m+ 1).ToString();
                    arrGridText[i, 1] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 4] = list[m].taxPrice.ToString("f2");

                    m = m + 1;
                    arrGridText[i, 5] = (m + 1).ToString();
                    arrGridText[i, 6] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                    arrGridText[i, 7] = list[m].goodsUnit;
                    arrGridText[i, 8] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 9] = list[m].taxPrice.ToString("f2");
                    m = m + 1;
                }
            }
            else
            {
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {
                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                    m = m + 1;
                    if (i!=mRows-1)
                    {
                        arrGridText[i, 5] = (m + 1).ToString();
                        arrGridText[i, 6] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                        arrGridText[i, 7] = list[m].goodsUnit;
                        arrGridText[i, 8] = list[m].deliveryNum.ToString("f3");
                        arrGridText[i, 9] = list[m].taxPrice.ToString("f2");

                        m = m + 1;
                    }
               
                }
            }
      

            return arrGridText;
        }



        public static string[,] ToRongDaArrFromList(List<ShipMentDetailVo> list)
        {

            if (list == null)
            {
                return new string[0, 0];
            }

            int mRows, mCols;
            string[,] arrGridText;

            if (list.Count % 2 == 0)
            {
                mRows = list.Count / 2;
            }
            else
            {
                mRows = list.Count / 2 + 1;
            }

            mCols = 12;

            arrGridText = new string[mRows, mCols];

            if (list.Count % 2 == 0)
            {
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {

                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].goodsName ;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                    arrGridText[i, 5] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                    m = m + 1;
                    arrGridText[i, 6] = (m + 1).ToString();
                    arrGridText[i, 7] = list[m].goodsName;

                    arrGridText[i, 8] = list[m].goodsUnit;
                    arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 10] = list[m].taxPrice.ToString("f2");
                    arrGridText[i, 11] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                    m = m + 1;
                }
            }
            else
            {
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {
                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].goodsName ;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                    arrGridText[i, 5] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                    m = m + 1;
                    if (i != mRows - 1)
                    {
                        arrGridText[i,6] = (m + 1).ToString();
                        arrGridText[i, 7] = list[m].goodsName;

                        arrGridText[i, 8] = list[m].goodsUnit;
                        arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                        arrGridText[i, 10] = list[m].taxPrice.ToString("f2");
                        arrGridText[i, 11] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                        m = m + 1;
                    }

                }
            }


            return arrGridText;
        }


        public static string[,] ToTangChenArrFromList(List<ShipMentDetailVo> list)
        {

            if (list == null)
            {
                return new string[0, 0];
            }

            int mRows, mCols;
            string[,] arrGridText;

            if (list.Count % 2 == 0)
            {
                mRows = list.Count / 2;
            }
            else
            {
                mRows = list.Count / 2 + 1;
            }

            mCols = 10;

            arrGridText = new string[mRows, mCols];

            if (list.Count % 2 == 0)
            {
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {

                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].goodsName;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].planNum.ToString("f2");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f3");
                 
                  //  arrGridText[i, 5] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                    m = m + 1;
                    arrGridText[i, 5] = (m + 1).ToString();
                    arrGridText[i, 6] = list[m].goodsName;

                    arrGridText[i, 7] = list[m].goodsUnit;
                    arrGridText[i, 8] = list[m].planNum.ToString("f2");
                    arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                
                   // arrGridText[i, 11] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                    m = m + 1;
                }
            }
            else
            {
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {
                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].goodsName;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].planNum.ToString("f3");
                    arrGridText[i,4] = list[m].deliveryNum.ToString("f3");
                 //   arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                //    arrGridText[i, 5] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                    m = m + 1;
                    if (i != mRows - 1)
                    {
                        arrGridText[i, 5] = (m + 1).ToString();
                        arrGridText[i, 6] = list[m].goodsName;

                        arrGridText[i, 7] = list[m].goodsUnit;
                        arrGridText[i, 8] = list[m].planNum.ToString("f3");
                        arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                  
                        // arrGridText[i, 11] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                        m = m + 1;
                    }

                }
            }


            return arrGridText;
        }



        public static string[,] ToXiBeiArrFromList(List<ShipMentDetailVo> list)
        {

            if (list == null)
            {
                return new string[0, 0];
            }

            int mRows, mCols;
            string[,] arrGridText;

            mRows = list.Count;
         
            mCols =9;

            arrGridText = new string[mRows, mCols];

   
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {

                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].goodsName;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].planNum.ToString("f2");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f2");
                    if (list[m].modelWeight != null && list[m].modelWeight!=0)
                    {
                        decimal curNum = list[m].deliveryNum * list[m].modelWeight.Value;
                        arrGridText[i, 4] = (curNum / 500).ToString("f2");
                    }
                 


                    arrGridText[i, 5] = list[m].taxPrice.ToString("f2");
                    arrGridText[i, 6] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");

                    arrGridText[i, 7] = list[m].taxNoPrice.ToString("f2");
                    arrGridText[i, 8] = (list[m].taxNoPrice * list[m].deliveryNum).ToString("f2");
        
                    m = m + 1;
                }
          
            return arrGridText;
        }



        public static string[,] ToPrintConfigArrFromList(List<ShipMentDetailVo> list)
        {

            if (list == null)
            {
                return new string[0, 0];
            }

            int mRows, mCols;
            string[,] arrGridText;

            mRows = list.Count;

            mCols = 8;

            arrGridText = new string[mRows, mCols];


            int m = 0;
            for (int i = 0; i < mRows; i++)
            {

                arrGridText[i, 0] = (m + 1).ToString();
                arrGridText[i, 1] = list[m].skuCode;
                arrGridText[i, 2] = list[m].goodsName;
                arrGridText[i, 3] = list[m].goodsUnit;

                if (list[m].changeNum==0)
                {
                    list[m].changeNum = 1;
                }
                arrGridText[i, 4] = (list[m].planNum / list[m].changeNum).ToString("f2");
                arrGridText[i, 5] =(list[m].deliveryNum / list[m].changeNum).ToString("f2");

                if (list[m].customerTaxPrice==null)
                {
                    list[m].customerTaxPrice = list[m].taxPrice;
                }
                arrGridText[i, 6] = list[m].customerTaxPrice.ToString("f2");
                arrGridText[i,7] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");
                m = m + 1;
            }

            return arrGridText;
        }




        public static Footer BuildFooter()
        {
            Footer footer = new Footer();
            footer.IsDrawAllPage = true;

            footer.Font = bodyBoldFont;

            return footer;
        }

    }
}
