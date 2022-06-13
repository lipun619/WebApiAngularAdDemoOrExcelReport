using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiAngularAdDemo.DataComponents.DomainModel;

namespace WebApiAngularAdDemo.DataComponents.IRepository
{
    public interface IDownloadReportRepository
    {
        ReportDomainModel GetReportData();
    }
}
