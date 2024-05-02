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
  CompanyName varchar(30) NOT NULL
)

CREATE TABLE orders 
(
  OrderId     int PRIMARY KEY,
  CustomerId  int NOT NULL REFERENCES customers(CustomerId),
  OrderDate   Date
)

CREATE INDEX IX_orders_customerId ON orders(CustomerId)

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
INSERT INTO customers (CustomerId, CompanyName) VALUES(6, 'Terry ltd');

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
INSERT INTO orders (OrderId, CustomerId, OrderDate) VALUES(2, 2, Date('2024-03-12'));
INSERT INTO orders (OrderId, CustomerId, OrderDate) VALUES(3, 1, Date('2024-01-03'));
INSERT INTO orders (OrderId, CustomerId, OrderDate) VALUES(4, 3, Date('2024-04-18'));




INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(1, 1, 1, 1, 240);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(2, 1, 2, 2, 150);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(3, 1, 4, 1, 105);

SELECT * FROM products p 

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(4, 2, 1, 1, 240);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(5, 2, 2, 2, 150);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(6, 2, 3, 2, 105);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(7, 2, 4, 3, 240);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(8, 3, 5, 2, 150);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(9, 3, 4, 2, 105);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(10, 3, 3, 4, 240);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(11, 4, 2, 1, 150);

INSERT INTO order_details (OrderDetailsId, OrderId, ProductId, Qty, Price) 
VALUES(12, 4, 1, 3, 105);


SELECT 
    o.OrderId,
    c.CompanyName,
    o.OrderDate 
FROM 
    orders o
INNER join 
    customers c 
ON 
    o.CustomerId = c.CustomerId 


SELECT 
    o.OrderId,
    c.CompanyName,
    o.OrderDate 
FROM
    customers c 
INNER join 
    orders o
ON 
    o.CustomerId = c.CustomerId
WHERE 
    strftime('%Y',o.OrderDate) = '2024'


    SELECT c.*
    FROM 
    customers c
    CROSS JOIN orders o
    
    
/*
 JOIN

 - CROSS
 - INNER
 - LEFT
 - RIGHT
 - FULL OUTER
 - LATERAL/(CROSS APPLY) 

*/

SELECT CustomerId  FROM customers c    

SELECT DISTINCT customerid FROM orders o 


SELECT c.* --, o.OrderId 
FROM customers c 
LEFT join 
    orders o
ON 
    o.CustomerId = c.CustomerId
WHERE 
    o.OrderId IS null

    
SELECT c.*
FROM customers c 
WHERE NOT EXISTS 
(
    SELECT 
        o.customerId 
    FROM 
        orders o 
    WHERE 
        o.customerId = c.CustomerId
)

SELECT c.*
FROM customers c 
WHERE c.CustomerId NOT in 
(
    SELECT 
        o.customerId 
    FROM 
        orders o 
)
    
    
SELECT c.* , o.OrderId 
FROM orders o  
right join 
    customers c
ON 
    o.CustomerId = c.CustomerId
WHERE 
    o.OrderId IS null
    
SELECT p.*
FROM products p 
LEFT JOIN order_details od 
ON p.ProductId = od.ProductId 
WHERE od.OrderDetailsId IS null


SELECT * FROM orders o 



SELECT 
    o.OrderId, 
    o.OrderDate, 
    c.CompanyName, 
    p.ProductCode,
    p.ProductDescription,
    od.Qty,
    od.Price,
    od.Price * od.Qty AS total_row
FROM orders o
INNER JOIN  customers c ON o.CustomerId = c.CustomerId 
INNER JOIN order_details od ON o.OrderId = od.OrderId 
INNER JOIN products p ON od.ProductId = p.ProductId 
WHERE o.OrderId = 1


SELECT 
    od.ProductId, 
    count(*) AS order_rows_count, 
    sum(od.Qty * od.Price) AS total_order, 
    min(od.Price) AS min_value,
    max(od.Price) AS max_value
FROM order_details od 
GROUP BY od.ProductId  




SELECT * FROM order_details od 

    
SELECT c.* , o.OrderId 
FROM orders o  
right join 
    customers c
ON 
    o.CustomerId = c.CustomerId
WHERE 
    o.OrderId IS null    


    
