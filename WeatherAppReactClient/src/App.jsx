import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import styled from 'styled-components';
import Weather from './Weather';

const MainConatiner = styled.div`

  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 2rem;
  background: #302f3d;
  height: 100vh;
  width: 100vw;
  padding-top: 2rem; 

`;

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

function App() {

  return (
    <MainConatiner>
      {/* For each city, generate card, dummy info below */}
      <Weather city={"Stockholm"} temp={"15°C"} weather={"Sunny"}/>
      <Weather city={"Oslo"} temp={"10°C"} weather={"Rainy"}/>



    </MainConatiner>
  )
}

export default App
