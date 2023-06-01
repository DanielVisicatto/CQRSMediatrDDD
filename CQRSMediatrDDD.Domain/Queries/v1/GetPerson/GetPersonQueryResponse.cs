namespace CQRSMediatrDDD.Domain.Queries.v1.GetPerson;

public class GetPersonQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}
