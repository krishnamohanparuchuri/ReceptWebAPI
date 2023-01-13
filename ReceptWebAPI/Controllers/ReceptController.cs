using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceptWebAPI.Models.DTO;
using ReceptWebAPI.Repository.Interfaces;

namespace ReceptWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptController : ControllerBase
    {
        private readonly IReceptRepo _receptRepo;

        public ReceptController(IReceptRepo receptRepo)
        {
            _receptRepo = receptRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_receptRepo.GetAllRecepts());
        }

        [HttpGet("{receptId}")]
        public IActionResult GetIndividualRecept(int receptId)
        {
            return Ok(_receptRepo.GetIndividualReceptById(receptId));
        }

        [HttpGet("search/{title}")]
        public IActionResult GetIndividualRecept(string title)
        {
            return Ok(_receptRepo.GetReceptByTitle(title));
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        public IActionResult GetReceptsByCustomer(int customerId)
        {
            return Ok(_receptRepo.GetAvailableReceptsByCustomerId(customerId));
        }

        [HttpPost("{customerId}")]
        public IActionResult InsertRecept(int customerId, [FromBody] ReceptInputInsertDto receptInputDto)
        {
            return Ok(_receptRepo.InsertReceptByCustomerId(receptInputDto,customerId));
        }

        [HttpPut("{receptId}")]
        public IActionResult updateRecept([FromQuery]int customerId,int receptId, [FromBody] ReceptUpdateDto receptUpadteDto)
        {
            return Ok(_receptRepo.UpdateReceptById(receptUpadteDto,customerId,receptId));
        }

        [HttpDelete("{receptId}")]
        public IActionResult DeleteRecept([FromQuery] int customerId, int receptId)
        {
            var message = _receptRepo.DeleteReceptById(customerId,receptId);
            return Ok(new
            {
                message
            });
        }

        [HttpGet]
        [Route("rating/{receptId}")]
        public IActionResult UpadteRatingValue(int receptId,[FromQuery]int customerId, [FromQuery]int ratingValue)
        {
            if (!(ratingValue >= 1 && ratingValue <= 5)) return BadRequest("Rating Value must be between 1 and 5 ");

            var recept = _receptRepo.GetIndividualReceptById(receptId);
            if(recept.CustomerId == customerId)
            {
                return BadRequest("Customer must be a different member");
            }
            var message = _receptRepo.SetRatingValue(customerId,receptId,ratingValue);

            return Ok(new
            {
                message
            });
        }
    }
}
