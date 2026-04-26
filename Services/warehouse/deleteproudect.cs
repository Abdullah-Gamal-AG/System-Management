using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class DeleteProduct
{
    private void submitButton_Click(object sender, EventArgs e)
    {
        string text = productNameInput.Text.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            statusLabel.Text = "Please enter a product name.";
            statusLabel.ForeColor = Color.Firebrick;
            return;
        }
        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + text + "'?\nThis will remove all stock and price history for this item.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (dialogResult != DialogResult.Yes)
        {
            return;
        }
        try
        {
            CoreSystem coreSystem = new CoreSystem();
            string query = "SELECT * FROM goods WHERE name = @name";
            List<SqliteParameter> list = new List<SqliteParameter>
            {
                new SqliteParameter("@name", text)
            };
            DataTable dataTable = coreSystem.Select(query, list.ToArray());
            if (dataTable.Rows.Count == 0)
            {
                statusLabel.Text = "No product found with that name.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }
            query = "DELETE FROM goods WHERE name = @name";
            coreSystem.Delete(query, list.ToArray());
            MessageBox.Show("Product removed from system.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Error: " + ex.Message;
            statusLabel.ForeColor = Color.Firebrick;
        }
    }
}