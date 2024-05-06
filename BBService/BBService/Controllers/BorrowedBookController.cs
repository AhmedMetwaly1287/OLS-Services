using Microsoft.AspNetCore.Mvc;
using BorrowedBookService.Data;
using BorrowedBookService.Repository;
using BorrowedBookService.Models;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BorrowedBookService.DTO;
using BorrowedBookService.Services;
using AutoMapper;


namespace BorrowedBookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBookController : ControllerBase
    {
        private readonly IBorrowedBookService _bbService;
        private readonly int MAX_NUM = 2; //Max Number of Books to be borrowed

        public BorrowedBookController(IBorrowedBookService bbService)
        {
            _bbService = bbService;
        }
        [HttpPost("SubmitRequest")]
        public async Task<IActionResult> SubmitRequest([FromBody] RequestDTO RequestDTO)
        {
            if(await _bbService.GetNumberOfRequests(RequestDTO.UserID) == MAX_NUM)
            {
                return BadRequest("You have exceeded the allowed number of requests");
            }
            await _bbService.AddRequest(RequestDTO);
            return Ok(new{msg="Request Submitted Successfully, Please await the Librarians decision."});
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRequests()
        {
            var Requests = await _bbService.GetAll();
            return Ok(Requests);
        }

        [HttpGet("GetUserRequests/{UserID}")]
        public async Task<IActionResult> GetRequestsByID(int UserID)
        {
           var UserRequests = await _bbService.GetBorrowedBooksByID(UserID);
            return Ok(UserRequests);
        }

        [HttpPut("ApproveRequest/{ReqID}")]
        public async Task<IActionResult> ApproveRequest(int ReqID)
        {
                if(await _bbService.RequestExists(ReqID))
            {
                await _bbService.ApproveRequest(ReqID);
                return Ok(new {msg ="Request has been Approved"});
            }
            return NotFound("Request with this ID doesn't exist");
        }

        [HttpPut("RejectRequest/{ReqID}")]
        public async Task<IActionResult> RejectRequest(int ReqID)
        {
            if (await _bbService.RequestExists(ReqID))
            {
                var Req = await _bbService.GetRequest(ReqID);
                await _bbService.RejectRequest(ReqID);
                return Ok(new { msg = "Request has been Rejected" });
            }
            return NotFound("Request with this ID doesn't exist");
        }

        [HttpPut("ReturnBook/{ReqID}")]
        public async Task<IActionResult> ReturnBook(int ReqID)
        {
            if (await _bbService.RequestExists(ReqID))
            {
                var Req = await _bbService.GetRequest(ReqID);
                await _bbService.ReturnBook(ReqID);
                return Ok("Book has been returned");
            }
            return NotFound("Request with this ID doesn't exist");
        }

        [HttpDelete("DeleteRequest/{ReqID}")]
        public async Task<IActionResult> DeleteRequest(int ReqID)
        {
            if (await _bbService.RequestExists(ReqID))
            {
                var Req = await _bbService.GetRequest(ReqID);
                await _bbService.DeleteRequest(ReqID);
                return Ok(new { msg = "Request has been deleted" });
            }
            return NotFound("Request with this ID doesn't exist");
        }

    }

}
