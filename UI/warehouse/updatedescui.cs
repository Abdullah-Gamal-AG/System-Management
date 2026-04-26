using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdateDesc : Form
{
	private TextBox productName;

	private TextBox currentDescDisplay;

	private TextBox newDescInput;

	private Label statusLabel;

	private void submitButton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(productName.Text))
		{
			statusLabel.Text = "Please enter a product name.";
			statusLabel.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "UPDATE goods SET description = @desc WHERE name = @name";
			List<SqliteParameter> list = new List<SqliteParameter>
			{
				new SqliteParameter("@desc", string.IsNullOrWhiteSpace(newDescInput.Text) ? ((IConvertible)DBNull.Value) : ((IConvertible)newDescInput.Text.Trim())),
				new SqliteParameter("@name", productName.Text.Trim())
			};
			coreSystem.Update(query, list.ToArray());
			MessageBox.Show("Description updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Error: " + ex.Message;
			statusLabel.ForeColor = Color.Firebrick;
		}
	}


	public UpdateDesc()
	{
		Text = "Inventory - Update Product Description";
		base.Size = new Size(420, 580);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		BackColor = Color.White;
		Font = new Font("Segoe UI", 9f);
		int margin = 30;
		int num = 20;
		int width = 340;
		Label label = new Label
		{
			Text = "Edit Description",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(margin, num),
			AutoSize = true
		};
		num += 50;
		base.Controls.Add(CreateLabel("Product Name", num));
		productName = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = width,
			PlaceholderText = "Type product name to search..."
		};
		num += 65;
		base.Controls.Add(CreateLabel("Current Description", num));
		currentDescDisplay = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = width,
			Height = 80,
			Multiline = true,
			ReadOnly = true,
			BackColor = Color.FromArgb(245, 245, 245),
			PlaceholderText = "Waiting for product selection...",
			ScrollBars = ScrollBars.Vertical
		};
		num += 115;
		base.Controls.Add(CreateLabel("New Description*", num));
		newDescInput = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = width,
			Height = 80,
			Multiline = true,
			PlaceholderText = "Enter the updated description here...",
			ScrollBars = ScrollBars.Vertical
		};
		num += 115;
		statusLabel = new Label
		{
			Text = "",
			Location = new Point(margin, num),
			Width = width,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Segoe UI", 8f, FontStyle.Italic)
		};
		num += 30;
		Button button = new Button
		{
			Text = "Update Description",
			Location = new Point(margin, num),
			Width = width,
			Height = 45,
			BackColor = Color.FromArgb(104, 33, 122),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		button.FlatAppearance.BorderSize = 0;
		base.Controls.AddRange(label, productName, currentDescDisplay, newDescInput, statusLabel, button);
		productName.TextChanged += delegate
		{
			LoadCurrentDescription();
		};
		button.Click += delegate (object? s, EventArgs e)
		{
			submitButton_Click(s!, e);
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
