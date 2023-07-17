using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/v1/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager service;

        public CompaniesController(IServiceManager service)
        {
            this.service = service;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = service.CompanyService.GetAllCompanies(trackChanges: false);
                return Ok(companies);
            }
            catch 
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
