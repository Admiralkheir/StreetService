﻿namespace StreetService.Domain
{
    public interface IEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; }
    }
}
