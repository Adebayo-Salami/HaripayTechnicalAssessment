using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HaripayTechnicalAssessmentData.Interface;
using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaripayTechnicalAssessment.ViewModel;

namespace HaripayTechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;
        private Models.Response apiResponse = new Models.Response();

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        public IActionResult GetAll()
        {
            return new JsonResult(_departmentService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDepartmentViewModel model)
        {
            if(_departmentService.Create(model.DepartmentName, out string message))
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
        public IActionResult Update([FromBody] CreateDepartmentViewModel model)
        {
            if (_departmentService.Update(model.DepartmentId, model.DepartmentName, out string message))
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

    }
}
