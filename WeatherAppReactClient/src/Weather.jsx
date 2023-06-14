import React from "react";
import styled from "styled-components";

const WeatherCard = styled.div`
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
`;



const leadingUpperCase = (str) => str.charAt(0).toUpperCase() + str.slice(1);

const Weather = (props) => {
  const { city, temp, weather, handleFav, isFav} = props;

  

  return (
    <WeatherCard className={ isFav ? 'fav' : 'notFav'}>
      <h2>{leadingUpperCase(city)}</h2>
      <p className="temp">{temp}°C</p>
      <p className="weather">{weather}</p>
      <button onClick={handleFav}>+</button>
    </WeatherCard>
  );
};

export default Weather;
