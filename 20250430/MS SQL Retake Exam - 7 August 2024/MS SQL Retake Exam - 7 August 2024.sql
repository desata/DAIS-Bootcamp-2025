--1
create database ShoesApplicationDatabase

use ShoesApplicationDatabase;

create table Users
(
Id int PRIMARY KEY IDENTITY,
Username nvarchar(50) UNIQUE not null,
FullName nvarchar(100) not null,
PhoneNumber nvarchar(15),
Email nvarchar(100) UNIQUE not null,
);

create table Brands
(
Id int PRIMARY KEY IDENTITY,
[Name] nvarchar(50) UNIQUE not null
);

create table Sizes
(
Id int PRIMARY KEY IDENTITY,
EU decimal(5,2) not null,
US decimal(5,2) not null,
UK decimal(5,2) not null,
CM decimal(5,2) not null,
[IN] decimal(5,2) not null
);

create table Shoes
(
Id int PRIMARY KEY IDENTITY,
Model nvarchar(30) not null,
Price decimal(10,2) not null,
BrandId int FOREIGN KEY REFERENCES Brands(ID) not null
);

create table Orders
(
Id int PRIMARY KEY IDENTITY,
ShoeId int FOREIGN KEY REFERENCES Shoes(ID) not null,
SizeId int FOREIGN KEY REFERENCES Sizes(ID) not null,
UserId int FOREIGN KEY REFERENCES Users(ID) not null,
);

create table ShoesSizes
(
ShoeId int FOREIGN KEY REFERENCES Shoes(ID) not null,
SizeId int FOREIGN KEY REFERENCES Sizes(ID) not null,
PRIMARY KEY (ShoeId, SizeId)
);


--02. Insert
insert into Brands([Name])
VALUES('Timberland'),
('Birkenstock');


insert into Shoes
VALUES('Reaxion Pro', 150.00, 12),
('Laurel Cort Lace-Up', 160.00, 12),
('Perkins Row Sandal', 170.00, 12),
('Arizona', 80.00, 13),
('Ben Mid Dip', 85.00, 13),
('Gizeh', 90.00, 13)


insert into ShoesSizes
VALUES
(70,1),
(70,2),
(70,3),
(71,2),
(71,3),
(71,4),
(72,4),
(72,5),
(72,6),
(73,1),
(73,3),
(73,5),
(74,2),
(74,4),
(74,6),
(75,1),
(75,2),
(75,3)



insert into Orders
VALUES(70, 2, 15),
(71, 3, 17),
(72, 6, 18),
(73, 5, 4 ),
(74, 4, 7 ),
(75, 1, 11)

--3

select * from shoes, brands

Update shoes 
set Price *=1.15
where BrandId = (select Id from Brands b where b.Name = 'Nike');

--4
select * from Shoes;
select * from Orders o where o.ShoeId = 8;
select * from ShoesSizes where ShoeId = 8

delete ShoesSizes where ShoeId = 8
delete Orders where ShoeId = 8;
delete Shoes where Id = 8;


--5
select s.Model as 'ShoeModel', s.Price from orders o
join Shoes s on s.Id = o.ShoeId
order by s.Price desc, s.Model 

--6
select b.Name as 'BrandName', s.Model as 'ShoeModel' from Shoes s
join Brands b on b.Id = s.BrandId
order by b.Name, s.Model

--7
select * from Users
select * from Orders
select * from Shoes

select * from Shoes s
join Orders o on o.ShoeId = s.Id

select top 10 o.UserId, u.FullName, sum(s.Price) as 'TotalSpent'  from Orders o
join Shoes s on o.ShoeId = s.Id
join Users u on o.UserId = u.Id
group by UserId, u.FullName
order by TotalSpent desc, u.FullName 

--8
select * from Users
select * from Orders
select * from Shoes

select u.Username, u.Email, cast(avg(s.Price) as decimal(6, 2)) as 'AvgPrice' from Orders o
join Shoes s on o.ShoeId = s.Id
join Users u on o.UserId = u.Id
group by Username, u.Email
having count(o.UserId) > 2
order by AvgPrice desc

--9
select * from Shoes
select * from ShoesSizes
select * from Brands

select s.Model, Count(ss.ShoeId) as 'CountOfSizes', b.Name as BrandName from Shoes s
join ShoesSizes ss on ss.ShoeId = s.Id
join Brands b on b.Id = s.BrandId
group by s.Model, b.Name
having b.Name = 'Nike' and s.Model like '%Run%'
order by s.Model desc;

--10
select * from Shoes
select * from ShoesSizes
select * from Sizes
select * from Brands
select * from Users


select u.FullName, u.PhoneNumber, s.Price, s.Id, b.Id, Concat(sss.EU,'EU/',sss.US,'US/',sss.UK,'UK' ) as ShoeSize 
from Orders o
join Users u on o.UserId = u.Id
join Shoes s on s.Id  = o.ShoeId
join ShoesSizes ss on ss.ShoeId = s.Id and ss.SizeId = o.SizeId
join Sizes sss on sss.Id = ss.SizeId
join Brands b on b.Id = s.BrandId
where u.PhoneNumber like '%345%'
order by s.Model

--11
create function udf_OrdersByEmail(@email nvarchar(100))
returns int
as
begin
	return( 
	select count(*) from orders o 
	where UserId in (select id from Users u where u.Email = @email))
end;


SELECT dbo.udf_OrdersByEmail('sstewart@example.com')--3
SELECT dbo.udf_OrdersByEmail('ohernandez@example.com')--2
SELECT dbo.udf_OrdersByEmail('nonexistent@example.com')--0

--12
create procedure usp_SearchByShoeSize @shoeSize decimal(5,2)
as
begin
	select s.Model as ModelName,
u.FullName as UserFullName, 
case
	when s.Price < 100 then 'Low'
	when s.Price <= 200 then 'Medium'
	when s.Price > 200 then 'High'
end as PriceLevel,
b.Name as BrandName,
sss.EU as SizeEU 
from Orders o 
join shoes s on s.Id = o.ShoeId
join ShoesSizes ss on ss.ShoeId = s.Id and ss.SizeId = o.SizeId
join Sizes sss on sss.Id = ss.SizeId
join users u on u.Id = o.UserId
join brands b on b.Id = s.BrandId
where sss.EU = @shoeSize
order by b.Name, u.FullName
end;






--
EXEC usp_SearchByShoeSize 40.00