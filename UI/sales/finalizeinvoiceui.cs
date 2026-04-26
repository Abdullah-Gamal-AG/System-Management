using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace System;

public class Finalizeinvoice : Form
{
	public Finalizeinvoice()
	{
		Text = "Final Invoice";
		base.Size = new Size(430, 650);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		BackColor = Color.FromArgb(242, 242, 242);
		Panel panel = new Panel
		{
			Size = new Size(380, 580),
			Location = new Point(20, 20),
			BackColor = Color.White,
			Padding = new Padding(15)
		};
		Label value = new Label
		{
			Text = "OFFICIAL RECEIPT",
			Font = new Font("Segoe UI", 14f, FontStyle.Bold),
			TextAlign = ContentAlignment.TopCenter,
			Dock = DockStyle.Top,
			Height = 35
		};
		Label value2 = new Label
		{
			Text = $"ID: #INV-{Invoice.orderid}-{DateTime.Now:yyyy-MM-dd-HH:mm}",
			Font = new Font("Segoe UI", 9f, FontStyle.Italic),
			ForeColor = Color.DimGray,
			TextAlign = ContentAlignment.TopCenter,
			Dock = DockStyle.Top,
			Height = 25
		};
		FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
		{
			Location = new Point(10, 75),
			Size = new Size(330, 380),
			AutoScroll = true,
			FlowDirection = FlowDirection.TopDown,
			WrapContents = false,
			BackColor = Color.White
		};
		Panel panel2 = new Panel
		{
			Size = new Size(310, 30),
			BackColor = Color.White
		};
		panel2.Controls.Add(new Label
		{
			Text = "ITEM",
			Font = new Font("Segoe UI", 8f, FontStyle.Bold),
			Location = new Point(0, 5),
			AutoSize = true
		});
		panel2.Controls.Add(new Label
		{
			Text = "QTY",
			Font = new Font("Segoe UI", 8f, FontStyle.Bold),
			Location = new Point(180, 5),
			AutoSize = true
		});
		panel2.Controls.Add(new Label
		{
			Text = "TOTAL",
			Font = new Font("Segoe UI", 8f, FontStyle.Bold),
			Location = new Point(250, 5),
			AutoSize = true
		});
		flowLayoutPanel.Controls.Add(panel2);
		foreach (DataRow row in Invoice.invoiceDataTable.Rows)
		{
			Panel panel3 = new Panel
			{
				Size = new Size(310, 35),
				Margin = new Padding(0, 2, 0, 2)
			};
			Label label = new Label
			{
				Text = row["Product"].ToString(),
				Font = new Font("Segoe UI", 9f),
				Location = new Point(0, 10),
				Width = 170
			};
			Label label2 = new Label
			{
				Text = "x" + row["Quantity"].ToString(),
				Font = new Font("Segoe UI", 9f),
				Location = new Point(180, 10),
				Width = 50
			};
			Label label3 = new Label
			{
				Text = $"${row["Total"]:F2}",
				Font = new Font("Segoe UI", 9f, FontStyle.Bold),
				Location = new Point(250, 10),
				Width = 60,
				TextAlign = ContentAlignment.MiddleRight
			};
			panel3.Controls.AddRange(label, label2, label3);
			flowLayoutPanel.Controls.Add(panel3);
		}
		Panel panel4 = new Panel
		{
			Dock = DockStyle.Bottom,
			Height = 100,
			BackColor = Color.White
		};
		Label label4 = new Label
		{
			Text = "--------------------------------------------------------",
			Location = new Point(10, 0),
			AutoSize = true,
			ForeColor = Color.Silver
		};
		Label label5 = new Label
		{
			Text = $"TOTAL AMOUNT: ${Invoice.total:F2}",
			Font = new Font("Segoe UI", 12f, FontStyle.Bold),
			ForeColor = Color.FromArgb(0, 122, 204),
			Location = new Point(10, 25),
			Size = new Size(330, 30),
			TextAlign = ContentAlignment.MiddleRight
		};
		Button button = new Button
		{
			Text = "PRINT & CLOSE",
			BackColor = Color.FromArgb(45, 45, 48),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Size = new Size(330, 40),
			Location = new Point(10, 60),
			Cursor = Cursors.Hand
		};
		button.FlatAppearance.BorderSize = 0;
		button.Click += delegate
		{
			Close();
		};
		panel4.Controls.AddRange(label4, label5, button);
		panel.Controls.Add(flowLayoutPanel);
		panel.Controls.Add(value2);
		panel.Controls.Add(value);
		panel.Controls.Add(panel4);
		base.Controls.Add(panel);
	}
}
