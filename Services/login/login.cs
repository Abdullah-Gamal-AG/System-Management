using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Login
{

	private async void submitbutton(object sender, EventArgs e)
	{
		string uId = userid?.Text?.Trim()!;
		string pass = password?.Text?.Trim()!;
		_ = new string[2] { uId, pass };
		if (string.IsNullOrEmpty(uId) || string.IsNullOrEmpty(pass))
		{
			Label? label = statusLabel;
			if (label != null)
			{
				label.Text = "Please enter both fields.";
			}
			Label? label2 = statusLabel;
			if (label2 != null)
			{
				label2.ForeColor = Color.Red;
			}
			return;
		}
		try
		{
			string result = await Task.Run(delegate
			{
				string query = "SELECT password, delete_flag deleted FROM workers WHERE id = @userId";
				DataTable dataTable = CoreSystem.Select(query, new SqliteParameter[1]
				{
					new SqliteParameter("@userId", uId)
				});

				if (dataTable.Rows.Count == 0 && Convert.ToInt32(dataTable.Rows[0]["deleted"]) == 1)
				{
					return "User not found.";
				}
				string hashedPassword = dataTable.Rows[0]["password"]?.ToString() ?? "";
				string text = Hashing.HashPassword(pass);
				return Hashing.VerifyPassword(pass, hashedPassword) ? "Success" : "Incorrect password.";
			});
			if (result == "Success")
			{
				Label? label3 = statusLabel;
				if (label3 != null)
				{
					label3.Text = "Login successful!";
				}
				Label? label4 = statusLabel;
				if (label4 != null)
				{
					label4.ForeColor = Color.Green;
				}
				DataUser.Id = uId;
				DataUser.Name = CoreSystem.Select("SELECT name FROM workers WHERE id = @userId", new SqliteParameter[1]
				{
					new SqliteParameter("@userId", uId)
				}).Rows[0]["name"]?.ToString() ?? "";
				DataUser.Email = CoreSystem.Select("SELECT email FROM workers WHERE id = @userId", new SqliteParameter[1]
				{
					new SqliteParameter("@userId", uId)
				}).Rows[0]["email"]?.ToString() ?? "";
				DataUser.Password = CoreSystem.Select("SELECT password FROM workers WHERE id = @userId", new SqliteParameter[1]
				{
					new SqliteParameter("@userId", uId)
				}).Rows[0]["password"]?.ToString() ?? "";
				DataUser.IsLoggedIn = true;
				if (DataUser.Id == "1000")
				{
					Form1? mainForm = FindForm() as Form1;
					Root rootControl = new Root(mainForm);
					rootControl.ShowDialog();
				}
				else if (DataUser.Id[0] == '1')
				{
					if (FindForm() is Form1 mainForm2)
					{
						mainForm2.ShowMainSales();
					}
				}
				else if (DataUser.Id[0] == '2')
				{
					if (FindForm() is Form1 mainForm3)
					{
						mainForm3.ShowWarehouse();
					}
				}
				else if (DataUser.Id[0] == '3' && FindForm() is Form1 mainForm4)
				{
					mainForm4.ShowAdmin();
				}
			}
			else
			{
				Label? label5 = statusLabel;
				if (label5 != null)
				{
					label5.Text = result;
				}
				Label? label6 = statusLabel;
				if (label6 != null)
				{
					label6.ForeColor = Color.Red;
				}
			}
		}
		catch (Exception ex)
		{
			Label? label7 = statusLabel;
			if (label7 != null)
			{
				label7.Text = "An error occurred: " + ex.Message;
			}
			Label? label8 = statusLabel;
			if (label8 != null)
			{
				label8.ForeColor = Color.Red;
			}
		}
	}
}