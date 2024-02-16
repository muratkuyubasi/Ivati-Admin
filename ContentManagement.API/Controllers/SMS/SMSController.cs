using ContentManagement.Helper;
using Hafiz.Core.Utilities.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : BaseController
    {
        ///<summary>Send a SMS</summary><returns></returns>
        [HttpPost("SendSMS")]
        public async Task<bool> SendSMS(string message, string phone)
        {
            var result = SMSHelper.Send(message, phone);
            if (result.Result)
            {
                return true;
            }
            else { return false; }
        }
    }
}
