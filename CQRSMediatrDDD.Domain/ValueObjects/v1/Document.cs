﻿using CQRSMediatrDDD.Domain.Helpers.v1;

namespace CQRSMediatrDDD.Domain.ValueObjects.v1;

public record Document
{
    public Document(string value)
    {
        Value = value.RemoveMaskCpf();
    }

    public string Value { get; set; }
}