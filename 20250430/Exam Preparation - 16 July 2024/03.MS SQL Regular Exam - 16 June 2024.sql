--03 MS SQL Regular Exam - 16 June 2024
UPDATE Contacts
SET Website = LOWER('www.' + REPLACE(a.Name, ' ', '') + '.com')
FROM Contacts c
INNER JOIN Authors a ON c.Id = a.ContactId
WHERE c.Website IS NULL;

--04. Delete
/*
You are required to delete 'Alex Michaelides' from the Authors table. 
This is challenging because the Authors table is referenced by the Books table, 
which in turn is referenced by the LibrariesBooks table. 
Therefore, you need to handle these references correctly to maintain the integrity of the database.
*/



DELETE lb
FROM LibrariesBooks lb JOIN Books b
ON lb.BookId = b.Id
JOIN Authors a
ON a.Id = b.AuthorId
WHERE a.Name = 'Alex Michaelides' ;

DELETE b
FROM Books b JOIN Authors a
ON b.AuthorId = a.Id
WHERE a.Name = 'Alex Michaelides' ;

DELETE Authors
WHERE [Name] = 'Alex Michaelides' ;

--05. Chronological Order
SELECT * FROM Authors;
SELECT * FROM Books;
SELECT * FROM Contacts;
SELECT * FROM Genres;
SELECT * FROM Libraries;
SELECT * FROM LibrariesBooks;


select b.Title AS 'Book Title', b.ISBN, b.YearPublished AS 'YearReleased' 
from Books b
order by b.YearPublished desc, b.Title;

--06. Books by Genre
SELECT b.Id, b.Title, b.ISBN, g.Name FROM Books b
JOIN Genres g on b.GenreId = g.Id
WHERE g.Name in('Biography', 'Historical Fiction' )
ORDER BY g.Name, b.Title

--07. Missing Genre

SELECT l.Name AS 'Library', c.Email 
FROM Libraries l
JOIN Contacts c on c.Id = l.ContactId
WHERE l.Id NOT IN (
    SELECT lb.LibraryId
    FROM LibrariesBooks lb
    JOIN Books b ON lb.BookId = b.Id
    WHERE b.GenreId = (SELECT Id FROM Genres WHERE Name = 'Mystery')
)
ORDER BY l.Name;


SELECT lb.LibraryId
    FROM LibrariesBooks lb
    JOIN Books b ON lb.BookId = b.Id
    WHERE b.GenreId = (SELECT Id FROM Genres WHERE Name = 'Mystery');


--08. First 3 Books

select b.Title, b.YearPublished AS 'Year', g.Name from Books b
join Genres g on b.GenreId = g.Id
where (b.YearPublished > 2000 
and b.Title like ('%a%')) 
OR
(b.YearPublished < 1950 and b.Title like('%Fantasy%'))
order by b.Title asc, b.YearPublished desc;

select top 3 b.Title, b.YearPublished AS 'Year', g.Name from Books b
join Genres g on b.GenreId = g.Id
where 
(b.YearPublished > 2000 and b.Title like ('%a%')) 
OR
(b.YearPublished < 1950 and g.Name like('%Fantasy%'))
order by b.Title asc, b.YearPublished desc;

--09. Authors from UK
SELECT a.Name as 'Autor', c.Email, c.PostAddress FROM Authors a
JOIN Contacts c on c.Id = a.ContactId
where c.PostAddress like ('%, UK')
ORDER BY a.Name;

--10. Fictions in Denver
SELECT * FROM Authors;
SELECT * FROM Books;
SELECT * FROM Contacts;
SELECT * FROM Genres;
SELECT * FROM Libraries;
SELECT * FROM LibrariesBooks;

select a.Name as 'Author', b.Title, l.Name as 'Library', c.PostAddress as 'Library Address' from Authors a
join Books b on b.AuthorId = a.Id
join LibrariesBooks lb on lb.BookId = b.Id
join Libraries l on l.Id = lb.LibraryId
join Contacts c on c.Id = l.ContactId
where b.GenreId = (select id from Genres where Name = 'Fiction')
and c.PostAddress like '%Denver%'
order by b.Title

--11

create function udf_AuthorsWithBooks (@author nvarchar(100))
returns int
as
begin
	return(select count(b.Id) as 'Output' from Books b
	join Authors a on a.Id = b.AuthorId
	where a.Name = @author 
	group by a.name)
end;

SELECT dbo.udf_AuthorsWithBooks('J.K. Rowling');


--12. Search by Genre

CREATE PROCEDURE usp_SearchByGenre(@genreName nvarchar(30))
as
begin
select b.Title, b.YearPublished as 'Year', b.ISBN, a.Name as Author, g.Name as Genre
from Books b
JOIN Authors a on a.Id = b.AuthorId
JOIN Genres g on g.Id = b.GenreId
where g.Name = @genreName
order by b.Title
end

EXEC usp_SearchByGenre 'Fantasy'

--13
create function udf_GenreFilter(@genreName nvarchar(30))
RETURNS TABLE
AS
RETURN
(select b.Id as 'BookId', b.Title, b.YearPublished, b.ISBN, a.Name, l.Name as 'Library' from  Books b
join Authors a on a.Id = b.AuthorId
join LibrariesBooks lb on lb.BookId = b.Id
join Libraries l on l.Id = lb.LibraryId
where b.GenreId = (select g.Id from Genres g where g.Name = @genreName)
);

SELECT dbo.udf_GenreFilter('Fiction')


select b.Id as 'BookId', b.Title, b.YearPublished, b.ISBN, a.Name, l.Name as 'Library' from  Books b
join Authors a on a.Id = b.AuthorId
join LibrariesBooks lb on lb.BookId = b.Id
join Libraries l on l.Id = lb.LibraryId
where b.GenreId = (select g.Id from Genres g where g.Name = 'Fiction')
