using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.IO;
using WebApiAngularAdDemo.BusinessComponent.IServices;
using WebApiAngularAdDemo.BusinessComponent.Utilities;
using WebApiAngularAdDemo.DataComponents.DomainModel;
using WebApiAngularAdDemo.DataComponents.Response;
using WebApiAngularAdDemo.DataComponents.UnitOfWork;

namespace WebApiAngularAdDemo.BusinessComponent.Services
{
    public class DownloadReportService : IDownloadReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DownloadReportService> _logger;
        private readonly IConfiguration _configuration;

        public DownloadReportService(IUnitOfWork unitOfWork, ILogger<DownloadReportService> logger,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        public Response<ReportDomainModel> GetReportData()
        {
            Response<ReportDomainModel> response = new Response<ReportDomainModel>();
            response.Data = _unitOfWork.downloadReportRepository.GetReportData();
            return response;
        }

        public MemoryStream GetReportStream(ReportDomainModel reportData)
        {
            string reportPath = _configuration.GetSection("ReportTemplatePath").Value;
            var reportTemplateDirectory = Path.Combine(Directory.GetCurrentDirectory(), reportPath);

            if (File.Exists(reportTemplateDirectory))
            {
                var reportTemplate = new FileInfo(reportTemplateDirectory);
                using ExcelPackage excelPackage = new ExcelPackage(reportTemplate);

                GeneratePersonalDetailsSheet(excelPackage, reportData);

                var ms = new MemoryStream();
                excelPackage.SaveAs(ms);
                return ms;
            }
            return null;
        }

        private static void GeneratePersonalDetailsSheet(ExcelPackage excelPackage, ReportDomainModel reportData)
        {
            ExcelWorksheet ws = excelPackage.Workbook.IsWorkSheetExists("Personal Details")
                ? excelPackage.Workbook.Worksheets["Personal Details"] : null;

            if (ws != null)
            {
                ws.Names["Name"].Value = reportData.FirstName + " " + reportData.LastName;
                ws.Names["Date"].Value = DateTime.Now.ToString("d");

                using(ExcelNamedRange range = ws.Names["FirstName"])
                {
                    range.Value = reportData?.FirstName;
                }
                using (ExcelNamedRange range = ws.Names["LastName"])
                {
                    range.Value = reportData?.LastName;
                }
                using (ExcelNamedRange range = ws.Names["Address"])
                {
                    range.Value = reportData?.Address;
                }
                using (ExcelNamedRange range = ws.Names["MobileNumber"])
                {
                    range.Value = reportData?.MobileNumber;
                }
                using (ExcelNamedRange range = ws.Names["Gender"])
                {
                    range.Value = reportData?.Gender;
                }
                using (ExcelNamedRange range = ws.Names["Company"])
                {
                    range.Value = reportData?.Company;
                }
            }
        }
    }
}
