import React from "react";
import './main.css';
import NavBar from '../nav/nav';
import Szolg from '../szolg/szolgaltatasok';
import Footer from '../footer/footer';
import Idopont from '../idopont/idopontfoglalas';
import Rolunk from '../rolunk/rolunk';
import SLIDER from '../nav/slider';



const Main = () => (
  <>
    <NavBar />
    <SLIDER/>
    <Rolunk/>
    <Szolg/>
    <Idopont/>
    <Footer/>
  </>
);

export default Main;