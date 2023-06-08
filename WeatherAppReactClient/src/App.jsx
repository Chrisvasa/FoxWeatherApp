import React, { useState } from "react";
import styled from "styled-components";
import Weather from "./Weather";
import Dropdown from "./Dropdown";

const MainConatiner = styled.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 2rem;
  background: #302f3d;
  min-height: 100vh;
  width: 100vw;
`;
const WeatherConatiner = styled.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 2rem;
  background: #f58f29;
  height: 100%;
  padding: 2rem;
  margin: 2rem;
  border-radius: 0.45rem;
  box-shadow: 0 10px 20px -15px black;
`;
const CityDropdownContainer = styled.div`
  display: flex;
  justify-content: center;
  margin: auto;
  width: 100%;
`;

const cities = ["Stockholm", "Oslo", "London", "Tokyo"];

function App() {
  const [selectedCity, setSelectedCity] = useState("");

  const handleCitySelect = (city) => {
    setSelectedCity(city);
    console.log("Selected city:", city);
  };

  return (
    <MainConatiner>
      <WeatherConatiner>
        <CityDropdownContainer>
          <Dropdown options={cities} onSelect={handleCitySelect} />
        </CityDropdownContainer>
        {selectedCity && (
          <Weather city={selectedCity} temp={"150°C"} weather={"Sunny"} />
        )}

        {/* For each city, generate card, dummy info below */}
        <Weather city={"Stockholm"} temp={"15°C"} weather={"Sunny"} />
        <Weather city={"Oslo"} temp={"10°C"} weather={"Rainy"} />
        <Weather city={"London"} temp={"5°C"} weather={"Rainy"} />
        <Weather city={"Tokyo"} temp={"20°C"} weather={"Cloudy"} />
      </WeatherConatiner>
    </MainConatiner>
  );
}

export default App;
