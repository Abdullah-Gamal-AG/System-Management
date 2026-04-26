PRAGMA foreign_keys = ON;

-- 1. Workers Table
CREATE TABLE IF NOT EXISTS workers(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    password TEXT NOT NULL,
    delete_flag INTEGER NOT NULL DEFAULT 0 CHECK (delete_flag IN (0, 1))
);

-- 2. Customers Table (تم حذف الفاصلة الزائدة)
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

-- 4. Orders Table (The "Invoice Header")
-- This stores info that applies to the WHOLE invoice
CREATE TABLE IF NOT EXISTS orders(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    worker_id INTEGER NOT NULL,
    customer_id INTEGER NOT NULL,
    total_invoice_price REAL DEFAULT 0, -- Sum of all items
    order_date TEXT NOT NULL DEFAULT (DATETIME('now')),
    FOREIGN KEY (worker_id) REFERENCES workers(id),
    FOREIGN KEY (customer_id) REFERENCES customers(id)
);

-- 5. Order_Items Table (The "Invoice Details")
-- This allows one Order ID to have many different goods
CREATE TABLE IF NOT EXISTS order_items(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    order_id INTEGER NOT NULL, -- This links many items to 1 Order ID
    good_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    unit_price REAL NOT NULL, -- Price at the time of sale
    FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
    FOREIGN KEY (good_id) REFERENCES goods(id)
);

-- 5. Purchases Table (تم إضافة عمود worker_id الناقص)
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

--- التريجرز (Triggers) ---

-- تحديث المخزون بعد الطلب
CREATE TRIGGER update_stock_after_item_added
AFTER INSERT ON order_items
BEGIN
    UPDATE goods
    SET quantity = quantity - NEW.quantity
    WHERE id = NEW.good_id;
END;

-- تحديث المخزون بعد الشراء (توريد بضاعة)
CREATE TRIGGER IF NOT EXISTS update_goods_quantity_after_purchase
AFTER INSERT ON purchases
BEGIN
    UPDATE goods
    SET quantity = quantity + NEW.quantity
    WHERE id = NEW.good_id;
END;



-- حساب السعر الإجمالي تلقائياً للمشتريات
CREATE TRIGGER IF NOT EXISTS update_total_price_after_purchase
AFTER INSERT ON purchases
BEGIN
    UPDATE purchases
    SET total_price = NEW.quantity * (SELECT price FROM goods WHERE id = NEW.good_id)
    WHERE id = NEW.id;
END;

-- الفهارس (Indexes) لتحسين سرعة البحث
CREATE INDEX IF NOT EXISTS idx_orders_worker_id ON orders(worker_id);
CREATE INDEX IF NOT EXISTS idx_orders_customer_id ON orders(customer_id);
CREATE INDEX IF NOT EXISTS idx_orders_good_id ON orders(good_id);
CREATE INDEX IF NOT EXISTS idx_purchases_good_id ON purchases(good_id);
CREATE INDEX IF NOT EXISTS idx_purchases_worker_id ON purchases(worker_id);