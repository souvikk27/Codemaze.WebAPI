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
            var companies = service.CompanyService.GetAllCompanies(trackChanges: false);

            return Ok(companies);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = service.CompanyService.GetCompany(id, trackChanges: false);

            return Ok(company);
        }
    }
}
