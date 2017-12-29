using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wms.Controls.Pager
{
    /// <summary>
    /// 分页器接口
    /// </summary>
    /// <typeparam name="T">IPagedList<T></typeparam>
    public interface IPagedList<T>
    {
        ///<summary>
        /// 记录列表
        ///</summary>
        List<T> ContentList { get; }
        /// <summary>
        /// 总记录数
        /// </summary>
        int RecordCount { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }
    }
}
