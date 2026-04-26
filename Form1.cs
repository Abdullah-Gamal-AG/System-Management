using System.Drawing;
using System.Windows.Forms;

namespace System;

public partial class Form1 : Form
{
	public Form1()
	{
		InitializeComponent();
		Text = "System Mangement";
		base.WindowState = FormWindowState.Maximized;
		ShowLogin();
	}

	public void ShowLogin()
	{
		base.Controls.Clear();
		Login login = new Login();
		login.Dock = DockStyle.Fill;
		base.Controls.Add(login);
	}

	public void ShowMainSales()
	{
		base.Controls.Clear();
		Mainsales mainsales = new Mainsales();
		mainsales.Dock = DockStyle.Fill;
		base.Controls.Add(mainsales);
	}

	public void ShowWarehouse()
	{
		base.Controls.Clear();
		WarehouseUI warehouseUI = new WarehouseUI();
		warehouseUI.Dock = DockStyle.Fill;
		base.Controls.Add(warehouseUI);
	}

	public void ShowProfile()
	{
		base.Controls.Clear();
		Profile profile = new Profile();
		profile.Dock = DockStyle.Fill;
		base.Controls.Add(profile);
	}

	public void ShowAdmin()
	{
		base.Controls.Clear();
		Admin admin = new Admin();
		admin.Dock = DockStyle.Fill;
		base.Controls.Add(admin);
	}

}
