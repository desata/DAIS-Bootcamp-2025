INSERT INTO Users (FullName, Unit)
VALUES
(N'Alice Carter', N'Development'),
(N'Bob Lane', N'HR'),
(N'Carla Stone', N'Marketing'),
(N'David Ng', N'Finance'),
(N'Emma Watson', N'IT Support'),
(N'Frank Liu', N'Design'),
(N'Grace Kim', N'Data Science'),
(N'Henry Zhao', N'Operations'),
(N'Isla Ford', N'Legal'),
(N'Jackie Chen', N'Security');


INSERT INTO ResourceCharacteristics (Description)
VALUES
(N'Projector'),
(N'Whiteboard'),
(N'High-Speed Internet'),
(N'Video Conferencing'),
(N'Air Conditioning'),
(N'Ergonomic Chairs'),
(N'Dual Monitors'),
(N'24/7 Access'),
(N'Natural Light'),
(N'Soundproofing');

INSERT INTO ResourceTypes (TypeName, Description)
VALUES
(N'Meeting Room', N'Spaces for internal or client meetings'),
(N'Workstation', N'Desks and cubicles for employees'),
(N'Lab', N'Specialized environments for testing'),
(N'Server Room', N'IT infrastructure space'),
(N'Parking Spot', N'Employee vehicle slots'),
(N'Locker', N'Secure personal storage'),
(N'Break Room', N'Area for employee rest'),
(N'Hot Desk', N'Shared flexible desks'),
(N'Conference Hall', N'Large presentation area'),
(N'Private Office', N'Enclosed workspace');

INSERT INTO ResourceCharacteristicsTypes (ResourceTypesId, ResourceCharacteristicsId)
VALUES
(1, 1), (1, 2), (1, 4),
(2, 6), (2, 7),
(3, 3), (3, 9),
(4, 8),
(5, 10),
(6, 5);


INSERT INTO Resources (Name, Available, ResourceTypesId)
VALUES
(N'Meeting Room A', 1, 1),
(N'Meeting Room B', 0, 1),
(N'Desk 101', 1, 2),
(N'Desk 102', 1, 2),
(N'QA Lab 1', 0, 3),
(N'Server Room 2', 1, 4),
(N'Parking Slot 12', 0, 5),
(N'Locker 7', 1, 6),
(N'Break Room Lounge', 1, 7),
(N'Office 201', 1, 10);


INSERT INTO Reservations (UsersCount, StartTime, EndTime, Purpose, CreatorId, ResourceId)
VALUES
(4, '2025-05-01 09:00', '2025-05-01 10:00', N'Sprint Planning', 1, 1),
(2, '2025-05-01 11:00', '2025-05-01 12:00', N'HR Interview', 2, 2),
(1, '2025-05-01 08:00', '2025-05-01 17:00', N'Workstation Booking', 3, 3),
(1, '2025-05-02 08:00', '2025-05-02 17:00', N'Daily Desk Use', 4, 4),
(3, '2025-05-03 10:00', '2025-05-03 13:00', N'Testing Session', 5, 5),
(1, '2025-05-01 00:00', '2025-05-01 23:59', N'Server Maintenance', 6, 6),
(1, '2025-05-01 08:30', '2025-05-01 17:30', N'Parking', 7, 7),
(1, '2025-05-01 09:00', '2025-05-01 17:00', N'Locker Use', 8, 8),
(5, '2025-05-01 12:00', '2025-05-01 13:00', N'Team Lunch', 9, 9),
(1, '2025-05-01 09:00', '2025-05-01 17:00', N'Private Work Session', 10, 10);

