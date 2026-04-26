using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Changepassword : Form
{
	private Label lblCurrent;

	private Label lblNew;

	private Label lblConfirm;

	private Label lblStatus;

	private TextBox txtCurrent;

	private TextBox txtNew;

	private TextBox txtConfirm;

	private Button btnSubmit;


	public Changepassword()
	{
		Text = "Account Security";
		base.Size = new Size(350, 420);
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.StartPosition = FormStartPosition.CenterParent;
		BackColor = Color.White;
		Font = new Font("Segoe UI", 9f);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		lblCurrent = new Label
		{
			Text = "Current Password",
			Top = 20,
			Left = 30,
			Width = 250
		};
		lblNew = new Label
		{
			Text = "New Password",
			Top = 90,
			Left = 30,
			Width = 250
		};
		lblConfirm = new Label
		{
			Text = "Confirm New Password",
			Top = 160,
			Left = 30,
			Width = 250
		};
		lblStatus = new Label
		{
			Text = "",
			Top = 235,
			Left = 30,
			Width = 270,
			ForeColor = Color.Crimson,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
		};
		txtCurrent = CreateStyledTextBox(45);
		txtNew = CreateStyledTextBox(115);
		txtConfirm = CreateStyledTextBox(185);
		btnSubmit = new Button
		{
			Text = "Update Password",
			Top = 280,
			Left = 30,
			Width = 275,
			Height = 45,
			FlatStyle = FlatStyle.Flat,
			BackColor = Color.FromArgb(0, 120, 215),
			ForeColor = Color.White,
			Cursor = Cursors.Hand
		};
		btnSubmit.FlatAppearance.BorderSize = 0;
		btnSubmit.Click += delegate (object? s, EventArgs e)
		{
			submit_click(s!, e);
		};
		base.Controls.AddRange(lblCurrent, lblNew, lblConfirm, lblStatus, txtCurrent, txtNew, txtConfirm, btnSubmit);
	}

	private TextBox CreateStyledTextBox(int top)
	{
		return new TextBox
		{
			Top = top,
			Left = 30,
			Width = 270,
			PasswordChar = '●',
			BorderStyle = BorderStyle.FixedSingle,
			Font = new Font("Segoe UI", 11f)
		};
	}
}
