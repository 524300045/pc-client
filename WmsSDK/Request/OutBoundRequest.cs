using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class OutBoundPrintQueryRequest : IWMSRequest<OutBoundPrintPageResponse>
    {
        public String storedCode;

        /** 开始时间 */
        public DateTime startTime;
        /** 结束时间 */
        public DateTime endTime;

        public String warehouseCode;

        public String customerCode;

        public int? isPrint;

     //   public int status;
        public string GetAPIPath()
        {
            return "/outBound/getOutBoundPrintPageList";
        }

    }

    public class OutBoundDetailPrintRequest : IWMSRequest<OutBoundPrintDetailResponse>
    {
        public string GetAPIPath()
        {
            return "/outBound/getOutBoundDetail";
        }

        public string updateUser { get; set; }

        public string printMan { get; set; }

        public string outboundTaskCode { get; set; }
    }


}
