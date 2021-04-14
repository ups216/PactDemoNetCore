using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Provider.Api.Web.Controllers
{
    public class Something
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
    }
    
    [ApiController]
    public class SomethingsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public SomethingsController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [Route("Somethings/{id}")]
        public Something GetSomething(string id)
        {
            var sth = new Something
            {
                Id = id,
                FirstName = "Totally",
                LastName = "Awesome"
            };
            return sth;
        }
    }
}