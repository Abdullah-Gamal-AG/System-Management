using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Mainsales
{

	private async void submitbuttonclick(object sender, EventArgs e)
	{
		if (Invoice.invoiceDataTable.Rows.Count == 0)
		{
			MessageBox.Show("Please add at least one product to the invoice before submitting.", "Empty Invoice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (customersNameTextBox.Text == "" || phoneNumberTextBox.Text == "")
		{
			MessageBox.Show("Please enter customer details before submitting.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		submitButton.Enabled = false;
		submitStatusLabel.Text = "Saving to database... Please wait.";
		try
		{
			string sql = "SELECT * FROM 'customers' WHERE name = @name AND phonenumber = @phone";
			CoreSystem core = new CoreSystem();
			DataTable rows = core.Select(sql, new SqliteParameter[2]
			{
				new SqliteParameter("@name", customersNameTextBox.Text),
				new SqliteParameter("@phone", phoneNumberTextBox.Text)
			});
			if (rows.Rows.Count == 0)
			{
				List<SqliteParameter> parametersList = new List<SqliteParameter>();
				if (int.TryParse(phoneNumberTextBox.Text, out var _))
				{
					sql = "INSERT INTO 'customers' (name, phonenumber) VALUES (@name, @phone)";
					parametersList.Add(new SqliteParameter("@name", customersNameTextBox.Text));
					parametersList.Add(new SqliteParameter("@phone", phoneNumberTextBox.Text));
				}
				else
				{
					sql = "INSERT INTO 'customers' (name) VALUES (@name)";
					parametersList.Add(new SqliteParameter("@name", customersNameTextBox.Text));
				}
				await core.InsertAsync(sql, parametersList.ToArray());
			}
			sql = "INSERT INTO 'orders'(worker_id,customer_id,total_invoice_price,order_date)\r\n                    VALUES(@workerId,\r\n                    (SELECT id FROM customers WHERE name = @customerName AND phonenumber = @customerPhone), @totalPrice, @orderDate)";
			SqliteParameter[] parameters = new SqliteParameter[5]
			{
				new SqliteParameter("@workerId", DataUser.Id),
				new SqliteParameter("@customerName", customersNameTextBox.Text),
				new SqliteParameter("@customerPhone", phoneNumberTextBox.Text),
				new SqliteParameter("@totalPrice", Invoice.total),
				new SqliteParameter("@orderDate", DateTime.Now)
			};
			await core.InsertAsync(sql, parameters);
			string idSql = "SELECT last_insert_rowid();";
			int orderId = Convert.ToInt32((await core.SelectAsync(idSql, new SqliteParameter[0])).Rows[0][0]);
			for (int i = 0; i < Invoice.invoiceDataTable.Rows.Count; i++)
			{
				sql = "INSERT INTO 'order_items' (order_id, good_id, quantity,unit_price) VALUES (@orderId, (SELECT id FROM goods WHERE name = @productName), @quantity, @unitPrice)";
				parameters = new SqliteParameter[4]
				{
					new SqliteParameter("@orderId", orderId),
					new SqliteParameter("@productName", Invoice.invoiceDataTable.Rows[i]["Product"].ToString()),
					new SqliteParameter("@quantity", Invoice.invoiceDataTable.Rows[i]["Quantity"].ToString()),
					new SqliteParameter("@unitPrice", Invoice.invoiceDataTable.Rows[i]["Price"].ToString())
				};
				await core.InsertAsync(sql, parameters);
			}
			Invoice.orderid = orderId;
			using (Finalizeinvoice form = new Finalizeinvoice())
			{
				form.ShowDialog();
			}
			Invoice.invoiceDataTable.Clear();
			Invoice.total = default(decimal);
			customersNameTextBox.Text = "";
			phoneNumberTextBox.Text = "";
			UpdateInvoice();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("An error occurred while submitting the order: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			submitButton.Enabled = true;
			submitStatusLabel.Text = "Ready for submission";
		}
	}
}