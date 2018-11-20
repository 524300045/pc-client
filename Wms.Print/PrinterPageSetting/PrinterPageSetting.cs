using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Print.PrinterPageSetting.Enum;

namespace Wms.Print.PrinterPageSetting
{
    /// <summary>
    /// 封装打印纸张设置,打印机设置,打印预览
    /// </summary>
    public class PrinterPageSetting
    {
        private IPrinterPageSetting mPrinterPageSetting;

        private PrintModeFlag mPrintModeFlag;
        /// <summary>
        /// 打印方式
        /// </summary>
        public PrintModeFlag PrintMode
        {
            get { return mPrintModeFlag; }
            set
            {
                mPrintModeFlag = value;
                if (this.mPrintModeFlag == PrintModeFlag.Win)
                {
                    mPrinterPageSetting = new WinPrinterPageSetting();
                }
                else if (this.mPrintModeFlag == PrintModeFlag.Web)
                {
                    mPrinterPageSetting = new WebPrinterPageSetting();
                }
            }
        }
        /// <summary>
        /// 获取或设置打印的文档
        /// </summary>
        public PrintDocument PrintDocument
        {
            get
            {
                return this.mPrinterPageSetting.PrintDocument;
            }
            set
            {
                this.mPrinterPageSetting.PrintDocument = value;
            }
        }

        /// <summary>
        /// 让具体的打印由实例化者来操作
        /// </summary>
        public PrintPageDelegate PrintPageValue
        {
            get
            {
                return this.mPrinterPageSetting.PrintPageValue;
            }
            set
            {
                this.mPrinterPageSetting.PrintPageValue = value;
            }
        }

        /// <summary>
        /// 当需要为当前页打印的输出时发生
        /// </summary>
        public event PrintPageDelegate PrintPage
        {
            add
            {
                this.mPrinterPageSetting.PrintPage += new PrintPageDelegate(value);
            }
            remove
            {
                this.mPrinterPageSetting.PrintPage -= new PrintPageDelegate(value);
            }
        }

        /// <summary>
        /// 导出Excel的实现
        /// </summary>
        public ImportExcelDelegate ImportExcelValue
        {
            set
            {
                mPrinterPageSetting.ImportExcelValue = value;
            }
            get
            {
                return mPrinterPageSetting.ImportExcelValue;
            }
        }

        #region	构造函数
        /// <summary>
        /// 
        /// </summary>
        public PrinterPageSetting()
            : this(null)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printDocument"></param>
        public PrinterPageSetting(PrintDocument printDocument)
        {
            string strPrintMode = "";

            //配置文件中键PrintMode
            strPrintMode = ConfigurationManager.AppSettings["PrintMode"] == null ?
                                                                                    null : ConfigurationManager.AppSettings["PrintMode"].ToString();

            if (strPrintMode == null)
            {
                //默认为Win方式			
                strPrintMode = "Win";
            }

            //配置文件中键PrintMode的值Win/Web
            if (strPrintMode.ToUpper() == "WIN")
            {
                this.PrintMode = PrintModeFlag.Win;
            }
            else
            {
                this.PrintMode = PrintModeFlag.Web;
            }
            if (printDocument != null)
            {
                mPrinterPageSetting.PrintDocument = printDocument;
            }

        }
        #endregion


        /// <summary>
        /// 显示页面设置对话框，并返回PageSettings
        /// </summary>
        /// <returns></returns>
        public PageSettings ShowPageSetupDialog()
        {
            return mPrinterPageSetting.ShowPageSetupDialog();
        }

        /// <summary>
        /// 显示打印机设置对话框，并返回PrinterSettings
        /// </summary>
        /// <returns></returns>
        public PrinterSettings ShowPrintSetupDialog()
        {
            return mPrinterPageSetting.ShowPrintSetupDialog();
        }

        /// <summary>
        /// 显示打印预览对话框
        /// </summary>
        public void ShowPrintPreviewDialog()
        {
            mPrinterPageSetting.ShowPrintPreviewDialog();
        }
    }
}
