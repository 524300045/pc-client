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

        /// <summary>
        /// 胖哥俩
        /// </summary>
        /// <returns></returns>
        public static MultiHeader BuildPangGeLiangMultiHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 12);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "产品分类.名称.规格", "单位", "发货数", "单价", "金额", "#", "产品分类.名称.规格", "单位", "发货数", "单价", "金额" } };
            mhObj.ColsWidth = BuildPangGeLiangColsWidth();
            return mhObj;
        }


        public static MultiHeader BuildQingNianMultiHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 12);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "产品分类.名称.规格", "单位", "订货数", "发货数", "单价", "#", "产品分类.名称.规格", "单位", "订货数", "发货数", "单价" } };
            mhObj.ColsWidth = BuildQingNianColsWidth();
            return mhObj;
        }

        public static MultiHeader BuildMultiHuangJiHuangHeader()
        {
            MultiHeader mhObj;
            mhObj = new MultiHeader(1, 10);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
            mhObj.Text = new string[,] { { "#", "产品分类.名称.规格", "单位", "下单数", "发货数", "#", "产品分类.名称.规格", "单位", "下单数", "发货数" } };
            mhObj.ColsWidth = BuildHuangJiHuangColsWidth();
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
            mhObj = new MultiHeader(1, 9);
            mhObj.Font = bodyBoldFont;
            mhObj.ColsAlign = "CCCCCCC";
           // mhObj.Text = new string[,] { { "#", "名称", "单位", "订货量", "发货斤数", "单价(含税)", "金额(含税)", "单价(不含税)", "金额(不含税)" } };
            mhObj.Text = new string[,] { { "#", "名称", "单位", "订货量", "发货斤数", "单价(含税)", "金额(含税)", "(斤)单价" } };
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
            return new int[] { 30, 175, 40, 60, 80, 100, 100, 110 };
        }

        public static int[] BuildHuangJiHuangColsWidth()
        {
            return new int[] { 40, 170, 40, 70, 70, 40, 170, 40, 70, 70 };
        }


        public static int[] BuildColsWidth()
        {
            return new int[] { 40, 170, 40, 70, 70, 40, 170, 40, 70, 70};
        }

        public static int[] BuildPangGeLiangColsWidth()
        {
            return new int[] { 25, 160, 40, 60, 60,55, 25, 160, 40,60, 60,55 };
        }


        public static int[] BuildQingNianColsWidth()
        {
            return new int[] { 40, 170, 40, 50, 50, 55, 40, 170, 40,50, 50, 55 };
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

        public static Header BuildPangGeLiangHeader(OutBoundPrintModel orderModel)
        {
            Header headerObj = new Header();
            headerObj.Font = bodyBoldFont;
            if (orderModel.receiverPhone == null || orderModel.receiverPhone.ToString() == "null")
            {
                orderModel.receiverPhone = "";
            }
            headerObj.DataSource = new string[,] { 
                                                                         { "购货单位:"+orderModel.storedName, "送货日期:"+Convert.ToDateTime(orderModel.deliveryDate).ToString("yyyy-MM-dd")+"  交货单号:"+orderModel.outboundTaskCode}, 
                                                                         { "联系人:"+orderModel.receiver, "地址:"+orderModel.address},
                                                                          { "客户电话:"+orderModel.receiverPhone,"业务&电话:"+orderModel.customerPhone},
                                                                           { "备注:"+orderModel.orderNo,"单据合计:"+orderModel.priceCount.ToString("f3")}
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
            //String leftAddress = "";
            //if (orderModel.address!=null&&orderModel.address.Length>=16)
            //{
            //    leftAddress = orderModel.address.Substring(16);
            //    orderModel.address = orderModel.address.Substring(0,16);
            //}
            //headerObj.DataSource = new string[,] { 
            //                                                             { "购货单位:"+orderModel.storedName, "送货日期:"+Convert.ToDateTime(orderModel.deliveryDate).ToString("yyyy-MM-dd"),"交货单号:"+orderModel.outboundTaskCode}, 
            //                                                             { "联系人:"+orderModel.receiver, "地址:"+orderModel.address,leftAddress},
            //                                                              { "客户电话:"+orderModel.receiverPhone,"业务&电话:"+orderModel.customerPhone,""},
            //                                                               { "备注:"+orderModel.orderNo,"",""}
            //                                                             };

            string firstRow= "购货单位:"+orderModel.storedName+"    送货日期:"+Convert.ToDateTime(orderModel.deliveryDate).ToString("yyyy-MM-dd")+"    交货单号:"+orderModel.outboundTaskCode;

            string stringSpcaeStr = "";
            for (int i = 0; i < orderModel.storedName.Length; i++)
            {
                stringSpcaeStr += " ";
            }
            string secondRow = "联系人:" + (String.IsNullOrWhiteSpace(orderModel.receiver) ? stringSpcaeStr+" " : orderModel.receiver) + "    地址:" + orderModel.address;

            string thirdRow = "";
            if (string.IsNullOrWhiteSpace(orderModel.receiverPhone))
            {
                 thirdRow = "客户电话:" + stringSpcaeStr + "     业务&电话:" + orderModel.customerPhone;
            }
            else
            {
                 thirdRow = "客户电话:" + orderModel.receiverPhone + "     业务&电话:" + orderModel.customerPhone;
            }
           
            string fourRow = "备注:" + orderModel.orderNo;
            headerObj.DataSource = new string[,] { 
                                                                         {firstRow}, 
                                                                         {secondRow },
                                                                          { thirdRow},
                                                                           { fourRow}
                                                                         };
            headerObj.DrawGrid.Merge = GridMergeFlag.Col;
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


        public static Body BuildPangGeLiangArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            body.DataSource = arr;
            body.ColsWidth = BuildPangGeLiangColsWidth();
            return body;
        }

        public static Body BuildQingNianArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildQingNianColsWidth();
            return body;
        }
        public static Body BuildHuangJiHuangArriveBody(string[,] arr)
        {
            Body body = new Body();
            body.IsAverageColsWidth = false;
            body.Font = bodyBodyFont2;
            //    body.ColsAlignString = "CCCCCCC";
            body.DataSource = arr;
            body.ColsWidth = BuildHuangJiHuangColsWidth();
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

        public static string[,] ToPangGeLiangArrFromList(List<ShipMentDetailVo> list)
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
                    arrGridText[i, 1] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;
                    arrGridText[i, 2] = list[m].goodsUnit;
                  //  arrGridText[i, 3] = list[m].deliveryNum.ToString("f3");

                    if (list[m].changeNum == 0)
                    {
                        list[m].changeNum = 1;
                    }
                    arrGridText[i, 3] = (list[m].deliveryNum / list[m].changeNum).ToString("f3");
                    //if (list[m].modelWeight != null && list[m].modelWeight != 0)
                    //{
                    //    //转换成斤单价
                    //    decimal curJin = list[m].modelWeight.Value / 500;
                    //    arrGridText[i, 4] = (list[m].taxPrice / curJin).ToString("f2");
                    //}
                    //else
                    //{

                    //    arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                    //}
                    arrGridText[i, 4] = (list[m].taxPrice * list[m].changeNum).ToString("f2");
                    arrGridText[i, 5] = (list[m].taxPrice * list[m].deliveryNum).ToString("f3");
                    m = m + 1;
                    arrGridText[i, 6] = (m + 1).ToString();
                    arrGridText[i, 7] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                    if (list[m].changeNum == 0)
                    {
                        list[m].changeNum = 1;
                    }

                    arrGridText[i, 8] = list[m].goodsUnit;
                    arrGridText[i, 9] = (list[m].deliveryNum / list[m].changeNum).ToString("f3");
                  //  arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                  //  arrGridText[i, 10] = list[m].taxPrice.ToString("f2");

                    //if (list[m].modelWeight != null && list[m].modelWeight != 0)
                    //{
                    //    //转换成斤单价
                    //    decimal curJin = list[m].modelWeight.Value / 500;
                    //    arrGridText[i, 10] = (list[m].taxPrice / curJin).ToString("f2");
                    //}
                    //else
                    //{

                    //    arrGridText[i, 10] = list[m].taxPrice.ToString("f2");
                    //}
                    arrGridText[i, 10] = (list[m].taxPrice * list[m].changeNum).ToString("f2");
                    arrGridText[i, 11] = (list[m].taxPrice * list[m].deliveryNum).ToString("f3"); ;
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
                   

                    if (list[m].changeNum == 0)
                    {
                        list[m].changeNum = 1;
                    }
                    arrGridText[i, 3] = (list[m].deliveryNum / list[m].changeNum).ToString("f3");
                    //if (list[m].modelWeight != null && list[m].modelWeight != 0)
                    //{
                    //    //转换成斤单价
                    //    decimal curJin = list[m].modelWeight.Value / 500;
                    //    arrGridText[i, 4] = (list[m].taxPrice / curJin).ToString("f2");
                    //}
                    //else
                    //{

                    //    arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                    //}
                    arrGridText[i, 4] = (list[m].taxPrice * list[m].changeNum).ToString("f2");
                    arrGridText[i, 5] = (list[m].taxPrice * list[m].deliveryNum).ToString("f3"); ;
                    m = m + 1;
                    if (i != mRows - 1)
                    {
                        arrGridText[i, 6] = (m + 1).ToString();
                        arrGridText[i, 7] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                        arrGridText[i, 8] = list[m].goodsUnit;
                     
                       // arrGridText[i, 10] = list[m].taxPrice.ToString("f2");

                        if (list[m].changeNum == 0)
                        {
                            list[m].changeNum = 1;
                        }
                        arrGridText[i, 9] = (list[m].deliveryNum / list[m].changeNum).ToString("f3");
                        arrGridText[i, 10] = (list[m].taxPrice * list[m].changeNum).ToString("f2");

                        //if (list[m].modelWeight != null && list[m].modelWeight != 0)
                        //{
                        //    //转换成斤单价
                        //    decimal curJin = list[m].modelWeight.Value / 500;
                        //    arrGridText[i, 10] = (list[m].taxPrice / curJin).ToString("f2");
                        //}
                        //else
                        //{

                        //    arrGridText[i, 10] = list[m].taxPrice.ToString("f2");
                        //}

                        arrGridText[i, 11] = (list[m].taxPrice * list[m].deliveryNum).ToString("f3");
                        m = m + 1;
                    }

                }
            }


            return arrGridText;
        }


        public static string[,] ToQingNianArrFromList(List<ShipMentDetailVo> list)
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
                    arrGridText[i, 1] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].planNum.ToString("f3");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 5] = list[m].taxPrice.ToString("f2");

                    m = m + 1;
                    arrGridText[i,6] = (m + 1).ToString();
                    arrGridText[i, 7] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                    arrGridText[i, 8] = list[m].goodsUnit;
                    arrGridText[i, 9] = list[m].planNum.ToString("f3");
                    arrGridText[i,10] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 11] = list[m].taxPrice.ToString("f2");
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
                    arrGridText[i, 3] = list[m].planNum.ToString("f3");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f3");
                    arrGridText[i, 5] = list[m].taxPrice.ToString("f2");
                    m = m + 1;
                    if (i != mRows - 1)
                    {
                        arrGridText[i, 6] = (m + 1).ToString();
                        arrGridText[i, 7] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                        arrGridText[i, 8] = list[m].goodsUnit;
                        arrGridText[i, 9] = list[m].planNum.ToString("f3");
                        arrGridText[i, 10] = list[m].deliveryNum.ToString("f3");
                        arrGridText[i, 11] = list[m].taxPrice.ToString("f2");

                        m = m + 1;
                    }

                }
            }


            return arrGridText;
        }


        public static string[,] ToHuangJiHuangArrFromList(List<ShipMentDetailVo> list)
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
                    arrGridText[i, 1] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].planNum.ToString("f3");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f3");
                    

                    m = m + 1;
                    arrGridText[i, 5] = (m + 1).ToString();
                    arrGridText[i, 6] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                    arrGridText[i,7] = list[m].goodsUnit;
                    arrGridText[i, 8] = list[m].planNum.ToString("f3");
                    arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                 //   arrGridText[i, 9] = list[m].taxPrice.ToString("f2");
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
                    arrGridText[i, 3] = list[m].planNum.ToString("f3");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f3");
                   // arrGridText[i, 4] = list[m].taxPrice.ToString("f2");
                    m = m + 1;
                    if (i != mRows - 1)
                    {
                        arrGridText[i, 5] = (m + 1).ToString();
                        arrGridText[i, 6] = list[m].twoCategoryName + "." + list[m].goodsName + "." + list[m].goodsModel;

                        arrGridText[i, 7] = list[m].goodsUnit;
                        arrGridText[i, 8] = list[m].planNum.ToString("f3");
                        arrGridText[i, 9] = list[m].deliveryNum.ToString("f3");
                      //  arrGridText[i, 9] = list[m].taxPrice.ToString("f2");

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
         
            mCols =8;

            arrGridText = new string[mRows+1, mCols];

   
                int m = 0;
                for (int i = 0; i < mRows; i++)
                {

                    arrGridText[i, 0] = (m + 1).ToString();
                    arrGridText[i, 1] = list[m].goodsName;
                    arrGridText[i, 2] = list[m].goodsUnit;
                    arrGridText[i, 3] = list[m].planNum.ToString("f2");
                    arrGridText[i, 4] = list[m].deliveryNum.ToString("f2");
                    if (list[m].modelWeight != null && list[m].modelWeight != 0)
                    {
                        decimal curNum = list[m].deliveryNum * list[m].modelWeight.Value;
                        arrGridText[i, 4] = (curNum / 500).ToString("f2");

                        //转换成斤单价
                        decimal curJin = list[m].modelWeight.Value / 500;
                        arrGridText[i, 7] = (list[m].taxPrice / curJin).ToString("f2");
                    }
                    else
                    {

                        arrGridText[i, 7] = list[m].taxPrice.ToString("f2");
                    }


                    arrGridText[i, 5] = list[m].taxPrice.ToString("f2");
                    arrGridText[i, 6] = (list[m].taxPrice * list[m].deliveryNum).ToString("f2");

                  //  arrGridText[i, 7] = list[m].taxNoPrice.ToString("f2");
                   // arrGridText[i, 8] = (list[m].taxNoPrice * list[m].deliveryNum).ToString("f2");
        
                    m = m + 1;
                }

                if (list.Count>0)
                {
                    decimal totalPlanNum = 0;
                    decimal totalDeliveryNum = 0;
                    decimal totalTaxAmount = 0;
                    decimal totalTaxNoAmount = 0;

                    for (int i = 0; i < list.Count; i++)
                    {
                        totalPlanNum += list[i].planNum;

                        if (list[i].modelWeight != null && list[i].modelWeight != 0)
                        {
                            decimal curNum = list[i].deliveryNum * list[i].modelWeight.Value;
                            totalDeliveryNum += (curNum / 500);
                        }
                        else
                        {
                            totalDeliveryNum += list[i].deliveryNum;
                        }

                        decimal taxAmount = list[i].taxPrice * list[i].deliveryNum;
                        totalTaxAmount += taxAmount;

                        decimal taxNoAmount = list[i].taxNoPrice * list[i].deliveryNum;
                        totalTaxNoAmount += taxNoAmount;
                    }

                    arrGridText[mRows, 0] ="";
                    arrGridText[mRows, 1] ="合计";
                    arrGridText[mRows, 2] ="";
                    arrGridText[mRows, 3] = totalPlanNum.ToString("f2");
                    arrGridText[mRows, 4] = totalDeliveryNum.ToString("f2"); ;
                  



                    arrGridText[mRows, 5] ="";
                    arrGridText[mRows, 6] = totalTaxAmount.ToString("f2");

                    arrGridText[mRows, 7] ="";
                  //  arrGridText[mRows, 8] = totalTaxNoAmount.ToString("f2");
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

              

                if (list[m].goodsUnit == "KG")
                {
                    arrGridText[i, 4] = ((list[m].planNum * list[m].modelWeight.Value) / 1000).ToString("f2");
                    arrGridText[i, 5] = ((list[m].deliveryNum * list[m].modelWeight.Value)/1000).ToString("f2");
                }
                else
                {
                    arrGridText[i, 4] = (list[m].planNum / list[m].changeNum).ToString("f2");
                    arrGridText[i, 5] = (list[m].deliveryNum / list[m].changeNum).ToString("f2");
                }
                //if (Math.Floor(list[m].changeNum) == list[m].changeNum)
                //{
                //    arrGridText[i, 4] = (list[m].planNum / list[m].changeNum).ToString("f2");
                //    arrGridText[i, 5] = (list[m].deliveryNum / list[m].changeNum).ToString("f2");
                //}
                //else
                //{
                //    arrGridText[i, 4] =Math.Floor(list[m].planNum / list[m].changeNum).ToString("f2");
                //    arrGridText[i, 5] = Math.Floor(list[m].deliveryNum / list[m].changeNum).ToString("f2");
                //}
           
 
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
