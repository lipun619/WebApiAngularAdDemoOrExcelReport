using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApiAngularAdDemo.DataComponents.DBContext;
using WebApiAngularAdDemo.DataComponents.IRepository;
using WebApiAngularAdDemo.DataComponents.Repository;

namespace WebApiAngularAdDemo.DataComponents.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(DbContextOptions<ReportContext> options, IHttpContextAccessor httpContextAccessor)
        {
            _context = new ReportContext(options);
            downloadReportRepository = new DownloadReportRepository(httpContextAccessor, _context);
            _httpContextAccessor = httpContextAccessor;
        }

        public IDownloadReportRepository downloadReportRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
