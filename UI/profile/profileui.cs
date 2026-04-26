using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class Profile : UserControl
{

	public Profile()
	{
		BackColor = Color.FromArgb(245, 245, 250);
		Dock = DockStyle.Fill;
		Font = new Font("Segoe UI", 10f);
		InitializeHeader();
		InitializeProfileSection();
		InitializeDataDetails();
	}

	private void InitializeHeader()
	{
		Panel panel = new Panel
		{
			Size = new Size(base.Width, 70),
			Dock = DockStyle.Top,
			BackColor = Color.FromArgb(45, 45, 48)
		};
		Label value = new Label
		{
			Text = "User Profile",
			ForeColor = Color.White,
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			Location = new Point(80, 18),
			AutoSize = true
		};
		Button button = new Button
		{
			Location = new Point(15, 10),
			Size = new Size(50, 50),
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			BackgroundImageLayout = ImageLayout.Zoom
		};
		button.FlatAppearance.BorderSize = 0;
		button.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 63, 65);
		try
		{
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "back.png");
			using Image original = Image.FromFile(imagePath);
			Bitmap bitmap = new Bitmap(original);
			bitmap.MakeTransparent(Color.White);
			button.BackgroundImage = bitmap;
		}
		catch
		{
			button.Text = "←";
			button.ForeColor = Color.White;
		}
		button.Click += delegate
		{
			Form1? form = FindForm() as Form1;
			if (DataUser.Id[0] == '1')
			{
				form?.ShowMainSales();
			}
			else if (DataUser.Id[0] == '2')
			{
				form?.ShowWarehouse();
			}
			else if (DataUser.Id[0] == '3')
			{
				form?.ShowAdmin();
			}
		};
		panel.Controls.Add(value);
		panel.Controls.Add(button);
		base.Controls.Add(panel);
	}

	private void InitializeProfileSection()
	{
		Button btnSettings = new Button
		{
			Size = new Size(40, 40),
			Location = new Point(base.Width - 60, 85),
			FlatStyle = FlatStyle.Flat,
			Cursor = Cursors.Hand,
			Anchor = (AnchorStyles.Top | AnchorStyles.Right),
			BackColor = Color.Transparent
		};
		btnSettings.FlatAppearance.BorderSize = 0;
		btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 220, 220);
		btnSettings.Click += delegate
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			ToolStripItem toolStripItem = contextMenuStrip.Items.Add("Change Password");
			toolStripItem.Click += delegate
			{
				Changepassword changepassword = new Changepassword();
				changepassword.ShowDialog();
			};
			contextMenuStrip.Show(btnSettings, new Point(0, btnSettings.Height));
		};
		try
		{
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "settings.png");
			using Image original = Image.FromFile(imagePath);
			Bitmap bitmap = new Bitmap(original);
			bitmap.MakeTransparent(Color.White);
			btnSettings.BackgroundImage = bitmap;
			btnSettings.BackgroundImageLayout = ImageLayout.Zoom;
		}
		catch
		{
			btnSettings.Text = "⚙";
			btnSettings.Font = new Font("Segoe UI Symbol", 18f);
		}
		base.Controls.Add(btnSettings);
		PictureBox pictureBox = new PictureBox
		{
			Size = new Size(500, 500),
			Location = new Point(base.Width - 1000, 200),
			SizeMode = PictureBoxSizeMode.Zoom,
			Anchor = (AnchorStyles.Top | AnchorStyles.Right),
			BackColor = Color.Transparent
		};
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
		pictureBox.Region = new Region(graphicsPath);
		try
		{
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "profile.png");
			pictureBox.Image = Image.FromFile(imagePath);
		}
		catch
		{
			pictureBox.Paint += delegate (object? s, PaintEventArgs e)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using (Brush brush = new SolidBrush(Color.FromArgb(200, 200, 200)))
				{
					e.Graphics.FillEllipse(brush, 0, 0, 499, 499);
				}
				TextRenderer.DrawText(e.Graphics, "\ud83d\udc64", new Font("Segoe UI", 60f), new Rectangle(0, 0, 500, 500), Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			};
		}
		base.Controls.Add(pictureBox);
	}

	private void InitializeDataDetails()
	{
		FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
		{
			Location = new Point(40, 100),
			Size = new Size(450, 600),
			FlowDirection = FlowDirection.TopDown,
			BackColor = Color.Transparent,
			WrapContents = false
		};
		flowLayoutPanel.Controls.Add(CreateInfoItem("USER ID", DataUser.Id));
		flowLayoutPanel.Controls.Add(CreateInfoItem("FULL NAME", DataUser.Name));
		flowLayoutPanel.Controls.Add(CreateInfoItem("EMAIL ADDRESS", DataUser.Email));
		if (DataUser.Id.StartsWith("1"))
		{
			flowLayoutPanel.Controls.Add(new Label
			{
				Height = 20,
				Width = 400
			});
			int salesData = GetSalesData("day");
			int salesData2 = GetSalesData("month");
			flowLayoutPanel.Controls.Add(CreateStatItem("TODAY'S TOTAL", $"{salesData} EGP", Color.FromArgb(0, 122, 204)));
			flowLayoutPanel.Controls.Add(CreateStatItem("MONTHLY TOTAL", $"{salesData2} EGP", Color.ForestGreen));
		}
		base.Controls.Add(flowLayoutPanel);
	}

	private Panel CreateInfoItem(string label, string value)
	{
		Panel panel = new Panel
		{
			Size = new Size(400, 65),
			Margin = new Padding(0, 10, 0, 10)
		};
		Label label2 = new Label
		{
			Text = label,
			Font = new Font("Segoe UI", 8f, FontStyle.Bold),
			ForeColor = Color.DarkGray,
			Location = new Point(0, 5),
			AutoSize = true
		};
		Label label3 = new Label
		{
			Text = value,
			Font = new Font("Segoe UI", 13f, FontStyle.Regular),
			ForeColor = Color.FromArgb(30, 30, 30),
			Location = new Point(0, 25),
			AutoSize = true
		};
		panel.Controls.AddRange(label2, label3);
		return panel;
	}

	private Panel CreateStatItem(string title, string value, Color accent)
	{
		Panel pnl = new Panel
		{
			Size = new Size(250, 80),
			BackColor = Color.White,
			Margin = new Padding(0, 5, 0, 15)
		};
		pnl.Paint += delegate (object? s, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(230, 230, 230), 1f), 0, 0, pnl.Width - 1, pnl.Height - 1);
		};
		Label label = new Label
		{
			Text = title,
			Font = new Font("Segoe UI", 8f, FontStyle.Bold),
			ForeColor = Color.Gray,
			Location = new Point(15, 15),
			AutoSize = true
		};
		Label label2 = new Label
		{
			Text = value,
			Font = new Font("Segoe UI", 14f, FontStyle.Bold),
			ForeColor = accent,
			Location = new Point(15, 35),
			AutoSize = true
		};
		pnl.Controls.AddRange(label, label2);
		return pnl;
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
