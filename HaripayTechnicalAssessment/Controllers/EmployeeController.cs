using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HaripayTechnicalAssessmentData.Interface;
using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaripayTechnicalAssessment.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace HaripayTechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private Models.Response apiResponse = new Models.Response();

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IConfiguration configuration, IWebHostEnvironment env)
        {
            _logger = logger;
            _employeeService = employeeService;
            _configuration = configuration;
            _env = env;
        }

        public IActionResult GetAll()
        {
            return new JsonResult(_employeeService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEmployeeViewModel model)
        {
            if (_employeeService.Create(model.EmployeeName, model.DepartmentId, model.ProfilePicName, out string message))
            {
                apiResponse.ResponseCode = "00";
                apiResponse.ResponseMessage = "SUCCESS!";
            }
            else
            {
                apiResponse.ResponseCode = "-01";
                apiResponse.ResponseMessage = message;
            }

            return new JsonResult(apiResponse);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CreateEmployeeViewModel model)
        {
            if (_employeeService.Update(model.EmployeeId, model.EmployeeName, model.ProfilePicName, out string message))
            {
                apiResponse.ResponseCode = "00";
                apiResponse.ResponseMessage = "SUCCESS!";
            }
            else
            {
                apiResponse.ResponseCode = "-01";
                apiResponse.ResponseMessage = message;
            }

            return new JsonResult(apiResponse);
        }

        [HttpPost]
        [Route("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new System.IO.FileStream(physicalPath, System.IO.FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch(Exception err)
            {
                return new JsonResult(err);
            }
        }

    }
}
