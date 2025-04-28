-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 22. 13:45
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

DROP DATABASE IF EXISTS jarmuszerviz;
CREATE DATABASE jarmuszerviz;


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
(12, 0xa1b2c3d4e5f6, 0xf6e5d4c3b2a1, 'szerelő', 'Nagy Béla', '+36709876543', 'nagy.bela@szerviz.hu'),
(13, 0xb2c3d4e5f6a7, 0xa7f6e5d4c3b2, 'művezető', 'Kiss Eszter', '+36201112233', 'kiss.eszter@szerviz.hu'),
(14, 0xdd19cede497e60ee53784add9dc6a18d40cbf7f60e9bb88ac9cd6e15ce22ad7e6923cb3235fad55cb93f161a4d7b6009369d0d58d01f773a0d1924893bb3aa15ee6f6aef42888dbe2832e4f42dca472fbef3f6a524b410c959be69f6398db8e683352e1f114a98efcd6da08f572f3c8853ec08453133136c8cedc9989b704b8a, 0x6e15eef6270402db334ce5e68987bf3aa7d7f750a787a8de5718da2e67cd1d4d60267dfb55e4ef6ab3ed860333e88b9c81df4116e78a172d49b92dc2065f00f5, 'szerelő', 'Kovács János', NULL, NULL);

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
(8, '2025-05-16 14:30:00', 'Fékrendszer ellenőrzés', 15);

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
(16, 'Tóth Eszter', '+36309876543', 'toth.eszter@example.com', 'Budapest, Kossuth tér 1.');

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
  MODIFY `AlkalmazottID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT a táblához `alkatreszek`
--
ALTER TABLE `alkatreszek`
  MODIFY `AlkatreszID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT a táblához `idopontfoglalasok`
--
ALTER TABLE `idopontfoglalasok`
  MODIFY `IdoPontID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `javitasok`
--
ALTER TABLE `javitasok`
  MODIFY `JavitasID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT a táblához `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `UgyfelID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

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
