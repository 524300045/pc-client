using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
     public class Goods
    {
        /** 主键id */
         [JsonProperty("id")]
         public long id { get; set; }
        /** 商品编码 */
           [JsonProperty("skuCode")]
         public String skuCode { get; set; }
        /** 客户编码 */
           [JsonProperty("customerCode")]
           public String customerCode { get; set; }
        /** 商品名称 */
           [JsonProperty("goodsName")]
           public String goodsName { get; set; }
        /** 最小分类编码 */
           [JsonProperty("categoryCode")]
           public String categoryCode { get; set; }
        /** 最小分类名称 */
           [JsonProperty("categoryName")]
           public String categoryName { get; set; }
        /** 规格型号 */
           [JsonProperty("goodsModel")]
           public String goodsModel { get; set; }
        /** 等级 */
           [JsonProperty("goodsGrade")]
           public string goodsGrade { get; set; }
        /** 品牌 */
           [JsonProperty("goodsBrand")]
           public string goodsBrand { get; set; }
        /** 产地 */
           [JsonProperty("madeIn")]
           public String madeIn { get; set; }
        /** 重量 */
           [JsonProperty("weight")]
           public decimal weight;
        /** 长 */
           [JsonProperty("packLong")]
           public decimal packLong { get; set; }
        /** 宽 */
           [JsonProperty("packWide")]
           public decimal packWide { get; set; }
        /** 高 */
           [JsonProperty("packHigh")]
           public decimal packHigh { get; set; }
        /** 建议采购单价 */
          [JsonProperty("suggestPrice")]
           public decimal suggestPrice { get; set; }
        /** 特殊要求 */
           [JsonProperty("specialAsk")]
          public int specialAsk { get; set; }
        /** 包规（数量） */
           [JsonProperty("modelNum")]
           public decimal modelNum { get; set; }
        /** 计价单位（斤、两） */
           [JsonProperty("goodsUnit")]
           public String goodsUnit { get; set; }
        /** 物理单位（包、箱、瓶） */
           [JsonProperty("physicsUnit")]
           public String physicsUnit { get; set; }
        /** 包装冗余上线 */
           [JsonProperty("upLimit")]
           public int upLimit { get; set; }
        /** 包装冗余下线 */
           [JsonProperty("downLimit")]
           public int downLimit;
        /** 是否生鲜 1：是 0：否 */
           [JsonProperty("isFresh")]
           public int isFresh { get; set; }
        /** 是否称重 1：是 0：否 */
           [JsonProperty("weighed")]
           public int weighed { get; set; }
        /** 是否预加工 1：是 0：否 */
           [JsonProperty("isPreprocess")]
           public int isPreprocess { get; set; }
        /** 参考成本 */
           [JsonProperty("referenceCost")]
           public String referenceCost { get; set; }
        /** abc分类 */
           [JsonProperty("abcClass")]
           public String abcClass { get; set; }
        /** 启用日期 */
           [JsonProperty("enableDate")]
           public DateTime enableDate { get; set; }
        /** 启用停用标识:1：是 0：否 */
           [JsonProperty("enabled")]
           public int enabled { get; set; }
        /** 国标码 */
           [JsonProperty("gbCode")]
           public String gbCode { get; set; }
        /** 创建时间 */
           [JsonProperty("createTime")]
           public DateTime createTime { get; set; }
        /** 创建人 */
           [JsonProperty("createUser")]
           public String createUser { get; set; }
        /** 更新时间 */
           [JsonProperty("updateTime")]
           public DateTime updateTime { get; set; }
        /** 更新人 */
           [JsonProperty("updateUser")]
           public String updateUser { get; set; }
        /** 是否有效 */
           [JsonProperty("yn")]
           public int yn { get; set; }

          [JsonProperty("packageNum")]
           public int? packageNum { get; set; }

          [JsonProperty("orderNum")]
          public int? orderNum { get; set; }


  

          public int diffNum { get; set; }

            [JsonProperty("twoCategoryCode")]
          public String twoCategoryCode;

            public String processProductAttrDesc { get; set; }

            public String productWorkshopAttrDesc { get; set; }

           [JsonProperty("isStandardProcess")]
            public int? isStandardProcess { get; set; }

          [JsonProperty("shortName")]
           public String shortName { get; set; }


                   [JsonProperty("foodWay")]
          public string foodWay { get; set; }

                   public String groupName { get; set; }

                   public int? productWorkshopAttr { get; set; }


                   [JsonProperty("customerGoodsName")]
                   public String customerGoodsName { get; set; }


          [JsonProperty("mixContent")]
                   public String mixContent { get; set; }

          [JsonProperty("nutrients")]
                   public String nutrients { get; set; }

          //BOM重量

                [JsonProperty("bomWeight")]
          public decimal bomWeight { get; set; }

              [JsonProperty("customerSkuCode")]
                public String customerSkuCode { get; set; }
    }
}
