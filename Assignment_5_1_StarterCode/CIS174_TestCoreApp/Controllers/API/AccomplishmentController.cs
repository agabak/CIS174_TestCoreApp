using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Filters;
using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers.API
{
    [LogsRequestAndResponseFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class AccomplishmentController : ControllerBase
    {
        private readonly IAccomplishmentService _accomplishment;

        public AccomplishmentController(IAccomplishmentService accomplishment)
        {
            _accomplishment = accomplishment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var accomplishments = _accomplishment.GetAccomplishments();
            if (accomplishments.Any()) return Ok(accomplishments);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var accomplishment = _accomplishment.GetAccomplisment(id);
            if (accomplishment != null) return Ok(accomplishment);
            return NotFound();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public IActionResult Post(PersonAccomplishmentViewModel model)
        {        
           if (_accomplishment.Create(model)) return Ok();

            return BadRequest();
            
        }

        [HttpPost("{createAccomplishment}")]
        public IActionResult Post(CreateAccomplishmentViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (_accomplishment.CreateAccomplishment(model)) return Ok();
            }
            return BadRequest();
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id > -1)
            {
                if (_accomplishment.Delete(id)) return NoContent();
                return BadRequest("Unable delete the content");

            }
            return BadRequest();
        }

        [HttpPut]
        [ValidatePersonInfoFilter]
        public IActionResult Put(PersonDetailAccomplishmentViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = _accomplishment.Edit(model);
                if (result != null) return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("{accomplishmentEdit}")]
        public IActionResult Put(AccomplishmentViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = _accomplishment.UpdateAccomplishmnet(model);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
