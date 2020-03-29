using System.Data.Entity;
using ZavenDotNetInterview.Data.Models;

namespace ZavenDotNetInterview.Data
{
    public class ZavenDotNetInterviewContext : DbContext
    {
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        public ZavenDotNetInterviewContext() : base("name=ZavenDotNetInterview")
        {
        }
    }
}