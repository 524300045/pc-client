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

        List<PreprocessInfo> curPreprocessInfoList = null;

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

        public static Bitmap CreateBig110QRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 110,
                Height = 110
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


        public static Bitmap CreateBig60QRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width =60,
                Height =60
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
         //   Bitmap bt = CreateBig80QRCode(curPreprocessInfo.preprocessCode);
            Bitmap bt = CreateBig110QRCode(curPreprocessInfo.preprocessCode);
            
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
                Rectangle destRect = new Rectangle(155, -15, image.Width, image.Height);
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

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString() + " 保质期至:" + sendDate.AddDays(expireDay-1).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);

          
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

                Rectangle destRect = new Rectangle(155, -15, image.Width, image.Height);
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
            Bitmap bt = CreateBig110QRCode(curPreprocessInfo.preprocessCode);
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

                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName + " 储存方式:0-8℃", new Font("宋体", 8f), brush, layoutRectangleRight);


                height += 13;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);

                height +=13;
                layoutRectangleRight = new RectangleF(pointX, height, 180f, 85f);
                g.Graphics.DrawString("配料:" + goods.mixContent, new Font("宋体", 6f), brush, layoutRectangleRight);


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString() + " 保质期至:" + DateTime.Now.AddDays(expireDay-1).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);



                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    height += 13;
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


        #region 空港


        public void PrintKongGang(List<PreprocessInfo> preprocessInfoList, Goods _goods, double _expireDay, DateTime dt)
        {
         //   curPreprocessInfoList
            goods = _goods;
            expireDay = _expireDay;
            this.sendDate = dt;
            curPreprocessInfoList = new List<PreprocessInfo>();

            for (int i = 0; i < preprocessInfoList.Count; i+=2)
            {
                if ((preprocessInfoList.Count - 1) == i)
                {
                    curPreprocessInfoList.Add(preprocessInfoList[i]);
                }
                else
                {
                    curPreprocessInfoList.Add(preprocessInfoList[i]);
                    curPreprocessInfoList.Add(preprocessInfoList[i + 1]);
                }

                #region 打印

                PrintDocument document = new PrintDocument();
                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 400, 270);
                document.OriginAtMargins = true;
                document.DefaultPageSettings.Landscape = false;
                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                PrintDialog dialog = new PrintDialog();
                document.PrintPage += new PrintPageEventHandler(this.pd_PrintKongGangPage);
                dialog.Document = document;
#else
                PrintPreviewDialog dialog = new PrintPreviewDialog();
                document.PrintPage += new PrintPageEventHandler(pd_PrintKongGangPage);
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
                finally
                {
                    curPreprocessInfoList.Clear();
                }
                #endregion
            }

           
        }

        private void pd_PrintKongGangPage(object sender, PrintPageEventArgs e) //触发打印事件
        {

            GetPrintKongGangPicture(e, curPreprocessInfo);
        }

        public void GetPrintKongGangPicture(PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

               
         
            if (curPreprocessInfoList.Count>0)
            {
                if (curPreprocessInfoList.Count==1)
                {
                    #region 一个标签

                    Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                    int height = 65;
                    Brush brush = new SolidBrush(Color.Black);
                    g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                    int pointX = -35;
                    RectangleF layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    //商品名称
                    layoutRectangle = new RectangleF(pointX, height, 190f, 30f);
                    g.Graphics.DrawString("产品名称:" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);

                    //生产日期
                    height += 35;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString(), fontCu11, brush, layoutRectangle);


                    decimal jingHanLiang = 0;
                    if (goods.weighed == 1)
                    {
                        jingHanLiang = curPreprocessInfoList[0].packWeight / 2;
                    }
                    else
                    {
                        if (goods.bomWeight != null)
                        {
                            jingHanLiang = goods.bomWeight / 1000;
                        }
                    }

                    //净含量
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("净 含 量:" + jingHanLiang.ToString("f2") + "KG", fontCu11, brush, layoutRectangle);



                    //存储条件
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("贮存条件:0-5℃", fontCu11, brush, layoutRectangle);



                    //保质期
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("保 质 期:0+" + expireDay + "天", fontCu11, brush, layoutRectangle);



                    //执行标准
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("执行标准:NY/T 1987-2011", fontCu11, brush, layoutRectangle);


                    height += 30;
                    //条码
                    Bitmap bt = CreateBig60QRCode(curPreprocessInfoList[0].preprocessCode);
                    Rectangle destRect = new Rectangle(pointX + 120, height, bt.Width, bt.Height);
                    g.Graphics.DrawImage(bt, destRect, 0, 0, bt.Width, bt.Height, GraphicsUnit.Pixel);
                    #endregion


                }
                if (curPreprocessInfoList.Count==2)
                {
                    #region 一次打印2个标签

                    Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                    int height = 65;
                    Brush brush = new SolidBrush(Color.Black);
                    g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                    int pointX = -35;
                    int width = 200;

                    RectangleF layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    //左边第一个商品名称
                    layoutRectangle = new RectangleF(pointX, height, 190f, 30f);
                    g.Graphics.DrawString("产品名称:" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);

                    //有变第一个商品名称
                    layoutRectangle = new RectangleF(pointX + width, height, 190f, 30f);
                    g.Graphics.DrawString("产品名称:" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);


                    //生产日期
                    height += 35;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString(), fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX+width, height, 300f, 30f);
                    g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString(), fontCu11, brush, layoutRectangle);


                

                    decimal jingHanLiang1 = 0;
                    if (goods.weighed == 1)
                    {
                        jingHanLiang1 = curPreprocessInfoList[0].packWeight / 2;
                    }
                    else
                    {
                        if (goods.bomWeight != null)
                        {
                            jingHanLiang1 = goods.bomWeight / 1000;
                        }
                    }

                    decimal jingHanLiang2 = 0;
                    if (goods.weighed == 1)
                    {
                        jingHanLiang2 = curPreprocessInfoList[1].packWeight / 2;
                    }
                    else
                    {
                        if (goods.bomWeight != null)
                        {
                            jingHanLiang2 = goods.bomWeight / 1000;
                        }
                    }


                    //净含量
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("净 含 量:" + jingHanLiang1.ToString("f2")+"KG", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX+width, height, 300f, 30f);
                    g.Graphics.DrawString("净 含 量:" + jingHanLiang2.ToString("f2") + "KG", fontCu11, brush, layoutRectangle);


                    //存储条件
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("贮存条件:0-5℃", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX+width, height, 300f, 30f);
                    g.Graphics.DrawString("贮存条件:0-5℃", fontCu11, brush, layoutRectangle);



                    //保质期
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("保 质 期:0+" + expireDay + "天", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX+width, height, 300f, 30f);
                    g.Graphics.DrawString("保 质 期:0+" + expireDay + "天", fontCu11, brush, layoutRectangle);

                    //执行标准
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("执行标准:NY/T 1987-2011", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX+width, height, 300f, 30f);
                    g.Graphics.DrawString("执行标准:NY/T 1987-2011", fontCu11, brush, layoutRectangle);


                    height += 30;
                    //条码
                    Bitmap btOne= CreateBig60QRCode(curPreprocessInfoList[0].preprocessCode);
                    Rectangle imgRec = new Rectangle(pointX + 120, height, btOne.Width, btOne.Height);
                    g.Graphics.DrawImage(btOne, imgRec, 0, 0, btOne.Width, btOne.Height, GraphicsUnit.Pixel);
                 
               
                    Bitmap btTwo= CreateBig60QRCode(curPreprocessInfoList[1].preprocessCode);
                    imgRec = new Rectangle(pointX + width + 120, height, btTwo.Width, btTwo.Height);
                    g.Graphics.DrawImage(btTwo, imgRec, 0, 0, btTwo.Width, btTwo.Height, GraphicsUnit.Pixel);

                    #endregion

                }

            }
               

 
       
        }
       

        #endregion

        #region 航食

        public void PrintHangShi(List<PreprocessInfo> preprocessInfoList, Goods _goods, double _expireDay, DateTime dt)
        {
            this.sendDate = dt;
            goods = _goods;
            expireDay = _expireDay;
            curPreprocessInfoList = new List<PreprocessInfo>();

            for (int i = 0; i < preprocessInfoList.Count; i += 2)
            {
                if ((preprocessInfoList.Count - 1) == i)
                {
                    curPreprocessInfoList.Add(preprocessInfoList[i]);
                }
                else
                {
                    curPreprocessInfoList.Add(preprocessInfoList[i]);
                    curPreprocessInfoList.Add(preprocessInfoList[i + 1]);
                }

                #region 打印

                PrintDocument document = new PrintDocument();
                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 400, 270);
                document.OriginAtMargins = true;
                document.DefaultPageSettings.Landscape = false;
                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                PrintDialog dialog = new PrintDialog();
                document.PrintPage += new PrintPageEventHandler(this.pd_PrintHangShiPage);
                dialog.Document = document;
#else
                PrintPreviewDialog dialog = new PrintPreviewDialog();
                document.PrintPage += new PrintPageEventHandler(pd_PrintHangShiPage);
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
                finally
                {
                    curPreprocessInfoList.Clear();
                }
                #endregion
            }


        }

        private void pd_PrintHangShiPage(object sender, PrintPageEventArgs e) //触发打印事件
        {

            GetPrintHangShiPicture(e, curPreprocessInfo);
        }

        public void GetPrintHangShiPicture(PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {



            if (curPreprocessInfoList.Count > 0)
            {
                if (curPreprocessInfoList.Count == 1)
                {
                    #region 一个标签

                    Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                    int height = 65;
                    Brush brush = new SolidBrush(Color.Black);
                    g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                    int pointX = -35;
                    RectangleF layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    //商品名称
                    layoutRectangle = new RectangleF(pointX, height,190f, 30f);
                    g.Graphics.DrawString("产品名称:" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);

                    //生产日期
                    height += 35;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString(), fontCu11, brush, layoutRectangle);


                    decimal jingHanLiang = 0;
                    if (goods.weighed == 1)
                    {
                        jingHanLiang = curPreprocessInfoList[0].packWeight / 2;
                    }
                    else
                    {
                        if (goods.bomWeight != null)
                        {
                            jingHanLiang = goods.bomWeight / 1000;
                        }
                    }


                    //净含量
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("净 含 量:" + jingHanLiang.ToString("f2") + "KG", fontCu11, brush, layoutRectangle);



                    //存储条件
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("贮存条件:", fontCu11, brush, layoutRectangle);



                    //保质期
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("保 质 期:" + expireDay + "天", fontCu11, brush, layoutRectangle);



                    //执行标准
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("执行标准:NY/T 1987-2011", fontCu11, brush, layoutRectangle);


                    height += 30;
                    //条码
                    Bitmap bt = CreateBig60QRCode(curPreprocessInfoList[0].preprocessCode);
                    Rectangle destRect = new Rectangle(pointX + 120, height, bt.Width, bt.Height);
                    g.Graphics.DrawImage(bt, destRect, 0, 0, bt.Width, bt.Height, GraphicsUnit.Pixel);

                    #endregion


                }
                if (curPreprocessInfoList.Count == 2)
                {
                    #region 一次打印2个标签

                    Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                    int height = 65;
                    Brush brush = new SolidBrush(Color.Black);
                    g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                    int pointX = -35;
                    int width = 200;

                    RectangleF layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    //左边第一个商品名称
                    layoutRectangle = new RectangleF(pointX, height, 190f, 30f);
                    g.Graphics.DrawString("产品名称:" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);

                    //有变第一个商品名称
                    layoutRectangle = new RectangleF(pointX + width, height, 190f, 30f);
                    g.Graphics.DrawString("产品名称:" + goods.customerGoodsName, fontCu11, brush, layoutRectangle);


                    //生产日期
                    height += 35;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString(), fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX + width, height, 300f, 30f);
                    g.Graphics.DrawString("生产日期:" + sendDate.ToShortDateString(), fontCu11, brush, layoutRectangle);


                  

                    decimal jingHanLiang1 = 0;
                    if (goods.weighed == 1)
                    {
                        jingHanLiang1 = curPreprocessInfoList[0].packWeight / 2;
                    }
                    else
                    {
                        if (goods.bomWeight != null)
                        {
                            jingHanLiang1 = goods.bomWeight / 1000;
                        }
                    }

                    decimal jingHanLiang2 = 0;
                    if (goods.weighed == 1)
                    {
                        jingHanLiang2 = curPreprocessInfoList[1].packWeight / 2;
                    }
                    else
                    {
                        if (goods.bomWeight != null)
                        {
                            jingHanLiang2 = goods.bomWeight / 1000;
                        }
                    }


                    //净含量
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("净 含 量:" + jingHanLiang1.ToString("f2") + "KG", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX + width, height, 300f, 30f);
                    g.Graphics.DrawString("净 含 量:" + jingHanLiang2.ToString("f2") + "KG", fontCu11, brush, layoutRectangle);


                    //存储条件
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("贮存条件:", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX + width, height, 300f, 30f);
                    g.Graphics.DrawString("贮存条件:", fontCu11, brush, layoutRectangle);



                    //保质期
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("保 质 期:" + expireDay + "天", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX + width, height, 300f, 30f);
                    g.Graphics.DrawString("保 质 期:" + expireDay + "天", fontCu11, brush, layoutRectangle);

                    //执行标准
                    height += 20;
                    layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                    g.Graphics.DrawString("执行标准:NY/T 1987-2011", fontCu11, brush, layoutRectangle);

                    layoutRectangle = new RectangleF(pointX + width, height, 300f, 30f);
                    g.Graphics.DrawString("执行标准:NY/T 1987-2011", fontCu11, brush, layoutRectangle);

                    height += 30;
                    //条码
                    Bitmap btOne = CreateBig60QRCode(curPreprocessInfoList[0].preprocessCode);
                    Rectangle imgRec = new Rectangle(pointX + 120, height, btOne.Width, btOne.Height);
                    g.Graphics.DrawImage(btOne, imgRec, 0, 0, btOne.Width, btOne.Height, GraphicsUnit.Pixel);


                    Bitmap btTwo = CreateBig60QRCode(curPreprocessInfoList[1].preprocessCode);
                    imgRec = new Rectangle(pointX + width + 120, height, btTwo.Width, btTwo.Height);
                    g.Graphics.DrawImage(btTwo, imgRec, 0, 0, btTwo.Width, btTwo.Height, GraphicsUnit.Pixel);

                    #endregion

                }

            }




        }
       


        #endregion
    }
}
