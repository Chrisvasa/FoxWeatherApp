import React, { useEffect, useState } from "react";
import styled from "styled-components";
import Weather from "./Weather";
import Dropdown from "./Dropdown";
import ErrorPopup from "./ErrorPopup";
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

const cities = ["Stockholm", "Gothenburg", "Tokyo", "Chicago"];
const host = "https://localhost:7107";

function App() {
  const [selectedCity, setSelectedCity] = useState("");
  const [cityInfo, setCityInfo] = useState([]);
  const [cityHistory, setCityHistory] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (selectedCity) {
      Promise.all(
        cityHistory.map((city) =>
          axios
            .get(`${host}/api/weather/${city.toLowerCase()}`)
            .then((response) => response.data)
            .catch((error) => {
              if (error.response && error.response.status === 404) {
                setError("City not found");
              } else {
                setError("An error occurred. The server may be down.");
                console.log("An error occurred:", error);
              }
              return null;
            })
        )
      )
        .then((cityDataArray) => {
          setCityInfo(cityDataArray.filter(Boolean));
        })
        .catch((error) => {
          setError("An error occurred while fetching weather information.");
          console.log("An error occurred:", error);
        });
    }
  }, [cityHistory]);

  const handleCitySelect = (city) => {
    setSelectedCity(city);
    setCityHistory((prevHistory) => {
      const updatedHistory = [...prevHistory];
      // Keep only the latest 2 cities
      updatedHistory.splice(0, 2);
      // Add new city at the beginning
      updatedHistory.unshift(city);
      return updatedHistory;
    });
    console.log("Selected city:", city);
  };

  const handleCloseError = () => {
    setError(null);
  };

  return (
    <MainConatiner>
      <WeatherConatiner>
        <CityDropdownContainer>
          <Dropdown options={cities} onSelect={handleCitySelect} />
        </CityDropdownContainer>
        {cityInfo.length > 0 && selectedCity && (
          <Weather
            city={selectedCity}
            temp={cityInfo[0]?.degrees}
            weather={cityInfo[0]?.weather}
          />
        )}
      </WeatherConatiner>
      {cityHistory.length > 1 && (
        <WeatherConatiner>
          <h3>History:</h3>
          {cityHistory.slice(1).map((city, index) => (
            <Weather
              key={index}
              city={city}
              temp={cityInfo[index + 1]?.degrees}
              weather={cityInfo[index + 1]?.weather}
            />
          ))}
        </WeatherConatiner>
      )}
      {error && <ErrorPopup message={error} onClose={handleCloseError} />}
    </MainConatiner>
  );
}

export default App;
