using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 定义打印过程中单件,即在系统过程中只会实例化一次的对象，并且只有同一个对象。
    /// </summary>
    public class PrinterSingleton
    {
        private PrinterSingleton() { }

        private static PrintDocument pdInstance = null;
        private static PrinterMargins pmInstance = null;

        /// <summary>
        /// 重置
        /// </summary>
        public static void Reset()
        {
            if (pdInstance != null)
                pdInstance.Dispose();
            pdInstance = null;
            pmInstance = null;
        }

        /// <summary>
        /// 获取或设置打印文档
        /// </summary>
        public static PrintDocument PrintDocument
        {
            get
            {
                return GetPrintDocumentInstance();
            }
            set
            {
                pdInstance = value;
            }
        }

        /// <summary>
        /// 获取或设置打印边距
        /// </summary>
        public static PrinterMargins PrinterMargins
        {
            get
            {
                return GetPrinterMarginsInstance();
            }
            set
            {
                pmInstance = value;
            }
        }

        /// <summary>
        /// 获取打印文档实例
        /// </summary>
        /// <returns></returns>
        private static PrintDocument GetPrintDocumentInstance()
        {
            if (pdInstance == null)
                pdInstance = new PrintDocument();
            return pdInstance;
        }

        /// <summary>
        /// 获取打印边距
        /// </summary>
        /// <returns></returns>
        private static PrinterMargins GetPrinterMarginsInstance()
        {
            if (pmInstance == null)
                pmInstance = new PrinterMargins(GetPrintDocumentInstance());
            //pmInstance = new PrinterMargins(10, 10, 10, 10, 800, 700);
            return pmInstance;
        }
    }
}
