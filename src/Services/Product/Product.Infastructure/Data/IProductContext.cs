using MongoDB.Driver;
using System;
using Domain.Entities;

namespace Infastructure.Data
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
