SET IDENTITY_INSERT Contacts ON;

INSERT INTO Contacts (Id, [Email], [PhoneNumber], [PostAddress], [Website]) VALUES
(21, NULL, NULL, NULL, NULL),
(22, NULL, NULL, NULL, NULL),
(23, 'stephen.king@example.com', '+4445556666', '15 Fiction Ave, Bangor, ME', 'www.stephenking.com'),
(24, 'suzanne.collins@example.com', '+7778889999', '10 Mockingbird Ln, NY, NY','www.suzannecollins.com');


SET IDENTITY_INSERT Contacts OFF;




SET IDENTITY_INSERT Authors ON;

INSERT INTO Authors (Id, [Name], [ContactId]) VALUES
(16, 'George Orwell', 21),
(17, 'Aldous Huxley', 22),
(18, 'Stephen King', 23),
(19, 'Suzanne Collins', 24);


SET IDENTITY_INSERT Authors OFF;


SET IDENTITY_INSERT Books ON;

INSERT INTO Books (Id, [Title], [YearPublished], [ISBN], [AuthorId], [GenreId]) VALUES
(36, '1984', 1949, '9780451524935', 16, 2),
(37, 'Animal Farm', 1945, '9780451526342', 16, 2),
(38, 'Brave New World', 1932, '9780060850524', 17, 2),
(39, 'The Doors of Perception', 1954, '9780060850531', 17, 2),
(40, 'The Shining', 1977, '9780307743657', 18, 9),
(41, 'It', 1986, '9781501142970', 18, 9),
(42, 'The Hunger Games', 2008, '9780439023481', 19, 7),
(43, 'Catching Fire', 2009, '9780439023498', 19, 7),
(44, 'Mockingjay', 2010, '9780439023511', 19, 7);


SET IDENTITY_INSERT Books OFF;


INSERT INTO LibrariesBooks (LibraryId, BookId) VALUES
(1, 36),
(1, 37),
(2, 38),
(2, 39),
(3, 40),
(3, 41),
(4, 42),
(4, 43),
(5, 44);



