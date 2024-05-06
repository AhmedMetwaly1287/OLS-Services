using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Repository;
using UserService.Models;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using UserService.DTO;
using UserService.Services;
using AutoMapper;
using System.Security.Cryptography;
using UserService.Helpers;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(AppDbContext db, IUserRepository userRepo,IMapper mapper,IUserService userService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userService = userService;
    }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            if(await _userService.UserExists(id))
            {
                var UserInfo = await _userService.GetUserByID(id);
                return Ok(UserInfo);
            }
            return NotFound($"User with the ID {id} does not exist");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO LoginDTO)
        {
            var JWT = await _userService.ValidateCreds(LoginDTO);

            if (JWT != null)
            {
                return Ok(new
                {
                    token = JWT
                });
            }

            return Unauthorized("Invalid Credentials");
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> AddUser([FromBody] RegisterDTO registerDTO)
        {
 
            if (await _userService.EmailExists(registerDTO.Email))
            {
                return BadRequest("A User with this Email Already Exists");
            }
            var HashedPassword = _userService.HashPassword(registerDTO.Password);

            registerDTO.Password = HashedPassword;

            await _userService.AddUser(registerDTO);

            return Ok(new{msg = "New User Added"});
        }
        
  
        [HttpPut("ApproveUser/{id}")]
        public async Task<IActionResult> ApproveUser(int id) 
        {
            if(await _userService.UserExists(id) && !await _userService.AlreadyApproved(id))
            {
                await _userService.ApproveUser(id);
                return Ok(new { msg = $"User with ID {id} has been approved" });
            }
            return BadRequest("Error while approving user (No Such User or User is Already Approved)");
        }

        [HttpPut("MakeLibrarian/{id}")]
        public async Task<IActionResult> MakeLibrarian(int id)
        {
            if (await _userService.UserExists(id) && !await _userService.CheckRole(id))
            {
                await _userService.MakeLibrarian(id);
                return Ok(new { msg = $"User with ID {id} has been made a Librarian" });
            }
            return BadRequest("Error while approving user (No Such User or User is Already a Librarian)");
        }




        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if(await _userService.UserExists(id))
            {
                await _userService.DeleteUser(id);
                return Ok(new { msg = $"User with the ID {id} has been deleted successfully" });
            }
            return NotFound($"User with the ID {id} does not exist");
        }



    }

}
