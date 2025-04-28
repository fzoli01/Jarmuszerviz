import React from 'react';
import '../footer/footer.css';

function Footer() {
    return (
    <div className="footerDiv">
        <div id="elerhetosegek">
            <div className="left-div">
                <span className="text" id='fontunk'>
                    Telephelyünk
                </span>
                <iframe
                    src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d10837.922004390597!2d16.622207747613533!3d47.22674633145008!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x476eb9d57f3c48eb%3A0xffb223d4850dca93!2sHorv%C3%A1th%20Boldizs%C3%A1r%20K%C3%B6zgazdas%C3%A1gi%20%C3%A9s%20Informatikai%20Technikum!5e0!3m2!1shu!2shu!4v1744014655969!5m2!1shu!2shu"
                    title="Telephelyünk elhelyezkedése"
                    width="80%"
                    height="60%"
                    style={{ border: 0 }}
                    allowFullScreen=""
                    loading="lazy"
                    referrerPolicy="no-referrer-when-downgrade"
                    className="terkep"
                ></iframe>
            </div>

            <footer className="footer">
                <div className="right-div">
                    <li className="listRight">
                        <ul id='fontunkAsd' className="adMail">Cím: Szombathely, Zrínyi Ilona u. 12, 9700</ul>
                        <ul id='fontunkAsd' className="tel">Tel: +36 30 123 4567</ul>
                        <ul id='fontunkAsd' className="adMail">E-Mail: JarmuSzerviz@outlook.hu</ul>
                    </li>
                </div>
                <div className="left-divTwo">
                    <li className="listLeft">
                        <ul id='fontunk'>Facebook:</ul>
                        <ul id='fontunk'>Instagram:</ul>
                        <ul id='fontunk'>Twitter:</ul>
                    </li>
                </div>
                <div className="rights">
                    © 2025 Los Santos szervíz - Minden jog fenntartva
                </div>
            </footer>
        </div>
        </div>
    );
}

export default Footer;