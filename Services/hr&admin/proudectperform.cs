using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class ProudectPerformance
{

	private void ProudectPerformance_Load(object? sender, EventArgs e)
	{
		string pattern = "^\\d{4}-\\d{2}-\\d{2}$";
		if (!Regex.IsMatch(startDate.Text, pattern) || !Regex.IsMatch(endDate.Text, pattern))
		{
			statue.Text = "Please enter valid dates in YYYY-MM-DD format.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		if (string.IsNullOrWhiteSpace(proudectname.Text))
		{
			statue.Text = "Please enter a valid product name.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		if (!DateTime.TryParse(startDate.Text.Trim(), out var result) || !DateTime.TryParse(endDate.Text.Trim(), out var result2))
		{
			statue.Text = "Invalid date values. Use YYYY-MM-DD format.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		if (result > result2)
		{
			statue.Text = "Start date must be before or equal to end date.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT \r\n                                COALESCE(SUM(oi.quantity * oi.unit_price), 0) AS TotalProfit,\r\n                                COALESCE(COUNT(DISTINCT o.id), 0) AS NumberOfOrders,\r\n                                COALESCE(SUM(oi.quantity), 0) AS NumberOfQuantitySold\r\n                           FROM order_items oi\r\n                           INNER JOIN orders o ON o.id = oi.order_id\r\n                           INNER JOIN goods g ON g.id = oi.good_id\r\n                           WHERE g.name = @productName\r\n                             AND date(o.order_date) BETWEEN date(@startDate) AND date(@endDate);";
			SqliteParameter[] parameters = new SqliteParameter[3]
			{
				new SqliteParameter("@productName", proudectname.Text.Trim()),
				new SqliteParameter("@startDate", startDate.Text.Trim()),
				new SqliteParameter("@endDate", endDate.Text.Trim())
			};
			DataTable dataTable = coreSystem.Select(query, parameters);
			if (dataTable.Rows.Count > 0)
			{
				totalProfit.Text = Convert.ToDecimal(dataTable.Rows[0]["TotalProfit"]).ToString("0.00");
				numberoforders.Text = dataTable.Rows[0]["NumberOfOrders"].ToString();
				numberofquntitysold.Text = dataTable.Rows[0]["NumberOfQuantitySold"].ToString();
				statue.Text = "Performance data loaded for product '" + proudectname.Text + "'.";
				statue.ForeColor = Color.FromArgb(0, 102, 102);
			}
			else
			{
				totalProfit.Text = "0";
				numberoforders.Text = "0";
				numberofquntitysold.Text = "0";
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