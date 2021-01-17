
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoringApi.InputModels
{
    public class UrlInputModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Title { get; set; }

        [Required]
        public string Method { get; set; }

        public bool Favourite { get; set; }

        public long RequestFrequencySeconds { get; set; }
    }
}
