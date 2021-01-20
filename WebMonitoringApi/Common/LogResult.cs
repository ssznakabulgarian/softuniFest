using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Common
{
    public class LogResult
    {
        public int Id { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime ReceivedOn { get; set; }

        public int ExecutionTime { get; set; }

        public string Url { get; set; }
    }
}
