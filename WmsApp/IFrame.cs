using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace WmsApp
{
    public interface IFrame
    {
        void AddToFrame(IDockContent dockContent);
    }
}
