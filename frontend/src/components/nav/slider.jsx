import React from "react";
import './slider.css';
import background from '../../assets/background.jpg';
import background2 from '../../assets/kep2.jpeg'
import background3 from '../../assets/background3.jpg';
import Slider from "react-slick";

function SLIDER(){
    const settings = {
            infinite: true,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            speed: 500,
            autoplaySpeed: 4000,
            cssEase: "linear"
          };
    return(
        <div className="sliderDiv">
            <Slider {...settings}>
                <div className="image-container">
                    <h3>
                    <img src={background} className="bg" alt="Background 1" />
                    <p className="image-button">Nálunk minden autó a legnagyobb figyelmet kapja.</p>
                    </h3>
                </div>
                <div className="image-container">
                    <h3>
                    <img src={background2} className="bg" alt="Background 2" />
                    <p className="image-button" >A minőséget és megbízhatóságot helyezzük előtérbe, hogy autója mindig a legjobb állapotban legyen.</p>
                    </h3>
                </div>
                <div className="image-container">
                    <h3>
                    <img src={background3} className="bg" alt="Background 3" />
                    <p className="image-button">Bízza ránk járművét, garantáljuk, hogy újra zökkenőmentesen fog működni.</p>
                </h3>
                </div>
            </Slider>
        </div>
    )
}

export default SLIDER;