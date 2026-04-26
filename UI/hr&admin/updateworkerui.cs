using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdateWorker : Form
{
	private TextBox id;

	private TextBox oldworkername;

	private TextBox oldworkeremail;

	private TextBox newworkername;

	private TextBox newworkeremail;

	private Label statue;

	private Button submitButton;

	private void submitbutton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(id.Text))
		{
			statue.Text = "Please enter the worker ID to update.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		if (string.IsNullOrWhiteSpace(newworkername.Text) && string.IsNullOrWhiteSpace(newworkeremail.Text))
		{
			statue.Text = "Please enter at least one new value to update.";
			statue.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "SELECT * FROM workers WHERE id = @id";
			SqliteParameter[] parameters = new SqliteParameter[1]
			{
				new SqliteParameter("@id", id.Text.Trim())
			};
			DataTable dataTable = coreSystem.Select(query, parameters);
			if (dataTable.Rows.Count == 0)
			{
				statue.Text = "No worker found with the provided ID.";
				statue.ForeColor = Color.Firebrick;
				return;
			}
			string text = "UPDATE workers SET ";
			List<SqliteParameter> list = new List<SqliteParameter>();
			if (!string.IsNullOrWhiteSpace(newworkername.Text))
			{
				text += "name = @name, ";
				list.Add(new SqliteParameter("@name", newworkername.Text.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(newworkeremail.Text))
			{
				text += "email = @email, ";
				list.Add(new SqliteParameter("@email", newworkeremail.Text.Trim()));
			}
			text = text.TrimEnd(new char[2] { ',', ' ' }) + " WHERE id = @id";
			list.Add(new SqliteParameter("@id", id.Text.Trim()));
			coreSystem.Update(text, list.ToArray());
			MessageBox.Show("Worker with ID: " + id.Text + " updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		catch (Exception ex)
		{
			statue.Text = "Error: " + ex.Message;
			statue.ForeColor = Color.Firebrick;
		}
	}


	public UpdateWorker()
	{
		Text = "Human Resources - Update Worker";
		base.Size = new Size(760, 540);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		BackColor = Color.White;
		Font = new Font("Segoe UI", 9f);
		int margin = 30;
		int num = 20;
		int num2 = 320;
		Label label = new Label
		{
			Text = "Update Worker",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(margin, num),
			AutoSize = true
		};
		num += 40;
		Label label2 = new Label
		{
			Text = "Search by worker ID and update basic details.",
			Font = new Font("Segoe UI", 9f, FontStyle.Bold),
			ForeColor = Color.DimGray,
			Location = new Point(margin, num),
			AutoSize = true
		};
		num += 35;
		Panel panel = new Panel
		{
			Location = new Point(margin, num),
			Size = new Size(680, 4),
			BackColor = Color.FromArgb(34, 139, 34)
		};
		num += 20;
		base.Controls.Add(CreateLabel("Worker ID*", num));
		id = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = 680,
			Height = 36,
			Multiline = true,
			PlaceholderText = "Enter worker ID..."
		};
		num += 72;
		int x = margin;
		int x2 = margin + num2 + 40;
		int num3 = num;
		Label label3 = new Label
		{
			Text = "Current Details",
			Font = new Font("Segoe UI", 11f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(x, num3),
			AutoSize = true
		};
		Label label4 = new Label
		{
			Text = "New Details",
			Font = new Font("Segoe UI", 11f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(x2, num3),
			AutoSize = true
		};
		int num4 = num3 + 32;
		int num5 = num3 + 32;
		base.Controls.Add(new Label
		{
			Text = "Current Worker Name",
			Location = new Point(x, num4),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		});
		oldworkername = new TextBox
		{
			Location = new Point(x, num4 + 22),
			Width = num2,
			ReadOnly = true,
			BackColor = Color.FromArgb(245, 245, 245),
			PlaceholderText = "Current name..."
		};
		num4 += 65;
		base.Controls.Add(new Label
		{
			Text = "Current Email",
			Location = new Point(x, num4),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		});
		oldworkeremail = new TextBox
		{
			Location = new Point(x, num4 + 22),
			Width = num2,
			ReadOnly = true,
			BackColor = Color.FromArgb(245, 245, 245),
			PlaceholderText = "Current email..."
		};
		num4 += 65;
		base.Controls.Add(new Label
		{
			Text = "New Worker Name*",
			Location = new Point(x2, num5),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		});
		newworkername = new TextBox
		{
			Location = new Point(x2, num5 + 22),
			Width = num2,
			PlaceholderText = "Enter updated worker name..."
		};
		num5 += 65;
		base.Controls.Add(new Label
		{
			Text = "New Email Address*",
			Location = new Point(x2, num5),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		});
		newworkeremail = new TextBox
		{
			Location = new Point(x2, num5 + 22),
			Width = num2,
			PlaceholderText = "Enter updated email..."
		};
		num5 += 65;
		num = Math.Max(num4, num5) + 35;
		statue = new Label
		{
			Text = string.Empty,
			Location = new Point(margin, num),
			Width = 680,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Segoe UI", 8f, FontStyle.Italic)
		};
		num += 30;
		submitButton = new Button
		{
			Text = "Update Worker",
			Location = new Point(margin, num),
			Width = 680,
			Height = 45,
			BackColor = Color.FromArgb(34, 139, 34),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		submitButton.FlatAppearance.BorderSize = 0;
		base.Controls.AddRange(label, label2, panel, label3, label4, id, oldworkername, oldworkeremail, newworkername, newworkeremail, statue, submitButton);
		submitButton.Click += delegate (object? s, EventArgs e)
		{
			submitbutton_Click(s!, e);
		};
		id.TextChanged += delegate (object? s, EventArgs e)
		{
			updateworker_Load(s, e);
		};
		Label CreateLabel(string text, int y)
		{
			return new Label
			{
				Text = text,
				Location = new Point(margin, y),
				AutoSize = true,
				ForeColor = Color.DimGray,
				Font = new Font("Segoe UI", 9f, FontStyle.Bold)
			};
		}
	}
}
