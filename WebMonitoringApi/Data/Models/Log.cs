using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebMonitoringApi.Data.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public HttpStatusCode StatusCode { get; set; }
        
        public bool Succeeded { get; set; }
        
        [Required]
        public DateTime Sent { get; set; }
        
        [Required]
        public DateTime Received { get; set; }
        
        [ForeignKey(nameof(Url))]
        public int UrlId { get; set; }
        
        public Url Url { get; set; }
        
        //TODO: add error list
    }
}
