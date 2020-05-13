using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public   class PackagePackTask
    {

        /** 序号 */
       public long id { get; set; }

        /** 门店名称 */
        public String storedName { get; set; }

        /** 包裹号 */
        public String packageCode { get; set; }

        /** 包裹状态 */
        public int? status { get; set; }

        public String skuCode { get; set; }

        public String goodsName { get; set; }

        /** 包规（数量） */
        public Decimal modelNum { get; set; }

        /** 计价单位（斤、两） */
        public String goodsUnit { get; set; }
        /** 物理单位（包、箱、瓶） */
        public String physicsUnit { get; set; }

        /** 创建时间 */
        public DateTime createTime { get; set; }
        /** 创建人 */
        public String createUser { get; set; }

        /** 更新时间 */
        public DateTime updateTime { get; set; }
        /** 更新人 */
        public String updateUser { get; set; }

        public String outboundTaskCode { get; set; }

        /** 门店编码 */
        public String storedCode { get; set; }

        public String boxCode { get; set; }

        public Decimal weight { get; set; }

        public String packTaskCode { get; set; }

        /** 是否生鲜 1：是 0：否 */
        public int? isFresh;
        /** 是否称重 1：是 0：否 */
        public int? weighed { get; set; }

        /** 计划数量 */
        public Decimal planNum { get; set; }
        /** 计划上限数量 */
        public Decimal upPlanNum { get; set; }
        /** 计划下限数量 */
        public Decimal downPlanNum { get; set; }
        /** 创建时间 */
        public DateTime operateTime { get; set; }


        /** 订单总数 */
        public int? orderNum { get; set; }
        /** 进度 */
        public int? progress { get; set; }
        /** 订单总量 */
        public Decimal orderCount { get; set; }
        /** 供应商编码 */
        public String partnerCode { get; set; }
        /** 供应商名称 */
        public String partnerName { get; set; }
        /** 仓库编码 */
        public String warehouseCode { get; set; }
        /** 城市编码 */
        public String regionCode { get; set; }
        /** 客户编码 */
        public String customerCode { get; set; }
        /** 订单日期 */
        public DateTime orderDate { get; set; }
        /** 配送日期 */
        public DateTime deliveryDate { get; set; }


        public int finishNum { get; set; }

        public Decimal realWeight { get; set; }

        public int packageNum { get; set; }


        public String waveCode { get; set; }

        public String waveName { get; set; }

        public string statusDes
        {
            get
            {
                if (status == 0)
                {
                    return "新建";
                }

                if (status == 10)
                {
                    return "包装中";
                }
                if (status == 20)
                {
                    return "已发运";
                }
                if (status == 15)
                {
                    return "已完成";
                }
                return "";
            }

        }
    }
}
