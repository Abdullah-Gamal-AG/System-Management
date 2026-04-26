using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class AddprouductForm : Form
{
	private TableLayoutPanel layout = new TableLayoutPanel
	{
		Dock = DockStyle.Fill,
		Padding = new Padding(30),
		ColumnCount = 2,
		RowCount = 5,
		BackColor = Color.White
	};

	private TextBox txtId = new TextBox
	{
		Width = 200,
		Font = new Font("Segoe UI", 11f),
		BorderStyle = BorderStyle.FixedSingle,
		Anchor = AnchorStyles.Left
	};

	private TextBox txtDescription = new TextBox
	{
		Width = 200,
		Font = new Font("Segoe UI", 10f),
		BorderStyle = BorderStyle.FixedSingle,
		ReadOnly = true,
		BackColor = Color.FromArgb(240, 240, 240),
		Multiline = true,
		Height = 60,
		Anchor = AnchorStyles.Left,
		ScrollBars = ScrollBars.Vertical
	};

	private Label priceLabel = new Label
	{
		Text = "$0.00",
		Font = new Font("Segoe UI", 14f, FontStyle.Bold),
		ForeColor = Color.ForestGreen,
		Anchor = AnchorStyles.Left,
		AutoSize = true
	};

	private NumericUpDown qty = new NumericUpDown
	{
		Width = 200,
		Font = new Font("Segoe UI", 11f),
		Value = 0m,
		Minimum = 0m,
		TextAlign = HorizontalAlignment.Center,
		Anchor = AnchorStyles.Left,
		ReadOnly = true
	};

	private Button btnSave = new Button
	{
		Text = "Add to Cart",
		Size = new Size(200, 45),
		BackColor = Color.FromArgb(0, 122, 204),
		ForeColor = Color.White,
		FlatStyle = FlatStyle.Flat,
		Font = new Font("Segoe UI", 10f, FontStyle.Bold),
		Cursor = Cursors.Hand,
		Anchor = AnchorStyles.Right
	};




	public AddprouductForm()
	{
		Text = "Add New Product";
		base.Size = new Size(450, 520);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
		layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
		layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
		layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70f));
		layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
		layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
		layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60f));
		layout.Controls.Add(new Label
		{
			Text = "Product ID:",
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Anchor = AnchorStyles.Left,
			AutoSize = true
		}, 0, 0);
		layout.Controls.Add(txtId, 1, 0);
		layout.Controls.Add(new Label
		{
			Text = "Description:",
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Anchor = AnchorStyles.Left,
			AutoSize = true
		}, 0, 1);
		layout.Controls.Add(txtDescription, 1, 1);
		layout.Controls.Add(new Label
		{
			Text = "Quantity:",
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Anchor = AnchorStyles.Left,
			AutoSize = true
		}, 0, 2);
		layout.Controls.Add(qty, 1, 2);
		layout.Controls.Add(new Label
		{
			Text = "Total Price:",
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Anchor = AnchorStyles.Left,
			AutoSize = true
		}, 0, 3);
		layout.Controls.Add(priceLabel, 1, 3);
		btnSave.FlatAppearance.BorderSize = 0;
		layout.Controls.Add(btnSave, 1, 4);
		btnSave.Click += delegate (object? s, EventArgs e)
		{
			btnsaveclick(s!, e);
			Close();
		};
		txtId.TextChanged += delegate
		{
			UpdateMaxQtyAndDescription();
		};
		qty.ValueChanged += updatetotalprice;
		base.Controls.Add(layout);
	}
}
