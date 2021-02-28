using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HaripayTechnicalAssessmentData.Interface;
using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaripayTechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly IDiscountService _discountService;

        public DiscountController(ILogger<DiscountController> logger, IDiscountService discountService)
        {
            _logger = logger;
            _discountService = discountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Models.Response response = new Models.Response();
            response.ResponseCode = "00";
            response.ResponseMessage = "Discount Api Is Up";

            return Ok(new JsonResult(response));
        }

        //GET: api/discount
        [HttpGet]
        public IActionResult GetAllDiscounts()
        {
            Models.Response response = new Models.Response();

            try
            {
                response.ResponseCode = "00";
                response.ResponseMessage = "Discounts Retrieved Successfully";
                response.ResponseObject = _discountService.GetAllDiscounts();
            }
            catch (Exception error)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = error.Message;
            }

            return Ok(new JsonResult(response));
        }

        //GET: api/discount/{type}
        [HttpGet("{type}")]
        public IActionResult GetDiscountByType(string type)
        {
            Models.Response response = new Models.Response();

            try
            {
                Discount discount = _discountService.GetDiscountByType(type);
                if (discount == null)
                {
                    response.ResponseCode = "-02";
                    response.ResponseMessage = "Discount Record Not Found";
                    return NotFound(new JsonResult(response));
                }

                response.ResponseCode = "00";
                response.ResponseMessage = "Discount Record Retrieved Successfully";
                response.ResponseObject = discount;
            }
            catch (Exception error)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = error.Message;
            }

            return Ok(new JsonResult(response));
        }
    }
}
