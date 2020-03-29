using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavenDotNetInterview.Data.Models;

namespace ZavenDotNetInterview.Core.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? DoAfter { get; set; }
        public List<LogViewModel> Logs { get; set; }

        public JobViewModel()
        {
            Logs = new List<LogViewModel>();
        }
    }
}
