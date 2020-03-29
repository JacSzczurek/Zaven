using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavenDotNetInterview.Data.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? DoAfter { get; set; }
        public virtual List<Log> Logs { get; set; }

        public Job()
        {
            Logs = new List<Log>();
        }
    }

    public enum JobStatus
    {
        Failed = -1,
        New = 0,
        InProgress = 1,
        Done = 2,
        Closed = 3
    }
}