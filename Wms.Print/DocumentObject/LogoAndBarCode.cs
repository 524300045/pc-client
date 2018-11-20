using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.DocumentObject
{
    /// <summary>
    /// logo
    /// </summary>
    public class LogoAndBarCode : Service.Base.Printer
    {
        /// <summary>
        ///  默认构造函数
        /// </summary>
        public LogoAndBarCode()
        {
            //初始值
            mText = string.Empty;
            mBarCodeIMG = null;
            mHasborder = false;
            Font = new Font("黑体", 36, FontStyle.Bold);
            font2 = new Font("宋体", 9, FontStyle.Regular);
            IsDrawAllPage = true;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogoAndBarCode(string text, Image img)
            : this()
        {
            mText = text;
            mBarCodeIMG = img;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogoAndBarCode(string text, string text2, Image img)
            : this()
        {
            mText = text;
            mText2 = text2;
            mBarCodeIMG = img;
        }
        /// <summary>
        /// 打印vip、svip图片的构造函数
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="img">vip、svip图片</param>
        /// <param name="distance">图片和文字的距离</param>
        public LogoAndBarCode(string text, Image img, int distance)
            : this(text, img)
        {
            this.distance = distance;
        }
        /// <summary>
        /// 图片与右边的距离
        /// </summary>
        private const int CONST_Right_SPACE = 30;
        private string mText;
        private Font font2;
        private string mText2;
        private int distance;

        private Image mCompanyIcon;

        /// <summary>
        /// 公司图标
        /// </summary>
        public Image CompanyIcon
        {
            get
            {
                return mCompanyIcon;
            }
            set { mCompanyIcon = value; }
        }
        /// <summary>
        /// 获取或设置文本
        /// </summary>
        public string Text
        {
            get { return this.mText; }
            set { this.mText = value; }
        }

        private Image mBarCodeIMG;
        /// <summary>
        /// 条码图片
        /// </summary>
        public Image BarCodeIMG
        {
            get { return mBarCodeIMG; }
            set { mBarCodeIMG = value; }
        }
        private bool mHasborder;
        /// <summary>
        /// 获取或设置是否启用边框
        /// </summary>
        public bool HasBorder
        {
            get { return this.mHasborder; }
            set { this.mHasborder = value; }
        }

        /// <summary>
        /// 文字和图片在x方向上的距离
        /// </summary>
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// 绘图
        /// </summary>
        public override void Draw()
        {
            if (mText.Length <= 0) throw new Exception("无文本");
            base.Draw();
            //绘图起始座标及字符串的宽与高
            int x = Rectangle.X;
            int y = Rectangle.Y;
            //相对移动
            x += MoveX;
            y += MoveY;
            //测量字符串尺寸是否过量
            int width = PrinterMargins.Width;
            int txtHeight = TextHeight(mText);	//获取文本的高，测量基宽为有效打印页的宽
            int imgW = 0;
            int imgH = 0;
            if (mBarCodeIMG != null)
            {
                imgW = mBarCodeIMG.Width;
                imgH = mBarCodeIMG.Height;
            }

            int height = txtHeight > imgH ? txtHeight : imgH;
            //不能超过最高
            if (height > PrinterMargins.Height)
            {
                height = PrinterMargins.Height;
            }
            Rectangle = new Rectangle(x, y, width, height);
            //画上打印有效区的线
            if (mHasborder)
            {
                Graphics.DrawRectangle(Pen, Rectangle);
            }

            //打印公司图标
            int imgCompanyW = 0;
            int imgCompanyH = 0;
            if (mCompanyIcon != null)
            {
                imgCompanyW = mCompanyIcon.Width;
                imgCompanyH = mCompanyIcon.Height;
                Rectangle recImg = new Rectangle(x, y, imgCompanyW, imgCompanyH);
                Graphics.DrawImageUnscaledAndClipped(mCompanyIcon, recImg);
            }

            int recTxtWidth = width - imgW - CONST_Right_SPACE;
            if (recTxtWidth <= 0) throw new Exception("供文本打印的区域太小");
            Rectangle recTxt;
            if (imgCompanyW != 0)
            {
                recTxt = new Rectangle(x + imgCompanyW + 20, y, recTxtWidth, height);
            }
            else
            {
                recTxt = new Rectangle(x, y, recTxtWidth, height);
            }
            StringFormat sf = new StringFormat();


            if (string.IsNullOrEmpty(mText2))
            {
                sf.Alignment = StringAlignment.Near;			//横向居中
                sf.LineAlignment = StringAlignment.Center;		//竖向居中			
                //输出文本
                Graphics.DrawString(mText, Font, Brush, recTxt, sf);
            }
            else
            {
                sf.Alignment = StringAlignment.Near;			//横向居中
                sf.LineAlignment = StringAlignment.Far;		//竖向居中			
                //输出文本
                Graphics.DrawString(mText, Font, Brush, recTxt, sf);

                sf.LineAlignment = StringAlignment.Far;

                Rectangle recTxt2 = new Rectangle(x + TextWidth(mText.Split(Convert.ToChar('\n'))[1]), y, recTxtWidth - TextWidth(mText), height);
                Graphics.DrawString(mText2, font2, Brush, recTxt2, sf);
            }

            if (mBarCodeIMG != null)
            {
                if (distance > 0)
                {
                    if (mCompanyIcon != null)
                    {
                        //
                        Rectangle recImg = new Rectangle(x + TextWidth(mText) + distance - imgCompanyW + 20 + 30, y, imgW, height);
                        Graphics.DrawImageUnscaledAndClipped(mBarCodeIMG, recImg);
                    }
                    else
                    {
                        Rectangle recImg = new Rectangle(x + TextWidth(mText) + distance, y, imgW, height);
                        Graphics.DrawImageUnscaledAndClipped(mBarCodeIMG, recImg);
                    }
                }
                else
                {
                    if (mCompanyIcon != null)
                    {
                        //
                        Rectangle recImg = new Rectangle(x + recTxtWidth - imgCompanyW + 20, y, imgW, height);
                        Graphics.DrawImageUnscaledAndClipped(mBarCodeIMG, recImg);
                    }
                    else
                    {
                        Rectangle recImg = new Rectangle(x + recTxtWidth, y, imgW, height);
                        Graphics.DrawImageUnscaledAndClipped(mBarCodeIMG, recImg);
                    }
                }
            }
            Height = height;
        }
    }
}
