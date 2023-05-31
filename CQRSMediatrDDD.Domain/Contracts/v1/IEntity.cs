﻿namespace CQRSMediatrDDD.Domain.Contracts.v1
{
    public interface IEntity : IEquatable<IEntity>
    {
        Guid Id { get; }
    }
}
