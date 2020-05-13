using Common;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

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
            //下载文件
          
#if(!DEBUG)

            //  DownloadFile(Application.StartupPath,"http://api.bjkalf.net:8088/pcUpload", "ReleaseList.xml");
            //升级
              if (!ApplicationDeployment.IsNetworkDeployed && args.Length == 0)
              {
                  Process.Start("SuperUpdate.exe");
                  Application.Exit();
                  return;
              }
#endif
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if(!DEBUG)
            DefalutWMSClient.DefaultServiceAddress = "http://api.cooperate.bjkalf.net/services";

#else
            //DefalutWMSClient.DefaultServiceAddress = " http://127.0.0.1:8480/webservice/services";
           DefalutWMSClient.DefaultServiceAddress = " http://test.api.vcps.bjshengeng.com/services";
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

        public static void DownloadFile(string localFolder, string remoteFolder, string fileName)
        {
            string url = remoteFolder + "/" + fileName;
            string path = localFolder + "/" + fileName;
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            if (res.ContentLength == 0)
                return;

            long fileLength = res.ContentLength;
            string totalSize = FormatFileSizeDescription(100);
            using (Stream srm = res.GetResponseStream())
            {
                var srmReader = new StreamReader(srm);
                var bufferbyte = new byte[fileLength];
                int allByte = bufferbyte.Length;
                int startByte = 0;
                while (fileLength > 0)
                {
                    int downByte = srm.Read(bufferbyte, startByte, allByte);
                    if (downByte == 0)
                    {
                        break;
                    }
                    ;
                    startByte += downByte;
                    allByte -= downByte;

                    //float part = (float)startByte / 1024;
                    //float total = (float)bufferbyte.Length / 1024;
                    //int percent = Convert.ToInt32((part / total) * 100);
                }

                var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(bufferbyte, 0, bufferbyte.Length);
                srm.Close();
                srmReader.Close();
                fs.Close();
            }
        }


        public static string FormatFileSizeDescription(int bytes)
        {
            if (bytes > 1024 * 1024)
                return string.Format("{0}M", Math.Round((double)bytes / (1024 * 1024), 2, MidpointRounding.AwayFromZero));
            if (bytes > 1024)
                return string.Format("{0}K", Math.Round((double)bytes / 1024, 2, MidpointRounding.AwayFromZero));
            return string.Format("{0}B", bytes);
        }
    }
}
