using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMonitoringApi.Data;
using WebMonitoringApi.InputModels;
using System.Linq;
using WebMonitoringApi.Common;
using System.Security.Claims;
using WebMonitoringApi.Data.Models;

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

        [HttpGet]
        public IActionResult Get(LogInputModel input)
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

                    logs.Select(log => new LogResult
                    {
                        Id = log.Id,
                        Sent = log.SentOn,
                        Received = log.ReceivedOn,
                        ExecutionTime = log.SentOn.Subtract(log.ReceivedOn).Milliseconds,
                        StatusCode = log.StatusCode,
                        Url = log.Url.Value,
                    });

                    return Ok(logs);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(LogInputModel input)
        {
            if(input != null)
            {
                _dbContext.Logs.Remove(_dbContext.Logs.FirstOrDefault(log => log.Id == input.Id));
                _dbContext.SaveChanges();

                return Ok();
            }
            return BadRequest();
        }
    }
}
