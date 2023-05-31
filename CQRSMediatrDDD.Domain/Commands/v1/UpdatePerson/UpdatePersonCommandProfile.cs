using AutoMapper;
using CQRSMediatrDDD.Domain.Entities.v1;
using CQRSMediatrDDD.Domain.ValueObjects.v1;

namespace CQRSMediatrDDD.Domain.Commands.v1.UpdatePerson;

public class UpdatePersonCommandProfile : Profile
{
    public UpdatePersonCommandProfile()
    {
        CreateMap<UpdatePersonCommand, Person>()
            .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                .MapFrom(input => new Document(input.Cpf!)))
            .ForMember(fieldOutput => fieldOutput.Name, option => option
                .MapFrom(input => new Document(input.Name!)))
              .ForMember(fieldOutput => fieldOutput.Email, option => option
                .MapFrom(input => new Document(input.Email!)));
    }
}