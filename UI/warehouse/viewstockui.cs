using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class ViewStock : Form
{
	private DataGridView stockDataGridView;

	private Label statusLabel;

	private TextBox searchBox;

	private Button refreshButton;

	private Button clearButton;




	public ViewStock()
	{
		Text = "Inventory - View Stock Levels";
		base.Size = new Size(760, 630);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		BackColor = Color.White;
		Font = new Font("Segoe UI", 9f);
		int num = 30;
		int num2 = 20;
		int width = 680;
		Label value = new Label
		{
			Text = "View Inventory",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(num, num2),
			AutoSize = true
		};
		num2 += 50;
		Label value2 = new Label
		{
			Text = "View and filter the current stock levels for all products",
			Location = new Point(num, num2),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		};
		num2 += 38;
		base.Controls.Add(value);
		base.Controls.Add(value2);
		Label value3 = new Label
		{
			Text = "Search Product",
			Location = new Point(num, num2),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		};
		searchBox = new TextBox
		{
			Location = new Point(num, num2 + 22),
			Width = 380,
			PlaceholderText = "Type to filter by product name..."
		};
		clearButton = new Button
		{
			Text = "Clear",
			Location = new Point(num + 400, num2 + 20),
			Width = 100,
			Height = 32,
			BackColor = Color.FromArgb(90, 90, 90),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		};
		clearButton.FlatAppearance.BorderSize = 0;
		refreshButton = new Button
		{
			Text = "Refresh Stock",
			Location = new Point(num + 510, num2 + 20),
			Width = 170,
			Height = 32,
			BackColor = Color.FromArgb(0, 153, 153),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		};
		refreshButton.FlatAppearance.BorderSize = 0;
		base.Controls.Add(value3);
		base.Controls.Add(searchBox);
		base.Controls.Add(clearButton);
		base.Controls.Add(refreshButton);
		num2 += 66;
		stockDataGridView = new DataGridView
		{
			Location = new Point(num, num2),
			Size = new Size(width, 360),
			ReadOnly = true,
			AllowUserToAddRows = false,
			AllowUserToDeleteRows = false,
			SelectionMode = DataGridViewSelectionMode.FullRowSelect,
			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
			BackgroundColor = Color.White,
			BorderStyle = BorderStyle.FixedSingle,
			RowHeadersVisible = false,
			MultiSelect = false,
			AllowUserToResizeRows = false,
			AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
			ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
		};
		stockDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
		stockDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 153, 153);
		stockDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
		stockDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
		stockDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(45, 45, 45);
		stockDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
		stockDataGridView.EnableHeadersVisualStyles = false;
		num2 += 375;
		statusLabel = new Label
		{
			Text = "",
			Location = new Point(num, num2),
			Width = width,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Segoe UI", 8f, FontStyle.Italic)
		};
		num2 += 30;
		refreshButton.Click += delegate(object? s, EventArgs e)
		{
			ViewStock_Load(s, e);
			ApplySearchFilter();
		};
		clearButton.Click += delegate
		{
			searchBox.Clear();
			ApplySearchFilter();
		};
		searchBox.TextChanged += delegate
		{
			ApplySearchFilter();
		};
		base.Controls.Add(stockDataGridView);
		base.Controls.Add(statusLabel);
		base.Load += ViewStock_Load;
		base.Load += delegate
		{
			ApplySearchFilter();
		};
	}
}
