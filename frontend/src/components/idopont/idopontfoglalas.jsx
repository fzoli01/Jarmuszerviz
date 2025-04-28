import React, { useState } from 'react';
import {
  eachDayOfInterval,
  endOfMonth,
  format,
  startOfMonth,
  getDay,
  isSameDay,
  addMonths,
  subMonths
} from 'date-fns';
import { hu } from 'date-fns/locale';
import './idopontfoglalas.css';
import { RxQuestionMarkCircled } from "react-icons/rx";
import Tooltip from '@mui/material/Tooltip';
import Button from '@mui/material/Button';
import { MdEmail } from "react-icons/md";
import { FaUser } from "react-icons/fa";
import { FaScrewdriverWrench } from "react-icons/fa6";
import { createUgyfel, createIdopont } from './fetch'; 
import { FaPhoneSquareAlt } from "react-icons/fa";
import { FaHouseUser } from "react-icons/fa";
import emailjs from '@emailjs/browser';

const WEEKDAYS = ["Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek", "Szombat", "Vasárnap"];

const HOURS = ["8:00","10:00","12:00","14:00","16:00"];

const Idopont = () => { 
  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');
  const [email, setEmail] = useState('');
  const [address, setAddress] = useState('');
  const [selectedDate, setSelectedDate] = useState(null);
  const [selectedHour, setSelectedHour] = useState(null);
  const [selectedEgyeb, setSelect] = useState('');
  const [emailError, setEmailError] = useState('');

  const validateEmail = (email) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    

    if (!name || !phone || !email || !selectedDate || !selectedEgyeb || !selectedHour) {
      alert('Minden kötelező mezőt ki kell tölteni!');
      return;
    }

    try {
      const ugyfel = await createUgyfel({
        Nev: name,
        Telefonszam: phone.replace(/ /g, ''),
        Email: email,
        Cim: address
      });

      const [hour, minute] = selectedHour.split(':').map(Number);
      const fullDateTime = new Date(
        selectedDate.getFullYear(),
        selectedDate.getMonth(),
        selectedDate.getDate(),
        hour,
        minute
      );

      await createIdopont({
        ugyfelId: ugyfel.UgyfelID,
        datum: fullDateTime.toISOString(),
        megjegyzes: selectedEgyeb
      });

      alert('Sikeres foglalás!');
      emailjs.sendForm('service_lytp0aj','template_ukogb1p', e.target,'nWrAjx4fceLS_TG4O');
      

      setName('');
      setPhone('');
      setEmail('');
      setAddress('');
      setSelectedDate(null);
      setSelectedHour(null);
      setSelect('');

    } catch (error) {
      alert(`Hiba történt: ${error.message}`);
    }
  };

  const [currentMonth, setCurrentMonth] = useState(new Date());
  const firstDayOfMonth = startOfMonth(currentMonth);
  const lastDayOfMonth = endOfMonth(currentMonth);
  const daysInMonth = eachDayOfInterval({ start: firstDayOfMonth, end: lastDayOfMonth });
  const startingDayIndex = (getDay(firstDayOfMonth) + 6) % 7;


  const handleDayClick = (day) => {
    if (getDay(day) !== 0) {
      setSelectedDate(day);
    }
    
  };

  const handlePrevMonth = () => {
    setCurrentMonth(subMonths(currentMonth, 1));
  };

  const handleNextMonth = () => {
    setCurrentMonth(addMonths(currentMonth, 1));
  };

  const longText = "A piros színnel a munkaszüneti napokat, a zöld színnel az Ön által kiválasztott napot jelöljük.";

  return (
    <div id='idopontfoglalas' className="idopontDiv">
      <div className="texter">
        <span id='fontunk' className='textSzolg'>Időpontfoglalás</span>
        <div className="monthNavigation">
          <button onClick={handlePrevMonth} className='monthButton'>◀</button>
          <p className="honap">
            {format(currentMonth, "LLLL yyyy", { locale: hu }).charAt(0).toUpperCase() + format(currentMonth, "LLLL yyyy", { locale: hu }).slice(1)}
          </p>
          <button onClick={handleNextMonth} className='monthButton'>▶</button>
        </div>
      </div>
      <div className='napok' id='fontunkAsd'>
        {WEEKDAYS.map((day) => (
          <div key={day} className='weekdays'>{day}</div>
        ))}

        {Array.from({ length: startingDayIndex }).map((_, index) => (
          <div key={`empty-${index}`} className='empty'></div>
        ))}

        {daysInMonth.map((day, index) => {
          const isToday = isSameDay(day, new Date());
          const isSelected = selectedDate && isSameDay(day, selectedDate);
          const isSunday = getDay(day) === 0;

          return (
            <div
              key={index}
              className={`daysNapok ${isToday ? 'isToday' : ''} ${isSelected ? 'selectedDay' : ''} ${isSunday ? 'sunDays' : ''}`}
              onClick={() => handleDayClick(day)}
              style={isSunday ? { pointerEvents: 'none' } : {}}
            >
              {format(day, 'd')}
            </div>
          );
        })}
      </div>

      <div className="iconContainer">
        <Tooltip title={longText}>
          <Button sx={{ m: 1 }}><RxQuestionMarkCircled className='icon' /></Button>
        </Tooltip>
      </div>

      <div className="appointContainrer">
        <div className="div-left">
          <div className="formWrapper">
            <form className="foglalasForm" onSubmit={handleSubmit}>
              {selectedDate && (
                <p id='fontunk' className="selectedDate">
                  Kiválasztott dátum: <strong>{format(selectedDate, 'yyyy. MMMM d.', { locale: hu })}</strong>
                </p>
              )}

              <input type="hidden" name="datum" value={selectedDate ? format(selectedDate, "yyyy. MMMM d. (EEEE)", { locale: hu }) : ""}  />
              <input type="hidden" name="szereles_tipus" value={selectedEgyeb}/>
              <div className="inputType">
                <label id='fontunk'><FaUser />Teljes név: </label>
                
                <input 
                  className="fullName" 
                  name="Nev"
                  type="text" 
                  placeholder='Bmws Botond'
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </div>

              <div className="inputType">
                <label id='fontunk'><FaPhoneSquareAlt />Telefonszám: </label>
                <input 
                  type="tel" 
                  placeholder='+36 30 123 4567'
                  value={phone}
                  onChange={(e) => setPhone(e.target.value.replace(/ /g, ''))}
                  pattern="\+[0-9]{8,14}"
                  title="Pl.: +36301234567 (szóközök nélkül)"
                />
              </div>

              <div className="inputType">
                <label id='fontunk'><FaHouseUser />Cím: </label>
                <input 
                  type="text" 
                  placeholder='1234 Budapest, Példa u. 5.'
                  value={address}
                  onChange={(e) => setAddress(e.target.value)}
                />
              </div>

              <div className="inputType">
                <label id='fontunk'><MdEmail />Email-cím: </label>
                <input 
                  className="email" 
                  type="email" 
                  name="email"
                  placeholder="telephely@email.hu"
                  value={email}
                  onChange={(e) => {
                    const value = e.target.value;
                    setEmail(value);
                    if (value === '') {
                      setEmailError('');
                    } else if (!validateEmail(value)) {
                      setEmailError('Nem megfelelő E-Mail formátum!');
                    } else {
                      setEmailError('');
                    }
                  }}
                />
                {emailError && <p className="error">{emailError}</p>}
              </div>

              <div className="inputType">
                <label id='fontunk'><FaScrewdriverWrench /> Szerelés típusa: </label>
                <select className="select" value={selectedEgyeb} onChange={(e) => setSelect(e.target.value)}>
                  <option value="">Válassz típust</option>
                  <option value="Olajcsere">Olajcsere</option>
                  <option value="Fékellenőrzés">Fékellenőrzés</option>
                  <option value="Gumi - és futómű">Gumi - és futómű</option>
                  <option value="Klíma töltés">Klíma töltés</option>
                  <option value="Akkumulátor">Akkumulátor</option>
                  <option value="Általános átvizsgálás">Általános átvizsgálás</option>
                  <option value="Egyéb">Egyéb</option>
                </select>
                {selectedEgyeb === "Egyéb" && (
                  <div className="inputType">
                    <label id='fontunkAsd' style={{ fontWeight: 'bold', color: '#aa0000' }}>
                      Amennyiben az autóján más problémát észlelt, keressen minket <a href="#elerhetosegek">telefonon</a> vagy <a href="#elerhetosegek">látogasson</a> el hozzánk időpontfoglalás érdekében!
                    </label>
                  </div>
                )}
              </div>

              <button id='fontunk' type="submit" className="submitButton">Időpont foglalás</button>
            </form>
          </div>
        </div>

        <div className="div-right">
          <div className="hoursAppoint">
            <span id="fontunkAsd" className="hourText">Hány órára szeretne időpontot foglalni</span>
          </div>
          <div>
            {HOURS.map((appoint) => (
              <div
                id='fontunkAsd'
                key={appoint}
                className={`appointmentHours ${selectedHour === appoint ? 'selectedHour' : ''}`}
                onClick={() => setSelectedHour(appoint)}
                style={{ cursor: 'pointer' }}
              >
                {appoint}
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Idopont;