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
                  { " 公司地址:"+address ,"",""},
                   { " 制单人:"+createby ,"物流:"+"","客户:"+""}
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


        public static Footer BuildFooter()
        {
            Footer footer = new Footer();
            footer.IsDrawAllPage = true;

            footer.Font = bodyBoldFont;

            return footer;
        }

    }
}
