using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Entities.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CQRSMediatrDDD.Infra.Repository.Repositories.v1
{
    internal class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository (IMongoClient client, IOptions<MongoRepositorySettings> settings) : base (client, settings) { }

        public async Task <Person?> FindByDocumentAsync(string? document, CancellationToken cancellationToken)
        {
            var filter = Builders<Person>.Filter.Eq(person => person.Cpf.Value, document.ToUpper());
            return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
