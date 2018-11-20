using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.DocumentObject
{
    /// <summary>
    /// 字符串
    /// </summary>
    public class Strings : Service.Base.Printer
    {
        /// <summary>
        ///  默认构造函数
        /// </summary>
        public Strings()
        {
            //初始值
            mText = string.Empty;
            mHasborder = false;
            this.IsDrawAllPage = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Strings(string text)
            : this()
        {
            this.mText = text;
        }
        public Strings(string txt, Font font)
            : this()
        {
            this.mText = txt;
            this.Font = font;
        }
        private string mText;
        /// <summary>
        /// 获取或设置文本
        /// </summary>
        public string Text
        {
            get { return this.mText; }
            set { this.mText = value; }
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
        /// 绘图
        /// </summary>
        public override void Draw()
        {
            if (mText.Length <= 0) throw new Exception("无文本或条码");

            base.Draw();
            //绘图起始座标及字符串的宽与高
            int x, y;
            x = this.Rectangle.X;
            y = this.Rectangle.Y;


            //相对移动
            x += this.MoveX;
            y += this.MoveY;


            //测量字符串尺寸是否过量
            int width = this.PrinterMargins.Width;
            int height = this.TextHeight(this.mText);	//获取文本的高，测量基宽为有效打印页的宽
            //不能超过最高
            if (height > this.PrinterMargins.Height)
            {
                height = this.PrinterMargins.Height;
            }
            this.Rectangle = new Rectangle(x, y, width, height);
            //画上打印有效区的线
            if (mHasborder)
            {
                this.Graphics.DrawRectangle(this.Pen, this.Rectangle);
            }
            //输出文本
            this.Graphics.DrawString(mText, this.Font, this.Brush, this.Rectangle);
            this.Height = height;
        }
    }
}
