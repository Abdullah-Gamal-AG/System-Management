using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Profile
{

	private int GetSalesData(string period)
	{
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string text = ((period == "day") ? "yyyy-MM-dd" : "yyyy-MM");
			string text2 = ((period == "day") ? "date(order_date) = @val" : "strftime('%Y-%m', order_date) = @val");
			string query = "SELECT SUM(total_invoice_price) FROM orders WHERE worker_id = @workerId AND " + text2;
			SqliteParameter[] parameters = new SqliteParameter[2]
			{
				new SqliteParameter("@workerId", DataUser.Id),
				new SqliteParameter("@val", DateTime.Now.ToString(text))
			};
			DataTable dataTable = coreSystem.Select(query, parameters);
			if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
			{
				return Convert.ToInt32(dataTable.Rows[0][0]);
			}
			return 0;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return 0;
		}
	}
}