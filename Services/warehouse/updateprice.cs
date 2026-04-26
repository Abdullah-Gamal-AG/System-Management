using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdatePrice
{

	private void UpdateOldPrice()
	{
		try
		{
			if (!string.IsNullOrWhiteSpace(productNameInput.Text))
			{
				CoreSystem coreSystem = new CoreSystem();
				string query = "SELECT price FROM goods WHERE name = @name";
				List<SqliteParameter> list = new List<SqliteParameter>
				{
					new SqliteParameter("@name", productNameInput.Text.Trim())
				};
				DataTable dataTable = coreSystem.Select(query, list.ToArray());
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					currentPriceDisplay.Text = Convert.ToDouble(dataTable.Rows[0]["price"]).ToString("F2");
					currentPriceDisplay.ForeColor = Color.Black;
				}
				else
				{
					currentPriceDisplay.Text = "Product not found";
					currentPriceDisplay.ForeColor = Color.Gray;
				}
			}
		}
		catch
		{
			currentPriceDisplay.Text = "---";
		}
	}
}