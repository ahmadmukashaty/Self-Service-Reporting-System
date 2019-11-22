using SSRS.WebAPi.Models.Helper;
using SSRS.WebAPi.Models.Trees.AttributesTree.NodeCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSRS.WebAPi.Controllers.ReportAttributeTree
{
    public class GetAttributeListController : ApiController
    {
        // GET: api/GetAttributeList
        public ResponseJson Get(string ClassificationName)
        {
            try
            {
                AttributeTreeCreation attributeTree = new AttributeTreeCreation(ClassificationName.ToLower());

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, attributeTree.TreeList);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
