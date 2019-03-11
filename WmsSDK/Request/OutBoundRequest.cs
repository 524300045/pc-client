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

        public int page;

     

     //   public int status;
        public string GetAPIPath()
        {
            return "/outBound/getOutBoundPrintPageList";
        }

    }


    public class OutBoundQueryRequest : IWMSRequest<OutBoundPageResponse>
    {
        public String storedCode;

        /** 开始时间 */
        public DateTime startTime;
        /** 结束时间 */
        public DateTime endTime;

        public String warehouseCode;

        public String customerCode;


        public int? status;

        public int? isPrint;

        public int page;



        //   public int status;
        public string GetAPIPath()
        {
            return "/outBound/getOutBoundPageList";
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


    //public class OutBoundSendRequest : IWMSRequest<OutBoundSendResponse>
    //{

    //    public string GetAPIPath()
    //    {
    //        return "/outBound/getOutBoundPrintPageList";
    //    }
    //    public List<string> codes { get; set; }

    //}

    public class OutBoundCheckRequest : IWMSRequest<OutBoundCheckResponse>
    {

        public string GetAPIPath()
        {
            return "/outBound/check";
        }
        public List<string> outboundTaskCodes { get; set; }

    }


    public class OutBoundSendRequest : IWMSRequest<OutBoundSendResponse>
    {

        public string GetAPIPath()
        {
            return "/outBound/send";
        }
        public List<string> outboundTaskCodes { get; set; }

        public String createUser { get; set; }

        public String customerCode { get; set; }

        public String warehouseCode { get; set; }

        public String customerName { get; set; }

    }
}
