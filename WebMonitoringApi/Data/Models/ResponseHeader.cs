using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoringApi.Data.Models
{
    public class ResponseHeader : Header
    {
        [Required]
        [ForeignKey(nameof(Log))]
        public int LogId { get; set; }
    }
}
