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
                UserId = userId
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
            return Ok(currentUrls);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var currentUrl = _dbContext.Users.FirstOrDefault(User => User.Id == userId)?
                            .Urls.FirstOrDefault(Url => Url.Id == id);

            if (currentUrl != null)
            {
                return Ok(currentUrl);
            }

            return BadRequest();
        }

        [HttpDelete] //delete by id
        public IActionResult Delete(UrlInputModel input)
        {
            if (input != null)
            {
                _dbContext.Urls.Remove(_dbContext.Urls.FirstOrDefault(url => url.Id == input.Id));
                _dbContext.SaveChanges();

                return Ok();
            }
            return BadRequest();
        }
    }
}
