using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a user
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Post /users
    ///     {
    ///         "name": "name",
    ///         "email": "email",
    ///         "password": "password"
    ///     }
    /// 
    /// </remarks>
    /// <param name="dto">Dto for creating a user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="201">Returns created with the user id</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(dto,cancellationToken);
        return Created(nameof(CreateUser), result);
    }

    /// <summary>
    /// Gets a user
    /// </summary>
    /// <param name="id">Id of the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="200">Returns OK with the user</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserGetResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        var requestDto = new UserGetRequestDto(Id: id);
        var user = await _mediator.Send(requestDto, cancellationToken);
        return Ok(user);
    }
}