using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Entities.v1;
using CQRSMediatrDDD.Domain.Helpers.v1;
using System.Collections.Immutable;

namespace CQRSMediatrDDD.Domain.Queries.v1.ListPerson;

public class ListPersonQueryHandler : BaseHandler
{
    private readonly ICacheRepository<List<Person>> _cacheRepository;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public ListPersonQueryHandler(
        INotificationContext notificationContext,
        IMapper mapper,
        IPersonRepository repository,
        ICacheRepository<List<Person>> cacheRepository) : base (notificationContext)
    {
        _cacheRepository = cacheRepository;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ListPersonQueryResponse>> HandleAsync(ListPersonQuery command, CancellationToken cancellationToken)
    {
        var key = GetKey(new[] { command.Name, command.Cpf });
        var cachePeople = await _cacheRepository.GetAsync(key);

        if (cachePeople != null && cachePeople.Any()) _mapper.Map<IEnumerable<ListPersonQueryResponse>>(cachePeople);

        var people = await _repository.FindAsync(
            person =>
            (command.Name == null || person.Name.Value.Contains(command.Name.ToUpper())) &&
            (command.Cpf == null || person.Cpf.Value.Contains(command.Cpf.RemoveMaskCpf())),
            cancellationToken);

        return _mapper.Map<List<ListPersonQueryResponse>>(people);
    }

    private static string GetKey(IEnumerable<string?> parameters)
    {
        var parametersWithoutEmpties = parameters.Where( p => !string.IsNullOrWhiteSpace(p)).ToImmutableArray();
        return !parametersWithoutEmpties.Any() ? "getAll" : string.Join('|', parametersWithoutEmpties);
    }
}
