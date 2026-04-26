using System.Data;

namespace System;

public static class Invoice
{
	public static int orderid;

	public static DataTable invoiceDataTable;

	public static decimal total;

	static Invoice()
	{
		invoiceDataTable = new DataTable("Invoice");
		total = default(decimal);
		InitializeInvoiceDataTable();
	}

	internal static void InitializeInvoiceDataTable()
	{
		if (invoiceDataTable.Columns.Count == 0)
		{
			invoiceDataTable.Columns.Add("Product", typeof(string));
			invoiceDataTable.Columns.Add("Quantity", typeof(int));
			invoiceDataTable.Columns.Add("Price", typeof(decimal));
			invoiceDataTable.Columns.Add("Total", typeof(decimal));
		}
	}
}
