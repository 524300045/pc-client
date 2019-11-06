using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WmsSDK.Model;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WmsApp
{
    public  class CustomerPrint
    {
        double expireDay = 0;
        PreprocessInfo curPreprocessInfo = null;
        Goods goods;

        DateTime sendDate;

        public static Bitmap CreateBigQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 140,
                Height = 140
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }

        public static Bitmap CreateBig80QRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 80,
                Height = 80
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }



        #region 云海肴
        
      
        public void PrintYunHaiYao(List<PreprocessInfo> preprocessInfoList, Goods _goods, double _expireDay,DateTime dt)
        {
            this.sendDate = dt;
            goods = _goods;
            expireDay = _expireDay;
            foreach (PreprocessInfo item in preprocessInfoList)
            {
                #region 打印
              
                 curPreprocessInfo = item;
                PrintDocument document = new PrintDocument();
                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                document.OriginAtMargins = true;
                document.DefaultPageSettings.Landscape = false;
                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintYunHaiYaoPage);
                                dialog.Document = document;
#else
                PrintPreviewDialog dialog = new PrintPreviewDialog();
                document.PrintPage += new PrintPageEventHandler(pd_PrintYunHaiYaoPage);
                dialog.Document = document;
#endif
                try
                {
                    document.Print();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("打印异常" + exception);
                    document.PrintController.OnEndPrint(document, new PrintEventArgs());
                }
                #endregion
            }
        }

        private  void pd_PrintYunHaiYaoPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateBig80QRCode(curPreprocessInfo.preprocessCode);
            GetPrintYunHaiYaoPicture(bt, e, curPreprocessInfo);
        }

        public void GetPrintYunHaiYaoPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

            if (goods.categoryCode == "10")
            {

                #region 分类为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);
                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


   
                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                    height += 15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString("储存方式:1-5℃", new Font("宋体", 8f), brush, layoutRectangleRight);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);

                    height += 15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString(goods.goodsModel + " 储存方式:1-5℃", new Font("宋体", 8f), brush, layoutRectangleRight);
                }

          

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);
                //g.Graphics.DrawString("货主:云海肴", new Font("宋体", 10f), brush, layoutRectangleRight);

                //height += 15;
                ////生产日期
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("生产日期:" + DateTime.Now.AddDays(1).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString() + " 保质期至:" + sendDate.AddDays(expireDay-1).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);

                //height += 15;
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("储存方式:0-8℃", new Font("宋体", 8f), brush, layoutRectangleRight);


                //编码
                //height += 15;
                //Font fontCode = new Font("宋体", 8f);
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangleRight);

               
                
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    height += 15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                if (goods.productWorkshopAttrDesc != null && goods.productWorkshopAttrDesc != "")
                {
                    if (goods.productWorkshopAttrDesc == "三河车间" || goods.productWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("地址:北京平谷区马坊镇英城南桥东400米", new Font("宋体", 8f), brush, layoutRectangleRight);
              
              


                #endregion
            }
            else
            {

                #region 分类不为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);

                    height += 15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString(goods.goodsModel, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                height +=15;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);

                //编码
                //height += 15;
                //Font fontCode = new Font("宋体", 8f);
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangleRight);



                height += 40;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                if (goods.productWorkshopAttrDesc != null && goods.productWorkshopAttrDesc != "")
                {
                    if (goods.productWorkshopAttrDesc == "三河车间" || goods.productWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);

            


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("地址:北京平谷区马坊镇英城南桥东400米", new Font("宋体", 8f), brush, layoutRectangleRight);

                #endregion
            }
        }

        #endregion

        #region 黄记煌


        public void PrintHuangJiHuang(List<PreprocessInfo> preprocessInfoList, Goods _goods, double _expireDay)
        {
            goods = _goods;
            expireDay = _expireDay;
            foreach (PreprocessInfo item in preprocessInfoList)
            {
                #region 打印

                curPreprocessInfo = item;
                PrintDocument document = new PrintDocument();
                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                document.OriginAtMargins = true;
                document.DefaultPageSettings.Landscape = false;
                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintHuangJiHuangPage);
                                dialog.Document = document;
#else
                PrintPreviewDialog dialog = new PrintPreviewDialog();
                document.PrintPage += new PrintPageEventHandler(pd_PrintHuangJiHuangPage);
                dialog.Document = document;
#endif
                try
                {
                    document.Print();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("打印异常" + exception);
                    document.PrintController.OnEndPrint(document, new PrintEventArgs());
                }
                #endregion
            }
        }

        private void pd_PrintHuangJiHuangPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateBig80QRCode(curPreprocessInfo.preprocessCode);
            GetPrintHuangJiHuangPicture(bt, e, curPreprocessInfo);
        }

        public void GetPrintHuangJiHuangPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

            if (goods.categoryCode == "10")
            {

                #region 分类为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);
                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit + "   " + goods.goodsModel, new Font("宋体", 8f), brush, layoutRectangle);

             
                }


                height +=15;
                layoutRectangleRight = new RectangleF(pointX, height, 180f, 85f);
                g.Graphics.DrawString("配料:" + goods.mixContent, new Font("宋体", 6f), brush, layoutRectangleRight);
              


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName + " 储存方式:0-8℃", new Font("宋体", 8f), brush, layoutRectangleRight);
              

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString() + " 保质期至:" + DateTime.Now.AddDays(expireDay-1).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);



                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    height += 15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                if (goods.productWorkshopAttrDesc != null && goods.productWorkshopAttrDesc != "")
                {
                    //if (goods.productWorkshopAttrDesc == "三河车间" || goods.productWorkshopAttrDesc == "腌菜车间")
                    //{
                    //    height += 15;
                    //    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    //    g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    //}
                }


                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);

                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);


                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("地址:北京平谷区马坊镇英城南桥东400米", new Font("宋体", 8f), brush, layoutRectangleRight);




                #endregion
            }
            else
            {

                #region 分类不为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString(preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit + "  " + goods.goodsModel, new Font("宋体", 8f), brush, layoutRectangle);

                    //height += 15;
                    //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    //g.Graphics.DrawString(goods.goodsModel, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 180f, 85f);
                g.Graphics.DrawString("配料:" + goods.mixContent, new Font("宋体", 6f), brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);

            


                height += 40;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                if (goods.productWorkshopAttrDesc != null && goods.productWorkshopAttrDesc != "")
                {
                    //if (goods.productWorkshopAttrDesc == "三河车间" || goods.productWorkshopAttrDesc == "腌菜车间")
                    //{
                    //    height += 15;
                    //    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    //    g.Graphics.DrawString("生产商：三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    //}
                }

                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);




                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);


                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("地址:北京平谷区马坊镇英城南桥东400米", new Font("宋体", 8f), brush, layoutRectangleRight);

                #endregion
            }
        }
        #endregion

    }
}
