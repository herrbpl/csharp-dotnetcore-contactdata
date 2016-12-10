using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ASTV.Services;
using ASTV.Models.Generic;

namespace ASTV.WebApi.Controllers {

    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private ILanguageRepository _languageRepository;
        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;            
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_languageRepository.GetAll());
            //return new string[] { "Language1", "Language2" };
        }
    }


    


}