using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ContentManagement.MediatR.Commands;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.IO;

namespace ContentManagement.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class EmailController : BaseController
    {
        IMediator _mediator;
        private IWebHostEnvironment _appEnvironment;
        public EmailController(IMediator mediator, IWebHostEnvironment appEnvironment)
        {
            _mediator = mediator;
            _appEnvironment = appEnvironment;
        }
        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="sendEmailCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendEmail")]
        [Produces("application/json", "application/xml", Type = typeof(void))]
        public async Task<IActionResult> SendEmail(SendEmailCommand sendEmailCommand)
        {
            //if (sendEmailCommand.AttachmentFileURL != null)
            //{
            //    for (int i = 0; i < sendEmailCommand.AttachmentFileURL.Count; i++)
            //    {
            //        string path = _appEnvironment.WebRootPath + "/" + sendEmailCommand.AttachmentFileURL[i];
            //        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            //        sendEmailCommand.AttachmentFileURL[i] = fs.Name;
            //        fs.Close();
            //    }
            //}
            sendEmailCommand.AttachmentRootPath = _appEnvironment.WebRootPath;
            var result = await _mediator.Send(sendEmailCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
