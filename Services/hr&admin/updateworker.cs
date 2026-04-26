using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdateWorker
{

	private void updateworker_Load(object? sender, EventArgs e)
	{
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT * FROM workers WHERE id = @id";
			SqliteParameter[] parameters = new SqliteParameter[1]
			{
				new SqliteParameter("@id", id.Text)
			};
			DataTable dataTable = coreSystem.Select(query, parameters);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				oldworkeremail.Text = dataTable.Rows[0]["email"].ToString();
				oldworkername.Text = dataTable.Rows[0]["name"].ToString();
			}
			else
			{
				oldworkeremail.Text = string.Empty;
				oldworkername.Text = string.Empty;
			}
		}
		catch
		{
		}
	}
}