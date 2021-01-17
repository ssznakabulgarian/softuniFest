using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMonitoringApi.Data.Models
{
    public class Url
    {
        public Url()
        {
            Logs = new HashSet<Log>();
            Headers = new HashSet<Header>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }

        public string Title { get; set; }

        public bool Favourite { get; set; }

        public long RequestFrequencySeconds { get; set; }

        [Required]
        public string Method { get; set; }

        public string Body { get; set; }

        public virtual ICollection<Log> Logs { get; set; }

        public virtual ICollection<Header> Headers { get; set; }
    }
}
