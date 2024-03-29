using System.Data;
using Dapper;
using Npgsql;

namespace Lab1.Data
{
  class DataContextDapper
  {
    private readonly IConfiguration _config;
    public DataContextDapper(IConfiguration config)
    {
      _config = config;
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {
      IDbConnection dbConnection = new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Query<T>(sql);
    }

    public T? LoadDataSingle<T>(string sql)
    {
      IDbConnection dbConnection = new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.QuerySingleOrDefault<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
      IDbConnection dbConnection = new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Execute(sql) > 0;
    }

    public int ExecuteSqlWithRows(string sql)
    {
      IDbConnection dbConnection = new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Execute(sql);
    }
  }
}