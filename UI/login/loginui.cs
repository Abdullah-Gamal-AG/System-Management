using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Login : UserControl
{
	private CoreSystem CoreSystem = new CoreSystem();

	private TextBox? userid;

	private TextBox? password;

	private Label? statusLabel;

	private Button? loginButton;


	public Login()
	{
		base.Size = new Size(400, 400);
		BackColor = Color.WhiteSmoke;
		Panel container = new Panel
		{
			Size = new Size(250, 300),
			Location = new Point((base.Width - 250) / 2, (base.Height - 300) / 2),
			Anchor = AnchorStyles.None
		};
		userid = new TextBox
		{
			Location = new Point(0, 60),
			Size = new Size(250, 35),
			PlaceholderText = "User ID",
			Font = new Font("Segoe UI", 10f)
		};
		password = new TextBox
		{
			Location = new Point(0, 110),
			Size = new Size(250, 35),
			PlaceholderText = "Password",
			UseSystemPasswordChar = true,
			Font = new Font("Segoe UI", 10f)
		};
		loginButton = new Button
		{
			Location = new Point(0, 170),
			Size = new Size(250, 40),
			Text = "Login",
			BackColor = Color.FromArgb(0, 122, 204),
			ForeColor = Color.White,
			FlatStyle = FlatStyle.Flat,
			Font = new Font("Segoe UI", 10f, FontStyle.Bold)
		};
		statusLabel = new Label
		{
			Location = new Point(0, 220),
			Size = new Size(250, 60),
			Text = "",
			TextAlign = ContentAlignment.TopCenter,
			Font = new Font("Segoe UI", 9f, FontStyle.Regular),
			AutoSize = true,
			MaximumSize = new Size(250, 0)
		};
		container.Controls.Add(userid);
		container.Controls.Add(password);
		container.Controls.Add(loginButton);
		container.Controls.Add(statusLabel);
		base.Controls.Add(container);
		loginButton.Click += async delegate (object? s, EventArgs e)
		{
			submitbutton(s!, e);
		};
		base.Resize += delegate
		{
			container.Location = new Point((base.Width - container.Width) / 2, (base.Height - container.Height) / 2);
		};
	}
}
