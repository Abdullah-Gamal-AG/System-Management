using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class WorkerPerformance
{

	private void WorkerPerformance_Load(object? sender, EventArgs e)
	{
		string pattern = "^\\d{4}-\\d{2}-\\d{2}$";
		if (!Regex.IsMatch(startDate.Text, pattern) || !Regex.IsMatch(endDate.Text, pattern))
		{
			statue.Text = "Please enter valid dates in YYYY-MM-DD format.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		if (string.IsNullOrWhiteSpace(workerId.Text))
		{
			statue.Text = "Please enter a valid numeric Worker ID.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT \r\n                            COALESCE(SUM(total_invoice_price), 0) AS TotalProfit,\r\n                            COUNT(*) AS NumberOfOrders\r\n                            FROM orders\r\n                            WHERE worker_id = @workerId\r\n                            AND date(order_date) BETWEEN date(@startDate) AND date(@endDate);";
			SqliteParameter[] parameters = new SqliteParameter[3]
			{
				new SqliteParameter("@workerId", workerId.Text.Trim()),
				new SqliteParameter("@startDate", startDate.Text.Trim()),
				new SqliteParameter("@endDate", endDate.Text.Trim())
			};
			DataTable dataTable = coreSystem.Select(query, parameters);
			if (dataTable.Rows.Count > 0)
			{
				totalProfit.Text = dataTable.Rows[0]["TotalProfit"].ToString();
				numberoforders.Text = dataTable.Rows[0]["NumberOfOrders"].ToString();
				statue.Text = "Performance data loaded for Worker ID " + workerId.Text + ".";
				statue.ForeColor = Color.FromArgb(0, 102, 102);
			}
			else
			{
				totalProfit.Text = "0";
				numberoforders.Text = "0";
				statue.Text = "No performance data found for the specified criteria.";
				statue.ForeColor = Color.FromArgb(255, 140, 0);
			}
		}
		catch (Exception ex)
		{
			statue.Text = "Error loading performance data: " + ex.Message;
			statue.ForeColor = Color.Firebrick;
		}
	}
}