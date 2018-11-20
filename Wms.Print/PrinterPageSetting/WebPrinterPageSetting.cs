using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.PrinterPageSetting
{
    /// <summary>
    /// 
    /// </summary>
    public class WebPrinterPageSetting : IPrinterPageSetting
    {
        /// <summary>
        /// PrintPage委托声明
        /// </summary>
        private PrintPageDelegate mPrintPageValue;
        /// <summary>
        /// ImportExcel委托声明
        /// </summary>
        private ImportExcelDelegate mImportExcelValue;
        /// <summary>
        /// 打印的文档
        /// </summary>
        private PrintDocument mPrintDocument;

        #region IPrinterPageSetting 成员

        /// <summary>
        /// 获取或设置打印的文档
        /// </summary>
        public PrintDocument PrintDocument
        {
            get
            {
                return this.mPrintDocument;
            }
            set
            {
                this.mPrintDocument = value;
            }
        }

        /// <summary>
        /// 实例化此类后在调用打印或预览之前要设置此属性,让具体打印由实例化者来操作
        /// </summary>
        public PrintPageDelegate PrintPageValue
        {
            get
            {
                return mPrintPageValue;
            }
            set
            {
                mPrintPageValue = value;
                mPrintDocument.PrintPage += new PrintPageEventHandler(this.mPrintPageValue);
            }
        }

        /// <summary>
        /// 当需要为当前页打印的输出时发生
        /// </summary>
        public event PrintPageDelegate PrintPage
        {
            add
            {
                mPrintDocument.PrintPage += new PrintPageEventHandler(value);
                mPrintPageValue = value;
            }
            remove
            {
                mPrintDocument.PrintPage -= new PrintPageEventHandler(value);
                mPrintPageValue = null;
            }
        }

        /// <summary>
        /// 导出Excel的实现
        /// </summary>
        public ImportExcelDelegate ImportExcelValue
        {
            get
            {
                return mImportExcelValue;
            }
            set
            {
                mImportExcelValue = value;
            }
        }

        /// <summary>
        /// 显示页面设置对话框
        /// </summary>
        /// <returns></returns>
        public PageSettings ShowPageSetupDialog()
        {
            //return ShowPageSetupDialog(mPrintDocument);
            return null;
        }
        /// <summary>
        /// 显示打印设置对话框
        /// </summary>
        /// <returns></returns>
        public PrinterSettings ShowPrintSetupDialog()
        {
            //return ShowPrintSetupDialog(mPrintDocument);
            return null;
        }

        /// <summary>
        /// 显示打印预览对话框
        /// </summary>
        public void ShowPrintPreviewDialog()
        {
            //ShowPrintPreviewDialog(this.mPrintDocument);
        }

        #endregion


    }
}
