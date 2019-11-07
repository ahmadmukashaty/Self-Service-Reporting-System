using SSRS.WebAPi.Models.Authentication;
using SSRS.WebAPi.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SSRS.WebAPi.Controllers.Authentication
{
    public class LoginController : ApiController
    {


        [HttpPost]
        public ResponseJson Post([FromBody]UserInfo user)
        {
            try
            {
                List<UserInfo> Users = new List<UserInfo>()
                {
                    new UserInfo {username = "Ahmad", password = "Ahmad123" },
                    new UserInfo {username = "Moayad", password = "Moayad123" }
                };

                bool alreadyExists = Users.Any(x => x.username == user.username && x.password == user.password);

                if(alreadyExists)
                    return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, null);

                return new ResponseJson(Constants.FAILED, Constants.Messages.USER_NOT_EXIST, null);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

    }
}
