using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdatePrice : Form
{
	private TextBox productNameInput;

	private TextBox currentPriceDisplay;

	private NumericUpDown productNewPrice;

	private Label statusLabel;

	private void submitButton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(productNameInput.Text))
		{
			statusLabel.Text = "Error: Please enter a product name.";
			statusLabel.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "UPDATE goods SET price = @price WHERE name = @name";
			List<SqliteParameter> list = new List<SqliteParameter>
			{
				new SqliteParameter("@price", productNewPrice.Value),
				new SqliteParameter("@name", productNameInput.Text.Trim())
			};
			coreSystem.Update(query, list.ToArray());
			MessageBox.Show("Price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Database Error: " + ex.Message;
			statusLabel.ForeColor = Color.Firebrick;
		}
	}


	public UpdatePrice()
	{
		Text = "Inventory - Update Unit Price";
		base.Size = new Size(420, 500);
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
			Text = "Price Adjustment",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(margin, num),
			AutoSize = true
		};
		num += 50;
		base.Controls.Add(CreateLabel("Product Name", num));
		productNameInput = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = width,
			PlaceholderText = "Search for a product..."
		};
		num += 65;
		base.Controls.Add(CreateLabel("Current Price (Read-only)", num));
		currentPriceDisplay = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = width,
			ReadOnly = true,
			BackColor = Color.FromArgb(245, 245, 245),
			PlaceholderText = "Waiting for product selection..."
		};
		num += 65;
		base.Controls.Add(CreateLabel("New Unit Price ($)*", num));
		productNewPrice = new NumericUpDown
		{
			Location = new Point(margin, num + 22),
			Width = width,
			Minimum = 0m,
			Maximum = 999999m,
			DecimalPlaces = 2,
			Font = new Font("Segoe UI", 10f)
		};
		num += 75;
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
			Text = "Save New Price",
			Location = new Point(margin, num),
			Width = width,
			Height = 45,
			BackColor = Color.FromArgb(202, 81, 0),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		button.FlatAppearance.BorderSize = 0;
		base.Controls.AddRange(label, productNameInput, currentPriceDisplay, productNewPrice, statusLabel, button);
		productNameInput.TextChanged += delegate
		{
			UpdateOldPrice();
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
