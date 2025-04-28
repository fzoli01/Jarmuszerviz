import React, { useState } from "react";
import './nav.css';
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import logo from '../../assets/logo.jpeg';
import { FaBars, FaTimes } from "react-icons/fa";

function NavBar(){
    const [menuOpen, setMenuOpen] = useState(false);

    const toggleMenu = () => {
        setMenuOpen(!menuOpen);
    }

    return(
    <div id="fooldal">
        <div  className="navbar">
            <div className="topDiv">
                <div className="navLeft">
                    <img src={logo} alt="logo" className="lsLogo" />
                </div>
                <div className="navRight">
                    <div className="hamburger" onClick={toggleMenu}>
                        {menuOpen ? <FaTimes size={30} /> : <FaBars size={30} />}
                    </div>
                    <ul className={menuOpen ? "nav-menu active" : "nav-menu"} id='fontunk'>
                        <li><a href="#fooldal" onClick={toggleMenu}>Főoldal</a></li>
                        <li><a href="#rolunk" onClick={toggleMenu}>Rólunk</a></li>
                        <li><a href="#szolgaltatasok" onClick={toggleMenu}>Szolgáltatások</a></li>
                        <li><a href="#idopontfoglalas" onClick={toggleMenu}>Időpontfoglalás</a></li>
                        <li><a href="#elerhetosegek" onClick={toggleMenu}>Elérhetőségek</a></li>
                    </ul>
                </div>
            </div>
        </div>  
    </div>
    )
}

export default NavBar;
