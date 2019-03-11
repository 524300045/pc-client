using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class ImportOperateInfo
    {
       /// <summary>
       /// 下单日期
       /// </summary>
       public string orderDate { get; set; }

       /// <summary>
       /// 发货日期
       /// </summary>
       public string deliveryDate { get; set; }

       /** 供应商编码 */
       public String partnerCode { get; set; }

       /** 供应商名称 */
       public String partnerName { get; set; }

       /** 门店编码 */
       public String storedCode { get; set; }

       /** 门店名称 */
       public String storedName { get; set; }

       /** 客户商品编码 */
       public String customerSkuCode { get; set; }
       /** 客户商品名称 */
       public String customerGoodsName { get; set; }

       /** 规格型号 */
       public String goodsModel { get; set; }


       /** 计价单位（斤、两） */
       public String goodsUnit { get; set; }

       /// <summary>
       /// 计划数量
       /// </summary>
       public decimal planNum { get; set; }

       /** 含税单价 */
       public decimal taxPrice { get; set; }

       public decimal taxRate { get; set; }
       

       /** 税率 */
       public string remark { get; set; }


       public string orderUser { get; set; }


       /** excel序号 */
       public int excelNo { get; set; }


    
       public string msg { get; set; }


       public String filename { get; set; }


       public String customerCode { get; set; }
       /** 客户名称 */
       public String customerName { get; set; }


       /** 仓库编码 */
       public String warehouseCode { get; set; }
       /** 仓库名称 */
       public String warehouseName { get; set; }

    }
}
