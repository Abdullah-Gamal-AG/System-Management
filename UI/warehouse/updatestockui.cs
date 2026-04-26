using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class UpdateStock : Form
{
	private TextBox productName;

	private TextBox currentStockDisplay;

	private NumericUpDown productNewStock;

	private Label statusLabel;

	private void submitButton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(productName.Text))
		{
			statusLabel.Text = "Please select a product first.";
			statusLabel.ForeColor = Color.Firebrick;
			return;
		}
		try
		{
			CoreSystem coreSystem = new CoreSystem();
			string query = "UPDATE goods SET quantity = @stock WHERE name = @name";
			List<SqliteParameter> list = new List<SqliteParameter>
			{
				new SqliteParameter("@stock", (int)productNewStock.Value),
				new SqliteParameter("@name", productName.Text.Trim())
			};
			coreSystem.Update(query, list.ToArray());
			MessageBox.Show("Stock for '" + productName.Text + "' updated successfully!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Error: " + ex.Message;
			statusLabel.ForeColor = Color.Firebrick;
		}
	}


	public UpdateStock()
	{
		Text = "Inventory - Update Stock Level";
		base.Size = new Size(420, 480);
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
			Text = "Update Inventory",
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
			PlaceholderText = "Type product name exactly..."
		};
		num += 65;
		base.Controls.Add(CreateLabel("Current Stock (Reference)", num));
		currentStockDisplay = new TextBox
		{
			Location = new Point(margin, num + 22),
			Width = width,
			ReadOnly = true,
			BackColor = Color.FromArgb(245, 245, 245),
			PlaceholderText = "Load a product to see current stock"
		};
		num += 65;
		base.Controls.Add(CreateLabel("New Stock Quantity*", num));
		productNewStock = new NumericUpDown
		{
			Location = new Point(margin, num + 22),
			Width = width,
			Minimum = 0m,
			Maximum = 999999m,
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
			Text = "Update Stock Level",
			Location = new Point(margin, num),
			Width = width,
			Height = 45,
			BackColor = Color.FromArgb(34, 139, 34),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		button.FlatAppearance.BorderSize = 0;
		base.Controls.AddRange(label, productName, currentStockDisplay, productNewStock, statusLabel, button);
		button.Click += delegate (object? s, EventArgs e)
		{
			submitButton_Click(s!, e);
		};
		productName.TextChanged += delegate (object? s, EventArgs e)
		{
			UpdateOldStock(s!, e);
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