SELECT 
    X.OrderId,
    (SELECT Price FROM order_details) AS TOTAL
FROM 
(
    SELECT o.OrderDate AS OrderId, o.OrderId, od.OrderId, od.ProductId  FROM 
    orders o 
    FULL outer JOIN order_details od 
    ON o.OrderId = od.OrderId 
) X



SELECT od.ProductId , od.Price AS OD_price, p.Price AS P_price, od.Price - p.price AS diff
FROM order_details od
INNER JOIN products p ON od.ProductId = p.ProductId 

WHERE od.OrderId <> 1



WITH updCTE AS (
    SELECT od.OrderDetailsId, p.price
    FROM order_details od
    INNER JOIN products p ON od.ProductId = p.ProductId  
    WHERE od.OrderId <> 1 
) 


UPDATE order_details 
SET price = p.price + (random() % 10)
FROM order_details od
INNER JOIN products p ON od.ProductId = p.ProductId  
WHERE 
od.OrderDetailsId = order_details.OrderDetailsId 
    
    
    
    SELECT (random() % 10)
    FROM order_details od 
    
/*
   numero di ordini fatti da OGNI cliente
   mostrare CustomerId, CompanyName, Orders_count
*/

SELECT * FROM customers c    
    
SELECT 
    c.CustomerId,
    c.CompanyName,
    count(o.OrderId) AS orders_count
FROM customers c
left JOIN orders o 
ON c.CustomerId = o.CustomerId 
GROUP BY
    c.CustomerId,
    c.CompanyName
    
    
-- Il  (o i) prodotto più venduto
-- id prodotto, codice del prodotto, quantità venduta

SELECT max(X.qty)
FROM 
(
    SELECT p.ProductId , p.ProductCode, sum(od.Qty) AS qty 
    FROM products p
    INNER JOIN  order_details od 
    ON p.ProductId = od.ProductId 
    GROUP BY p.ProductId , p.ProductCode    
) X   

-- soluzione con nested query
SELECT X.*
FROM 
(
    SELECT p.ProductId , p.ProductCode, sum(od.Qty) AS qty 
    FROM products p
    INNER JOIN  order_details od 
    ON p.ProductId = od.ProductId 
    GROUP BY p.ProductId , p.ProductCode    
) X
WHERE X.qty =
    (
        SELECT max(Y.qty)
        FROM 
        (
            SELECT p.ProductId , p.ProductCode, sum(od.Qty) AS qty 
            FROM products p
            INNER JOIN  order_details od 
            ON p.ProductId = od.ProductId 
            GROUP BY p.ProductId , p.ProductCode    
        ) Y
    )

    
-- soluzione 1 con Common Table Expression
WITH products_sum AS
(
    SELECT p.ProductId , p.ProductCode, sum(od.Qty) AS qty 
    FROM products p
    INNER JOIN  order_details od 
    ON p.ProductId = od.ProductId 
    GROUP BY p.ProductId , p.ProductCode     
)    
SELECT X.* FROM products_sum X
WHERE X.qty = 
(
    SELECT max(Y.qty) FROM products_sum Y
)

-- soluzione 2 con Common Table Expression

WITH products_with_total_qty AS 
(
    SELECT od.ProductId, sum(od.Qty) AS qty 
    from order_details od 
    GROUP BY od.ProductId
    ORDER BY 2 desc
),
max_quantities AS 
(
    SELECT max(qty) AS max_qty FROM products_with_total_qty
)
SELECT p.ProductId, p.ProductCode, a.qty
FROM products p
INNER JOIN products_with_total_qty a ON p.ProductId = a.ProductID
INNER JOIN max_quantities m ON m.max_qty = a.qty





-- cliente (o clienti) che ha speso più di tutti
-- customerid, companyname, amount
WITH A AS 
(
    SELECT o.CustomerId, sum(od.Qty * od.Price) AS Total
    FROM orders o 
    INNER JOIN order_details od 
    ON o.OrderId = od.OrderId 
    GROUP BY o.CustomerId 
),
B AS 
(
    SELECT max(Total) AS max_total FROM A
)    
select A.CustomerId, c.CompanyName, A.Total
FROM A
INNER JOIN customers c ON c.CustomerId = A.CustomerId
INNER JOIN B ON A.Total = B.max_total



