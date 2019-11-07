using SSRS.WebAPi.Models.Authentication;
using SSRS.WebAPi.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SSRS.WebAPi.Controllers.Authentication
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public ResponseJson Post([FromBody]UserRegister user)
        {
            try
            {
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, null);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
