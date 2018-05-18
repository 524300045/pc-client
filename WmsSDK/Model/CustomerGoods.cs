using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class CustomerGoods
    {

       public long id { get; set; }
    /** 客户编码 */
       public String customerCode { get; set; }
    /** 客户商品编码 */
       public String customerSkuCode { get; set; } 
    /** 客户商品名称 */
       public String customerGoodsName { get; set; }
    /** 一级分类编码 */
       public String categoryCode { get; set; }
    /** 一级分类名称 */
       public String categoryName { get; set; }
    /** 二级分类编码 */
       public String twoCategoryCode { get; set; }
    /** 二级分类名称 */
       public String twoCategoryName { get; set; }
    /** 三级分类编码 */
       public String threeCategoryCode { get; set; }
    /** 三级分类名称 */
       public String threeCategoryName { get; set; }
    /** 规格型号 */
       public String goodsModel { get; set; }
    /** 等级 */
       public String goodsGrade { get; set; }
    /** 品牌 */
       public String goodsBrand { get; set; }
    /** 产地 */
       public String madeIn { get; set; }
    /** 重量 */
       public decimal weight { get; set; }
    /** 长 */
       public decimal packLong { get; set; }
    /** 宽 */
       public decimal packWide { get; set; } 
    /** 高 */
       public decimal packHigh { get; set; }
    /** 建议采购单价 */
       public decimal suggestPrice { get; set; }
    /** 特殊要求 */
       public int? specialAsk { get; set; }
    /** 包规（数量） */
       public decimal modelNum { get; set; }
    /** 计价单位（斤、两） */
       public String goodsUnit { get; set; }
    /** 物理单位（包、箱、瓶） */
       public String physicsUnit { get; set; }
    /** 包装冗余上线 */
       public int? upLimit { get; set; }
    /** 包装冗余下线 */
       public int? downLimit { get; set; }
    /** 是否生鲜 1：是 0：否 */
       public int? isFresh { get; set; }
    /** 是否称重 1：是 0：否 */
       public int? weighed { get; set; }
    /** 是否预加工 1：是 0：否 */
       public int? isPreprocess { get; set; }
    /** 参考成本 */
       public String referenceCost { get; set; }
    /** abc分类 */
       public String abcClass { get; set; } 
    /** 启用日期 */
   //    public DateTime enableDate { get; set; } 
    /** 启用停用标识:1：是 0：否 */
       public int? enabled { get; set; }
    /** 国标码 */
       public String gbCode { get; set; }
    /** 税率 */
       public decimal taxRate { get; set; } 
    /** 效期 */
   //    public Double expiryDate { get; set; }
    /** 创建时间 */
     //  public DateTime createTime { get; set; } 
    /** 创建人 */
       public String createUser { get; set; }
    /** 更新时间 */
     //  public DateTime updateTime { get; set; }
    /** 更新人 */
       public String updateUser { get; set; }
    /** 是否有效 */
       public int? yn { get; set; }


       public Double expiryDate { get; set; }
    }
}
