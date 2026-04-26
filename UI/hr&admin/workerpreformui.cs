using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class WorkerPerformance : Form
{
	private TextBox workerId;

	private TextBox startDate;

	private TextBox endDate;

	private Label statue;

	private TextBox totalProfit;

	private TextBox numberoforders;


	public WorkerPerformance()
	{
		Text = "HR&ADMIN - Worker Performance";
		base.Size = new Size(760, 500);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		BackColor = Color.White;
		Font = new Font("Segoe UI", 9f);
		int num = 30;
		int num2 = 20;
		Label label = new Label
		{
			Text = "Worker Performance",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(num, num2),
			AutoSize = true
		};
		num2 += 50;
		Label label2 = new Label
		{
			Text = "Choose a worker and date range to view profit performance.",
			Location = new Point(num, num2),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		};
		num2 += 35;
		Panel panel = new Panel
		{
			Location = new Point(num, num2),
			Size = new Size(680, 4),
			BackColor = Color.FromArgb(255, 140, 0)
		};
		num2 += 20;
		int x = num;
		int x2 = 430;
		base.Controls.Add(CreateLabel("Worker ID*", x, num2));
		workerId = new TextBox
		{
			Location = new Point(x, num2 + 22),
			Width = 360,
			PlaceholderText = "Enter worker ID..."
		};
		num2 += 65;
		base.Controls.Add(CreateLabel("Performance Period", x, num2));
		base.Controls.Add(CreateLabel("Start Date", x, num2 + 30));
		startDate = new TextBox
		{
			Location = new Point(x, num2 + 52),
			Width = 360,
			PlaceholderText = "yyyy-MM-dd"
		};
		base.Controls.Add(CreateLabel("End Date", x, num2 + 94));
		endDate = new TextBox
		{
			Location = new Point(x, num2 + 116),
			Width = 360,
			PlaceholderText = "yyyy-MM-dd"
		};
		Panel panel2 = new Panel
		{
			Location = new Point(x2, num2 - 60),
			Size = new Size(280, 230),
			BackColor = Color.FromArgb(248, 248, 248),
			BorderStyle = BorderStyle.FixedSingle
		};
		Label label3 = new Label
		{
			Text = "Performance Summary",
			Font = new Font("Segoe UI", 11f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(15, 15),
			AutoSize = true
		};
		Label label4 = new Label
		{
			Text = "Total Profit",
			Font = new Font("Segoe UI", 9f, FontStyle.Bold),
			ForeColor = Color.DimGray,
			Location = new Point(15, 55),
			AutoSize = true
		};
		totalProfit = new TextBox
		{
			Location = new Point(15, 78),
			Width = 248,
			ReadOnly = true,
			BackColor = Color.White,
			PlaceholderText = "Profit result will appear here"
		};
		Label label5 = new Label
		{
			Text = "Number of Orders",
			Font = new Font("Segoe UI", 9f, FontStyle.Bold),
			ForeColor = Color.DimGray,
			Location = new Point(15, 116),
			AutoSize = true
		};
		numberoforders = new TextBox
		{
			Location = new Point(15, 139),
			Width = 248,
			ReadOnly = true,
			BackColor = Color.White,
			PlaceholderText = "Order count will appear here"
		};
		Label label6 = new Label
		{
			Text = "Tip: Enter start and end date as\r\nyyyy-MM-dd format.",
			Font = new Font("Segoe UI", 8f, FontStyle.Italic),
			ForeColor = Color.Gray,
			Location = new Point(15, 177),
			Size = new Size(248, 45)
		};
		panel2.Controls.AddRange(label3, label4, totalProfit, label5, numberoforders, label6);
		num2 += 260;
		statue = new Label
		{
			Text = string.Empty,
			Location = new Point(num, num2),
			Width = 680,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Segoe UI", 8f, FontStyle.Italic)
		};
		base.Controls.AddRange(label, label2, panel, workerId, startDate, endDate, panel2, statue);
		base.Load += WorkerPerformance_Load;
		workerId.TextChanged += delegate(object? s, EventArgs e)
		{
			WorkerPerformance_Load(s, e);
		};
		startDate.TextChanged += delegate(object? s, EventArgs e)
		{
			WorkerPerformance_Load(s, e);
		};
		endDate.TextChanged += delegate(object? s, EventArgs e)
		{
			WorkerPerformance_Load(s, e);
		};
		static Label CreateLabel(string text, int x3, int y)
		{
			return new Label
			{
				Text = text,
				Location = new Point(x3, y),
				AutoSize = true,
				ForeColor = Color.DimGray,
				Font = new Font("Segoe UI", 9f, FontStyle.Bold)
			};
		}
	}
}
