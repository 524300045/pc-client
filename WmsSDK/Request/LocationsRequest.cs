using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class LocationsQueryRequest : IWMSRequest<LocationsResponse>
    {
        public string GetAPIPath()
        {
            return "/locations/getLocationsPageList";
        }

        [JsonProperty("locationName")]
        public String locationName;

        [JsonProperty("areaCode")]
        public String areaCode;

            [JsonProperty("warehouseCode")]
        public string warehouseCode { get; set; }

         [JsonProperty("page")]
            public int page;

         [JsonProperty("pageSize")]
         public int pageSize;
    }
}
