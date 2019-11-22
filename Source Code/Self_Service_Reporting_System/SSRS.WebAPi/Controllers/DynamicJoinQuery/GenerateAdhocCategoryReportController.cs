using SSRS.WebAPi.Models.Helper;
using Syriatel.OSS.API.Models.DynamicReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSRS.WebAPi.Controllers.DynamicJoinQuery
{
    public class GenerateAdhocCategoryReportController : ApiController
    {
        [HttpPost]
        public ResponseJson Post([FromBody]AdhocReportQueryData Response)
        {
            try
            {
                AdhocCategoryReportCreation adhocReportCreation = new AdhocCategoryReportCreation(Response);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, adhocReportCreation.ReportData);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
