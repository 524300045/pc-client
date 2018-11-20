using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.PrinterPageSetting
{
    /// <summary>
    /// ImportExcelArgs
    /// </summary>
    public class ImportExcelArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Icon ButtonIcon = null;
    }

    /// <summary>
    /// 显示打印纸张设置,打印机设置,打印预览对话框
    /// </summary>
    public interface IPrinterPageSetting
    {
        /// <summary>
        /// 获取或设置打印的文档
        /// </summary>
        PrintDocument PrintDocument
        {
            get;
            set;
        }
        /// <summary>
        /// 关联一个方法, 让具体的打印让实例化者来操作
        /// </summary>
        PrintPageDelegate PrintPageValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当需要为当前页打印的输出时发生
        /// </summary>
        event PrintPageDelegate PrintPage;

        /// <summary>
        /// 导出到Excel委托定义的实现
        /// </summary>
        ImportExcelDelegate ImportExcelValue
        {
            get;
            set;
        }

        /// <summary>
        /// 显示页面设置对话框
        /// </summary>
        /// <returns></returns>
        PageSettings ShowPageSetupDialog();
        /// <summary>
        /// 显示打印机设置对话框
        /// </summary>
        /// <returns></returns>
        PrinterSettings ShowPrintSetupDialog();
        /// <summary>
        /// 显示打印预览对话框
        /// </summary>
        void ShowPrintPreviewDialog();
    }
}
