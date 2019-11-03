using SSRS.WebAPi.Models.Helper;
using SSRS.WebAPi.Models.Trees.AttributesTree.NodeCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SSRS.WebAPi.Controllers.ReportAttributeTree
{
    public class GetAttributeTreeController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(string ClassificationName)
        {
            try
            {
                AttributeTreeCreation attributeTree = new AttributeTreeCreation(ClassificationName.ToLower());

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, attributeTree.Tree);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
