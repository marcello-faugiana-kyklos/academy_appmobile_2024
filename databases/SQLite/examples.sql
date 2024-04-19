-- DDL Data Definition Language

-- DROP TABLE people;

-- alter

CREATE TABLE people
(
    person_id int PRIMARY KEY,
    firstname nvarchar(20) NOT null,
    lastname nvarchar(20) NOT null,
    phone_number nvarchar(30),
    birthday DATE,
    ral DECIMAL(10, 2)  -- 10 digits 2 decimali    
)

/*
    
    DML Data Manipulation Language
    
    - insert 
    - update
    - delete
*/ 

INSERT INTO people (person_id, firstname, lastname, phone_number, birthday, ral)
VALUES (1, 'Marcello', 'Faugiana', null, NULL, 20000.35);

INSERT INTO people (person_id, firstname, lastname, phone_number, birthday, ral)
VALUES (2, 'Tullio', 'Manlio', '+39-335847155', NULL, 10000);

INSERT INTO people (person_id, firstname, lastname, phone_number, birthday, ral)
VALUES (3, 'Cippa', 'Lippa', '+39-3418932540', date('1977-05-19'), 50000)

INSERT INTO people (person_id, firstname, lastname, birthday)
VALUES (4, 'Andrea', 'Pirri', date('1998-10-03') );

INSERT INTO people (person_id, firstname, lastname, birthday)
VALUES (5, 'Tom', 'Jerry', date('1998-01-03') );

INSERT INTO people (person_id, firstname, lastname, phone_number, birthday, ral)
VALUES (6, 'Enrico', 'Manuardi', '+39-3412340', date('1998-11-19'), 23000)


UPDATE people 
SET birthday = date('1980-01-01')
WHERE birthday IS null

 
 SELECT T.id FROM 
 (
     SELECT
        person_id AS ID,
        firstname,
        lastname,
        firstname || ' ' || lastname AS "Full name",
        coalesce(phone_number, 'N/A') AS Phone,
        birthday
      --  ral
    FROM
        people p
) T    
WHERE T."Full name" LIKE 'M%'

  --COALESCE(ral, 0) < 15000
    --ral >= 10000 AND  ral <= 25000
    --ral BETWEEN 10000 AND 25000

SELECT * FROM people p 
WHERE p.birthday is NULL

SELECT julianday('2024-04-18') - julianday('2000-01-01') AS today


SELECT * FROM 
(
SELECT 
    A.person_id AS "A_ID", 
 --   A.firstname, 
 --   A.birthday,
    strftime('%Y', A.birthday) AS "A_year_of_birth",
    B.person_id AS "B_id", 
--    B.firstname, 
--    B.birthday,
    strftime('%Y', B.birthday) AS "B_year_of_birth"
FROM 
    people A, people B
) T
WHERE 
    T."A_year_of_birth" = T."B_year_of_birth"
    AND T."a_id" < T."b_ID"


/*     A.person_id, B.person_id, year
 *     1            2            1980       
 *     4            5            1998
 *     4            6            1998
 *     5            6            1998
 * 
 * 
 * 
 */



SELECT 
    strftime('%Y',date('now')) || '|' ||
    strftime('%m',date('now')) || '|' ||
    strftime('%d',date('now')) AS current_date



SELECT coalesce(NULL, ) AS RESULT



/*

1NF: PK + tipo campi atomici




- Orders  (viola la 2NF perché CustomerId, CompanyName e OrderDate dipendono solo da OrderID e non da ProductId

  OrderId     int      PK
  CustomerId  int      
  CompanyName string
  ProductId   int      PK
  ProductDescription string
  OrderDate   Date
  Qty         int
  Price       numeric(6, 2)
  
--   

Orders    (viola ls 3NF perché CompanyName dipende da CustomerId che NON è PK)

  OrderId     int      PK
  CustomerId  int      
  CompanyName string
  OrderDate   Date

OrderDetails (viola la 2NF perché ProductDescription dipende solo da ProductId)

  OrderId     int      PK - FK
  ProductId   int      PK
  ProductDescription string
  Qty         int
  Price       numeric(6, 2)

-- 

Customers

  CustomerId  int     PK   
  CompanyName string


Orders 

  OrderId     int      PK
  CustomerId  int      FK
  OrderDate   Date

Products

  ProductId   int      PK
  ProductDescription string
  Price       numeric(6, 2)


OrderDetails

  OrderId     int      PK - FK
  ProductId   int      PK - FK
  Qty         int
  Price       numeric(6, 2)


2NF e 3NF: ogni campo NON chiave deve dipendere solo ed eclusivamente dalla PK, non altro!

*/


CREATE TABLE customers
(
  CustomerId  int PRIMARY KEY,   
  CompanyName varchar(30) NOT null
)

CREATE TABLE orders 
(
  OrderId     int PRIMARY KEY,
  CustomerId  int NOT NULL REFERENCES customers(CustomerId),
  OrderDate   Date
)

CREATE TABLE products
(
  ProductId   int PRIMARY key,
  ProductCode varchar(10) NOT NULL,
  ProductDescription varchar(100) NOT NULL,
  Price       numeric(6, 2) NOT NULL,
  Notes varchar(1000),
  CHECK (price >= 0),
  UNIQUE (ProductCode)
)

 CREATE INDEX IX_products_description ON products(ProductDescription)


CREATE TABLE order_details
(
  OrderDetailsId int PRIMARY KEY,
  OrderId     int NOT NULL references orders(OrderId),
  ProductId   int NOT NULL references products(ProductId),
  Qty         int NOT NULL,
  Price       numeric(6, 2) NOT NULL,
  CHECK (Qty >= 0 AND Price >= 0),
  unique(OrderId, ProductId)
)

INSERT INTO customers (CustomerId, CompanyName) VALUES(1, 'Terry ltd');
INSERT INTO customers (CustomerId, CompanyName) VALUES(2, 'Frank''s Shop');
INSERT INTO customers (CustomerId, CompanyName) VALUES(3, 'Cloud Frog');
INSERT INTO customers (CustomerId, CompanyName) VALUES(4, 'Intent Corp.');
INSERT INTO customers (CustomerId, CompanyName) VALUES(5, 'Manotti srl');


INSERT INTO products (ProductId, ProductCode, ProductDescription, Price, Notes)
VALUES(1, 'HDMB01', 'Mother board XYZ', 250, null);
INSERT INTO products (ProductId, ProductCode, ProductDescription, Price, Notes) 
VALUES(2, 'HDPR01', 'Processor 1', 150, 'Base processor');
INSERT INTO products (ProductId, ProductCode, ProductDescription, Price, Notes)
VALUES(3, 'HDPR02', 'Processor 2', 180, 'Advanced processor');
INSERT INTO products (ProductId, ProductCode, ProductDescription, Price, Notes)
VALUES(4, 'HDRAM16', 'RAM 16GB', 110, '16GB RAM module');
INSERT INTO products (ProductId, ProductCode, ProductDescription, Price, Notes)
VALUES(5, 'HDRAM32', 'RAM 32GB', 190, '32GB RAM module');
INSERT INTO products (ProductId, ProductCode, ProductDescription, Price, Notes)
VALUES(6, 'HDRAM64', 'RAM 64GB', 230, '32GB RAM module');


INSERT INTO orders (OrderId, CustomerId, OrderDate) VALUES(1, 1, Date('2024-02-13'));

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(1, 1, 1, 1, 240);



















