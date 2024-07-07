using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceSneaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("/users")]
        public IActionResult GetUser()
        {
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            return Ok(_userServices.GetUsers());
        }

        [HttpGet("/admin")]
        public IActionResult GetAdmin()
        {
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            return Ok(_userServices.GetAdmins());
        }

        [HttpGet("/client")]
        public IActionResult GetClient()
        {
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            return Ok(_userServices.GetClients());
        }

        [HttpGet("/AllReservationUser")]
        public IActionResult GetAllReservationUser()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Client")
                return Forbid();
            return Ok(_userServices.GetAllReservationUser(userId));
        }

        [HttpGet("/userbyid{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            return Ok(_userServices.GetById(id));
        }

        [HttpPost("/Admin")]
        public IActionResult CreateAdmin([FromBody] UserCreateRequest admin)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid();
            _userServices.CreateAdmin(admin);
            return Ok();
        }
        [HttpPost("/Client")]
        public IActionResult CerateClient([FromBody] UserCreateRequest client)
        {
            _userServices.CreateClient(client);
            return Ok();
        }


        [HttpPut("/Update{idUser}")]
        public IActionResult Update([FromBody] UserCreateRequest user, [FromRoute] int idUser)
        {
            _userServices.Update(user, idUser);
            return Ok();
        }


        [HttpDelete("/Delete{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userServices.DeleteById(id);
            return Ok();
        }
    }
}
