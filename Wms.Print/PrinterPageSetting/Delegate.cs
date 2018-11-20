using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.PrinterPageSetting
{
    /// <summary>
    /// PrintDocument.PrintPage的委托定义
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="ev"></param>
    public delegate void PrintPageDelegate(Object obj, PrintPageEventArgs ev);

    /// <summary>
    /// 导出到Excel委托定义
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="ev"></param>
    public delegate void ImportExcelDelegate(Object obj, ImportExcelArgs ev);
}
