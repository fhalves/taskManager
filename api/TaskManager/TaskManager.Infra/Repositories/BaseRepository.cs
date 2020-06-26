using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Infra.Common;

namespace TaskManager.Infra.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly DbConnection dbConnection;
        private DynamicParameters parametros;
        private readonly IDbFactory _dbFactory;

        protected BaseRepository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public T Get(object id)
        {
            var connection = _dbFactory.GetDbConnection().Result;

            T ret = connection.Get<T>(id);
            DisposeRepository(connection);
            return ret;
        }

        public IEnumerable<T> GetAll()
        {
            var connection = _dbFactory.GetDbConnection().Result;

            IEnumerable<T> ret = connection.GetAll<T>();
            DisposeRepository(connection);
            return ret;
        }

        public T Post(T obj)
        {
            var connection = _dbFactory.GetDbConnection().Result;

            int ret = (int)connection.Insert<T>(obj);
            DisposeRepository(connection);
            return ret > 0 ? obj : null;
        }

        public T Put(T obj)
        {
            var connection = _dbFactory.GetDbConnection().Result;

            bool ret = connection.Update<T>(obj);
            DisposeRepository(connection);
            return ret == true ? obj : null;
        }

        public T Delete(T obj)
        {
            var connection = _dbFactory.GetDbConnection().Result;

            bool ret = connection.Delete<T>(obj);
            DisposeRepository(connection);
            return ret == true ? obj : null;
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            var connection = _dbFactory.GetDbConnection().Result;

            T ret = connection.QueryFirstOrDefault<T>(sql, param);

            DisposeRepository(connection);

            return ret;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            var connection = _dbFactory.GetDbConnection().Result;

            IEnumerable<T> ret = connection.Query<T>(sql, param);

            DisposeRepository(connection);

            return ret;
        }

        public void DisposeRepository(DbConnection connection)
        {
            connection.Close();
        }

        public void CreateParameters()
        {
            parametros = new DynamicParameters();
        }

        public void AddParameters(DapperParameters parameters)
        {
            parametros.Add(parameters.Name, parameters.Value, parameters.Type);
        }

        public DynamicParameters GetParameters()
        {
            return parametros;
        }
    }
}
