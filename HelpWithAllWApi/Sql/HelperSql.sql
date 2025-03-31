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
