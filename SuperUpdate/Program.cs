using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WmsSDK;
using WmsSDK.Request;
using WmsSDK.Response;

namespace SuperUpdate
{
    static class Program
    {

        public const string UPDATER_EXE_NAME = "SuperUpgrade.exe";
        public const string ReleaseConfigFileName = "ReleaseList.xml";
        private static ReleaseList localRelease;
        private static ReleaseList remoteRelease;
        private static string tempPath;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string localXmlPath = string.Format("{0}\\{1}", Application.StartupPath, ReleaseConfigFileName);
            localRelease = new ReleaseList(localXmlPath);

            tempPath = Path.GetTempPath();
            //获取服务器版本信息和本地客户端版本信息进行比较
            IWMSClient client = new DefalutWMSClient();
            //DefalutWMSClient.DefaultServiceAddress = "http://172.31.24.17:8089/webservice/services";

            DefalutWMSClient.DefaultServiceAddress = " http://api.cooperate.bjkalf.net:8080/services";

            VersionInfoRequest request = new VersionInfoRequest();
            request.systemCode = "1";
            VersionResponse response = client.Execute(request);
            if (!response.IsError)
            {
                string localVersion= GetFileVersion();//本地版本号
                if (response.result.versionCode != localVersion)
                {
                    if (CheckProcessing() != DialogResult.OK)
                    {
                        AppTool.DeleteTempFolder(tempPath);
                        Application.Exit();
                        return;
                    }
                    //启动升级
                    Form1 form = new Form1(tempPath, localVersion, localRelease,response.result);
                    Application.Run(form);
                }
                else
                {

                    AppTool.DeleteTempFolder(tempPath);
                    Application.Exit();
                    AppTool.Start("CpsApp.exe");
                }
            }
            else
            {
                AppTool.DeleteTempFolder(tempPath);
                Application.Exit();
                AppTool.Start("CpsApp.exe");
            }

        }

        public static String GetFileVersion()
        {
            // Get the file version for the notepad. 
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo("CpsApp.exe");

            return myFileVersionInfo.FileVersion;
      
        }


        static DialogResult CheckProcessing()
        {
            string exeName = localRelease.ApplicationStart.Substring(0, localRelease.ApplicationStart.Length - 4);
            if (Process.GetProcessesByName(exeName).Length > 0)
            {
                var rs = MessageBox.Show(string.Format("请先退出正在运行的{0}", exeName), "警告", MessageBoxButtons.RetryCancel,
                                         MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.Retry)
                {
                    return CheckProcessing();
                }
                return rs;
            }
            return DialogResult.OK;
        }
  
    
         
    }
}
