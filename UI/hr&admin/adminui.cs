using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System;

public class Admin : UserControl
{
	private FlowLayoutPanel mainFlowPanel;

	public Admin()
	{
		Dock = DockStyle.Fill;
		BackColor = Color.FromArgb(240, 242, 245);
		Panel panel = new Panel
		{
			Size = new Size(base.Width, 70),
			Dock = DockStyle.Top,
			BackColor = Color.FromArgb(45, 45, 48),
			Padding = new Padding(20, 0, 20, 0)
		};
		Label value = new Label
		{
			Text = "Warehouse Dashboard",
			ForeColor = Color.White,
			Font = new Font("Segoe UI", 16f, FontStyle.Bold),
			Dock = DockStyle.Left,
			TextAlign = ContentAlignment.MiddleLeft,
			AutoSize = true,
			Padding = new Padding(0, 10, 0, 0)
		};
		Button profileButton = CreateCircularButton();
		profileButton.Location = new Point(panel.Width - 100, (panel.Height - 50) / 2);
		panel.Controls.Add(value);
		panel.Controls.Add(profileButton);
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
		mainFlowPanel = new FlowLayoutPanel
		{
			Dock = DockStyle.Fill,
			Padding = new Padding(30),
			AutoScroll = true,
			BackColor = Color.Transparent
		};
		mainFlowPanel.Controls.Add(CreateActionCard("Add workers", "Register new workers in the system.", Color.FromArgb(0, 122, 204), AddWorker_Click!));
		mainFlowPanel.Controls.Add(CreateActionCard("Update workers", "Adjust worker details and roles.", Color.FromArgb(34, 139, 34), UpdateWorker_Click!));
		mainFlowPanel.Controls.Add(CreateActionCard("Delete workers", "Remove workers permanently.", Color.FromArgb(209, 52, 56), DeleteWorker_Click!));
		mainFlowPanel.Controls.Add(CreateActionCard("View workers", "See all registered workers.", Color.FromArgb(0, 153, 153), ViewWorker_Click!));
		mainFlowPanel.Controls.Add(CreateActionCard("worker perform", "View worker profit in specfic time.", Color.FromArgb(255, 140, 0), WorkerPerformance_Click!));
		mainFlowPanel.Controls.Add(CreateActionCard("Proudect perform", "View product profit in specfic time.", Color.FromArgb(106, 90, 205), ProudectPerformance_Click!));
		base.Controls.Add(mainFlowPanel);
		base.Controls.Add(panel);
	}

	private Panel CreateActionCard(string title, string description, Color accentColor, EventHandler clickEvent)
	{
		Panel card = new Panel
		{
			Size = new Size(220, 160),
			BackColor = Color.White,
			Margin = new Padding(15),
			Cursor = Cursors.Hand
		};
		Panel panel = new Panel
		{
			Dock = DockStyle.Top,
			Height = 5,
			BackColor = accentColor
		};
		Label label = new Label
		{
			Text = title,
			Font = new Font("Segoe UI", 12f, FontStyle.Bold),
			Location = new Point(15, 20),
			AutoSize = true
		};
		Label label2 = new Label
		{
			Text = description,
			Font = new Font("Segoe UI", 9f, FontStyle.Regular),
			ForeColor = Color.Gray,
			Location = new Point(15, 50),
			Size = new Size(180, 45)
		};
		Button button = new Button
		{
			Text = "Manage",
			FlatStyle = FlatStyle.Flat,
			BackColor = accentColor,
			ForeColor = Color.White,
			Font = new Font("Segoe UI", 9f, FontStyle.Bold),
			Size = new Size(190, 35),
			Location = new Point(15, 110),
			Cursor = Cursors.Hand
		};
		button.FlatAppearance.BorderSize = 0;
		button.Click += clickEvent;
		card.Controls.AddRange(panel, label, label2, button);
		card.MouseEnter += delegate
		{
			card.BackColor = Color.FromArgb(252, 252, 252);
		};
		card.MouseLeave += delegate
		{
			card.BackColor = Color.White;
		};
		return card;
	}

	private Button CreateCircularButton()
	{
		Button button = new Button
		{
			Size = new Size(45, 45),
			Location = new Point(base.Width - 65, 12),
			Anchor = (AnchorStyles.Top | AnchorStyles.Right),
			FlatStyle = FlatStyle.Flat,
			BackColor = Color.FromArgb(63, 63, 70),
			Cursor = Cursors.Hand
		};
		button.FlatAppearance.BorderSize = 0;
		try
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddEllipse(0, 0, button.Width, button.Height);
			button.Region = new Region(graphicsPath);
			string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "profile.png");
			button.BackgroundImage = Image.FromFile(imagePath);
			button.BackgroundImageLayout = ImageLayout.Stretch;
		}
		catch
		{
			button.Text = "\ud83d\udc64";
			button.ForeColor = Color.White;
		}
		return button;
	}

	private void AddWorker_Click(object sender, EventArgs e)
	{
		AddWorker addWorker = new AddWorker();
		addWorker.ShowDialog();
	}

	private void UpdateWorker_Click(object sender, EventArgs e)
	{
		UpdateWorker updateWorker = new UpdateWorker();
		updateWorker.ShowDialog();
	}

	private void DeleteWorker_Click(object sender, EventArgs e)
	{
		DeletWorker deletWorker = new DeletWorker();
		deletWorker.ShowDialog();
	}

	private void ViewWorker_Click(object sender, EventArgs e)
	{
		ViewWorker viewWorker = new ViewWorker();
		viewWorker.ShowDialog();
	}

	private void WorkerPerformance_Click(object sender, EventArgs e)
	{
		WorkerPerformance workerPerformance = new WorkerPerformance();
		workerPerformance.ShowDialog();
	}

	private void ProudectPerformance_Click(object sender, EventArgs e)
	{
		ProudectPerformance proudectPerformance = new ProudectPerformance();
		proudectPerformance.ShowDialog();
	}
}
