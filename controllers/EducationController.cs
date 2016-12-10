using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ASTV.Services;
using ASTV.Models.Generic;

namespace ASTV.WebApi.Controllers {

    [Route("api/[controller]")]
    public class EducationController : Controller
    {
        private IEducationRepository _educationRepository;
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;            
        }
        // GET api/values
        [HttpGet("EducationLevels", Name = "GetEducationLevels")]        
        public IActionResult GetEducationLevels()
        {
            return new OkObjectResult(_educationRepository.GetAll());
            //return new string[] { "Language1", "Language2" };
        }
    }

}