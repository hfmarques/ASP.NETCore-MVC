using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace SistemaVendas.Context
{
    public interface IDatabaseContext<T> where T : class, new()
    {
        IMongoQueryable<T> Query { get; set; }

        T GetOne(Expression<Func<T, bool>> expression);

        T FindOneAndUpdate(Expression<Func<T, bool>> expression, UpdateDefinition<T> update,
            FindOneAndUpdateOptions<T> option);

        void UpdateOne(Expression<Func<T, bool>> expression, UpdateDefinition<T> update);

        void DeleteOne(Expression<Func<T, bool>> expression);

        void InsertMany(IEnumerable<T> items);

        void InsertOne(T item);
    }
}
