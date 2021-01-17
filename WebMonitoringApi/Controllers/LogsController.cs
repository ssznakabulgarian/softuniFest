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
        public IActionResult Get()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var currentUrls = _dbContext.Urls.Where(Url => Url.UserId == userId);

            if (currentUrls.Count() == 0
                || currentUrls == null)
            {
                return BadRequest();
            }

            var logs = new List<Log>();

            for(int i = 0; i < currentUrls.ToArray().Length; i++)
            {
                var currentUrl = currentUrls.ToArray()[i];
                for(int j = 0; j < currentUrl.Logs.ToArray().Length; j++)
                {
                    logs.Add(currentUrl.Logs.ToArray()[j]);
                }
            }

            if(logs.Count() == 0)
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _dbContext.Logs.Remove(_dbContext.Logs.FirstOrDefault(log => log.Id == id));

            if(response.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
            {
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
