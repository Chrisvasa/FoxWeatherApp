import React, { useEffect, useState } from "react";
import styled from "styled-components";
import Weather from "./Weather";
import Dropdown from "./Dropdown";
import axios from "axios";

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

const cities = ["Stockholm", "Gothenburg"];
const host = "https://localhost:7107";

function App() {
  const [selectedCity, setSelectedCity] = useState("");
  const [cityInfo, setCityInfo] = useState([]);

  useEffect(() => {
    axios.get(`${host}/weather/${selectedCity.toLowerCase()}`)
    .then(response => setCityInfo(response.data))
    
    
    

  },[selectedCity])

  


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
          <Weather city={cityInfo.name} temp={cityInfo.degrees} weather={cityInfo.weather} />
        )}

 

       
      </WeatherConatiner>
    </MainConatiner>
  );
}

export default App;
