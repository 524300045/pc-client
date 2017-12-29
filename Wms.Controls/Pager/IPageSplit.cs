using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wms.Controls.Pager
{
    public interface IPageSplit
    {
        void MoveFirst();
        void MovePrev();
        void MoveNext();
        void MoveLast();
        void PageGo(int page);
    }
}
