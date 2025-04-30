--2 Find All the Information About Departments
select * from Departments;

--3 Find all Department Names

select Name from Departments 

--4 Find Salary of Each Employee
SELECT [FirstName]
      ,[LastName]
      ,[Salary]
  FROM [SoftUni].[dbo].[Employees]

--05. Find Full Name of Each Employee
select t.FirstName, t.MiddleName, t.LastName from Employees t;

--06. Find Email Address of Each Employee
select (FirstName+'.'+LastName+'@softuni.bg') from Employees t;

--07. Find All Different Employee’s Salaries
select distinct(t.Salary) from Employees t;

--08. Find all Information About Employees
select * from Employees t where t.JobTitle = 'Sales Representative';

--09. Find Names of All Employees by Salary in Range
select t.FirstName, t.LastName, t.JobTitle from Employees t where t.Salary between 20000 and 30000

--10. Find Names of All Employees
select ( FirstName+' ' +t.MiddleName+' '+t.LastName) as 'Full Name' from Employees t where t.Salary in (25000, 14000, 12500, 23600);

--11. Find All Employees Without Manager
select t.FirstName, t.LastName from Employees t where t.ManagerID IS NULL

--12. Find All Employees with Salary More Than
select t.FirstName, t.LastName, t.Salary from Employees t where t.salary > 50000 order by salary desc;

--13. Find 5 Best Paid Employees
select top 5 t.FirstName, t.LastName from Employees t order by Salary desc;

--14. Find All Employees Except Marketing
select t.FirstName, t.LastName from Employees t where t.DepartmentID <>4;

--15. Sort Employees Table
select * from Employees t order by t.Salary desc, t.FirstName asc, t.LastName desc, t.MiddleName asc;  

--16. Create View Employees with Salaries
create view V_EmployeesSalaries as
select t.FirstName, t.LastName, t.Salary from Employees t;

--17. Create View Employees with Job Titles
  CREATE VIEW V_EmployeeNameJobTitle AS
 select CONCAT(FirstName, ' ', MiddleName, ' ', LastName) AS 'Full Name',
 JobTitle as 'Job Title'
 from Employees

--18. Distinct Job Titles
select distinct(JobTitle) from Employees t;

--19. Find First 10 Started Projects
select top 10 * from Projects t order by t.StartDate, t.Name;

--20. Last 7 Hired Employees
select top 7 t.FirstName, t.LastName, t.HireDate from Employees t order by t.HireDate desc;

--21. Increase Salaries
--Create a SQL query that increases salaries by 12% for all employees that work in one of the following departments –
--Engineering, Tool Design, Marketing or Information Services. As a result, select and display only the
--&quot;Salaries&quot; column from the Employees table. After this, you should restore the database to the original data.

update Employees
set Salary*=1.12
where DepartmentID in (1, 2, 4, 11);

select Salary from Employees t where t.DepartmentID in (1, 2, 4, 11);

SELECT * FROM Departments

--22. All Mountain Peaks
SELECT t.PeakName FROM Peaks t order by t.PeakName;


--23. Biggest Countries by Population
Find the 30 biggest countries by population, located in Europe. Display the &quot;CountryName&quot; and &quot;Population&quot;.
Order the results by population (from biggest to smallest), then by country alphabetically.

select top 30 CountryName, Population from countries t where t.ContinentCode = 'EU' order by 2 desc;


--24. Countries and Currency (Euro / Not Euro)
Find all the countries with information about their currency. Display the &quot;CountryName&quot;, &quot;CountryCode&quot;, and
information about its &quot;Currency&quot;: either &quot;Euro&quot; or &quot;Not Euro&quot;. Sort the results by country name alphabetically.

select CountryName, CountryCode, 
CASE
    WHEN CurrencyCode = 'EUR' THEN 'Euro'
    ELSE 'Not Euro'
END AS Currency
from countries
order by CountryName


--25. All Diablo Characters
select name from characters order by name