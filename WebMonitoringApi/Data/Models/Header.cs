using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMonitoringApi.Data.Models
{
    public class Header
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Key { get; set; }
        
        [Required]
        public string Value { get; set; }
    }
}
