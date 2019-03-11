using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WmsApp
{
    public partial class WaitForm : Form
    {
        public string sValue = "加载数据 请稍等.......";
        private Cursor currentCursor;
        private Form FatherForm;
        public WaitForm()
        {
           InitializeComponent();
           timer1.Enabled = true;
           timer1.Start();
           // this.FormBorderStyle = FormBorderStyle.None;
           // this.StartPosition = FormStartPosition.CenterScreen;
           //this.ClientSize = new Size(220, 40);
           // this.ControlBox = false;
           // this.ShowInTaskbar = false;
           // this.TopMost = true;
           // this.Paint += new PaintEventHandler(WaitDialogPaint);
            
        }

        //public void Show(string sValue)
        //{
        //    this.sValue = sValue;
        //    this.Show();

        //}
        //public void Show()
        //{
        //    currentCursor = Cursor.Current;
        //    Cursor.Current = Cursors.WaitCursor;
        //    base.Show();
        //    this.Refresh();
        //}
        //public void Refresh(string sValue)
        //{
        //    this.sValue = sValue;
        //    this.Refresh();
        //}
        //public void Close()
        //{
        //    Cursor.Current = currentCursor;
        //    base.Close();
        //}
        private void WaitDialogPaint(object sender, PaintEventArgs e)
        {
            Rectangle r = e.ClipRectangle;
            r.Inflate(-1, -1);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.RaisedInner);
            e.Graphics.DrawString(sValue, new Font("Arial", 9, FontStyle.Regular), SystemBrushes.WindowText, r, sf);
        }

        int sencond = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sencond=+1;
            lbSecond.Text = sencond.ToString() + "秒";
            Application.DoEvents();
        }
    }
}
