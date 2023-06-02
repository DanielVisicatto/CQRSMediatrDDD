﻿using MediatR;

namespace CQRSMediatrDDD.Domain.Queries.v1.GetPerson;

public class GetPersonQuery : IRequest<GetPersonQueryResponse>
{
    public GetPersonQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}