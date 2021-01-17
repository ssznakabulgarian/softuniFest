using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMonitoringApi.Data.Models
{
    public class Url
    {
        public Url()
        {
            Logs = new HashSet<Log>();
            Headers = new HashSet<RequestHeader>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Log> Logs { get; set; }

        public virtual ICollection<RequestHeader> Headers { get; set; }
    }
}
