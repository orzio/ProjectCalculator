using Microsoft.AspNetCore.Mvc;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BendingController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public BendingController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            BendingCommand bendingCommand = new BendingCommand();
            _commandDispatcher.DispatchAsync(bendingCommand);
            return null;
        }

        //[HttpPost]
        //public IEnumerable<WeatherForecast> Post()
        //{

        //}
    }
}
