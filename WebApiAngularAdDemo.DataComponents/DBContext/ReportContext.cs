using Microsoft.EntityFrameworkCore;
using WebApiAngularAdDemo.DataComponents.DomainModel;

namespace WebApiAngularAdDemo.DataComponents.DBContext
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options) { }
        public virtual DbSet<ReportDomainModel> ReportQueryData { get; set; }
    }
}
