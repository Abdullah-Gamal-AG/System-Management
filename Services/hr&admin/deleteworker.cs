using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace System;

public partial class DeletWorker
{
    private void submitButton_Click(object sender, EventArgs e)
    {
        string text = IDInput.Text.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            statusLabel.Text = "Please enter a product name.";
            statusLabel.ForeColor = Color.Firebrick;
            return;
        }
        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + text + "'?\nThis will remove all data belong for this ID", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (dialogResult != DialogResult.Yes)
        {
            return;
        }
        try
        {
            CoreSystem coreSystem = new CoreSystem();
            string query = "SELECT * FROM workers WHERE id = @id";
            List<SqliteParameter> list = new List<SqliteParameter>
            {
                new SqliteParameter("@id", text)
            };
            DataTable dataTable = coreSystem.Select(query, list.ToArray());
            if (dataTable.Rows.Count == 0)
            {
                statusLabel.Text = "No workers found with that ID.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }
            query = "UPDATE workers SET delete_flag = 1 WHERE id = @id";
            coreSystem.Delete(query, list.ToArray());
            MessageBox.Show("Worker removed from system.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Error: " + ex.Message;
            statusLabel.ForeColor = Color.Firebrick;
        }
    }
}