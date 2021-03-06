CREATE DATABASE Wypozyczalnia_Gier_komputerowych
GO
USE [Wypozyczalnia_Gier_komputerowych]
CREATE TABLE Klienci(
ID_klienta INT PRIMARY KEY NOT NULL,
Nazwisko nChar(50) NOT NULL,
Imie nChar(50) NOT NULL,
Adres nChar(50) NOT NULL,
Kod_pocztowy nChar(10) NOT NULL,
Data_urodzenia date NOT NULL,
Numer_DO nChar(50) NOT NULL);
GO
CREATE TABLE Gry(
ID_gry INT PRIMARY KEY NOT NULL,
Nazwa nChar(50) NOT NULL,
Kategoria nChar(50) NOT NULL,
Kategoria_wiekowa nChar(50) NOT NULL,
Data_wydania nChar(10) NOT NULL,
Cena_dzien INT NOT NULL);
GO
CREATE TABLE Pracownicy(
ID_pracownika INT PRIMARY KEY NOT NULL,
Imie nChar(50) NOT NULL,
Nazwisko nChar(50) NOT NULL,
Data_urodzenia date NOT NULL,
Adres nChar(50) NOT NULL,
Stanowisko nChar(50) NOT NULL);
GO
CREATE TABLE Wypozyczenia(
ID_wypozyczenia INT PRIMARY KEY NOT NULL,
ID_Gry INT NOT NULL,
ID_pracownika INT NOT NULL,
ID_klienta INT NOT NULL,
Data_wypozyczenia date NOT NULL,
CONSTRAINT RelacjaGry FOREIGN KEY (ID_Gry)
REFERENCES Gry(ID_Gry)
ON DELETE CASCADE,
CONSTRAINT RelacjaPracownicy FOREIGN KEY (ID_pracownika)
REFERENCES Pracownicy(ID_pracownika)
ON DELETE NO ACTION,
CONSTRAINT RelacjaDoKlient FOREIGN KEY (ID_klienta)
REFERENCES Klienci(ID_klienta)
ON DELETE CASCADE,
);
GO
CREATE TABLE Zwroty(
ID_wypozyczenia INT PRIMARY KEY NOT NULL,
ID_pracownika INT NOT NULL,
data_zwrotu date NOT NULL,
doplaty INT NOT NULL,
CONSTRAINT RelacjaWypozyczenia FOREIGN KEY (ID_wypozyczenia)
REFERENCES Wypozyczenia(ID_wypozyczenia)
ON DELETE no action,
CONSTRAINT Relacjapracownicy2 FOREIGN KEY (ID_pracownika)
REFERENCES Pracownicy(ID_pracownika)
ON DELETE NO ACTION,
);
GO

INSERT INTO Pracownicy(ID_pracownika,imie,nazwisko,data_urodzenia,adres,stanowisko) VALUES('5','Maciej','Kowalski','1997-04-16','Miechów 8','Właściciel'),
('1','Jan','Pierwszy','1977-12-13','Miechów 3','Kierownik'),
('2','Jan','Drugi','1957-04-29','Kraków 4','Kierownik'),
('3', 'Jan', 'Trzeci', '1972-10-22', 'Miechów 5', 'Kasjer'),
('4', 'Jan', 'Czwarty', '1978-08-21', 'Kraków 6', 'Kasjer');
INSERT INTO Klienci(ID_klienta,nazwisko,imie,adres,kod_pocztowy,data_urodzenia,numer_DO) VALUES ('1','Nowak','Andrzej','Poznań 1','42-500','1990-01-01','AKB 987123'),
('2','Kowalski','Marek','Wieliczka','33-120','1978-01-11','TRQ 432543'),
('3','Słowak','Krzysztof','Katowice','30-003','1970-03-12','OOP 878456'),
('4','Nowak','Eugeniusz','Gdynia','27-412','1990-02-27','ARK 413948'),
('5','Zawadzki','Alojzy','Sopot','12-321','1993-07-19','POA 172931');
INSERT INTO Gry(ID_gry,Nazwa,Kategoria,Kategoria_wiekowa,Data_wydania,cena_dzien) VALUES ('1','World Of WarCraft','MMORPG','15','2004','15'),
('2','Dota 2','MOBA','12','2012','20'),
('3','Diablo III','Hack-and-Slash','15','2008','31'),
('4','Heroes','Kampania','16','2000','15'),
('5','Fifa','Sport','12','2020','35');
INSERT INTO Wypozyczenia(ID_wypozyczenia,ID_Gry,ID_pracownika,ID_klienta,data_wypozyczenia) VALUES('1','3','3','1','2020-12-25'),
('2','1','4','2','2020-12-23'),
('3','2','2','3','2020-12-22'),
('4','5','3','4','2020-12-09'),
('5','4','4','5','2020-12-05');
INSERT INTO Zwroty(ID_wypozyczenia,ID_pracownika,data_zwrotu,doplaty) VALUES ('1','3','2020-12-30','2322'),
('2','4','2020-12-31','995'),
('3','1','2020-12-30','553'),
('4','3','2020-12-29','453'),
('5','5','2020-12-28','767');
GO