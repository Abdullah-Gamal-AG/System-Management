using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Changepassword
{

	private void submit_click(object sender, EventArgs e)
	{
		lblStatus.ForeColor = Color.Crimson;
		if (string.IsNullOrWhiteSpace(txtCurrent.Text) || string.IsNullOrWhiteSpace(txtNew.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
		{
			lblStatus.Text = "Please fill in all fields.";
			return;
		}
		if (txtNew.Text != txtConfirm.Text)
		{
			lblStatus.Text = "New passwords do not match.";
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT password FROM workers WHERE id = @id";
			DataTable dataTable = coreSystem.Select(query, new SqliteParameter[1]
			{
				new SqliteParameter("@id", DataUser.Id)
			});
			if (!Hashing.VerifyPassword(txtCurrent.Text, dataTable.Rows[0][0].ToString()!))
			{
				lblStatus.Text = "Current password is incorrect.";
				return;
			}
			string query2 = "UPDATE workers SET password = @newPass WHERE id = @id";
			coreSystem.Update(query2, new SqliteParameter[2]
			{
				new SqliteParameter("@newPass", Hashing.HashPassword(txtNew.Text)),
				new SqliteParameter("@id", DataUser.Id)
			});
			MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		catch
		{
			lblStatus.Text = "An error occurred. Please try again.";
		}
	}
}