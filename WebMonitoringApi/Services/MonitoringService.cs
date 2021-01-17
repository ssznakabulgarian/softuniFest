using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using WebMonitoringApi.Data;
using WebMonitoringApi.Hubs;
using WebMonitoringApi.Common;
using System.Collections.Generic;
using WebMonitoringApi.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebMonitoringApi.Services
{
    public class MonitoringService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<NotificationHub> _hub;
        private readonly List<TimerManager> _timers;

        public MonitoringService(ApplicationDbContext dbContext, IHubContext<NotificationHub> hub)
        {
            _dbContext = dbContext;
            _hub = hub;
            _timers = new List<TimerManager>();
        }

        public void AttachTimerToUrl(Url url)
        {
            var timer = new TimerManager(async () =>
            {
                var webRequest = WebRequest.Create(url.Value);
                webRequest.Method = url.Method.ToUpper();

                byte[] webRequestBytes = Encoding.UTF8.GetBytes(url.Body);

                var contentTypeHeader = url.Headers.FirstOrDefault(h => h.Key == "Content-Type");
                webRequest.ContentType = contentTypeHeader == null ? "text/plain" : contentTypeHeader.Key;

                Stream dataStream = webRequest.GetRequestStream();
                dataStream.Write(webRequestBytes, 0, webRequestBytes.Length);

                DateTime sentOn = DateTime.UtcNow;
                dataStream.Close();

                WebResponse webResponse = await webRequest.GetResponseAsync();
                DateTime receivedOn = DateTime.UtcNow;

                HttpWebResponse httpResponse = (HttpWebResponse)webResponse;

                var headers = new List<ResponseHeader>();

                foreach (var key in httpResponse.Headers.AllKeys)
                {
                    headers.Add(new ResponseHeader
                    {
                        Key = key,
                        Value = string.Join("; ", httpResponse.Headers.GetValues(key))
                    });
                }

                string responseBody;

                using (dataStream = webResponse.GetResponseStream())
                {
                    var streamReader = new StreamReader(dataStream);
                    responseBody = streamReader.ReadToEnd();
                }

                dataStream.Close();
                webResponse.Close();

                int statusCodeNumber = (int)httpResponse.StatusCode;
                bool succeeded = statusCodeNumber >= 200 && statusCodeNumber < 300;

                var log = new Log
                {
                    StatusCode = httpResponse.StatusCode,
                    Succeeded = succeeded,
                    SentOn = sentOn,
                    ReceivedOn = receivedOn,
                    Body = responseBody,
                    UrlId = url.Id,
                    Headers = headers
                };

                url.Logs.Add(log);

            }, 2000, 1000);

            _timers.Add(timer);
        }

        // request -> urlsController -> MonitoringService (Create new timer) -> 
    }
}
