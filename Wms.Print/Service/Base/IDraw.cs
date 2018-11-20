using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 绘制接口
    /// </summary>
    public interface IDraw
    {
        /// <summary>
        /// 获取或设置绘图表面
        /// </summary>
        Graphics Graphics
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置绘图区域
        /// </summary>
        Rectangle Rectangle
        {
            get;
            set;
        }

        /// <summary>
        /// 画笔
        /// </summary>
        Pen Pen
        { get; set; }
        /// <summary>
        /// 画刷
        /// </summary>
        Brush Brush
        { get; set; }

        /// <summary>
        /// 绘制
        /// </summary>
        void Draw();
    }
}
