using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class ViewStock
{

	private void ViewStock_Load(object? sender, EventArgs e)
	{
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT name AS 'Product Name', quantity AS 'Current Stock',price AS 'Price',description AS 'Description' FROM goods";
			SqliteParameter[] parameters = new SqliteParameter[0];
			DataTable dataTable = coreSystem.Select(query, parameters);
			stockDataGridView.DataSource = dataTable;
			statusLabel.Text = $"Loaded {dataTable.Rows.Count} product(s).";
			statusLabel.ForeColor = Color.FromArgb(0, 102, 102);
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Error loading stock data: " + ex.Message;
			statusLabel.ForeColor = Color.Firebrick;
		}
	}

	private void ApplySearchFilter()
	{
		if (!(stockDataGridView.DataSource is DataTable dataTable))
		{
			return;
		}
		string value = searchBox.Text.Trim();
		try
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				dataTable.DefaultView.RowFilter = string.Empty;
			}
			else
			{
				string text = EscapeFilterValue(value);
				dataTable.DefaultView.RowFilter = "[Product Name] LIKE '%" + text + "%'";
			}
			statusLabel.Text = $"Showing {dataTable.DefaultView.Count} product(s).";
			statusLabel.ForeColor = Color.FromArgb(0, 102, 102);
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Filter error: " + ex.Message;
			statusLabel.ForeColor = Color.Firebrick;
		}
	}

	private static string EscapeFilterValue(string value)
	{
		return value.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]")
			.Replace("*", "[*]");
	}
}