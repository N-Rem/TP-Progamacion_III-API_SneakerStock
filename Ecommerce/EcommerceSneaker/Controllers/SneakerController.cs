using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceSneaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SneakerController : ControllerBase
    {
        private readonly ISneakerService _senakerServices;

        public SneakerController(ISneakerService senakerServices)
        {
            _senakerServices = senakerServices;
        }

        [HttpGet("sneaker")]
        public IActionResult GetAll()
        {
            return Ok(_senakerServices.GetSneaker());
        }

        [HttpGet("sneakerById{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_senakerServices.GetById(id));
        }

        [HttpGet("GetByBrand{brand}")]
        public IActionResult GetByBrand([FromRoute] string brand)
        {
            return Ok(_senakerServices.GetByBrand(brand));
        }

        [HttpGet("GetByCategory{category}")]
        public IActionResult GetByCategor([FromRoute] string category)
        {
            return Ok(_senakerServices.GetByCategory(category));
        }

        [HttpPost]
        public IActionResult Cretate(SneakerDto sneakerDto)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            try
            {
                return Ok(_senakerServices.Create(sneakerDto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("updateSneaker{idSneaker}")]
        public IActionResult Update([FromBody] SneakerDto sneakerDto, [FromRoute] int idSneaker)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            try
            {
                _senakerServices.Update(sneakerDto, idSneaker);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            _senakerServices.DeleteById(id);
            return Ok();
        }
    }
}
