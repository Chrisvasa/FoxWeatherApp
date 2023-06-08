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
`;

const Weather = ({ city, temp, weather }) => {
  return (
    <WeatherCard>
      <h2>{city}</h2>
      <p className="temp">{temp}</p>
      <p className="weather">{weather}</p>
    </WeatherCard>
  );
};

export default Weather;
