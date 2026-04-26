using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class AddprouductForm
{

	private void UpdateMaxQtyAndDescription()
	{
		CoreSystem coreSystem = new CoreSystem();
		string query = "SELECT * FROM 'goods' WHERE name = @idtext";
		DataTable dataTable = coreSystem.Select(query, new SqliteParameter[1]
		{
			new SqliteParameter("@idtext", txtId.Text)
		});
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			if (dataTable.Rows[0]["description"]?.ToString() == "")
			{
				txtDescription.Text = "No description available.";
			}
			else
			{
				txtDescription.Text = dataTable.Rows[0]["description"]?.ToString();
			}
			EnumerableRowCollection<DataRow> source = from r in Invoice.invoiceDataTable.AsEnumerable()
													  where r.Field<string>("Product") == txtId.Text
													  select r;
			int num = (source.Any() ? source.Sum((DataRow r) => r.Field<int>("Quantity")) : 0);
			int num2 = Convert.ToInt32(dataTable.Rows[0]["quantity"]);
			qty.Maximum = Math.Max(0, num2 - num);
		}
		else
		{
			txtDescription.Text = "";
			qty.Maximum = 0m;
		}
	}

	private void updatetotalprice(object? sender, EventArgs e)
	{
		CoreSystem coreSystem = new CoreSystem();
		string query = "SELECT price FROM 'goods' WHERE name = @idtext";
		DataTable dataTable = coreSystem.Select(query, new SqliteParameter[1]
		{
			new SqliteParameter("@idtext", txtId.Text)
		});
		if (dataTable.Rows.Count > 0)
		{
			decimal num = Convert.ToDecimal(dataTable.Rows[0]["price"]);
			priceLabel.Text = $"${num * qty.Value:F2}";
		}
		else
		{
			priceLabel.Text = "$0.00";
		}
	}

	private void btnsaveclick(object sender, EventArgs e)
	{
		if (txtId.Text == "" || qty.Value == 0m)
		{
			MessageBox.Show("Please enter a valid Product ID and Quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string query = "SELECT * FROM 'goods' WHERE name = @idtext";
		CoreSystem coreSystem = new CoreSystem();
		DataTable dataTable = coreSystem.Select(query, new SqliteParameter[1]
		{
			new SqliteParameter("@idtext", txtId.Text)
		});
		decimal num = ((dataTable.Rows.Count == 0) ? 0m : Convert.ToDecimal(dataTable.Rows[0]["price"]));
		decimal num2 = num * qty.Value;
		Invoice.total += num2;
		Invoice.invoiceDataTable.Rows.Add(txtId.Text, (int)qty.Value, num, num2);
	}
}