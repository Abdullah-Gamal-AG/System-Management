using System.Windows.Forms;

namespace System;

public partial class AddWorker : Form
{
    private TextBox workername;

    private ComboBox role;

    private TextBox workeremail;

    private Label statue;
    public AddWorker()
    {
        Text = "Human Resources - Add Worker";
        base.Size = new Size(420, 480);
        base.StartPosition = FormStartPosition.CenterParent;
        base.FormBorderStyle = FormBorderStyle.FixedDialog;
        base.MaximizeBox = false;
        BackColor = Color.White;
        Font = new Font("Segoe UI", 9f);
        int margin = 30;
        int num = 20;
        int width = 340;
        Label label = new Label
        {
            Text = "Add New Worker",
            Font = new Font("Segoe UI", 16f, FontStyle.Bold),
            ForeColor = Color.FromArgb(45, 45, 45),
            Location = new Point(margin, num),
            AutoSize = true
        };
        num += 45;
        base.Controls.Add(CreateLabel("Worker Name*", num));
        workername = new TextBox
        {
            Location = new Point(margin, num + 22),
            Width = width,
            PlaceholderText = "Enter worker full name..."
        };
        num += 65;
        base.Controls.Add(CreateLabel("Email Address*", num));
        workeremail = new TextBox
        {
            Location = new Point(margin, num + 22),
            Width = width,
            PlaceholderText = "Enter email address..."
        };
        num += 65;
        base.Controls.Add(CreateLabel("Role*", num));
        role = new ComboBox
        {
            Location = new Point(margin, num + 22),
            Width = width,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 9f)
        };
        role.Items.AddRange("Sales", "warehouse", "Admin");
        num += 65;
        statue = new Label
        {
            Text = "",
            Location = new Point(margin, num),
            Width = width,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 8f, FontStyle.Italic)
        };
        num += 30;
        Button button = new Button
        {
            Text = "Add Worker",
            Location = new Point(margin, num),
            Width = width,
            Height = 45,
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 10f, FontStyle.Bold)
        };
        button.FlatAppearance.BorderSize = 0;
        base.Controls.AddRange(label, workername, workeremail, role, statue, button);
        button.Click += delegate (object? s, EventArgs e)
        {
            submitButton_Click(s!, e);
        };
        Label CreateLabel(string text, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(margin, y),
                AutoSize = true,
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
        }
    }
}