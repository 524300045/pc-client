using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 矩形边框线 
    /// </summary>
    public enum BordersEdgeFlag
    {
        /// <summary>
        /// 四边
        /// </summary>
        FourEdge,
        /// <summary>
        /// 左
        /// </summary>
        Left,
        /// <summary>
        /// 右
        /// </summary>
        Right,
        /// <summary>
        /// 顶
        /// </summary>
        Top,
        /// <summary>
        /// 底
        /// </summary>
        Bottom,
        /// <summary>
        /// 右下对角线
        /// </summary>
        DiagonalDown,
        /// <summary>
        /// 右上对角线
        /// </summary>
        DiagonalUp
    }

    /// <summary>
    /// 提供在绘图表面用指定的笔在指定的矩形区画线，如左、右、顶、底及四边、右下对角线、右上对角线，可以改变Pen来设置线颜色、宽度、样式。
    /// </summary>
    public class DrawRectangle : DrawBase
    {
        //矩形边框线
        private BordersEdgeFlag mBorderEdge;

        //背景颜色
        private Color mBackColor;

        #region 字段属性

        /// <summary>
        /// 获取或设置矩形边框线，有左、右、顶、底及四边、右下对角线、右上对角线等
        /// </summary>
        public BordersEdgeFlag BordersEdge
        {
            get
            {
                return mBorderEdge;
            }
            set
            {
                mBorderEdge = value;
            }
        }

        /// <summary>
        /// 获取或设置填充矩形的背景颜色， 默认为Color.White，只有改变为其它的颜色或显示调用FillRectangle()方法才会去绘背景
        /// </summary>
        public Color BackColor
        {
            get
            {
                return mBackColor;
            }
            set
            {
                mBackColor = value;
            }
        }

        #endregion

        /// <summary>
        /// 在绘图表面用指定的画笔在指定的矩形区绘制矩形或四周边框线或对角线
        /// </summary>
        public DrawRectangle()
        {
            mBorderEdge = BordersEdgeFlag.FourEdge;

            mBackColor = Color.White;
        }

        /// <summary>
        /// 绘形矩形或边框，如没指定BorderEdge，默认绘制整个矩形。
        /// </summary>
        public override void Draw()
        {
            switch (this.BordersEdge)
            {
                case BordersEdgeFlag.Left:
                    DrawLeftLine();
                    break;
                case BordersEdgeFlag.Right:
                    DrawRightLine();
                    break;
                case BordersEdgeFlag.Top:
                    DrawTopLine();
                    break;
                case BordersEdgeFlag.Bottom:
                    DrawBottomLine();
                    break;
                case BordersEdgeFlag.DiagonalDown:
                    DrawDiagonalDownLine();
                    break;
                case BordersEdgeFlag.DiagonalUp:
                    DrawDiagonalUpLine();
                    break;
                default:	//case BordersEdgeFlag.FourEdge:	
                    Draw(this.Graphics, this.Pen, this.Rectangle);
                    break;
            }

            //只有改变颜色才画背景
            if (this.BackColor != Color.White)
            {
                FillRectangle();
            }
        }


        /// <summary>
        /// 用背景颜色填充矩形
        /// </summary>
        public void FillRectangle()
        {
            Pen pen = new Pen(this.BackColor);
            this.Brush = pen.Brush;

            DrawBackColor(this.Graphics, this.Brush, Rectangle);
        }

        /// <summary>
        /// 画矩形左端线
        /// </summary>
        public void DrawLeftLine()
        {
            DrawLeftLine(this.Graphics, this.Pen, this.Rectangle);
        }

        /// <summary>
        /// 画矩形右端线
        /// </summary>
        public void DrawRightLine()
        {
            DrawRightLine(this.Graphics, this.Pen, this.Rectangle);
        }

        /// <summary>
        /// 画矩形顶端线
        /// </summary>
        public void DrawTopLine()
        {
            DrawTopLine(this.Graphics, this.Pen, this.Rectangle);
        }

        /// <summary>
        /// 画矩形底端线
        /// </summary>
        public void DrawBottomLine()
        {
            DrawBottomLine(this.Graphics, this.Pen, this.Rectangle);
        }

        /// <summary>
        /// 画左上角到右下角的对角线
        /// </summary>
        public void DrawDiagonalDownLine()
        {
            DrawDiagonalDownLine(this.Graphics, this.Pen, this.Rectangle);
        }

        /// <summary>
        /// 画左下角到右上角的对角线
        /// </summary>
        public void DrawDiagonalUpLine()
        {
            DrawDiagonalUpLine(this.Graphics, this.Pen, this.Rectangle);
        }

        #region protected 全参数函数
        /// <summary>
        /// 画矩形线
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="rec"></param>
        protected void Draw(Graphics g, Pen pen, Rectangle rec)
        {
            //画网格顶端横线
            g.DrawRectangle(pen, rec.X, rec.Y, rec.Width, rec.Height);
        }

        /// <summary>
        /// 画矩形左端线
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="rec"></param>
        protected void DrawLeftLine(Graphics g, Pen pen, Rectangle rec)
        {
            int X = rec.Left;
            int Y1 = rec.Top;
            int Y2 = rec.Bottom;

            //画网格左端横线
            g.DrawLine(pen, X, Y1, X, Y2);
        }

        /// <summary>
        /// 画矩形右端线
        /// </summary>
        /// <param name="g"></param>
        ///  <param name="pen"></param>
        /// <param name="rec"></param>
        protected void DrawRightLine(Graphics g, Pen pen, Rectangle rec)
        {
            int X = rec.Right;
            int Y1 = rec.Top;
            int Y2 = rec.Bottom;

            //画网格右端横线
            g.DrawLine(pen, X, Y1, X, Y2);
        }

        /// <summary>
        /// 画矩形顶端线
        /// </summary>
        /// <param name="g"></param>
        ///  <param name="pen"></param>
        /// <param name="rec"></param>
        protected void DrawTopLine(Graphics g, Pen pen, Rectangle rec)
        {
            int X1 = rec.Left;
            int X2 = rec.Right;
            int Y = rec.Top;

            //画网格顶端横线
            g.DrawLine(pen, X1, Y, X2, Y);
        }

        /// <summary>
        /// 画矩形底端线
        /// </summary>
        /// <param name="g"></param>
        ///  <param name="pen"></param>
        /// <param name="rec"></param>
        protected void DrawBottomLine(Graphics g, Pen pen, Rectangle rec)
        {
            int X1 = rec.Left;
            int X2 = rec.Right;
            int Y = rec.Bottom;

            //画网格底端横线
            g.DrawLine(pen, X1, Y, X2, Y);
        }

        /// <summary>
        /// 画左上角到右下角的对角线
        /// </summary>
        /// <param name="g"></param>
        ///  <param name="pen"></param>
        /// <param name="rec"></param>
        protected void DrawDiagonalDownLine(Graphics g, Pen pen, Rectangle rec)
        {
            int X1 = rec.X;			//左上角坐标
            int Y1 = rec.Y;
            int X2 = rec.Right;		//右下角
            int Y2 = rec.Bottom;

            //画网格底端横线
            g.DrawLine(pen, X1, Y1, X2, Y2);
        }

        /// <summary>
        /// 画左下角到右上角的对角线
        /// </summary>
        /// <param name="g"></param>
        ///  <param name="pen"></param>
        /// <param name="rec"></param>
        protected void DrawDiagonalUpLine(Graphics g, Pen pen, Rectangle rec)
        {
            int X1 = rec.X;			//左下角坐标
            int Y1 = rec.Bottom;
            int X2 = rec.Right;		//右下角
            int Y2 = rec.Top;

            //画网格底端横线
            g.DrawLine(pen, X1, Y1, X2, Y2);
        }

        /// <summary>
        /// 填充矩形背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="brush"></param>
        /// <param name="rec"></param>
        protected void DrawBackColor(Graphics g, Brush brush, Rectangle rec)
        {
            g.FillRectangle(brush, rec);
        }

        #endregion

    }
}
