using System.ComponentModel.DataAnnotations;

namespace WebMonitoringApi.Data.Models
{
    public class Header
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Key { get; set; }
        
        [Required]
        public string Value { get; set; }
    }
}
