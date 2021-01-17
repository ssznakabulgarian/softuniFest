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

namespace WebMonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UrlsController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;

        public UrlsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UrlInputModel input)
        {
            if(input == null)
            {
                return BadRequest();
            }

            /*await _dbContext.Urls.AddAsync(new Url
            {
                Id = input.Id,
                Favourite = input.Favourite,
                Value = input.Value,
                Title = input.Title,
                RequestFrequencySeconds = input.RequestFrequencySeconds,
            });*/

            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            _dbContext.Users.FirstOrDefault(User => User.Id == userId)?.Urls.Add(new Url
            {
                Favourite = input.Favourite,
                Value = input.Value,
                Title = input.Title,
                RequestFrequencySeconds = input.RequestFrequencySeconds,
                Method = input.Method,
                UserId = userId
        });

            _dbContext.SaveChanges();

            //check if added?

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var currentUrls = _dbContext.Users.FirstOrDefault(User => User.Id == userId).Urls;

            if(currentUrls.Count == 0
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

            if(currentUrl != null)
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
