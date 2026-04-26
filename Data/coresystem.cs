using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace System;

public class CoreSystem
{
	private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyDatabase.db")}";

	private SqliteConnection CreateOpenConnection()
	{
		SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
		sqliteConnection.Open();
		return sqliteConnection;
	}

	private void ExecuteNonQuery(string query, SqliteParameter[] parameters)
	{
		using SqliteConnection sqliteConnection = CreateOpenConnection();
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		sqliteCommand.CommandText = query;
		if (parameters != null)
		{
			sqliteCommand.Parameters.AddRange(parameters);
		}
		sqliteCommand.ExecuteNonQuery();
	}

	public DataTable Select(string query, SqliteParameter[] parameters)
	{
		DataTable dataTable = new DataTable();
		using (SqliteConnection sqliteConnection = CreateOpenConnection())
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = query;
			if (parameters != null)
			{
				sqliteCommand.Parameters.AddRange(parameters);
			}
			using SqliteDataReader reader = sqliteCommand.ExecuteReader();
			dataTable.Load(reader);
		}
		return dataTable;
	}

	public async Task InsertAsync(string query, SqliteParameter[] parameters)
	{
		using SqliteConnection connection = CreateOpenConnection();
		using SqliteCommand command = connection.CreateCommand();
		command.CommandText = query;
		if (parameters != null)
		{
			command.Parameters.AddRange(parameters);
		}
		await command.ExecuteNonQueryAsync();
	}

	public async Task<DataTable> SelectAsync(string query, SqliteParameter[] parameters)
	{
		DataTable dataTable = new DataTable();
		using (SqliteConnection connection = CreateOpenConnection())
		{
			using SqliteCommand command = connection.CreateCommand();
			command.CommandText = query;
			if (parameters != null)
			{
				command.Parameters.AddRange(parameters);
			}
			using SqliteDataReader reader = await command.ExecuteReaderAsync();
			dataTable.Load(reader);
		}
		return dataTable;
	}

	public async Task UpdateAsync(string query, SqliteParameter[] parameters)
	{
		await InsertAsync(query, parameters);
	}

	public async Task DeleteAsync(string query, SqliteParameter[] parameters)
	{
		await InsertAsync(query, parameters);
	}

	public void Insert(string query, SqliteParameter[] parameters)
	{
		ExecuteNonQuery(query, parameters);
	}

	public void Update(string query, SqliteParameter[] parameters)
	{
		ExecuteNonQuery(query, parameters);
	}

	public void Delete(string query, SqliteParameter[] parameters)
	{
		ExecuteNonQuery(query, parameters);
	}
}
