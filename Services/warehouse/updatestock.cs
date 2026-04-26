using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdateStock
{

	private void UpdateOldStock(object sender, EventArgs e)
	{
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT quantity FROM goods WHERE name = @name";
			List<SqliteParameter> list = new List<SqliteParameter>
			{
				new SqliteParameter("@name", productName.Text.Trim())
			};
			DataTable dataTable = coreSystem.Select(query, list.ToArray());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				currentStockDisplay.Text = Convert.ToInt32(dataTable.Rows[0]["quantity"]).ToString();
			}
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Error: " + ex.Message;
			statusLabel.ForeColor = Color.Firebrick;
		}
	}
}