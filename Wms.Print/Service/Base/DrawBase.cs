using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 绘制基本类(抽象类)
    /// </summary>
    public abstract class DrawBase : IDraw, IDisposable
    {
        /// <summary>
        /// 绘图表面
        /// </summary>
        private Graphics mGraphics;
        /// <summary>
        /// 绘制区域
        /// </summary>
        private Rectangle mRectangle;
        /// <summary>
        /// 画笔
        /// </summary>
        private Pen mPen;
        /// <summary>
        /// 画刷
        /// </summary>
        private Brush mBrush;

        #region IDisposable 成员
        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            mPen.Dispose();
            mBrush.Dispose();
        }

        #endregion

        #region IDraw 成员
        /// <summary>
        /// 绘图表面
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return mGraphics;
            }
            set
            {
                mGraphics = value;
            }
        }
        /// <summary>
        /// 绘制区域
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return mRectangle;
            }
            set
            {
                mRectangle = value;
            }
        }
        /// <summary>
        /// 画笔
        /// </summary>
        public Pen Pen
        {
            get
            {
                return mPen;
            }
            set
            {
                if (value != null)
                    mPen = value;
            }
        }
        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Brush
        {
            get
            {
                return mBrush;
            }
            set
            {
                if (value != null)
                    mBrush = value;
            }
        }
        /// <summary>
        /// 抽象方法, 绘制,由子类具体实现
        /// </summary>
        public abstract void Draw();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public DrawBase()
        {
            mRectangle = new Rectangle(0, 0, 0, 0);
            mBrush = Brushes.Black;
            mPen = new Pen(mBrush);
            mPen = Pens.Black;
        }
    }
}
