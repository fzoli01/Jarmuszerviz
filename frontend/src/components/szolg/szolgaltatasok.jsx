import React from 'react';
import Olaj from '../../assets/oilChange.avif'
import AC from '../../assets/AC.avif'
import SUSP from '../../assets/suspAlignment.avif'
import BRAKE from '../../assets/brakePads.avif'
import HEADLIGHTS from '../../assets/headlights.avif'
import COMMON from '../../assets/common.avif'
import '../szolg/szolgaltatasok.css'

function Szolg(){
    return(
    <div id="szolgaltatasok" className="background">
        <div>
            <span className="textSzolg" id='fontunk'>Szolgáltatásaink</span>
        </div>
        
        

        <div className="services-container">
            <div className="service-card">
                <img src={Olaj} alt="Olajcsere" className="service-img" />
                <h3 className='title' id='fontunk'>Olajcsere</h3>
                <p className='descript' id='fontunkAsd'>Minőségi olaj és szűrőcsere, hogy a motorja hibátlanul működjön.</p>
            </div>

            <div className="service-card">
                <img src={BRAKE} alt="Fékellenőrzés" className="service-img" />
                <h3 className='title' id='fontunk'>Fékellenőrzés</h3>
                <p className='descript' id='fontunkAsd'>Betétek, tárcsák, folyadékok – mindent ellenőrzünk a biztonságáért.</p>
            </div>

            <div className="service-card">
                <img src={SUSP} alt="Gumi" className="service-img" />
                <h3 className='title' id='fontunk'>Gumi- és futómű</h3>
                <p className='descript' id='fontunkAsd'>Centírozás, állapotellenőrzés, hogy mindig stabilan guruljon tovább.</p>
            </div>

            <div className="service-card">
                <img src={AC} alt="Klíma töltés" className="service-img" />
                <h3 className='title' id='fontunk'>Klíma töltés</h3>
                <p className='descript' id='fontunkAsd'>Hűtés, páramentesítés, ellenőrzés – hogy minden évszakban komfortban utazzon.</p>
            </div>

            <div className="service-card">
                <img src={HEADLIGHTS} alt="Akkumulátor" className="service-img" />
                <h3 className='title' id='fontunk'>Akkumulátor teszt</h3>
                <p className='descript' id='fontunkAsd'>Megnézzük az indítóképességet, cserélünk, ha kell – ne hagyjon cserben reggel!</p>
            </div>

            <div className="service-card">
                <img src={COMMON} alt="Átvizsgálás" className="service-img" />
                <h3 className='title' id='fontunk'>Általános átvizsgálás</h3>
                <p className='descript' id='fontunkAsd'>Autóvásálás, műszaki vizsga előtt átnézzük gépjárművét.</p>
            </div>
        </div>
    </div>
    )
}

export default Szolg;