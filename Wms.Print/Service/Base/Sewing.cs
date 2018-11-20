using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 装订线方位
    /// </summary>
    public enum SewingDirectionFlag
    {
        /// <summary>
        /// 在左边装订
        /// </summary>
        Left
        ,
        /// <summary>
        /// 在顶端装订
        /// </summary>
        Top
    }

    /// <summary>
    /// 装订类，在打印页面时可能在页面的左边或顶端为装订专门另预留一部分。    
    /// </summary>
    public class Sewing : IDisposable
    {
        //装订方向、边界、装订线长
        private SewingDirectionFlag mSewingDirection;
        private int mMargin;
        private int mLineLength;

        #region 字段属性
        /// <summary>
        /// 获取或设置装订方向
        /// </summary>
        public SewingDirectionFlag SewingDirection
        {
            set
            {
                this.mSewingDirection = value;
            }
            get
            {
                return this.mSewingDirection;
            }
        }

        /// <summary>
        /// 获取或设置装订预留空白
        /// </summary>
        public int Margin
        {
            set
            {
                this.mMargin = value;
            }
            get
            {
                return this.mMargin;
            }
        }

        /// <summary>
        /// 获取或设置装订线长
        /// </summary>
        public int LineLen
        {
            set
            {
                this.mLineLength = value;
            }
            get
            {
                return this.mLineLength;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public Sewing()
        {
            this.mMargin = 0;
            this.mSewingDirection = SewingDirectionFlag.Left;
            mLineLength = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="margin"></param>
        public Sewing(int margin)
            : this(margin, SewingDirectionFlag.Left, 0)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="margin"></param>
        /// <param name="lineLength"></param>
        public Sewing(int margin, int lineLength)
            : this(margin, SewingDirectionFlag.Left, lineLength)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="margin"></param>
        /// <param name="sewingDirection"></param>
        public Sewing(int margin, SewingDirectionFlag sewingDirection)
            : this(margin, sewingDirection, 0)
        {

        }

        /// <summary>
        /// 完整的构造函数，参数包括全部的字段
        /// </summary>
        /// <param name="margin"></param>
        /// <param name="sewingDirection"></param>
        /// <param name="lineLength"></param>
        public Sewing(int margin, SewingDirectionFlag sewingDirection, int lineLength)
        {
            this.mMargin = margin;
            this.mSewingDirection = sewingDirection;
            this.mLineLength = lineLength;
        }
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {

        }

        #endregion

        /// <summary>
        /// 在指定的绘图表面画装订线
        /// </summary>
        /// <param name="g">绘图表面</param>        
        public void Draw(Graphics g)
        {
            //字体
            Font font = new Font("宋体", 8);
            //装订线文本
            string strText = "装                    订                    线";
            //写文字格式
            StringFormat sf = new StringFormat();
            //居中放
            sf.Alignment = StringAlignment.Center;

            int LeftMargin, TopMargin;
            int PageHeight, PageWidth;
            LeftMargin = TopMargin = this.mMargin;
            PageHeight = PageWidth = this.mLineLength;

            Pen pen = new Pen(Color.Red);
            pen.DashStyle = DashStyle.Dot;

            //左装订线边界
            if (this.mSewingDirection == SewingDirectionFlag.Left)
            {
                //画竖线
                g.DrawLine(pen, LeftMargin, 0, LeftMargin, PageHeight);

                //写文字
                sf.FormatFlags = StringFormatFlags.DirectionVertical;	//文字竖放

                int textWidth = (int)(g.MeasureString("装", font).Width);
                textWidth /= 2;

                Rectangle rec = new Rectangle(LeftMargin - textWidth, 0, LeftMargin - textWidth, PageHeight);

                g.DrawString(strText, font, Brushes.DodgerBlue, rec, sf);
            }
            //上装订线边界
            else if (this.mSewingDirection == SewingDirectionFlag.Top)
            {
                //画横线
                g.DrawLine(pen, 0, TopMargin, PageWidth, TopMargin);

                //写文字
                int textHeight = (int)(g.MeasureString("装", font).Height);
                textHeight /= 2;

                Rectangle rec = new Rectangle(0, TopMargin - textHeight, PageWidth, TopMargin - textHeight);

                g.DrawString(strText, font, Brushes.DodgerBlue, rec, sf);
            }
            pen.Dispose();
            font.Dispose();
            sf.Dispose();
        }

    }
}
