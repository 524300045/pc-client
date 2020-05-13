using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class PreprocessInfo
    {
        /** id */
        public long id;
        /** 预加工单号(小包裹号) */
        public String preprocessCode { get; set; }
        /** 状态 */
        public int status { get; set; }
        /** 供应商编码 */
        public String partnerCode { get; set; }
        /** 供应商名称 */
        public String partnerName { get; set; }
        /** 商品编码 */
        public String skuCode { get; set; }
        /** 商品名称 */
        public String goodsName { get; set; }
        /** 打包量 */
        public decimal packWeight { get; set; }
        /** 包规（数量） */
        public decimal modelNum { get; set; }
        /** 计价单位（斤、两） */
        public String goodsUnit { get; set; }
        /** 物理单位（包、箱、瓶） */
        public String physicsUnit { get; set; }
        /** 创建人 */
        public String operateUser { get; set; }
        /** 创建时间 */
        public DateTime operateTime { get; set; }
        /** 创建时间 */
        public DateTime createTime { get; set; }
        /** 创建人 */
        public String createUser { get; set; }
        /** 更新时间 */
        public DateTime updateTime { get; set; }
        /** 更新人 */
        public String updateUser { get; set; }
        /** 是否有效 */
        public int yn { get; set; }

        public string productWorkshopAttrDesc { get; set; }

        public string goodsModel { get; set; }


        public string waveCode { get; set; }

        public string waveName { get; set; }

        public string StatusDes {
            get
            {
                if (status==0)
                {
                    return "未使用";
                }
                return "已使用";
            }
        }
    }
}
