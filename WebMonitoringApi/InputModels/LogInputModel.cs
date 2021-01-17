using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoringApi.InputModels
{
    public class LogInputModel
    {
        public string SortBy { get; set; }
        
        public string Url { get; set; }

        public int Limit { get; set; }
    }
}
