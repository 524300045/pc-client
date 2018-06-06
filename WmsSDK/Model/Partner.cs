using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class Partner
    {

     
        /** 供应商编码 */
       public String partnerCode { get; set; }
        /** 供应商名称 */
       public String partnerName { get; set; }
        /** 简称 */
       public String partnerShorter { get; set; }
        /** 供应商分类编码 */
       public String code { get; set; }
        /** 地区名称 */
        public String areaName { get; set; }
   
        /** 电话 */
        public String phone { get; set; }
        /** 联系人 */
        public String contacts { get; set; }
        /** 专营业务员 */
        public String salesman { get; set; }
        /** 地址 */
        public String address { get; set; }
        /** 类型 */
        public int type { get; set; }
        /** 银行账户 */
        public String bankAccount { get; set; }
        /** 开户银行 */
        public String openingBank { get; set; }
    
        /** 传真 */
        public String fax { get; set; }
        /** 法人 */
        public String legalPerson { get; set; }
        /** 邮箱 */
        public String email { get; set; }
        /** 绿茶分类编码 */
        public String customerCategoryCode { get; set; }
        /** 分类名称 */
        public String className { get; set; }
        /** 货主ID */
        public String customerCode { get; set; }
        /** 创建名称 */
        public String createUser { get; set; }
   
        /** 修改名称 */
        public String updateUser { get; set; }
   
        /** 有效标示（1：有效；0：无效） */
        public int? yn;

        /**标签线上名称**/
        public String labelName { get; set; }
    }
}