-- cliente (o clienti) che ha speso meno di tutti (fra i clientii che hanno fatto almeno un ordine).
-- In aggiunta si vogliono anche i clienti che NON hanno fatto mai un ordine
-- customerid, companyname, amount


WITH A AS 
(
    SELECT o.CustomerId, sum(od.Qty * od.Price) AS Total
    FROM orders o 
    INNER JOIN order_details od 
    ON o.OrderId = od.OrderId 
    GROUP BY o.CustomerId 
),
B AS 
(
    SELECT min(Total) AS min_total FROM A
)    
select A.CustomerId, c.CompanyName, A.Total
FROM A
INNER JOIN customers c ON c.CustomerId = A.CustomerId
INNER JOIN B ON A.Total = B.min_total
UNION all
SELECT c.CustomerId, c.CompanyName, 0
FROM customers c 
WHERE NOT EXISTS 
(
    SELECT 
        o.customerId 
    FROM 
        orders o 
    WHERE 
        o.customerId = c.CustomerId
)



-- UNKNOWN op something -> UNKNOWN -> FALSE



-- 1 4 5 6
SELECT c.CustomerId, c.CompanyName 
FROM customers c 
WHERE c.CompanyName LIKE '%T%'
EXCEPT
--3 4
SELECT c.CustomerId, c.CompanyName 
FROM customers c 
WHERE c.CompanyName LIKE '%C%'




SELECT * FROM customers c WHERE c.CompanyName



CREATE VIEW v_totals_for_customers
AS 
SELECT o.CustomerId, sum(od.Qty * od.Price) AS Total
    FROM orders o 
    INNER JOIN order_details od 
    ON o.OrderId = od.OrderId 
    GROUP BY o.CustomerId

    
SELECT * FROM v_totals_for_customers x
WHERE x.total > 1000


WITH CTE AS
(
    SELECT * FROM v_totals_for_customers 
)   
SELECT * FROM CTE 
LIMIT 1 offset 2

WHERE total > 1000

-- scrivere una query per generare i numeri interi da m a n (da 1 a 100)

CREATE TABLE digits
(
  d int PRIMARY key
)


INSERT INTO digits(d) values(0);
INSERT INTO digits(d) values(1);
INSERT INTO digits(d) values(2);
INSERT INTO digits(d) values(3);
INSERT INTO digits(d) values(4);
INSERT INTO digits(d) values(5);
INSERT INTO digits(d) values(6);
INSERT INTO digits(d) values(7);
INSERT INTO digits(d) values(8);
INSERT INTO digits(d) values(9);


WITH numbers AS
(
    SELECT --d3.d, d2.d, d1.d, 
        d3.d * 100 + d2.d * 10 + d1.d AS n
    FROM digits d1
    CROSS JOIN digits d2
    CROSS JOIN digits d3
)
SELECT num.n FROM  numbers num
WHERE num.n BETWEEN 10 AND 73
ORDER BY 1 desc

173 = 3 * 10^0 + 7 * 10 + 1 * 10^2


-- CTE ricorsiva

WITH numbers AS
(
    SELECT 1 AS n
    UNION ALL 
    SELECT n + 1
    FROM numbers
    WHERE n < 100
)
SELECT * FROM numbers
WHERE n BETWEEN 10 AND 73
ORDER BY 1


-- I primi 3 prodotti più venduti
-- id prodotto, codice del prodotto, quantità venduta


>= 2000 -> VIP
>= 1000 e < 2000 -> STANDARD
< 1000   -> POOR

WITH customers_with_amounts AS
(
    SELECT 
        o.CustomerId, 
        sum(od.Qty * od.Price) AS Total
    FROM orders o 
    INNER JOIN order_details od 
    ON o.OrderId = od.OrderId 
    GROUP BY o.CustomerId 
),
customers_with_label AS 
(
    SELECT 
        A.CustomerId, 
        A.Total,
        CASE 
            WHEN A.Total >= 1900 THEN 'VIP' 
            WHEN A.Total >= 1000 AND A.Total < 1900 THEN 'Standard'
            ELSE 'Poor'
        END AS Label    
    FROM 
    customers_with_amounts A
)
SELECT c.CustomerId, c.CompanyName , cl.Total, cl.Label FROM 
Customers c
INNER JOIN customers_with_label cl ON c.CustomerId = cl.CustomerId
ORDER BY cl.Total DESC, c.CompanyName
    

