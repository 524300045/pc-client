using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SuperUpdate
{
    public class ReleaseList : XmlDocument
    {
        private readonly string fileName;
        private string applicationStart;

        private string appName;
        private IList<ReleaseFile> files;
        private string minVersion;
        private string releaseDate;
        private string releaseUrl;
        private string releaseVersion;
        private string shortcutIcon;
        private string updateDes;

        public ReleaseList()
        {
            LoadXml(
                @"<?xml version=""1.0"" encoding=""utf-8""?>
<AutoUpdater>
  <AppName></AppName>
  <ReleaseURL></ReleaseURL>
  <ReleaseDate></ReleaseDate>
  <ReleaseVersion></ReleaseVersion>
  <MinVersion></MinVersion>
  <UpdateDes></UpdateDes>
  <ApplicationStart></ApplicationStart>
  <ShortcutIcon></ShortcutIcon>
  <Releases>
  </Releases>
</AutoUpdater>
			");
        }

        public ReleaseList(string filePath)
        {
            fileName = filePath;
            Load(filePath);
            appName = GetNodeValue("/AutoUpdater/AppName");
            releaseDate = GetNodeValue("/AutoUpdater/ReleaseDate");
            releaseUrl = GetNodeValue("/AutoUpdater/ReleaseURL");
            releaseVersion = GetNodeValue("/AutoUpdater/ReleaseVersion");
            minVersion = GetNodeValue("/AutoUpdater/MinVersion");
            updateDes = GetNodeValue("/AutoUpdater/UpdateDes");
            applicationStart = GetNodeValue("/AutoUpdater/ApplicationStart");
            shortcutIcon = GetNodeValue("/AutoUpdater/ShortcutIcon");
            XmlNodeList fileNodes = GetNodeList("/AutoUpdater/Releases");
            files = new List<ReleaseFile>();
            foreach (XmlNode node in fileNodes)
            {
                files.Add(new ReleaseFile(node.Attributes[0].Value, node.Attributes[1].Value,
                                          Convert.ToInt32(node.Attributes[2].Value)));
            }
        }

        public string AppName
        {
            set
            {
                appName = value;
                SetNodeValue("AutoUpdater/AppName", value);
            }
            get { return appName; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public string ReleaseUrl
        {
            get { return releaseUrl; }
            set
            {
                releaseUrl = value;
                SetNodeValue("AutoUpdater/ReleaseURL", value);
            }
        }

        public string ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                releaseDate = value;
                SetNodeValue("AutoUpdater/ReleaseDate", value);
            }
        }

        public string ReleaseVersion
        {
            get { return releaseVersion; }
            set
            {
                releaseVersion = value;
                SetNodeValue("AutoUpdater/ReleaseVersion", value);
            }
        }

        //public long ReleaseVersionNumber
        //{
        //    get { return string.IsNullOrEmpty(releaseVersion) ? 0 : long.Parse(releaseVersion.Replace(".", "")); }
        //}
        public string MinVersion
        {
            get { return minVersion; }
            set
            {
                minVersion = value;
                SetNodeValue("AutoUpdater/MinVersion", value);
            }
        }

        //public long MinVersionNumber
        //{
        //    get { return string.IsNullOrEmpty(minVersion) ? 0 : long.Parse(minVersion.Replace(".", "")); }
        //}
        public string UpdateDescription
        {
            get { return updateDes; }
            set
            {
                updateDes = value;
                SetNodeValue("AutoUpdater/UpdateDes", value);
            }
        }

        public string ShortcutIcon
        {
            get { return shortcutIcon; }
            set
            {
                shortcutIcon = value;
                SetNodeValue("AutoUpdater/ShortcutIcon", value);
            }
        }

        public string ApplicationStart
        {
            get { return applicationStart; }
            set
            {
                applicationStart = value;
                SetNodeValue("AutoUpdater/ApplicationStart", value);
            }
        }

        public IList<ReleaseFile> Files
        {
            get { return files; }
            set
            {
                files = value;
                RefreshFileNodes();
            }
        }

        public int Compare(string version)
        {
            string[] myVersion = releaseVersion.Split('.');
            string[] otherVersion = version.Split('.');
            int i = 0;
            foreach (string v in myVersion)
            {
                int myNumber = int.Parse(v);
                int otherNumber = int.Parse(otherVersion[i]);
                if (myNumber != otherNumber)
                    return myNumber - otherNumber;
                i++;
            }
            return 0;
        }

        public int Compare(ReleaseList otherList)
        {
            if (otherList == null)
                throw new ArgumentNullException("otherList");
            int diff = Compare(otherList.ReleaseVersion);
            if (diff != 0)
                return diff;
            return (releaseDate == otherList.ReleaseDate)
                    ? 0
                    : (DateTime.Parse(releaseDate) > DateTime.Parse(otherList.ReleaseDate) ? 1 : -1);
        }

        public ReleaseFile[] GetDifferences(ReleaseList otherList, out int fileSize)
        {
            fileSize = 0;
            if (otherList == null || Compare(otherList) == 0)
                return null;
            var ht = new Hashtable();
            foreach (ReleaseFile file in files)
            {
                ht.Add(file.FileName, file.ReleaseDate);
            }
            var diffrences = new List<ReleaseFile>();
            foreach (ReleaseFile file in otherList.files)
            {
                if (!ht.ContainsKey(file.FileName) ||
                    DateTime.Parse(file.ReleaseDate) > DateTime.Parse(ht[file.FileName].ToString()))
                {
                    diffrences.Add(file);
                    fileSize += file.FileSize;
                }
            }
            return diffrences.ToArray();
        }

        /// <summary>
        /// 给定一个节点的xPath表达式并返回一个节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public XmlNode FindNode(string xPath)
        {
            XmlNode xmlNode = SelectSingleNode(xPath);
            return xmlNode;
        }

        /// <summary>
        /// 给定一个节点的xPath表达式返回其值
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public string GetNodeValue(string xPath)
        {
            XmlNode xmlNode = SelectSingleNode(xPath);
            return xmlNode.InnerText;
        }

        public void SetNodeValue(string xPath, string value)
        {
            XmlNode xmlNode = SelectSingleNode(xPath);
            xmlNode.InnerXml = value;
        }

        /// <summary>
        /// 给定一个节点的表达式返回此节点下的孩子节点列表
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(string xPath)
        {
            XmlNodeList nodeList = SelectSingleNode(xPath).ChildNodes;
            return nodeList;
        }

        public void RefreshFileNodes()
        {
            if (files == null) return;
            XmlNode node = SelectSingleNode("AutoUpdater/Releases");
            node.RemoveAll();
            foreach (ReleaseFile file in files)
            {
                XmlElement el = CreateElement("File");
                XmlAttribute attrName = CreateAttribute("name");
                attrName.Value = file.FileName;
                XmlAttribute attrDate = CreateAttribute("date");
                attrDate.Value = file.ReleaseDate;
                XmlAttribute attrSize = CreateAttribute("size");
                attrSize.Value = file.FileSize.ToString();
                el.Attributes.Append(attrName);
                el.Attributes.Append(attrDate);
                el.Attributes.Append(attrSize);
                node.AppendChild(el);
            }
        }
    }

    public class ReleaseFile
    {
        public ReleaseFile()
        {
        }

        public ReleaseFile(string fileName, string releaseDate, int fileSize)
        {
            this.FileName = fileName;
            this.ReleaseDate = releaseDate;
            this.FileSize = fileSize;
        }

        public string FileName { get; set; }

        public string ReleaseDate { get; set; }

        public int FileSize { get; set; }
    }
}
