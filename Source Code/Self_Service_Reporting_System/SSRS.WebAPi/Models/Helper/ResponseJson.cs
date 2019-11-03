using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSRS.WebAPi.Models.Helper
{
    public class ResponseJson
    {
        public int success { get; set; }

        public string errorMessage { get; set; }

        public object data { get; set; }

        public ResponseJson(int success, string errorMessage, object data)
        {
            this.success = success;
            this.errorMessage = errorMessage;
            this.data = data;
        }
    }
}