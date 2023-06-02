using MediatR;
using System.Text.Json.Serialization;

namespace CQRSMediatrDDD.Domain.Commands.v1.UpdatePerson;

public class UpdatePersonCommand : IRequest
{
    public UpdatePersonCommand(string? name, string? cpf, string? email, DateTime birthDate)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        BirthDate = birthDate;
    }

    [JsonIgnore]
    public Guid Id { get; set; }
    public string? Name { get; }
    public string? Cpf { get; }
    public string? Email { get; }
    public DateTime BirthDate { get; }
}