using FluentValidation;
using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Queries;
using MailAPI.Application.Queries.Users;
using MailAPI.Domain.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<UserController> _logger;

    public UserController(ISender sender, ILogger<UserController> logger)
    {
        _sender = sender;
        _logger = logger;
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
    [AllowAnonymous]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sender.Send(dto, cancellationToken);
            return Created(nameof(CreateUser), result);
        }

        catch (ValidationException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Gets a user
    /// </summary>
    /// <param name="id">Id of the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="200">Returns OK with the user</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserGetResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var requestDto = new UserGetQuery(Id: id);
            var result = await _sender.Send(requestDto, cancellationToken);
            return Ok(result);
        }

        catch (UserNotFoundException e)
        {
            _logger.LogError(e.Message);
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Gets a list of users
    /// </summary>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="200">Returns OK when the list isn't empty</response>
    /// <response code="404">Returns NotFound when the list is empty</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<UserGetResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var results = await _sender.Send(new UsersGetQuery(), cancellationToken);
        return results.Any() ? Ok(results) : NotFound();
    }

    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="id">Id of the user to update</param>
    /// <param name="dto">Command for updating user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="200">Returns OK with updated user</response>
    /// <response code="404">Returns NotFound if the user doesn't exist</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(List<UserGetResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateCommand dto, CancellationToken cancellationToken)
    {
        try
        {
            var requestDto = new UserUpdateCommand(Id: id, dto.Name, dto.Email, dto.Password);
            var result = await _sender.Send(requestDto, cancellationToken);
            return Ok(result);
        }

        catch (UserNotFoundException e)
        {
            _logger.LogError(e.Message);
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id">Id of the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="204">Returns NoContent</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        var requestDto = new UserDeleteCommand(Id: id);
        await _sender.Send(requestDto, cancellationToken);
        return NoContent();
    }
}