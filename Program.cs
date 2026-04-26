using System.Data;
using System.Windows.Forms;
namespace System;

public static class Program
{
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.SetHighDpiMode(HighDpiMode.SystemAware);
		DatabaseSetup.SetupDatabase();
		Application.Run(new Form1());
	}
}
