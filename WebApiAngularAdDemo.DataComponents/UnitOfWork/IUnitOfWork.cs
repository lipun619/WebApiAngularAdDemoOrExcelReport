using System;
using WebApiAngularAdDemo.DataComponents.IRepository;

namespace WebApiAngularAdDemo.DataComponents.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDownloadReportRepository downloadReportRepository { get; }
    }
}
