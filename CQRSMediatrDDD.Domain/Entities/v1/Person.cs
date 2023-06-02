using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.ValueObjects.v1;

namespace CQRSMediatrDDD.Domain.Entities.v1;

public class Person : IEntity
{
    private Person(Guid id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Person(Name name, Document cpf, Email email, DateTime birthDate) : this(Guid.NewGuid(), DateTime.Now, DateTime.Now)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        BirthDate = birthDate;
    }

    public Person(Guid id, Name name, Document cpf, Email email, DateTime birthDate, DateTime createdAt) : this(id, createdAt, DateTime.Now)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
        BirthDate = birthDate;
    }

    public Person() { }

    public Guid Id { get; set; }
    public Name Name { get; set; }
    public Document Cpf { get; set; }
    public Email Email { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}