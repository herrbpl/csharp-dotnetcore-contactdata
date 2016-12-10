using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ASTV.Services;
using ASTV.Models.Generic;
using ASTV.Models.Employee;

namespace ASTV.WebApi.Controllers {

    [Route("api/[controller]")]
    public class ContactDataController : Controller
    {
        private IContactDataRepository _contactDataRepository;
        public ContactDataController(IContactDataRepository contactDataRepository)
        {
            _contactDataRepository = contactDataRepository;            
        }
        // GET api/values
        [HttpGet("{id}")]        
        public IActionResult Get(string id)
        {
            var cd = _contactDataRepository.Get(id);
            if (cd != null) {
                return new OkObjectResult(cd);
            }  else {
                return NotFound();
            }                        
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]ContactData cd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            var cd2 = _contactDataRepository.Get(id);
            
    
            if (cd2 == null)
            {
                return NotFound();
            }

            if (cd.EmployeeId != id) {
                return BadRequest();
            }

            _contactDataRepository.Update(cd);
            return new NoContentResult();

        }
        
    }

}