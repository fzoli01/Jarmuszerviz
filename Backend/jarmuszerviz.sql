-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 28. 01:35
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `jarmuszerviz`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `alkalmazottak`
--

CREATE TABLE `alkalmazottak` (
  `AlkalmazottID` int(11) NOT NULL,
  `Jelszo` blob NOT NULL,
  `Jelszo_hash` blob NOT NULL,
  `Rang` varchar(20) DEFAULT NULL,
  `Nev` varchar(100) NOT NULL,
  `Telefonszam` varchar(20) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `alkalmazottak`
--

INSERT INTO `alkalmazottak` (`AlkalmazottID`, `Jelszo`, `Jelszo_hash`, `Rang`, `Nev`, `Telefonszam`, `Email`) VALUES
(12, 0xa530cbe2959ada814a5273844e4ef0e7ef7d79f8e1804c518b98163db9565f2d676bb9988d73ffc3c52ca80433674742e536473beeb6cf92c5e8baffd0e4f7ac304d8c661ff9354376c69b1894481684c487d55e12693311b745794b480d8074bc19c88f91dc7a6619c7007f84a6d8c3354a7ac738253a6034cac9a37f8a40df, 0xecce4d8ea2f0db84b46718008743e287a4664ee55212d427c1517c472090c39a9ab033e0261a1b567a5989a7c0691b3f7bc4d175978f27c712655197391e750f, 'szerelő', 'Nagy Béla', '+36709876543', 'nagy.bela@szerviz.hu'),
(13, 0xb933ebd44ab6004a5aed46865f6adf7aceecd56563dbb18f316a3efced6f1a22d016a782cfd795af2d039e2dface89c0285eba38aa161d83a3119c542c387ab11da9be312cc756b74602f01a2323949dd760bf3d9a96ed4ad0618b83afac532b98bfac7dbf20e0fd5d31345c4ed51b2758507b5fe778f29a6f915dc4825c6a3e, 0x4d43f14d36cc5c55817a3d016a5d96405d818ad4f5de302b5678f2329188f13fc79e4f33a35dc708dbb2d42d4fb5a29e8a3c6017d231b67c533d50af54a7a0a4, 'szerelő', 'Kiss Eszter', '+36709876543', 'kiss.eszter@szerviz.hu'),
(15, 0xe38e02a434b75c3311a61c116dbd901e30ffffcc7522f7eb95b1d6b11b45cafa6a297987141126845ce061fbd35e3e16fcf14dadae427312343830bef27537b186415a076c87799a132a172687fb4633afa9baaf8307981325e82b8428c88c29f8e9434d53a3c7d26b77e6494ed70c458cd016219fcd11cb455b9a0bab2460b2, 0xf90c861b52a2b799be146aa0d590b98afba1b138eb2d1b2f1c3222f83c8397e3e50e1ded36a9427a1881382925ae81c4da534546a7bcda4bd126c0fe97102d43, 'Admin', 'Admin', '+36709876543', 'test@email.com');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `alkatreszek`
--

CREATE TABLE `alkatreszek` (
  `AlkatreszID` int(11) NOT NULL,
  `AlkatreszNev` varchar(100) DEFAULT NULL,
  `Cikkszam` varchar(50) DEFAULT NULL,
  `Ar` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `alkatreszek`
--

INSERT INTO `alkatreszek` (`AlkatreszID`, `AlkatreszNev`, `Cikkszam`, `Ar`) VALUES
(8, 'Légfilter', 'LF-300', 9500.00),
(9, 'Féktárcsa', 'FT-200', 35000.00),
(10, 'Generátor', 'GEN-100', 45000.00),
(11, 'Olajszűrő', 'OL-500', 12500.00);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznaltalkatreszek`
--

CREATE TABLE `felhasznaltalkatreszek` (
  `JavitasID` int(11) NOT NULL,
  `AlkatreszID` int(11) NOT NULL,
  `Mennyiseg` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `felhasznaltalkatreszek`
--

INSERT INTO `felhasznaltalkatreszek` (`JavitasID`, `AlkatreszID`, `Mennyiseg`) VALUES
(9, 8, 1),
(10, 10, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `idopontfoglalasok`
--

CREATE TABLE `idopontfoglalasok` (
  `IdoPontID` int(11) NOT NULL,
  `datum` datetime DEFAULT NULL,
  `megjegyzes` text DEFAULT NULL,
  `UgyfelID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `idopontfoglalasok`
--

INSERT INTO `idopontfoglalasok` (`IdoPontID`, `datum`, `megjegyzes`, `UgyfelID`) VALUES
(6, '2025-05-12 09:30:00', 'Olajcsere', 14),
(7, '2025-05-13 11:00:00', 'Fékellenőrzés', 15),
(8, '2025-05-16 14:30:00', 'Fékrendszer ellenőrzés', 15),
(9, '2025-04-30 06:00:00', 'Olajcsere', 17),
(10, '2025-04-30 06:00:00', 'Általános átvizsgálás', 18),
(11, '2025-04-30 06:00:00', 'Általános átvizsgálás', 19);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `jarmuvek`
--

CREATE TABLE `jarmuvek` (
  `Alvazszam` varchar(17) NOT NULL,
  `Marka` varchar(50) DEFAULT NULL,
  `Tipus` varchar(50) DEFAULT NULL,
  `Evjarat` int(11) DEFAULT NULL,
  `UgyfelID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `jarmuvek`
--

INSERT INTO `jarmuvek` (`Alvazszam`, `Marka`, `Tipus`, `Evjarat`, `UgyfelID`) VALUES
('WBA123456789ABCDE', 'BMW', 'X5', 2019, 14),
('ZFA12345678901234', 'Opel', 'Astra', 2020, 14),
('ZFA5678901234BCDE', 'Fiat', '500', 2021, 15);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `javitasok`
--

CREATE TABLE `javitasok` (
  `JavitasID` int(11) NOT NULL,
  `JarmuID` varchar(17) DEFAULT NULL,
  `AlkalmazottID` int(11) DEFAULT NULL,
  `Datum` datetime DEFAULT NULL,
  `Leiras` text DEFAULT NULL,
  `Koltseg` decimal(10,2) DEFAULT NULL,
  `elkeszult` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `javitasok`
--

INSERT INTO `javitasok` (`JavitasID`, `JarmuID`, `AlkalmazottID`, `Datum`, `Leiras`, `Koltseg`, `elkeszult`) VALUES
(9, 'WBA123456789ABCDE', 12, '2025-05-10 10:00:00', 'Légfilter csere', 60000.00, 1),
(10, 'ZFA5678901234BCDE', 13, '2025-05-11 14:00:00', 'Generátor javítás', 85000.00, 0),
(11, 'WBA123456789ABCDE', 12, '2025-05-15 10:00:00', 'Olajcsere és szűrőcsere', 35000.00, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfelek`
--

CREATE TABLE `ugyfelek` (
  `UgyfelID` int(11) NOT NULL,
  `Nev` varchar(100) NOT NULL,
  `Telefonszam` varchar(20) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Cim` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `ugyfelek`
--

INSERT INTO `ugyfelek` (`UgyfelID`, `Nev`, `Telefonszam`, `Email`, `Cim`) VALUES
(14, 'Kovács Anna', '+36201234567', 'kovacs.anna@example.com', 'Szeged, Petőfi tér 3.'),
(15, 'Molnár Gábor', '+36307654321', 'molnar.gabor@example.com', 'Pécs, Széchenyi utca 7.'),
(16, 'Tóth Eszter', '+36309876543', 'toth.eszter@example.com', 'Budapest, Kossuth tér 1.'),
(17, 'asd', '+36302597212', 'hajbamartin@gmail.com', 'asd'),
(18, 'asd', '+36302597212', 'hajbamartin@gmail.com', 'asd'),
(19, 'asd', '+36302597212', 'hajbamartin@gmail.com', 'asd');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `alkalmazottak`
--
ALTER TABLE `alkalmazottak`
  ADD PRIMARY KEY (`AlkalmazottID`);

--
-- A tábla indexei `alkatreszek`
--
ALTER TABLE `alkatreszek`
  ADD PRIMARY KEY (`AlkatreszID`);

--
-- A tábla indexei `felhasznaltalkatreszek`
--
ALTER TABLE `felhasznaltalkatreszek`
  ADD PRIMARY KEY (`JavitasID`,`AlkatreszID`),
  ADD KEY `AlkatreszID` (`AlkatreszID`);

--
-- A tábla indexei `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  ADD PRIMARY KEY (`IdoPontID`),
  ADD KEY `fk_idopont_ugyfel` (`UgyfelID`);

--
-- A tábla indexei `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD PRIMARY KEY (`Alvazszam`),
  ADD KEY `UgyfelID` (`UgyfelID`);

--
-- A tábla indexei `javitasok`
--
ALTER TABLE `javitasok`
  ADD PRIMARY KEY (`JavitasID`),
  ADD KEY `JarmuID` (`JarmuID`),
  ADD KEY `AlkalmazottID` (`AlkalmazottID`);

--
-- A tábla indexei `ugyfelek`
--
ALTER TABLE `ugyfelek`
  ADD PRIMARY KEY (`UgyfelID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `alkalmazottak`
--
ALTER TABLE `alkalmazottak`
  MODIFY `AlkalmazottID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT a táblához `alkatreszek`
--
ALTER TABLE `alkatreszek`
  MODIFY `AlkatreszID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  MODIFY `IdoPontID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT a táblához `javitasok`
--
ALTER TABLE `javitasok`
  MODIFY `JavitasID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT a táblához `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `UgyfelID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `felhasznaltalkatreszek`
--
ALTER TABLE `felhasznaltalkatreszek`
  ADD CONSTRAINT `felhasznaltalkatreszek_ibfk_1` FOREIGN KEY (`JavitasID`) REFERENCES `javitasok` (`JavitasID`),
  ADD CONSTRAINT `felhasznaltalkatreszek_ibfk_2` FOREIGN KEY (`AlkatreszID`) REFERENCES `alkatreszek` (`AlkatreszID`);

--
-- Megkötések a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  ADD CONSTRAINT `fk_idopont_ugyfel` FOREIGN KEY (`UgyfelID`) REFERENCES `ugyfelek` (`UgyfelID`);

--
-- Megkötések a táblához `jarmuvek`
--
ALTER TABLE `jarmuvek`
  ADD CONSTRAINT `jarmuvek_ibfk_1` FOREIGN KEY (`UgyfelID`) REFERENCES `ugyfelek` (`UgyfelID`);

--
-- Megkötések a táblához `javitasok`
--
ALTER TABLE `javitasok`
  ADD CONSTRAINT `fk_javitas_jarmu` FOREIGN KEY (`JarmuID`) REFERENCES `jarmuvek` (`Alvazszam`),
  ADD CONSTRAINT `javitasok_ibfk_2` FOREIGN KEY (`AlkalmazottID`) REFERENCES `alkalmazottak` (`AlkalmazottID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
