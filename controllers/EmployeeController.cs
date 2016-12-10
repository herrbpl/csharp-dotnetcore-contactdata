using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;


using ASTV.Services;
using ASTV.Models.Generic;

namespace ASTV.WebApi.Controllers {

    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _EmployeeRepository;
        private ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository EmployeeRepository, ILoggerFactory loggerFactory)
        {
            _EmployeeRepository = EmployeeRepository;      
            _logger = loggerFactory.CreateLogger<EmployeeController>();    
            _logger.LogInformation("Controller started! JHJHJ");
        }


        [HttpGet]        
        public IActionResult Get()
        {
            return new OkObjectResult(_EmployeeRepository.GetAll().OrderBy( p => p.Name).ToList());            
        }

       
    }

}