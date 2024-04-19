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


