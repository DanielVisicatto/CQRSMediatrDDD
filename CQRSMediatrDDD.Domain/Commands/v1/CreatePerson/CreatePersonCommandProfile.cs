using AutoMapper;
using CQRSMediatrDDD.Domain.Entities.v1;
using CQRSMediatrDDD.Domain.ValueObjects.v1;

namespace CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommandProfile : Profile
{
    public CreatePersonCommandProfile()
    {
        CreateMap<CreatePersonCommand, Person>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => new Document(input.Cpf!)))
            .ForMember(fieldOutput => fieldOutput.Name, option => option
                .MapFrom(input => new Name(input.Name!)))
            .ForMember(fieldOutput => fieldOutput.Email, option => option
                .MapFrom(input => new Name(input.Email!)));
    }
}