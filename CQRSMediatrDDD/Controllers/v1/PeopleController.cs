using CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;
using CQRSMediatrDDD.Domain.Commands.v1.DeletePerson;
using CQRSMediatrDDD.Domain.Commands.v1.UpdatePerson;
using CQRSMediatrDDD.Domain.Queries.v1.GetPerson;
using CQRSMediatrDDD.Domain.Queries.v1.ListPerson;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRSMediatrDDD.API.Controllers.v1;

[Route("api/v1/people")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly CreatePersonCommandHandler _createPersonCommandHandler;
    private readonly GetPersonQueryHandler _getPersonQueryHandler;
    private readonly ListPersonQueryHandler _listPersonQueryHandler;
    private readonly UpdatePersonCommandHandler _updatePersonCommandHandler;
    private readonly DeletePersonCommandHandler _deletePersonCommandHandler;

    public PeopleController(
        CreatePersonCommandHandler createPersonCommandHandler,
        GetPersonQueryHandler getPersonQueryHandler,
        ListPersonQueryHandler listPersonQueryHandler,
        UpdatePersonCommandHandler updatePersonCommandHandler,
        DeletePersonCommandHandler deletePersonCommandHandler)
    {
        _createPersonCommandHandler = createPersonCommandHandler;
        _getPersonQueryHandler = getPersonQueryHandler;
        _listPersonQueryHandler = listPersonQueryHandler;
        _updatePersonCommandHandler = updatePersonCommandHandler;
        _deletePersonCommandHandler = deletePersonCommandHandler;
    }

    [HttpPost(Name = "Insert Person")]
    public async Task<IActionResult> InsertAsync([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var response = await _createPersonCommandHandler.HandleAsync(command, cancellationToken);

        return Created($"api/v1/people/{response}", new
        {
            id = response,
            command.Name,
            command.Email,
            command.BirthDate,
            command.Cpf
        });
    }

    [HttpGet("{id:guid}", Name = "Get Person By Id")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _getPersonQueryHandler.HandleAsync(new GetPersonQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet(Name = "List People")]
    public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] string? cpf, CancellationToken cancellationToken)
    {
        var response = await _listPersonQueryHandler.HandleAsync(new ListPersonQuery(name, cpf), cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:guid}", Name = "Update Person")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await _updatePersonCommandHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:guid}", Name = "Delete Person By Id")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _deletePersonCommandHandler.Handleasync(new DeletepersonCommand(id), cancellationToken);
        return NoContent();
    }
}
