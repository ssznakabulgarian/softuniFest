using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
