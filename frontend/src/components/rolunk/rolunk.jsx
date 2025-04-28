import React from 'react';
import '../rolunk/rolunk.css'
import Faq from "react-faq-component";
import photoS from '../../assets/projektKep.png';
import zoli from '../../assets/foldessi.jpg';
import martin from '../../assets/martin.jpg';
import botond from '../../assets/botondo.jpg';

function Rolunk() {
  const data = {
    rows: [
        {
            title: "Megbízhatóság első helyen",
            content: `Nálunk az ügyfél bizalma mindennél fontosabb, gépjárműve javítását lelkiismeretesen, átláthatóan végezzük.`,
        },
        {
            title: "Több éves szakmai tapasztalat",
            content:
                "Tapasztalt szerelőcsapatunk gyorsan felismeri a problémát, melyet hatékonyan megoldunk, felesleges költségek nélkül.",
        },
        {
            title: "Gyors és precíz munkavégzés",
            content: `Tudjuk, hogy mindenki számára mennyire fontos az idő, a lehető leggyorsabban dolgozunk anélkül, hogy a minőség rovására menne.`,
        },
        {
            title: "Modern eszközök, korszerű diagnosztika",
            content: "A legújabb technológiával dolgozunk, hogy minden hibát pontosan feltárjunk.",
        },
    ],
};

const styles = {
  bgColor: "white",
  rowTitleColor: "red",
  arrowColor: "red",
};

const config = {    
  tabFocus: true
};


  return (
    <div className="rolunkDiv" id="rolunk">
      <div>
        <span className="textRolunk" id='fontunk'>Miért mi?</span>
      </div>

      <div className="ketteOsztott">
        <div className="nemOsztott">
          <div className="FAQ">
            <Faq
                  data={data}
                  styles={styles}
                  config={config}
            />
          </div>
          
          <div className="dolgozoinkFelirat">
            <h2 id="fontunk">Szerelőink</h2>
            <hr className="elvalasztoVonal" />
          </div>
          <div className="nagyKartya">
            
            <div className="cardsDiv">
                <p className="cardSzoveg" id="fontunkAsd">Földessi Zoltán</p>
                <div className="card1">
                
                    <div className="cardFront">
                      <img src={zoli} className="pictures" alt="Nagy Botond"></img>
                    </div>
                    
                    <div className="cardBack">
                    <p className="cardSzoveg" id="fontunkAsd">"Számomra legfontosabb az ügyfél elégedettsége. Minden autót úgy javítok, mintha a sajátom lenne."</p>
                  </div>
                </div>
            </div>
            
            <div className="cardsDiv">
              <p className="cardSzoveg" id="fontunkAsd">Nagy Botond</p>
                <div className="card1">
                    <div className="cardFront">
                      <img src={botond} className="pictures" alt="Nagy Botond"></img>
                    </div>

                    <div className="cardBack">
                      <p className="cardSzoveg" id="fontunkAsd">"Szenvedélyem az autók világa. Legyen szó kisebb javításról vagy komolyabb munkákról."</p>
                  </div>
                </div>
            </div>
            
            <div className="cardsDiv">
              <p className="cardSzoveg" id="fontunkAsd">Hajba Martin</p>
                <div className="card1">
                    <div className="cardFront">
                      <img src={martin} alt="Nagy Botond" className="pictures"></img>
                    </div>

                    <div className="cardBack">
                      <p className="cardSzoveg" id="fontunkAsd">"Ügyfeleink járművei mindig a legjobb kezekben vannak. Számomra a minőség és a pontosság alapkövetelmény."</p>
                  </div>
                </div>
            </div>
          </div>
          

        </div>

        <div className="photoShop">
          <img src={photoS} alt="kép" />
        </div>
      </div>

    </div>
  );
}

export default Rolunk;
