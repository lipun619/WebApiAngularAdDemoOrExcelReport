using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using WebApiAngularAdDemo.BusinessComponent.IServices;
using WebApiAngularAdDemo.DataComponents.Response;

namespace WebApiAngularAdDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DownloadReportController : Controller
    {
        private readonly ILogger<DownloadReportController> _logger;
        private IDownloadReportService _downloadReportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string currentUser;
        private readonly IPAddress IP;

        public DownloadReportController(ILogger<DownloadReportController> logger, IDownloadReportService downloadReportManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _downloadReportService = downloadReportManager;
            _httpContextAccessor = httpContextAccessor;
            currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
            IP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            _logger.LogInformation("Logged in user name: " + currentUser + "with IP: " + _httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            _logger.LogInformation("Logged in user identity: " + _httpContextAccessor.HttpContext.User.Identity.ToString());
        }

        [HttpPost]
        [Route("DownloadReport")]
        public IActionResult DownloadReport()
        {
            try
            {
                var data = _downloadReportService.GetReportData();
                MemoryStream stream = _downloadReportService.GetReportStream(data.Data);
                if (stream != null)
                {
                    byte[] content = stream.ToArray();
                    string fileName = string.Format("ReportFile" + "_{0}.xlsx", DateTime.Now.ToString("d"));
                    HttpContext.Response.Headers.Add("access-control-expose-headers", "Content-Disposition");
                    return File(content, "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", fileName);
                }
                return Json(new { status = ResponseStatus.Failure, Message = "Template Not Found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "UserIP:" + IP + "DownloadReportController - DownloadReport", currentUser);
                return BadRequest(new Response<string>
                {
                    Data = "Error Occured",
                    Message = "Error While Fetching Personal Data",
                    Status = HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
