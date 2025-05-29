--All Public documents  
SELECT 
    d.DocumentId,
    d.Title,
    d.Tags,
	c.Name AS Category,
    e.FullName AS Creator,
    di.DocumentVersion,
    di.CreateDate
FROM Documents d
JOIN Categories c ON d.CategoryId = c.CategoryId
JOIN Employees e ON d.CreatorId = e.EmployeeId
JOIN DocumentsInformation di ON d.DocumentId = di.DocumentId
WHERE d.IsDeleted = 0 
AND  d.AccessLevel = 3

--All document by category
SELECT d.Title, d.Tags, c.Name AS Category
FROM Documents d
JOIN Categories c ON d.CategoryId = c.CategoryId
WHERE c.Name = 'Cloud Computing';

--All document by Creator
SELECT d.DocumentId, d.Title, e.FullName as 'Creator',di.DocumentVersion
FROM Documents d
JOIN DocumentsInformation di ON d.DocumentId = di.DocumentId
JOIN Employees e ON e.EmployeeId = d.CreatorId
WHERE d.CreatorId = 2
-- or e.FullName = 'Liam Scott'

--All item in one's collection
SELECT 
    cd.CollectionId,
    col.Name AS CollectionName,
    d.Title,
    d.Tags,
    d.AccessLevel
FROM CollectionDocuments cd
JOIN Collections col ON cd.CollectionId = col.CollectionId
JOIN Documents d ON cd.DocumentId = d.DocumentId
WHERE cd.CollectionId = 2;


