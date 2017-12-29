using Common;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WmsSDK;

namespace WmsApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length == 2)
            {
                MessageBox.Show("请不要运行多个系统！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }
#if(!DEBUG)
            //升级
            //if (!ApplicationDeployment.IsNetworkDeployed && args.Length == 0)
            //{
            //    Process.Start("SuperUpdate.exe");
            //    Application.Exit();
            //    return;
            //}
#endif
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if(!DEBUG)
           // DefalutWMSClient.DefaultServiceAddress = " http://api.cooperate.bjkalf.net:8080/services";
            DefalutWMSClient.DefaultServiceAddress = " http://api.cooperate.bjkalf.net/services";
#else
   DefalutWMSClient.DefaultServiceAddress = " http://127.0.0.1:8089/webservice/services";
           // DefalutWMSClient.DefaultServiceAddress = " http://47.92.86.16:81/services";
    // DefalutWMSClient.DefaultServiceAddress = " http://api.cooperate.bjkalf.net/services";
#endif

            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterParent;
            loginForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            loginForm.ShowDialog();
            if (loginForm.DialogResult != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            Application.Run(MainForm.Instance);

        }

        /// <summary>
        /// 处理应用程序域内的未处理异常（非UI线程异常）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex) {
                LogHelper.Log("CurrentDomain_UnhandledException" + ex.Message);
            }
        }

        /// <summary>
        /// 处理应用程序的未处理异常（UI线程异常）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
