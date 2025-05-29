USE BirthdayGifts;
GO

INSERT INTO Employees (Username, Password, FullName, BirthDate) VALUES
-- Password is 'Password123!' with SHA256
('john.doe', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'John Doe', '1985-03-15'),
('jane.smith', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Jane Smith', '1990-06-22'),
('bob.wilson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Bob Wilson', '1988-09-30'),
('alice.johnson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Alice Johnson', '1992-12-05'),
('mike.brown', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Mike Brown', '1987-04-18'),
('sarah.davis', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Sarah Davis', '1991-08-25'),
('david.miller', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'David Miller', '1986-01-12'),
('emma.wilson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Emma Wilson', '1993-07-08'),
('chris.taylor', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Chris Taylor', '1989-11-20'),
('lisa.anderson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Lisa Anderson', '1994-02-28');

INSERT INTO Gifts (Name, Description, Price) VALUES
('Smart Watch', 'Modern smartwatch with multiple features', 299.99),
('Coffee Set', 'Luxury coffee brewing kit', 89.99),
('Wireless Headphones', 'High-quality wireless noise-canceling headphones', 199.99),
('Bestseller Book', 'Latest bestseller in hardcover', 24.99),
('Leather Wallet', 'Elegant genuine leather wallet', 79.99),
('Sports Bag', 'Quality sports bag with multiple compartments', 45.99),
('Scented Candle Set', 'Luxury set of 3 scented candles', 34.99),
('Photo Album', 'Personalized photo album', 29.99),
('Wine Set', 'Wine set with corkscrew and accessories', 59.99),
('Spa Kit', 'Luxury spa treatment kit', 69.99),
('Portable Speaker', 'Waterproof Bluetooth speaker', 89.99),
('BBQ Tool Set', 'Professional barbecue tools', 79.99),
('Leather Organizer', 'Luxury desk organizer', 49.99),
('Travel Kit', 'Suitcase with travel accessories', 129.99),
('Garden Tool Set', 'Gardening tools collection', 39.99),
('Art Print', 'Framed artistic print', 69.99),
('Yoga Set', 'Yoga mat with accessories', 44.99),
('Designer Vase', 'Hand-crafted ceramic vase', 89.99),
('Tea Collection', 'Luxury collection of various teas', 49.99),
('Board Game', 'Popular strategy board game', 54.99);