SELECT * FROM orders o 

SELECT * FROM games g 
    WHERE g.main_game_id IS null

WITH rnd AS
(SELECT (abs(random()) % 100) AS n)
select
    CASE  
       WHEN n  > 70 THEN '> 70'
       WHEN n between 30 AND 69 THEN '[30, 69]'
       ELSE '< 30'
    END 
FROM rnd    

SELECT CAST(strftime('%m', o.OrderDate) AS int) FROM 
orders o 

--strftime('%m'    
WITH months AS
(
    SELECT 1 AS month_n, 'Gennaio' AS month_desc
    UNION all
    SELECT 2 AS month_n, 'Febbraio' AS month_desc
    UNION all
    SELECT 3 AS month_n, 'Marzo' AS month_desc
    UNION all
    SELECT 4 AS month_n, 'Aprile' AS month_desc
    UNION all
    SELECT 5 AS month_n, 'Maggio' AS month_desc
    UNION all
    SELECT 6 AS month_n, 'Giugno' AS month_desc
    UNION all
    SELECT 7 AS month_n, 'Luglio' AS month_desc
    UNION all
    SELECT 8 AS month_n, 'Agosto' AS month_desc
    UNION all
    SELECT 9 AS month_n, 'Settembre' AS month_desc
    UNION all
    SELECT 10 AS month_n, 'Ottobre' AS month_desc
    UNION all
    SELECT 11 AS month_n, 'Novembre' AS month_desc
    UNION all
    SELECT 12 AS month_n, 'Dicembre' AS month_desc
),
cust_amount_by_month as
(
SELECT 
        o.CustomerId, 
        CAST(strftime('%m', o.OrderDate) AS int) AS order_month,
        sum(od.Qty * od.Price) AS Total
    FROM orders o     
    CROSS join months
    INNER JOIN order_details od ON o.OrderId = od.OrderId 
    WHERE strftime('%Y', o.OrderDate) = '2024'
    GROUP BY 
        o.CustomerId, 
        CAST(strftime('%m', o.OrderDate) AS int)
 )
 SELECT c.customerid, m.month_n, c.Total FROM
 cust_amount_by_month c
 CROSS JOIN months m
 ORDER BY 1, m.month_n
 
 
 
 SELECT *
 FROM 
 
 
WITH months AS
(
    SELECT 1 AS month_n, 'Gennaio' AS month_desc
    UNION all
    SELECT 2 AS month_n, 'Febbraio' AS month_desc
    UNION all
    SELECT 3 AS month_n, 'Marzo' AS month_desc
    UNION all
    SELECT 4 AS month_n, 'Aprile' AS month_desc
    UNION all
    SELECT 5 AS month_n, 'Maggio' AS month_desc
    UNION all
    SELECT 6 AS month_n, 'Giugno' AS month_desc
    UNION all
    SELECT 7 AS month_n, 'Luglio' AS month_desc
    UNION all
    SELECT 8 AS month_n, 'Agosto' AS month_desc
    UNION all
    SELECT 9 AS month_n, 'Settembre' AS month_desc
    UNION all
    SELECT 10 AS month_n, 'Ottobre' AS month_desc
    UNION all
    SELECT 11 AS month_n, 'Novembre' AS month_desc
    UNION all
    SELECT 12 AS month_n, 'Dicembre' AS month_desc
)
SELECT 
  c.CustomerId, 
  m.month_n,
  COALESCE(sum(od.Qty * od.Price), 0) AS Total
FROM customers c 
    CROSS join months m
    left JOIN orders o ON c.CustomerId = o.CustomerId     
    left JOIN order_details od 
        ON o.OrderId = od.OrderId     
        AND CAST(strftime('%m', o.OrderDate) AS int) = m.month_n
    GROUP by
        c.CustomerId, 
        m.month_n
ORDER BY 1, 2 






SELECT * FROM stores s 


select game_id, game_title, json_data, main_game_id 
               from games
               --where game_title like '%fa%'
               ORDER BY 1

