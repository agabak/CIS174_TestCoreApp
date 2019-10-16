using CIS174_TestCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : Controller
    {
        public IEnumerable<HouseViewModel> Houses { get; } = new List<HouseViewModel>
                                                {
                                                    new HouseViewModel {
                                                                         Id = 1,
                                                                         Bedrooms = 4 ,
                                                                         SquareFeet =  1854,
                                                                         DateBuilt = new DateTime(1972, 5, 28),
                                                                         Flooring = "Carpet"},
                                                    new HouseViewModel {
                                                                         Id = 2,
                                                                         Bedrooms = 3,
                                                                         SquareFeet =  1675,
                                                                         DateBuilt = new DateTime(2015,10, 17),
                                                                         Flooring = "Hardwood"}
                                                };

        public HouseController()
        {
                
        }

        [HttpGet]
        public IEnumerable<HouseViewModel> Get()
        {
            return Houses;
        }
        
        [HttpGet("{id}")]
        public HouseViewModel Get(int id)
        {
            var house = Houses.FirstOrDefault(x => x.Id == id);
            if(house != null) return house;

            return null;
        }

        [HttpPost("{create}")]
        public IActionResult Post(HouseViewModel model)
        {
            if(ModelState.IsValid)
            {
                return StatusCode(201);
            }

            return BadRequest();
        }
    }
}
