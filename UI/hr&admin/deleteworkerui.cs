using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class DeletWorker : Form
{
	private TextBox IDInput;

	private Label statusLabel;

	public DeletWorker()
	{
		Text = "Inventory - Remove Worker";
		base.Size = new Size(400, 320);
		base.StartPosition = FormStartPosition.CenterParent;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		BackColor = Color.White;
		Font = new Font("Segoe UI", 9f);
		int x = 30;
		int num = 20;
		int width = 320;
		Label label = new Label
		{
			Text = "Delete Worker",
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			ForeColor = Color.FromArgb(45, 45, 45),
			Location = new Point(x, num),
			AutoSize = true
		};
		num += 50;
		Label label2 = new Label
		{
			Text = "Warning: This action cannot be undone.",
			Font = new Font("Segoe UI", 8f, FontStyle.Bold),
			ForeColor = Color.Firebrick,
			Location = new Point(x, num),
			AutoSize = true
		};
		num += 25;
		Label label3 = new Label
		{
			Text = "Enter ID Worker to Delete",
			Location = new Point(x, num),
			AutoSize = true,
			ForeColor = Color.DimGray,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold)
		};
		num += 22;
		IDInput = new TextBox
		{
			Location = new Point(x, num),
			Width = width,
			PlaceholderText = "Exactly as it appears in Worker Profile..."
		};
		num += 50;
		statusLabel = new Label
		{
			Text = "",
			Location = new Point(x, num),
			Width = width,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Segoe UI", 8f, FontStyle.Italic)
		};
		num += 25;
		Button button = new Button
		{
			Text = "Permanently Delete",
			Location = new Point(x, num),
			Width = width,
			Height = 45,
			BackColor = Color.FromArgb(209, 52, 56),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		button.FlatAppearance.BorderSize = 0;
		base.Controls.AddRange(label, label2, label3, IDInput, statusLabel, button);
		button.Click += delegate (object? s, EventArgs e)
		{
			submitButton_Click(s!, e);
		};
	}
}
