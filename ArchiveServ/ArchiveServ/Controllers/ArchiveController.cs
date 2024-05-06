using Microsoft.AspNetCore.Mvc;
using ArchiveService.Data;
using ArchiveService.Repository;
using ArchiveService.Models;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ArchiveService.DTO;
using ArchiveService.Services;
using AutoMapper;

namespace OLS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchiveService _aService;


        public ArchiveController(IArchiveService aService)
        {
            _aService = aService;
        }

        [HttpPost("AddArchive")]
        public async Task<IActionResult> AddArchive(AddArchiveDTO ArchiveDTO)
        {
            await _aService.AddToArchive(ArchiveDTO);
            return Ok("Request Archived Successfully");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllArchives()
        {
            var Archives = await _aService.GetAll();
            return Ok(Archives);
        }

        [HttpGet("GetUserArchive/{UserID}")]
        public async Task<IActionResult> GetUserArchives(int UserID)
        {
            var UserRequests = await _aService.GetArchiveByID(UserID);
            return Ok(UserRequests);
        }


    }

}
