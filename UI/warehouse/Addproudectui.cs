using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class AddProduct : Form
{
	private TextBox productPrice;

	private TextBox productName;

	private TextBox productStock;

	private TextBox productDescription;

	private Label statusLabel;

	public AddProduct()
	{
		Text = "Inventory Management - Add Product";
		base.Size = new Size(420, 520);
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
			Text = "Create New Product",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(margin, num),
			AutoSize = true
		};
		num += 45;
		base.Controls.Add(CreateLabel("Product Name*", num));
		productName = new TextBox
		{
			Location = new Point(margin, num + 20),
			Width = width,
			PlaceholderText = "Enter product name..."
		};
		num += 60;
		base.Controls.Add(CreateLabel("Price ($)*", num));
		productPrice = new TextBox
		{
			Location = new Point(margin, num + 20),
			Width = width,
			PlaceholderText = "0.00"
		};
		num += 60;
		base.Controls.Add(CreateLabel("Initial Stock Quantity", num));
		productStock = new TextBox
		{
			Location = new Point(margin, num + 20),
			Width = width,
			Text = "0"
		};
		num += 60;
		base.Controls.Add(CreateLabel("Description", num));
		productDescription = new TextBox
		{
			Location = new Point(margin, num + 20),
			Width = width,
			Height = 80,
			Multiline = true,
			PlaceholderText = "Optional details..."
		};
		num += 110;
		statusLabel = new Label
		{
			Text = "",
			Location = new Point(margin, num),
			Width = width,
			TextAlign = ContentAlignment.MiddleCenter
		};
		num += 30;
		Button button = new Button
		{
			Text = "Save Product",
			Location = new Point(margin, num),
			Width = width,
			Height = 40,
			BackColor = Color.FromArgb(0, 120, 215),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		button.FlatAppearance.BorderSize = 0;
		base.Controls.AddRange(label, productName, productPrice, productStock, productDescription, statusLabel, button);
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
				ForeColor = Color.DimGray
			};
		}
	}
}
