CREATE TABLE vendor_details (
  vendor_id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  vendor_name NVARCHAR(100) NOT NULL,
  country NVARCHAR(100) NULL,
  state NVARCHAR(100) NULL,
  city NVARCHAR(100) NULL,
  phone BIGINT NOT NULL UNIQUE,
  pin NVARCHAR(20) NULL,
  latitude DECIMAL(12,9) NULL,
  longitude DECIMAL(12,9) NULL,
  is_social_distance BIT DEFAULT 0,
  is_fever_screen BIT DEFAULT 0,
  is_sanitizer_used BIT DEFAULT 0,
  is_stamp_check INT DEFAULT 0,
);
GO
CREATE TABLE category (
  category_id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  category_name NVARCHAR(100) NOT NULL
);
GO
CREATE TABLE product (
  product_id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  product_name NVARCHAR(500) NOT NULL
);
GO
CREATE TABLE vendor_category (
  vendor_category_id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  category_id BIGINT NOT NULL,
  vendor_id BIGINT NOT NULL,
  is_active BIT NOT NULL
  --FOREIGN KEY (vendor_id) REFERENCES vendor_details(vendor_id),
  --FOREIGN KEY (category_id) REFERENCES category(category_id)
);
GO
CREATE TABLE vendor_product (
vendor_product_id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
vendor_id BIGINT NOT NULL,
product_id BIGINT NOT NULL,
min_order_quantity DECIMAL(20,0) NULL,
price DECIMAL(20,2) NULL,
Units NVARCHAR(20) NULL,
--FOREIGN KEY (vendor_id) REFERENCES vendor_details(vendor_id),
--FOREIGN KEY (product_id) REFERENCES product(product_id),
);
GO
INSERT INTO product
VALUES ('Apple'),
('Mango'),
('Grapes Green Round'),
('Banana'),
('Santra'),
('Peru'),
('Anaar'),
('KairiKacchi'),
('Mossambi'),
('Chikoo'),
('Sitafal'),
('Papaya'),
('Fanas'),
('Pineapple'),
('Water Melon'),
('Lychee'),
('Strawberry'),
('Aamla'),
('Plum'),
('Peach'),
('Anjir'),
('Kiwi'),
('Grapes Green Long'),
('Grapes Purple Round'),
('Melon Kharbuj'),
('Tomato'),
('Lemon'),
('Peas'),
('Doodhi'),
('Karela'),
('Turai')
GO