using MediatR;

namespace CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommand : IRequest<Guid>
{
    public CreatePersonCommand(string? name, string? cpf, string? email, DateTime birthDate)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        BirthDate = birthDate;
    }

    public string? Name { get; }
    public string? Cpf { get; }
    public string? Email { get; }
    public DateTime BirthDate { get; }
}