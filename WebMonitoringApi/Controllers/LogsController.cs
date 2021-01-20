using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMonitoringApi.Data;
using WebMonitoringApi.InputModels;
using System.Linq;
using WebMonitoringApi.Common;
using System.Security.Claims;
using WebMonitoringApi.Data.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebMonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogsController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;

        public LogsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //This function is used for testing, should be deleted later. It is as simple as it can be.
        [HttpPost]
        public IActionResult Create()
        {
            var url = _dbContext.Urls.FirstOrDefault(Url => Url.Id == 6);

            var log = new Log
            {
                Body = "Log's body",
                StatusCode = System.Net.HttpStatusCode.OK,
                SentOn = DateTime.UtcNow,
                ReceivedOn = DateTime.UtcNow,
                Succeeded = true,
            };

            log.Headers.Add(new ResponseHeader
            {
                Key = "key",
                Value = "value"
            });

            url.Logs.Add(log);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var currentUrls = _dbContext.Urls.Include(Url => Url.Logs).Where(Url => Url.UserId == userId);

            if (currentUrls.Count() == 0
                || currentUrls == null)
            {
                return BadRequest();
            }

            var logs = new List<LogResult>();

            Log tempLog;
            Url tempUrl;

            for(int i = 0; i < currentUrls.ToArray().Length; i++)
            {
                tempUrl = currentUrls.ToArray()[i];
                for(int j = 0; j < tempUrl.Logs.ToArray().Length; j++)
                {
                    tempLog = tempUrl.Logs.ToArray()[j];
                    logs.Add(new LogResult
                    {
                        Id = tempLog.Id,
                        ExecutionTime = tempLog.SentOn.Subtract(tempLog.ReceivedOn).Milliseconds,
                        SentOn = tempLog.SentOn,
                        ReceivedOn = tempLog.ReceivedOn,
                        Url = tempUrl.Value,
                        StatusCode = tempLog.StatusCode
                    });
                }
            }

            if(logs.Count == 0)
            {
                return BadRequest();
            }

            return Ok(logs);
        }

        [HttpGet("ByRequest")]
        public IActionResult GetByRequest(LogInputModel input)
        {
            if (input != null)
            {
                if (input.Url != null && input.Url != string.Empty)
                {
                    var logs = _dbContext.Logs.Where(log => log.Url.Value == input.Url);

                    if (input.SortBy == "dsc")
                    {
                        logs.OrderByDescending(log => log.ReceivedOn);
                    }
                    else if (input.SortBy == "asc")
                    {
                        logs.OrderBy(log => log.ReceivedOn);
                    }

                    if (input.Limit != 0)
                    {
                        logs.Take(input.Limit);
                    }

                    var logResults = logs.Select(log => new LogResult
                    {
                        Id = log.Id,
                        SentOn = log.SentOn,
                        ReceivedOn = log.ReceivedOn,
                        ExecutionTime = log.SentOn.Subtract(log.ReceivedOn).Milliseconds,
                        StatusCode = log.StatusCode,
                        Url = log.Url.Value,
                    });

                    return Ok(logResults);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var log = _dbContext.Logs.FirstOrDefault(log => log.Id == id);

            if(log == null)
            {
                return BadRequest();
            }

            var response = _dbContext.Logs.Remove(log);

            if(response.Entity.Id == id)
            {
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
