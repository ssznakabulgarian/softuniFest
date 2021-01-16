using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMonitoringApi.Data.Models
{
    public class Url
    {
        public Url()
        {
            Logs = new HashSet<Log>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }

        public string Title { get; set; }

        public bool Favourite { get; set; }

        //TODO: add favIcon

        public long RequestFrequencySeconds { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}
