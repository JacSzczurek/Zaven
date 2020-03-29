using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ZavenDotNetInterview.Data.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}