using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiAngularAdDemo.DataComponents.DomainModel;
using WebApiAngularAdDemo.DataComponents.Response;

namespace WebApiAngularAdDemo.BusinessComponent.IServices
{
    public interface IDownloadReportService
    {
        Response<ReportDomainModel> GetReportData();
        MemoryStream GetReportStream(ReportDomainModel data);
    }
}
