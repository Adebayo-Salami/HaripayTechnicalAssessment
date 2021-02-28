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
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    Models.Response response = new Models.Response();
        //    response.ResponseCode = "00";
        //    response.ResponseMessage = "Customer Api Is Up";

        //    return Ok(new JsonResult(response));
        //}

        //GET: api/customer
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            Models.Response response = new Models.Response();

            try
            {
                response.ResponseCode = "00";
                response.ResponseMessage = "Customer Records Retrieved Successfully";
                response.ResponseObject = _customerService.GetAllCustomers();
            }
            catch(Exception error)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = error.Message;
            }

            return Ok(new JsonResult(response));
        }

        //POST: api/customer
        [HttpPost]
        public IActionResult AddStudent([FromBody] CreateCustomerViewModel customer)
        {
            Models.Response response = new Models.Response();

            try
            {
                Customer customerCreated = _customerService.CreateCustomer(customer.Fullname, customer.CustomerType);

                response.ResponseCode = "00";
                response.ResponseMessage = "Customer created successfully.";
                response.ResponseObject = customerCreated;
            }
            catch(Exception error)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = error.Message;
            }

            return Ok(new JsonResult(response));
        }

        //GET: api/customer/{ID}
        [HttpGet("{id}")]
        public IActionResult GetCustomerByID(int id)
        {
            Models.Response response = new Models.Response();

            try
            {
                Customer customer = _customerService.GetCustomerByID(id);
                if (customer == null)
                {
                    response.ResponseCode = "-02";
                    response.ResponseMessage = "Customer Record Not Found";
                    return NotFound(new JsonResult(response));
                }

                response.ResponseCode = "00";
                response.ResponseMessage = "Customer Record Retrieved Successfully";
                response.ResponseObject = customer;
            }
            catch(Exception error)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = error.Message;
            }

            return Ok(new JsonResult(response));
        }

        //GET: api/customer/GetCustomerByName{name}
        [HttpGet("GetCustomerByName/{name}")]
        public IActionResult GetCustomerByName(string name)
        {
            Models.Response response = new Models.Response();

            try
            {
                Customer customer = _customerService.GetCustomerByName(name);
                if (customer == null)
                {
                    response.ResponseCode = "-02";
                    response.ResponseMessage = "Customer Record Not Found";
                    return NotFound(new JsonResult(response));
                }

                response.ResponseCode = "00";
                response.ResponseMessage = "Customer Record Retrieved Successfully";
                response.ResponseObject = customer;
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
