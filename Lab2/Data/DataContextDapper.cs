using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Lab2.Data
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
      IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Query<T>(sql);
    }

    public T? LoadDataSingle<T>(string sql)
    {
      IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.QuerySingleOrDefault<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
      IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Execute(sql) > 0;
    }

    public int ExecuteSqlWithRows(string sql)
    {
      IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Execute(sql);
    }

    public void ShowInfo()
    {
      IDbConnection dbConnection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
      System.Console.WriteLine(dbConnection.ConnectionString);
      System.Console.WriteLine(dbConnection.Database);
      System.Console.WriteLine(dbConnection.State);
    }
  }
}