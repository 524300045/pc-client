using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WmsSDK.Model;

namespace SuperUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        	string tempPath;
		ReleaseList localRelease;
		int totalSize;
		bool downloaded;
		const int ProgressStep = 10;
		const string UpgradeCompleted = "升级完成";
		const string RetryText = " 重  试 ";
        const string FinishText = " 完 成 ";

        private VersionInfo versionInfo;

        private string localVersion;

       
     
        public Form1(
                string _tempPath,
            string _localVersion,
            ReleaseList remoteRelease,
            VersionInfo  info
           )
        {
            InitializeComponent();
            btnUpgrade.Focus();
            this.tempPath = _tempPath;
            this.versionInfo = info;
            this.localVersion = _localVersion;
            this.localRelease = remoteRelease;
        }


        private bool OptionalUpdate
        {
            set
            {
                isEnableCloseButton = btnUpgrade.Enabled = btnCancel.Enabled = value;
            }
        }

        bool isEnableCloseButton;
        protected override CreateParams CreateParams
        {
            get
            {
                if (isEnableCloseButton)
                {
                    CreateParams parameters = base.CreateParams;
                    return parameters;
                }
                else
                {
                    int CS_NOCLOSE = 0x200;
                    CreateParams parameters = base.CreateParams;
                    parameters.ClassStyle |= CS_NOCLOSE;
                    return parameters;
                }
            }
        }


        private void Init()
        {
            lblSize.Text = "下载进度";
            lblTitle.Text = string.Format("当前版本：{0} 最新版本：{1} ", localVersion, versionInfo.versionCode);
            boxDes.Text = versionInfo.remark;
            progressBar1.Maximum = 443 * 1024;
            progressBar1.Step = ProgressStep;
            if (!btnUpgrade.Enabled)
                Upgrade();
            //DoUpgrade();
        }

        Thread trd;
        private void Upgrade()
        {
            trd = new Thread(new ThreadStart(DoUpgrade));
            trd.IsBackground = true;
            trd.Start();
        }

        private void DoUpgrade()
        {
            downloaded = false;
            progressBar1.Value = 0;
            foreach (ReleaseFile file in localRelease.Files)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    DownloadTool.DownloadFile(tempPath,
                        versionInfo.url, file.FileName, progressBar1, lblSize);
                }
                catch (Exception ex)
                {
                    AppTool.DeleteTempFolder(tempPath);
                    MessageBox.Show(file.FileName + "下载失败，请稍后再试");//+ex.Message);
                    OptionalUpdate = true;
                    btnUpgrade.Text = RetryText;
                    trd.Abort();
                    return;
                }
                //progressBar1.Value += file.FileSize*1024;
                //lblSize.Text = string.Format("已完成{0}K/{1}K", progressBar1.Value/1024, totalSize);
            }
            try
            {
                foreach (ReleaseFile file in localRelease.Files)
                {
                    string dir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory + file.FileName);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    File.Copy(tempPath + file.FileName, AppDomain.CurrentDomain.BaseDirectory + file.FileName, true);
                }
            }
            catch (Exception ex)
            {
                AppTool.DeleteTempFolder(tempPath);
                MessageBox.Show(ex.Message, "更新失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OptionalUpdate = true;
                btnUpgrade.Text = RetryText;
                trd.Abort();
                return;
            }
            localRelease.Save(localRelease.FileName);
            //Directory.Delete(tempPath,true);
            btnUpgrade.Enabled = true;
            btnUpgrade.Text = FinishText;
            downloaded = true;
            CreateShortcut();
            Application.Exit();
            AppTool.Start(localRelease.ApplicationStart);
            trd.Abort();
        }

        private void CreateShortcut()
        {
            string fileName = localRelease.AppName;
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) +
                "\\" + fileName + ".lnk";
            if (File.Exists(path))
                return;
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            btnUpgrade.Enabled = false;
            btnCancel.Enabled = false;
            if (!downloaded)
                Upgrade();
            else
            {
                Application.Exit();
                AppTool.Start(localRelease.ApplicationStart);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            AppTool.Start(localRelease.ApplicationStart);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppTool.DeleteTempFolder(tempPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblSize.Text = string.Empty;
            Init();
        }


    }
}
