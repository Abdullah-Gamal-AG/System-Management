using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdateDesc
{

	private void LoadCurrentDescription()
	{
		try
		{
			if (string.IsNullOrWhiteSpace(productName.Text))
			{
				currentDescDisplay.Clear();
				return;
			}
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT description FROM goods WHERE name = @name";
			List<SqliteParameter> list = new List<SqliteParameter>
			{
				new SqliteParameter("@name", productName.Text.Trim())
			};
			DataTable dataTable = coreSystem.Select(query, list.ToArray());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				string text = dataTable.Rows[0]["description"]?.ToString()!;
				currentDescDisplay.Text = (string.IsNullOrEmpty(text) ? "No description set." : text);
				currentDescDisplay.ForeColor = Color.Black;
			}
			else
			{
				currentDescDisplay.Text = "Product not found";
				currentDescDisplay.ForeColor = Color.Gray;
			}
		}
		catch
		{
			currentDescDisplay.Text = "---";
		}
	}
}