using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Common
{
    public class UrlResult
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Title { get; set; }

        public bool Favourite { get; set; }

        public long RequestFrequencySeconds { get; set; }

        public string Method { get; set; }

        public string Body { get; set; }

        public virtual IEnumerable<RequestHeaderResult> Headers { get; set; }
    }
}
