using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Mainsales : UserControl
{
	private DataGridView invoiceGrid;

	private Label totalAmountLabel;

	private Panel headerpanel;

	private Button submitButton;

	private Label submitStatusLabel;

	private TextBox customersNameTextBox;

	private TextBox phoneNumberTextBox;


	public void UpdateInvoice()
	{
		totalAmountLabel.Text = $"Total: ${Invoice.total:F2}";
	}

	public Mainsales()
	{
		BackColor = Color.White;
		Dock = DockStyle.Fill;
		headerpanel = new Panel
		{
			Size = new Size(base.Width, 80),
			Dock = DockStyle.Top,
			BackColor = Color.FromArgb(45, 45, 48)
		};
		Label value = new Label
		{
			Text = "Sales Management System",
			ForeColor = Color.White,
			Font = new Font("Segoe UI", 14f, FontStyle.Bold),
			Location = new Point(20, 25),
			AutoSize = true
		};
		Button profileButton = new Button
		{
			Text = "",
			Size = new Size(50, 50),
			Location = new Point(headerpanel.Width - 100, (headerpanel.Height - 50) / 2),
			Anchor = (AnchorStyles.Top | AnchorStyles.Right),
			Cursor = Cursors.Hand,
			FlatStyle = FlatStyle.Flat,
			BackgroundImageLayout = ImageLayout.Stretch,
			BackColor = Color.Gray
		};
		try
		{
			profileButton.FlatAppearance.BorderSize = 0;
			profileButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
			profileButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddEllipse(0, 0, profileButton.Width, profileButton.Height);
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "profile.png");
			profileButton.BackgroundImage = Image.FromFile(imagePath);
			profileButton.Region = new Region(graphicsPath);
			headerpanel.Controls.Add(value);
			headerpanel.Controls.Add(profileButton);
			profileButton.BringToFront();
			base.Controls.Add(headerpanel);
		}
		catch
		{
			Label label = new Label
			{
				Text = "\ud83d\udc64",
				Font = new Font("Segoe UI Emoji", 24f),
				ForeColor = Color.White,
				Location = profileButton.Location,
				Size = profileButton.Size,
				TextAlign = ContentAlignment.MiddleCenter,
				Cursor = Cursors.Hand
			};
			headerpanel.Controls.Add(label);
			label.BringToFront();
			base.Controls.Add(headerpanel);
		}
		Label label2 = new Label
		{
			Text = "Product Selection",
			Font = new Font("Segoe UI", 12f, FontStyle.Bold),
			Location = new Point(30, 100),
			AutoSize = true
		};
		Button button = new Button
		{
			Text = "+",
			BackColor = Color.FromArgb(0, 122, 204),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			Location = new Point(30, 135),
			Size = new Size(150, 45),
			Cursor = Cursors.Hand,
			TextAlign = ContentAlignment.MiddleCenter
		};
		button.TextAlign = ContentAlignment.MiddleCenter;
		button.Padding = new Padding(0);
		button.FlatAppearance.BorderSize = 0;
		button.UseCompatibleTextRendering = true;
		Label label3 = new Label
		{
			Text = "Customer Details",
			Font = new Font("Segoe UI", 12f, FontStyle.Bold),
			Location = new Point(30, 210),
			AutoSize = true
		};
		customersNameTextBox = new TextBox
		{
			Location = new Point(30, 250),
			Size = new Size(250, 35),
			Font = new Font("Segoe UI", 11f),
			BorderStyle = BorderStyle.FixedSingle,
			ForeColor = Color.Gray,
			PlaceholderText = "Customer Name..."
		};
		phoneNumberTextBox = new TextBox
		{
			Location = new Point(30, 295),
			Size = new Size(250, 35),
			Font = new Font("Segoe UI", 11f),
			BorderStyle = BorderStyle.FixedSingle,
			ForeColor = Color.Gray,
			PlaceholderText = "Phone Number..."
		};
		invoiceGrid = new DataGridView
		{
			DataSource = Invoice.invoiceDataTable,
			Location = new Point(base.Width - 550, 100),
			Size = new Size(520, 400),
			Anchor = (AnchorStyles.Top | AnchorStyles.Right),
			BackgroundColor = Color.White,
			BorderStyle = BorderStyle.None,
			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
			RowHeadersVisible = false,
			AllowUserToAddRows = false,
			ReadOnly = true,
			SelectionMode = DataGridViewSelectionMode.FullRowSelect,
			GridColor = Color.FromArgb(240, 240, 240)
		};
		invoiceGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
		invoiceGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
		invoiceGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
		invoiceGrid.EnableHeadersVisualStyles = false;
		invoiceGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
		invoiceGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
		DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn
		{
			Name = "Remove",
			HeaderText = "",
			Text = "X",
			UseColumnTextForButtonValue = true,
			Width = 30,
			FlatStyle = FlatStyle.Flat
		};
		dataGridViewButtonColumn.DefaultCellStyle.ForeColor = Color.Red;
		dataGridViewButtonColumn.DefaultCellStyle.SelectionForeColor = Color.Red;
		dataGridViewButtonColumn.DefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
		invoiceGrid.Columns.Add(dataGridViewButtonColumn);
		invoiceGrid.CellContentClick += delegate (object? s, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == invoiceGrid.Columns?["Remove"]?.Index && e.RowIndex >= 0)
			{
				decimal num = Convert.ToDecimal(Invoice.invoiceDataTable.Rows[e.RowIndex]["Total"]);
				Invoice.total -= num;
				Invoice.invoiceDataTable.Rows.RemoveAt(e.RowIndex);
				UpdateInvoice();
			}
		};
		totalAmountLabel = new Label
		{
			Text = "Total: $0.00",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(34, 139, 34),
			Location = new Point(base.Width - 550, 530),
			Size = new Size(520, 50),
			TextAlign = ContentAlignment.MiddleRight,
			Anchor = (AnchorStyles.Top | AnchorStyles.Right)
		};
		submitButton = new Button
		{
			Text = "Confirm Order",
			BackColor = Color.FromArgb(34, 139, 34),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Font = new Font("Segoe UI", 11f, FontStyle.Bold),
			Size = new Size(180, 50),
			Location = new Point(base.ClientSize.Width - 210, base.ClientSize.Height - 80),
			Anchor = (AnchorStyles.Bottom | AnchorStyles.Right),
			Cursor = Cursors.Hand
		};
		submitButton.FlatAppearance.BorderSize = 0;
		submitStatusLabel = new Label
		{
			Text = "Ready for submission",
			ForeColor = Color.Gray,
			Font = new Font("Segoe UI", 10f, FontStyle.Italic),
			Location = new Point(30, base.Height - 60),
			Anchor = (AnchorStyles.Bottom | AnchorStyles.Left),
			AutoSize = true
		};
		button.Click += delegate
		{
			using (AddprouductForm addprouductForm = new AddprouductForm())
			{
				addprouductForm.ShowDialog();
			}
			UpdateInvoice();
		};
		profileButton.Click += delegate
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			ToolStripItem toolStripItem = contextMenuStrip.Items.Add("View Profile");
			ToolStripItem toolStripItem2 = contextMenuStrip.Items.Add("Logout");
			toolStripItem.Click += delegate
			{
				if (FindForm() is Form1 form)
				{
					form.ShowProfile();
				}
			};
			toolStripItem2.Click += delegate
			{
				if (FindForm() is Form1 form)
				{
					form.ShowLogin();
					DataUser.Clear();
				}
			};
			contextMenuStrip.Show(profileButton, new Point(0, profileButton.Height));
		};
		submitButton.Click += async delegate (object? s, EventArgs e)
		{
			submitbuttonclick(s!, e);
		};
		base.Controls.AddRange(headerpanel, label2, button, label3, customersNameTextBox, phoneNumberTextBox, invoiceGrid, totalAmountLabel, submitButton, submitStatusLabel);
	}

	private TextBox CreateStyledTextBox(string placeholder, int x, int y)
	{
		return new TextBox
		{
			Location = new Point(x, y),
			Size = new Size(250, 35),
			Font = new Font("Segoe UI", 11f),
			BorderStyle = BorderStyle.FixedSingle,
			ForeColor = Color.Gray,
			Text = placeholder
		};
	}
}
