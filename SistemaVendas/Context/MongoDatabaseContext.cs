using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace SistemaVendas.Context
{
    public class MongoDatabaseContext<T> : IDatabaseContext<T> where T : class, new()
    {
        private readonly IMongoDatabase _databaseContext;
        private IMongoCollection<T> Collection => _databaseContext.GetCollection<T>(typeof(T).Name);

        public MongoDatabaseContext(string connectionString)
        {
            _databaseContext = new MongoClient(connectionString).GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
        }

        public IMongoQueryable<T> Query
        {
            get => Collection.AsQueryable<T>();
            set => Query = value;
        }

        public T GetOne(Expression<Func<T, bool>> expression)
        {
            return Collection.Find(expression).SingleOrDefault();
        }

        public T FindOneAndUpdate(Expression<Func<T, bool>> expression, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> option)
        {
            return Collection.FindOneAndUpdate(expression, update, option);
        }

        public void UpdateOne(Expression<Func<T, bool>> expression, UpdateDefinition<T> update)
        {
            Collection.UpdateOne(expression, update);
        }

        public void DeleteOne(Expression<Func<T, bool>> expression)
        {
            Collection.DeleteOne(expression);
        }

        public void InsertMany(IEnumerable<T> items)
        {
            Collection.InsertMany(items);
        }

        public void InsertOne(T item)
        {
            Collection.InsertOne(item);
        }
    }
}
