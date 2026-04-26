using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class AddWorker : Form
{
	private void submitButton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(workername.Text) || string.IsNullOrWhiteSpace(workeremail.Text) || role.SelectedItem == null)
		{
			statue.Text = "Please fill in all required fields.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			int num;
			string query;
			if ((string)role.SelectedItem == "Sales")
			{
				query = "SELECT id FROM workers WHERE id BETWEEN 1001 AND 1999 ORDER BY id DESC LIMIT 1";
				if (coreSystem.Select(query, new SqliteParameter[0]).Rows.Count == 0)
				{
					num = 1001;
				}
				else
				{
					num = Convert.ToInt32(coreSystem.Select(query, new SqliteParameter[0]).Rows[0]["id"]) + 1;
				}
			}
			else if ((string)role.SelectedItem == "warehouse")
			{
				query = "SELECT id FROM workers WHERE id BETWEEN 2001 AND 2999 ORDER BY id DESC LIMIT 1";
				if (coreSystem.Select(query, new SqliteParameter[0]).Rows.Count == 0)
				{
					num = 2001;
				}
				else
				{
					num = Convert.ToInt32(coreSystem.Select(query, new SqliteParameter[0]).Rows[0]["id"]) + 1;
				}
			}
			else
			{
				query = "SELECT id FROM workers WHERE id BETWEEN 3001 AND 3999 ORDER BY id DESC LIMIT 1";
				if (coreSystem.Select(query, new SqliteParameter[0]).Rows.Count == 0)
				{
					num = 3001;
				}
				else
				{
					num = Convert.ToInt32(coreSystem.Select(query, new SqliteParameter[0]).Rows[0]["id"]) + 1;
				}
			}
			query = "INSERT INTO workers (id, name, email, password) VALUES (@id, @name, @email, @password)";
			SqliteParameter[] parameters = new SqliteParameter[4]
			{
				new SqliteParameter("@id", num),
				new SqliteParameter("@name", workername.Text.Trim()),
				new SqliteParameter("@email", workeremail.Text.Trim()),
				new SqliteParameter("@password", Hashing.HashPassword("0000"))
			};
			coreSystem.Insert(query, parameters);
			MessageBox.Show($"Worker '{workername.Text}' added successfully with ID: {num}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		catch (Exception ex)
		{
			statue.Text = "Error: " + ex.Message;
			statue.ForeColor = Color.Firebrick;
		}
	}
}
