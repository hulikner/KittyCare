USE MASTER
GO

IF NOT EXISTS (
    SELECT [name]
    FROM sys.databases
    WHERE [name] = N'KittyCare'
)
CREATE DATABASE KittyCare
GO

USE KittyCare
GO


DROP TABLE IF EXISTS Provision;
DROP TABLE IF EXISTS Provider;
DROP TABLE IF EXISTS Cat;
DROP TABLE IF EXISTS [Owner];
DROP TABLE IF EXISTS Neighborhood;


CREATE TABLE Neighborhood (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(55) NOT NULL
);

CREATE TABLE [Owner] (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	Email VARCHAR(255) NOT NULL,
	[FirstName] VARCHAR(55) NOT NULL,
	[LastName] VARCHAR(55) NOT NULL,
	[Address] VARCHAR(255) NOT NULL,
	NeighborhoodId INTEGER,
	Phone VARCHAR(55) NOT NULL,
	CONSTRAINT FK_Owner_Neighborhood FOREIGN KEY (NeighborhoodId) REFERENCES Neighborhood(Id),
	CONSTRAINT UQ_Email UNIQUE(Email)
);

CREATE TABLE Cat (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(55) NOT NULL,
	OwnerId INTEGER NOT NULL,
	Breed VARCHAR(55) NOT NULL,
	ImageUrl VARCHAR(255),
	CONSTRAINT FK_Cat_Owner FOREIGN KEY (OwnerId) REFERENCES [Owner](Id) ON DELETE CASCADE
);

CREATE TABLE Provider (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    [FirstName] VARCHAR(55) NOT NULL,
	[LastName] VARCHAR(55) NOT NULL,
    ImageUrl VARCHAR(255),
	NeighborhoodId INTEGER,
	CONSTRAINT FK_Provider_Neighborhood FOREIGN KEY (NeighborhoodId) REFERENCES Neighborhood(Id)
);

CREATE TABLE Provision (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Date] DATETIME NOT NULL,
	Duration INTEGER NOT NULL,
	ProviderId INTEGER NOT NULL,
	CatId INTEGER NOT NULL,
	CONSTRAINT FK_Provision_Provider FOREIGN KEY (ProviderId) REFERENCES Provider(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Provision_Cat FOREIGN KEY (CatId) REFERENCES Cat(Id) ON DELETE CASCADE
);

INSERT INTO Neighborhood ([Name]) VALUES ('Nashville');
INSERT INTO Neighborhood ([Name]) VALUES ('Antioch');
INSERT INTO Neighborhood ([Name]) VALUES ('Berry Hill');
INSERT INTO Neighborhood ([Name]) VALUES ('Germantown');
INSERT INTO Neighborhood ([Name]) VALUES ('The Gulch');
INSERT INTO Neighborhood ([Name]) VALUES ('Downtown');
INSERT INTO Neighborhood ([Name]) VALUES ('Music Row');
INSERT INTO Neighborhood ([Name]) VALUES ('Hermitage');
INSERT INTO Neighborhood ([Name]) VALUES ('Madison');
INSERT INTO Neighborhood ([Name]) VALUES ('Green Hills');
INSERT INTO Neighborhood ([Name]) VALUES ('Midtown');
INSERT INTO Neighborhood ([Name]) VALUES ('West Nashville');
INSERT INTO Neighborhood ([Name]) VALUES ('Donelson');
INSERT INTO Neighborhood ([Name]) VALUES ('North Nashville');
INSERT INTO Neighborhood ([Name]) VALUES ('Bellvue');

INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('John', 'Sanchez', 'john@gmail.com', '355 Main St', 1, '(615)-553-2456');
INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('Patricia', 'Young', 'patty@gmail.com', '233 Washington St', 2, '(615)-448-5521');
INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('Robert', 'Brown', 'robert@gmail.com', '145 Sixth Ave', 3, '(615)-323-7711');
INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('Jennifer', 'Wilson', 'Jennifer@gmail.com', '495 Cedar Rd', 1, '(615)-919-8944');
INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('Michael', 'Moore', 'mike@gmail.com', '88 Oak St', 2, '(615)-556-7273');
INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('Linda', 'Green', 'linda@gmail.com', '53 Lake Cir', 3, '(615)-339-4488');
INSERT INTO [Owner] ([FirstName], [LastName], Email, [Address], NeighborhoodId, Phone) VALUES ('William', 'Anderson', 'willy@gmail.com', '223 Hill St', 1, '(615)-232-6768');

INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('SharkTank', 1, 'Siamese', 'https://cdn-aahmh.nitrocdn.com/mwIJloVUffDtKiCgRcivopdgojcJrVwT/assets/static/optimized/rev-4db3d4c/image/siamese-cat-cover.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Princess', 1, 'American Shorthair', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-01229b9.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Charkey', 2, 'American Curl', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-eb0143f.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Xyla', 3, 'Bengal', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-9ce1fd3.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Tilly', 3, 'Exotic Shorthair', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-376fa3c.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Swinky', 4, 'Maine Coon', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-c3ec3ac.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Finley', 5, 'Ragdoll', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-5ae4dab.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Casper', 6, 'Balinese', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-097ed2d.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Jack', 7, 'Selkirk Rex', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-0d6e6bc.jpg');
INSERT INTO Cat ([Name], OwnerId, Breed, ImageUrl) VALUES ('Binx', 7, 'Bombay', 'https://foreblog.net/wp-content/uploads/2022/04/15-best-american-cat-breeds-c95d5ef.jpg');


INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Claudelle', 'https://avatars.dicebear.com/v2/female/c117aa483c649ecbc46c6d65172bf6e6.svg', 15);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Roi', 'https://avatars.dicebear.com/v2/male/ebf2f3a7c07a83e6bce11358860bec57.svg', 9);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Shena', 'https://avatars.dicebear.com/v2/female/08c75cdd62072da8400654c560a5ed6b.svg', 10);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Gibb', 'https://avatars.dicebear.com/v2/male/6640bd96cd90587bf8e00d9e7f187b36.svg', 8);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Tammy', 'https://avatars.dicebear.com/v2/female/e092d74ffd42e2d1d1be3e3f71b88289.svg', 6);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Rufe', 'https://avatars.dicebear.com/v2/male/3a47dbe77df7fcab368e15983a39725c.svg', 11);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Cassandry', 'https://avatars.dicebear.com/v2/female/91c2e90fb83e3a21d388c84de5746b60.svg', 12);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Cully', 'https://avatars.dicebear.com/v2/male/bdd61e876afcf343969a266c9fdfb111.svg', 4);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Agnesse', 'https://avatars.dicebear.com/v2/female/08f7c5edb1dd8760e24283373c640a7b.svg', 14);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Corney', 'https://avatars.dicebear.com/v2/male/19fa24bf6e089d4c591304f9a79f5102.svg', 2);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Janeczka', 'https://avatars.dicebear.com/v2/female/fa94752e3c6101f5abb2743fcd701619.svg', 10);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Golda', 'https://avatars.dicebear.com/v2/male/a335368f726b814c43da589c07244b47.svg', 15);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Polly', 'https://avatars.dicebear.com/v2/female/163709759da882ea4e2cc3d3861a3096.svg', 7);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Shaughn', 'https://avatars.dicebear.com/v2/male/8138849e241c885652134ca6ab7cd337.svg', 9);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Hilliary', 'https://avatars.dicebear.com/v2/female/8eb9c006f613724ee3ca7a55901c8a49.svg', 15);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Beilul', 'https://avatars.dicebear.com/v2/male/3483b1f7f691e7ded77bb828df752554.svg', 6);
INSERT INTO Provider ([FirstName], [LastName], ImageUrl, NeighborhoodId) values ('Mr','Marcille', 'https://avatars.dicebear.com/v2/male/e62a9cecd6a394ddc316865298fcdae2.svg', 12);

INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-09 17:30:00', 1200, 1, 1);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-10 17:30:00', 1200, 1, 2);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-08 16:00:00', 900, 2, 9);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-09 08:30:00', 1800, 2, 6);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-10 12:00:00', 1750, 3, 3);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-08 09:00:00', 1275, 3, 7);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-09 13:30:00', 2000, 4, 4);
INSERT INTO Provision ([Date], Duration, ProviderId, CatId) VALUES ('2020-04-09 13:30:00', 2000, 4, 5);