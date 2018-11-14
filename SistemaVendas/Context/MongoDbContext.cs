using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace SistemaVendas.Context
{
    public class MongoDbContext
    {
        private readonly IConfiguration _configuration;

        public MongoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoCollection<T> Get<T>()
        {
            var client = new MongoClient(_configuration.GetConnectionString("ConexaoCatalogo"));
            var db = client.GetDatabase("DBCatalogo");

            return db.GetCollection<T>(typeof(T).Name);
        }

        public IMongoQueryable<T> GetFiltered<T>(Expression<Func<T, bool>> predicate)
        {
            var client = new MongoClient(_configuration.GetConnectionString("ConexaoCatalogo"));
            var db = client.GetDatabase("DBCatalogo");

            return db.GetCollection<T>(typeof(T).Name).AsQueryable().Where(predicate);
        }
    }
}
