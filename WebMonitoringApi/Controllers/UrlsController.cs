using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMonitoringApi.Data;
using WebMonitoringApi.InputModels;
using WebMonitoringApi.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using WebMonitoringApi.Services;
using WebMonitoringApi.Common;
using Microsoft.EntityFrameworkCore;

namespace WebMonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UrlsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MonitoringService _monitoringService;

        public UrlsController(ApplicationDbContext dbContext, MonitoringService monitoringService)
        {
            _dbContext = dbContext;
            _monitoringService = monitoringService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UrlInputModel input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var newUrl = new Url
            {
                Favourite = input.Favourite,
                Value = input.Value,
                Title = input.Title,
                RequestFrequencySeconds = input.RequestFrequencySeconds,
                Method = input.Method,
                UserId = userId,
                Body = input.Body,
                Headers = input.Headers
            };

            _dbContext.Users.FirstOrDefault(User => User.Id == userId)?.Urls.Add(newUrl);

            _dbContext.SaveChanges();

            _monitoringService.AttachTimerToUrl(newUrl);

            return Ok();
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

            var urlResults = currentUrls.Select(Url => new UrlResult
            {
                Id = Url.Id,
                Value = Url.Value,
                Title = Url.Title,
                Body = Url.Body,
                Favourite = Url.Favourite,
                Method = Url.Method,
                RequestFrequencySeconds = Url.RequestFrequencySeconds,
                Headers = Url.Headers.Select(header => new RequestHeaderResult
                {
                    Key = header.Key,
                    Value = header.Value
                })
            });

            return Ok(urlResults);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var currentUrls = _dbContext.Urls.Where(Url => Url.UserId == userId);

            if (currentUrls.Count() == 0
                || currentUrls == null)
            {
                return BadRequest();
            }

            var urlResult = currentUrls.Select(Url => new UrlResult
            {
                Id = Url.Id,
                Value = Url.Value,
                Title = Url.Title,
                Body = Url.Body,
                Favourite = Url.Favourite,
                Method = Url.Method,
                RequestFrequencySeconds = Url.RequestFrequencySeconds,
                Headers = Url.Headers.Select(header => new RequestHeaderResult
                {
                    Key = header.Key,
                    Value = header.Value
                })
            }).Where(Url => Url.Id == id);

            if (urlResult != null)
            {
                return Ok(urlResult);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")] //delete by id
        public IActionResult Delete(int id)
        {
            var url = _dbContext.Urls.FirstOrDefault(url => url.Id == id);
            
            if(url == null)
            {
                return BadRequest();
            }

            var result = _dbContext.Urls.Remove(url);
            _dbContext.SaveChanges();

            if(result.Entity.Id == id)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
