using CQRSMediatrDDD.Domain.ValueObjects.v1;
namespace CQRSMediatrDDD.Domain.Entities.v1
{
    public class Person : Entity
    {
        public Person(Name name, Document cpf, Email email, DateTime birthDate)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            BirthDate = birthDate;
        }

        public Person(Guid id, Name name, Document cpf, Email email, DateTime birthDate, DateTime createdAt) : base (id, createdAt, DateTime.Now)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            BirthDate = birthDate;
        }

        public Name Name { get; set; }
        public Document Cpf { get; set; }
        public Email Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
