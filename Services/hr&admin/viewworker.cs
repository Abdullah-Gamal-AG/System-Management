using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class ViewWorker
{

	private void ViewWorker_Load(object? sender, EventArgs e)
	{
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = @"SELECT  w.id AS 'ID',  w.name AS 'Name', w.email AS 'Email',
			-- day profit 
			(SELECT COALESCE(SUM(total_invoice_price), 0) FROM orders 
			WHERE worker_id = w.id AND date(order_date) = date('now')) AS 'Day Profit',
			 -- month profit
			 (SELECT COALESCE(SUM(total_invoice_price), 0) FROM orders 
			 WHERE worker_id = w.id AND strftime('%Y-%m', order_date) = strftime('%Y-%m', 'now')) AS 'Month Profit',
			 -- year profit
			 (SELECT COALESCE(SUM(total_invoice_price), 0) FROM orders
			 WHERE worker_id = w.id AND strftime('%Y', order_date) = strftime('%Y', 'now')) AS 'Year Profit',
			 delete_flag AS 'Deleted'
			 FROM workers w;";
			SqliteParameter[] parameters = new SqliteParameter[0];
			DataTable dataTable = coreSystem.Select(query, parameters);
			stockDataGridView.DataSource = dataTable;
			statusLabel.Text = $"Loaded {dataTable.Rows.Count} worker(s).";
			statusLabel.ForeColor = Color.FromArgb(0, 102, 102);
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Error loading worker data: " + ex.Message;
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
				dataTable.DefaultView.RowFilter = "Convert([ID], 'System.String') LIKE '%" + text + "%'";
			}
			statusLabel.Text = $"Showing {dataTable.DefaultView.Count} worker(s).";
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