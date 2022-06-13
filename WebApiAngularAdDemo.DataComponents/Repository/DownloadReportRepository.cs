using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using WebApiAngularAdDemo.DataComponents.DBContext;
using WebApiAngularAdDemo.DataComponents.DomainModel;
using WebApiAngularAdDemo.DataComponents.Helpers;
using WebApiAngularAdDemo.DataComponents.IRepository;

namespace WebApiAngularAdDemo.DataComponents.Repository
{
    class DownloadReportRepository : IDownloadReportRepository
    {
        private readonly ReportContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string currentUser;

        public DownloadReportRepository(IHttpContextAccessor httpContextAccessor, ReportContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public ReportDomainModel GetReportData()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            try
            {
                var conString = _context.Database.GetDbConnection().ConnectionString;
                con = new SqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[sp_getPersonalDetails]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", 1));
                var ds = new DataSet();
                con.Open();
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }

                var result = ds.Tables;

                var finalData = result[0].AbsoluteConvertToDataTable<ReportDomainModel>();
                return finalData[0];
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                da.Dispose();
            }
        }
    }
}
