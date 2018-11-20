using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms.Print.PrinterPageSetting
{
    /// <summary>
    /// WinForm下打印纸张设置,打印机设置,打印预览对话框显示
    /// </summary>
    public class WinPrinterPageSetting : IPrinterPageSetting
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public WinPrinterPageSetting()
            : this(null)
        { }

        /// <summary>
        /// 构造函数（重载）
        /// </summary>
        /// <param name="printDocument">打印的文档</param>
        public WinPrinterPageSetting(PrintDocument printDocument)
        {
            if (printDocument != null)
                this.mPrintDocument = printDocument;
            else
                this.mPrintDocument = new PrintDocument();
        }


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
            return ShowPageSetupDialog(mPrintDocument);
        }
        /// <summary>
        /// 显示打印设置对话框
        /// </summary>
        /// <returns></returns>
        public PrinterSettings ShowPrintSetupDialog()
        {
            return ShowPrintSetupDialog(mPrintDocument);
        }

        /// <summary>
        /// 显示打印预览对话框
        /// </summary>
        public void ShowPrintPreviewDialog()
        {
            ShowPrintPreviewDialog(this.mPrintDocument);
        }

        #endregion



        /// <summary>
        /// 页面设置对话框
        /// </summary>
        /// <param name="printDocument">打印的文档</param>
        /// <returns></returns>
        protected virtual PageSettings ShowPageSetupDialog(PrintDocument printDocument)
        {
            ThrowPrintDocumentNullException(printDocument);

            PageSettings ps = new PageSettings();
            PageSetupDialog psDlg = new PageSetupDialog();
            ps = printDocument.DefaultPageSettings;
            try
            {
                psDlg.Document = printDocument;
                Margins mg = printDocument.DefaultPageSettings.Margins;
                if (RegionInfo.CurrentRegion.IsMetric)
                {
                    mg = PrinterUnitConvert.Convert(mg, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
                }
                PageSettings psBack = (PageSettings)(printDocument.DefaultPageSettings.Clone());
                psDlg.PageSettings = psBack;
                psDlg.PageSettings.Margins = mg;

                DialogResult result = psDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ps = psDlg.PageSettings;
                    //ps.PaperSize = new PaperSize("Custom", 279, 241);
                    printDocument.DefaultPageSettings = psDlg.PageSettings;
                }

            }
            catch (InvalidPrinterException ex)
            {
                ShowInvalidPrinterExecption(ex);
            }
            catch (System.Exception ex)
            {
                ShowPrinterExecption(ex);
            }
            finally
            {
                psDlg.Dispose();
                psDlg = null;
            }
            return ps;
        }
        /// <summary>
        /// 打印设置对话框
        /// </summary>
        /// <param name="printDocument">打印的文档</param>
        /// <returns></returns>
        protected virtual PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
        {
            ThrowPrintDocumentNullException(printDocument);

            PrinterSettings ps = new PrinterSettings();
            PrintDialog pDlg = new PrintDialog();
            try
            {
                pDlg.AllowSomePages = true;
                pDlg.Document = printDocument;
                DialogResult reslut = pDlg.ShowDialog();
                if (reslut == DialogResult.OK)
                {
                    ps = pDlg.PrinterSettings;
                    printDocument.Print();
                }
            }
            catch (InvalidPrinterException ex)
            {
                ShowInvalidPrinterExecption(ex);
            }
            catch (System.Exception ex)
            {
                ShowPrinterExecption(ex);
            }
            finally
            {
                pDlg.Dispose();
                pDlg = null;
            }
            return ps;
        }
        /// <summary>
        /// 打印预览对话框
        /// </summary>
        /// <param name="printDocument">打印的文档</param>
        protected virtual void ShowPrintPreviewDialog(PrintDocument printDocument)
        {
            ThrowPrintDocumentNullException(printDocument);
            PrintPreviewDialog ppDlg = new PrintPreviewDialog();
            ppDlg.Text = printDocument.DocumentName;
            ppDlg.WindowState = FormWindowState.Maximized;
            if (this.mImportExcelValue != null)
            {
                ToolBar tb = null;
                if (ppDlg.Controls[1] is ToolBar)
                {
                    tb = (ToolBar)ppDlg.Controls[1];
                    ToolBarButton toolbtn = new ToolBarButton();
                    toolbtn.ToolTipText = "Import Excel";
                    toolbtn.ImageIndex = 2;
                    tb.ButtonClick += new ToolBarButtonClickEventHandler(Excel_ButtonClick);
                    tb.Buttons.Add(toolbtn);
                }
                else
                {
                    //vs2005
                    //tb = ((ToolStrip)ppDlg.Controls[1]).Items.Add(new ToolStripButton("Excel"));				
                }
            }
            try
            {
                ppDlg.Document = printDocument;
                DialogResult result = ppDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                }
            }
            catch (InvalidPrinterException ex)
            {
                ShowInvalidPrinterExecption(ex);
            }
            catch (System.Exception ex)
            {
                ShowPrinterExecption(ex);
            }
            finally
            {
                ppDlg.Dispose();
                ppDlg = null;
            }
        }

        /// <summary>
        /// Excel_ButtonClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ToolBarButtonClickEventArgs</param>
        void Excel_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ToolBarButton tb = e.Button;
            string strToolTipText = tb.ToolTipText;
            if (strToolTipText == "Import Excel" && this.mImportExcelValue != null)
            {
                this.mImportExcelValue.BeginInvoke(sender, null, null, null);
            }
        }

        /// <summary>
        /// 检查打印的文档是否为空, 空则抛出异常
        /// </summary>
        /// <param name="printDocument"></param>
        protected virtual void ThrowPrintDocumentNullException(PrintDocument printDocument)
        {
            if (printDocument == null)
            {
                throw new System.Exception("关联的打印文档不能为空");
            }
        }
        /// <summary>
        /// 未安装打印机信息提示
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ShowInvalidPrinterExecption(InvalidPrinterException e)
        {
            MessageBox.Show("未安装打印机,请在系统控制面板中添加打印机", "打印错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 显示打印机其他错误
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ShowPrinterExecption(System.Exception e)
        {
            MessageBox.Show(e.Message, "打印错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
