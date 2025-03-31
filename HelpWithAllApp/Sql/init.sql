create database HelperDb

CREATE TABLE Helpers (
    Id SERIAL PRIMARY KEY NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Surname VARCHAR(100) NOT NULL,
    Profession VARCHAR(255) NOT NULL,
    PaymentPerHour REAL NOT NULL,
    Age INT CHECK (Age > 0 and Age<151) NOT NULL,
    Experience INT CHECK (Experience >= 0) NOT NULL,
    Avalibility BOOLEAN NOT NULL,
    Rating REAL CHECK (Rating BETWEEN 0 AND 5)
);
INSERT INTO Helpers (Name, Surname, Profession, PaymentPerHour, Age, Experience, Avalibility, Rating)
VALUES 
('John', 'Doe', 'Electrician', 25.5, 35, 10, TRUE, 4.5),
('Jane', 'Smith', 'Plumber', 30.0, 40, 15, TRUE, 4.8),
('Mike', 'Johnson', 'Carpenter', 22.0, 29, 7, FALSE, 4.2),
('Emily', 'Davis', 'Painter', 20.0, 32, 8, TRUE, 4.0),
('David', 'Wilson', 'Mechanic', 28.0, 45, 20, FALSE, 4.7),
('Sarah', 'Brown', 'Chef', 35.0, 38, 12, TRUE, 4.9),
('Robert', 'Taylor', 'Software Developer', 50.0, 30, 6, TRUE, 4.6),
('Jessica', 'Anderson', 'Photographer', 18.0, 27, 5, FALSE, 4.1),
('Daniel', 'Thomas', 'Music Teacher', 40.0, 50, 25, TRUE, 4.9),
('Laura', 'Martinez', 'Personal Trainer', 45.0, 33, 9, TRUE, 4.3);


CREATE TABLE Customers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255),
    Surname VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(50),
    IsHelper BOOLEAN
);


INSERT INTO Customers (Name, Surname, Email, PhoneNumber)
VALUES
('John', 'Doe', 'john.doe@example.com', '123-456-7890'),
('Jane', 'Smith', 'jane.smith@example.com', '987-654-3210'),
('Alice', 'Johnson', 'alice.johnson@example.com', '555-123-4567'),
('Bob', 'Williams', 'bob.williams@example.com', '555-765-4321'),
('Charlie', 'Brown', 'charlie.brown@example.com', '123-555-7890'),
('David', 'Martinez', 'david.martinez@example.com', '987-321-6540'),
('Eva', 'Garcia', 'eva.garcia@example.com', '555-987-6543'),
('Frank', 'Wilson', 'frank.wilson@example.com', '555-678-9012'),
('Grace', 'Taylor', 'grace.taylor@example.com', '123-789-2345'),
('Henry', 'Moore', 'henry.moore@example.com', '987-456-1230'),
('Ivy', 'Anderson', 'ivy.anderson@example.com', '555-234-5678'),
('Jack', 'Thomas', 'jack.thomas@example.com', '555-890-1234'),
('Kathy', 'Jackson', 'kathy.jackson@example.com', '123-234-5679'),
('Leo', 'White', 'leo.white@example.com', '987-654-4321'),
('Mia', 'Harris', 'mia.harris@example.com', '555-432-8765'),
('Nathan', 'Clark', 'nathan.clark@example.com', '555-123-7894'),
('Olivia', 'Lewis', 'olivia.lewis@example.com', '123-555-7891'),
('Peter', 'Young', 'peter.young@example.com', '987-321-7650'),
('Quincy', 'Scott', 'quincy.scott@example.com', '555-876-4321'),
('Rachel', 'Adams', 'rachel.adams@example.com', '555-567-1234');
