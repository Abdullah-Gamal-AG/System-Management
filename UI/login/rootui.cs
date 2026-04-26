using System.Drawing;
using System.Windows.Forms;

namespace System;

public class Root : Form
{
	private readonly Form1? _mainForm;

	public Root(Form1? mainForm)
	{
		_mainForm = mainForm;
		base.StartPosition = FormStartPosition.CenterScreen;
		base.Size = new Size(400, 400);
		BackColor = Color.WhiteSmoke;
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.ControlBox = false;
		Button button = new Button
		{
			Location = new Point(50, 50),
			Size = new Size(300, 50),
			Text = "Sales",
			BackColor = Color.FromArgb(0, 122, 204),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Cursor = Cursors.Hand
		};
		Button button2 = new Button
		{
			Location = new Point(50, 120),
			Size = new Size(300, 50),
			Text = "Warehouse",
			BackColor = Color.FromArgb(0, 122, 204),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Cursor = Cursors.Hand
		};
		Button button3 = new Button
		{
			Location = new Point(50, 190),
			Size = new Size(300, 50),
			Text = "Admin",
			BackColor = Color.FromArgb(0, 122, 204),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold),
			Cursor = Cursors.Hand
		};
		base.Controls.Add(button);
		base.Controls.Add(button2);
		base.Controls.Add(button3);
		button.Click += delegate
		{
			if (_mainForm != null)
			{
				_mainForm.ShowMainSales();
				Close();
			}
		};
		button2.Click += delegate
		{
			if (_mainForm != null)
			{
				_mainForm.ShowWarehouse();
				Close();
			}
		};
		button3.Click += delegate
		{
			if (_mainForm != null)
			{
				_mainForm.ShowAdmin();
				Close();
			}
		};
	}

	public Root()
	{
		_mainForm = null;
	}
}
