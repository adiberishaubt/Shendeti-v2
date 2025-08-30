INSERT INTO Countries(Name) VALUES('Kosova');

INSERT INTO [dbo].[Cities]
           ([Name]
           ,[CountryId]
           ,[Latitude]
           ,[Longitude])
     VALUES
           ('Vushtrri', 1, 42.8235, 20.9533),
           ('Deçan', 1, 42.5369, 20.2738),
           ('Dragash', 1, 42.0583, 20.648),
           ('Drenas', 1, 42.63, 20.8708),
           ('Ferizaj', 1, 42.3664, 21.1276),
           ('Fushë Kosovë', 1, 42.6377, 21.0703),
           ('Gjakova', 1, 42.3921, 20.3855),
           ('Gjilan', 1, 42.4618, 21.4416),
           ('Istog', 1, 42.7793, 20.4819),
           ('Kaçanik', 1, 42.2331, 21.2334),
           ('Kamenicë', 1, 42.5875, 21.5736),
           ('Klinë', 1, 42.6232, 20.5728),
           ('Lipjan', 1, 42.5244, 21.102),
           ('Malishevë', 1, 42.4819, 20.731),
           ('Mitrovicë', 1, 42.8778, 20.8346),
           ('Obiliq', 1, 42.6862, 21.0553),
           ('Pejë', 1, 42.6606, 20.2776),
           ('Podujevë', 1, 42.9098, 21.175),
           ('Prishtina', 1, 42.6664, 21.1175),
           ('Prizren', 1, 42.2234, 20.6933),
           ('Rahovec', 1, 42.3977, 20.6299),
           ('Skenderaj', 1, 42.745, 20.7726),
           ('Suharekë', 1, 42.3602, 20.8209),
           ('Shtërpcë', 1, 42.2376, 21.0153),
           ('Shtime', 1, 42.4373, 21.0289),
           ('Viti', 1, 42.3385, 21.3589);

INSERT INTO Specializations(Name) VALUES ('Dentist');
INSERT INTO Specializations(Name) VALUES ('Kardiolog');
INSERT INTO Services(Name, SpecializationId) VALUES ('Pastrimi i dhëmbëve', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Plombimi i dhëmbëve', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Ekstraktimi i dhëmbëve', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Implantet dentare', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Ortodontia', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Korrigjimi i dhëmbëve të vështirësuar', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Protetika dentare', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Endodontia', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Terapia kundër kafshimit të dhëmbëve', 1);
INSERT INTO Services(Name, SpecializationId) VALUES ('Elektrokardiogram', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Ekokardiogram', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Testim Stresi', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Monitorim Holter', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Angiografi', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Implantimi i stimuluesit kardiak', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Implantimi i defibrilatorit', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Menaxhimi i dështimit të zemrës', 2);
INSERT INTO Services(Name, SpecializationId) VALUES ('Menaxhimi i aritmisë', 2);
INSERT INTO Levels(Name, RequiredXP) VALUES 
('Blood Buddy', 0),
('Plasma Prodigy', 50),
('Platelet Patron', 150),
('Hemoglobin Hero', 300),
('Lifesaver Legend', 500),
('Plasma Paladin', 750),
('Platelet Pioneer', 1000),
('Hemoglobin Highness', 1500),
('Lifesaver Luminary', 2000),
('Plasma Paragon', 3000);