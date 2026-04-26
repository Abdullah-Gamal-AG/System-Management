using Microsoft.Data.Sqlite;
using System.IO;

namespace System;

public static class DatabaseSetup
{
    public static void SetupDatabase()
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyDatabase.db");

        SqliteConnectionStringBuilder sb = new SqliteConnectionStringBuilder();
        sb.DataSource = dbPath;

        using SqliteConnection sqliteConnection = new SqliteConnection(sb.ConnectionString);
        sqliteConnection.Open();

        string commandText = @"
        PRAGMA foreign_keys = ON;

        -- 1. Workers Table 
        CREATE TABLE IF NOT EXISTS workers(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            email TEXT NOT NULL UNIQUE,
            password TEXT NOT NULL,
            delete_flag INTEGER NOT NULL DEFAULT 0 CHECK (delete_flag IN (0, 1))
        );

        -- 2. Customers Table
        CREATE TABLE IF NOT EXISTS customers(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            phonenumber TEXT DEFAULT NULL UNIQUE
        );

        -- 3. Goods Table
        CREATE TABLE IF NOT EXISTS goods(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            description TEXT DEFAULT NULL,
            price REAL NOT NULL CHECK (price >= 0),
            quantity INTEGER NOT NULL CHECK (quantity >= 0)
        );

        -- 4. Orders Table
        CREATE TABLE IF NOT EXISTS orders(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            worker_id INTEGER NOT NULL,
            customer_id INTEGER NOT NULL,
            total_invoice_price REAL DEFAULT 0,
            order_date TEXT NOT NULL DEFAULT (DATETIME('now')),
            FOREIGN KEY (worker_id) REFERENCES workers(id),
            FOREIGN KEY (customer_id) REFERENCES customers(id)
        );

        -- 5. Order_Items Table
        CREATE TABLE IF NOT EXISTS order_items(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            order_id INTEGER NOT NULL,
            good_id INTEGER NOT NULL,
            quantity INTEGER NOT NULL CHECK (quantity > 0),
            unit_price REAL NOT NULL,
            FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
            FOREIGN KEY (good_id) REFERENCES goods(id)
        );

        -- 6. Purchases Table
        CREATE TABLE IF NOT EXISTS purchases(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            worker_id INTEGER NOT NULL, 
            good_id INTEGER NOT NULL,
            quantity INTEGER NOT NULL CHECK (quantity > 0),
            total_price REAL DEFAULT 0 CHECK (total_price >= 0),
            purchase_date TEXT NOT NULL DEFAULT (DATETIME('now')),
            FOREIGN KEY (worker_id) REFERENCES workers(id) ON DELETE CASCADE,
            FOREIGN KEY (good_id) REFERENCES goods(id) ON DELETE CASCADE
        );

        --- Triggers ---
        CREATE TRIGGER IF NOT EXISTS update_stock_after_item_added
        AFTER INSERT ON order_items
        BEGIN
            UPDATE goods SET quantity = quantity - NEW.quantity WHERE id = NEW.good_id;
        END;

        CREATE TRIGGER IF NOT EXISTS update_goods_quantity_after_purchase
        AFTER INSERT ON purchases
        BEGIN
            UPDATE goods SET quantity = quantity + NEW.quantity WHERE id = NEW.good_id;
        END;

        --- Indexes ---
        CREATE INDEX IF NOT EXISTS idx_orders_worker_id ON orders(worker_id);
        CREATE INDEX IF NOT EXISTS idx_orders_customer_id ON orders(customer_id);
        CREATE INDEX IF NOT EXISTS idx_order_items_order_id ON order_items(order_id);
        CREATE INDEX IF NOT EXISTS idx_purchases_good_id ON purchases(good_id);
        ";

        try
        {
            using SqliteCommand sqliteCommand = new SqliteCommand(commandText, sqliteConnection);
            sqliteCommand.ExecuteNonQuery();
            Console.WriteLine("Database and Schema created successfully.");
            CoreSystem coreSystem = new CoreSystem();
            string sql = "INSERT OR IGNORE INTO workers (id, name, email, password) VALUES (@id, @name, @email, @password)";
            var parameters = new List<Microsoft.Data.Sqlite.SqliteParameter>
            {
                new Microsoft.Data.Sqlite.SqliteParameter("@id", 1000),
                new Microsoft.Data.Sqlite.SqliteParameter("@name", "Admin"),
                new Microsoft.Data.Sqlite.SqliteParameter("@email", "Admin"),
                new Microsoft.Data.Sqlite.SqliteParameter("@password", Hashing.HashPassword("0000"))
            };
            coreSystem.Insert(sql, parameters.ToArray());
        }
        catch (Exception ex)
        {
            // في WinForms يفضل عرض MessageBox بدلاً من Console
            Console.WriteLine("Database Error: " + ex.Message);
        }
    }
}