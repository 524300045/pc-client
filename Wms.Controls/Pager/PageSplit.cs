using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wms.Controls.Pager
{
    public partial class PageSplit : UserControl
    {

        int pageNo = 1;
        int pageCount = 1;

        public PageSplit()
        {
            InitializeComponent();
            lblDesp.Text = "";
            lblTotalPage.Text = "";
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo
        {
            set
            {
                pageNo = value;
                if (pageNo < 1) pageNo = 1;
            }
            get { return pageNo; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            set
            {
                pageCount = value;
                if (pageCount < 0) pageCount = 0;
            }
            get
            {
                return pageCount;
            }
        }

        public string Description
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    lblDesp.Visible = false;
                    return;
                }
                lblDesp.Visible = true;
                lblDesp.Text = value;
            }
            get
            {
                return lblDesp.Text;
            }
        }

        private IPageSplit GetIPageSplitObj()
        {
            Form pForm = this.FindForm();
            if (pForm is IPageSplit)
            {
                return (IPageSplit)pForm;
            }
            Control parent = this.Parent;
            while (parent != null)
            {
                if (parent is IPageSplit)
                {
                    return (IPageSplit)parent;
                }
                parent = parent.Parent;
            }
            return null;
        }

        public event System.EventHandler PageChanged;

        private void btfirst_Click(object sender, EventArgs e)
        {
            pageNo = 1;
            if (PageChanged != null)
            {
                PageChanged(sender, e);
                DataBind();
                return;
            }

            IPageSplit iPage = GetIPageSplitObj();
            if (iPage != null)
            {
                iPage.MoveFirst();
            }
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            pageNo--;
            if (PageChanged != null)
            {
                PageChanged(sender, e);
                DataBind();
                return;
            }

            IPageSplit iPage = GetIPageSplitObj();
            if (iPage != null)
            {
                iPage.MovePrev();
            }
        }

        private void btnext_Click(object sender, EventArgs e)
        {
            pageNo++;
            if (PageChanged != null)
            {
                PageChanged(sender, e);
                DataBind();
                return;
            }

            IPageSplit iPage = GetIPageSplitObj();
            if (iPage != null)
            {
                iPage.MoveNext();
            }
        }

        private void btend_Click(object sender, EventArgs e)
        {
            pageNo = pageCount;
            if (PageChanged != null)
            {
                PageChanged(sender, e);
                DataBind();
                return;
            }

            IPageSplit iPage = GetIPageSplitObj();
            if (iPage != null)
            {
                iPage.MoveLast();
            }
        }

        public void DataBind()
        {
            if (pageNo > pageCount)
            {
                pageNo = pageCount;
            }


            btfirst.Enabled = btPrev.Enabled = btnext.Enabled = btend.Enabled = true;


            if (pageNo <= 1)
            {
                btfirst.Enabled = btPrev.Enabled = false;
            }
            if (pageNo == pageCount)
            {
                btend.Enabled = btnext.Enabled = false;
            }

            tbPage.Text = pageNo.ToString();
            lblTotalPage.Text = pageCount.ToString();
        }

        private void PageGo(object sender, EventArgs e)
        {

            int inputPageNo = 0;
            if (!int.TryParse(tbPage.Text, out inputPageNo))
            {
                tbPage.Clear();
                return;
            }
            if (inputPageNo < 1 || inputPageNo > pageCount)
            {
                tbPage.Clear();
                return;
            }
            if (inputPageNo == pageNo)
            {
                return;
            }

            pageNo = inputPageNo;

            if (PageChanged != null)
            {
                PageChanged(sender, e);
                return;
            }

            IPageSplit iPage = GetIPageSplitObj();
            if (iPage != null)
            {
                iPage.PageGo(inputPageNo);
            }
        }

    }
}
