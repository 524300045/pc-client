
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

namespace WmsApp
{
    public partial class LoginForm : Form
    {

        private IWMSClient client = new DefalutWMSClient();
        public LoginForm()
        {
            InitializeComponent();
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }



        public static string PostMoths(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }


   

        private void btnOk_Click(object sender, EventArgs e)
        {
    
            if (string.IsNullOrWhiteSpace(tbUserName.Text.Trim()))
            {
                MessageBox.Show("用户名不能为空!");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPwd.Text.Trim()))
            {
                MessageBox.Show("密码不能为空!");
                return;
            }

            if (cbWare.SelectedIndex==0)
            {
                  MessageBox.Show("请选择仓库!");
                 return;
            }

            try
            {
                btnOk.Enabled = false;
                btnOk.Text = "登录中...";
                LoginRequest request = new LoginRequest();
                request.name = tbUserName.Text.Trim();
                request.password = tbPwd.Text.Trim();
                request.warehouseCode = cbWare.SelectedValue.ToString();
                string json = DefalutWMSClient.GetJson(request);

                string result = PostMoths("http://www.bjkalf.net:8090/services/user/checkAndGetUserResource", json);

                //string result = PostMoths("http://test.api.portal.bjshengeng.com/services/user/checkAndGetUserResource", json);

                LoginResponse response = DefalutWMSClient.ToObject<LoginResponse>(result);

                if (response.Code != "200")
                {
                    MessageBox.Show("用户名或密码错误!");
                    tbPwd.SelectAll();
                    tbPwd.Focus();
                    return;
                }

                if (response.result == null)
                {
                    MessageBox.Show("获取不到用户信息!");
                    tbPwd.SelectAll();
                    tbPwd.Focus();
                    return;
                }
                if (response.result.customerMap==null||response.result.customerMap.Count==0)
                {
                    MessageBox.Show("用户还未绑定货主!");
                    return;
                }

                UserInfo.UserName = tbUserName.Text.Trim();
                UserInfo.PartnerName = response.result.companyName;
                UserInfo.PartnerCode = response.result.companyCode;
                //UserInfo.WareHouseCode = "10";
                //UserInfo.WareHouseName = "北京康安利丰平谷1仓";
                UserInfo.RealName = response.result.cnName;
                UserInfo.CompanyName = response.result.companyName;
                UserInfo.menuDtos = response.result.menuDtos;
                UserInfo.id = response.result.id.ToString();
                UserInfo.cnName = response.result.cnName;

                UserInfo.WareHouseCode = cbWare.SelectedValue.ToString();
                UserInfo.WareHouseName = cbWare.Text;

                CustomerSelectForm selectForm = new CustomerSelectForm(response.result.customerMap, response.result.defaultCustomerCode);
                selectForm.ShowDialog();
                if (selectForm.DialogResult==DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                }

               
            }
            catch (Exception ex)
            {
                LogHelper.Log("Login:" + ex.Message);
                MessageBox.Show("出现异常" + ex.Message);
                return;
            }
            finally
            {
                btnOk.Enabled = true;
                btnOk.Text = "登录";
            }
           

          
        }

        private void tbPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnOk_Click(null,null);
            }
        }

        private void tbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                tbPwd.Focus();
                tbPwd.SelectAll();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
               string version=System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
               tbVersion.Text = "Version " + version;
               try
               {
                   BindWare();
               }
               catch (Exception ex)
               {
                   MessageBox.Show("获取数据异常");
               }
        }

        private  void BindWare()
        {
            WarehouseInfoRequest request = new WarehouseInfoRequest();
            WarehouseInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<WarehouseInfo> wareList = new List<WarehouseInfo>();
                    foreach (WarehouseInfo item in response.result)
                    {
                        wareList.Add(item);
                    }
                    wareList.Insert(0, new WarehouseInfo() { WarehouseCode = "", WarehouseName = "请选择" });
                    this.cbWare.DataSource = wareList;
                    this.cbWare.ValueMember = "WarehouseCode";
                    this.cbWare.DisplayMember = "WarehouseName";
                }
            }
        }

      


        private static void BindCustomer()
        {

        }
    }
}
