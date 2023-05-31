using CQRSMediatrDDD.Domain.Entities.v1;

namespace CQRSMediatrDDD.Domain.Contracts.v1;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<Person> FindByDocumentAsync(string? document, CancellationToken cancellationToken);
}