import React from "react";
import styled from "styled-components";

const WeatherCard = styled.div`
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: #0086a5;
  color: #ffffff;
  flex-grow: 3;

  width: 15rem;
  height: 15rem;

  border-radius: 0.45rem;
  font-family: Roboto, Inter, system-ui, Avenir, Helvetica, Arial, sans-serif;

  box-shadow: 0 10px 20px -15px black;

  

  & h2 {
    font-size: 1.5rem;
    font-weight: 600;
  }

  & .temp {
    font-size: 3rem;
    font-weight: 700;
    color: #ef8354;
  }
  & .weather {
    font-size: 1.25rem;
  }

  &.fav{
    background: #007268;
 
  }

  & a{
    text-decoration: none;
  }
`;


const FavButton = styled.div`
  position: absolute;
  right: 0;
  bottom: 0;
  margin: 0 .5rem .5rem 0;

  display: flex;
  justify-content: center;
  
  background: #71ca69;
  border-radius: 100%;
  height: 25px;
  width: 25px;

  color: white;

  font-size: 1rem;

  &.remove-btn{

    background: #ca6969;


  }
  
  




`;



const leadingUpperCase = (str) => {
  if(str != undefined){
    
    return str.charAt(0).toUpperCase() + str.slice(1)
  }

};

const Weather = (props) => {
  const { city, temp, weather, handleFav, isFav} = props;

  

  return (
    <WeatherCard className={ isFav ? 'fav' : 'notFav'}>
      <h2>{leadingUpperCase(city)}</h2>
      <p className="temp">{temp}°C</p>
      <p className="weather">{weather}</p>
      <a href="#" onClick={handleFav}>
        {isFav ? null : <FavButton>+</FavButton>}
      </a>
    </WeatherCard>
  );
};

export default Weather;
