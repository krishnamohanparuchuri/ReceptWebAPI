using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptWebAPI.Models.DTO;
using ReceptWebAPI.Repository.Interfaces;

namespace ReceptWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]CustomerLoginInputDTO customerInput)
        {
            if (customerInput == null)
            {
                return BadRequest("Please send the right input");
            }
            return Ok(_customerRepo.LoginCustomer(customerInput));
        }

        [HttpPost("InsertCustomer")]
        public IActionResult InsertCustomer([FromBody]CustomerInsertInputDTO customerInsert)
        {
         
            if(customerInsert == null)
            {
                return BadRequest("Please send the right input");
            }
            var successMessage = _customerRepo.InsertCustomer(customerInsert);
            return Ok(new
            {
             successMessage
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromBody] CustomerInsertInputDTO customerUpdate,int id)
        {
            if(id <= 0 || customerUpdate == null)
            {
                return BadRequest("please check the supplied credentials");
            }
            var successMessage = _customerRepo.UpdateCustomer(customerUpdate,id);
            return Ok(new
            {
                successMessage
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("please check the supplied credentials");
            }
            var successMessage = _customerRepo.DeleteCustomer(id);
            return NoContent();
        }
    }
}
