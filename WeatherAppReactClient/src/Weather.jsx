import React from 'react';
import styled from 'styled-components';


const WeatherCard = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: #363546;
  color: #b4b4bf;
  
  width: 15rem;
  height: 15rem;
  
  border-radius: .3rem;
  font-family: Roboto, Inter, system-ui, Avenir, Helvetica, Arial, sans-serif;

  box-shadow: 0 10px 20px -15px black; 

  & h2{
    margin: 0;

    font-size: 1rem;

  }

  & .temp{
    
    font-size: 3rem;
    margin: 0;
  }
  & .weather{
    font-size: 1rem;
    margin: 0;
  }

`;

const Weather = ({ city, temp, weather }) => {
  return (
    <WeatherCard>
        <h2>{city}</h2>
        <p className='temp'>{temp}</p>
        <p className='weather'>{weather}</p>
    </WeatherCard>
  )
}

export default Weather