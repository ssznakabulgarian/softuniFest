using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMonitoringApi.Data.Models
{
    public class Log
    {
        public Log()
        {
            Headers = new HashSet<Header>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public HttpStatusCode StatusCode { get; set; }

        public bool Succeeded { get; set; }

        [Required]
        public DateTime SentOn { get; set; }

        [Required]
        public DateTime ReceivedOn { get; set; }

        public string Body { get; set; }

        [ForeignKey(nameof(Url))]
        public int UrlId { get; set; }

        public Url Url { get; set; }

        public virtual ICollection<Header> Headers { get; set; }
    }
}
