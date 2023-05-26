using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.CommandHandlers.User;
using UserManagement.Application.DTOs;
using UserManagement.Application.QueryHandlers.User;

namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //[ProducesDefaultResponseType(typeof(IEnumerable<UserDto>))]
        //public async Task<IActionResult> GetUsers([FromQuery] GetUserQuery query)
        //{
        //    return Ok(await _mediator.Send(query));
        //}

        [HttpGet("{userId}")]
        [ProducesDefaultResponseType(typeof(UserDto))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery() { UserId = userId }));
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(UserDto))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var user = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserDetails), new { userId = user.Id }, user);
        }

        //[HttpPut("{userId}")]
        //[ProducesDefaultResponseType(typeof(UserDto))]
        //public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserCommand command)
        //{
        //    command.Id = userId;
        //    return Ok(await _mediator.Send(command));
        //}

        //[HttpDelete("{userId}")]
        //[ProducesDefaultResponseType(typeof(int))]
        //public async Task<IActionResult> DeleteUser(string userId)
        //{
        //    if (IsDeleteForbidden(userId))
        //    {
        //        return StatusCode((int)System.Net.HttpStatusCode.Forbidden);
        //    }
        //    //TODO: We might need to implement soft deletion.
        //    return Ok(await _mediator.Send(new DeleteUserCommand(userId)));
        //}
    }
}
