using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wms.Controls.Pager
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> contents, int recordCount, int pageCount)
        {
            ContentList = EnumerableExtension.ToList(contents);
            RecordCount = recordCount;
            PageCount = pageCount;
        }

        #region IPagedList<T> Members

        public List<T> ContentList { get; set; }

        public int RecordCount { get; set; }

        public int PageCount { get; set; }

        #endregion
    }
}
