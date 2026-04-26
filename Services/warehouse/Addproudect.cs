using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class AddProduct
{
    private void submitButton_Click(object s, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(productName.Text) || !double.TryParse(productPrice.Text, out var result))
        {
            statusLabel.Text = "Please provide a valid name and price.";
            statusLabel.ForeColor = Color.Firebrick;
            return;
        }
        try
        {
            CoreSystem coreSystem = new CoreSystem();
            string query = "SELECT * FROM goods WHERE name = @name";
            List<SqliteParameter> list = new List<SqliteParameter>
            {
                new SqliteParameter("@name", productName.Text)
            };
            coreSystem.Select(query, list.ToArray());
            if (list.Count == 0)
            {
                statusLabel.Text = "A product with this name already exists.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }
            query = "INSERT INTO goods (name, description, price, quantity) VALUES (@name, @description, @price, @stock)";
            int result2;
            List<SqliteParameter> list2 = new List<SqliteParameter>
            {
                new SqliteParameter("@name", productName.Text),
                new SqliteParameter("@description", string.IsNullOrWhiteSpace(productDescription.Text) ? ((IConvertible)DBNull.Value) : ((IConvertible)productDescription.Text)),
                new SqliteParameter("@price", result),
                new SqliteParameter("@stock", (productStock.Text != "" && int.TryParse(productStock.Text, out result2)) ? result2 : 0)
            };
            coreSystem.Insert(query, list2.ToArray());
            MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred while adding the product. Please try again." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            statusLabel.Text = "Database Error: " + ex.Message;
            statusLabel.ForeColor = Color.Firebrick;
        }
    }
}