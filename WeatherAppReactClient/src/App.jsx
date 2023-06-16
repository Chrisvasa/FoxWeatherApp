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

  z-index: 1;
`;

const host = "http://dev.kjeld.io:20100";

function App() {

  const [selectedCity, setSelectedCity] = useState("");
  const [cityInfo, setCityInfo] = useState([]);
  const [cityHistory, setCityHistory] = useState([]);
  const [error, setError] = useState(null);
  const [fav, setFav] = useState([]);

  const [cityList, setCityList] = useState([]);

  useEffect(() => {

    axios.get(`${host}/api/cities/get`)
    .then((res) => res.data.cities)
    .then((cities) => setCityList(cities))

  }, [])



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


  //Add city as favorite
  const addToFav = () => {

    const updatedFav = [...fav, cityInfo[0]]
      
    setFav(updatedFav);
    setSelectedCity("");

    setCityList(prevCityList =>
      prevCityList.filter(city => city !== cityInfo[0].name)
    );
      
  }

  //Remove city as favorite
  const removeFav = (cityNameInput) => {
    // To display placeholder text if last favorite is removed
    if(selectedCity.length == 0 && favCount == 1){
      setCityInfo([]);
    }

    setFav(prevFav => prevFav.filter(city => city.name !== cityNameInput));
    setCityList(prevCityList => [...prevCityList, cityNameInput]);
      
  }

  return (
    <MainConatiner>
      <WeatherConatiner>
        <CityDropdownContainer>
          <Dropdown options={cityList} onSelect={handleCitySelect} />
        </CityDropdownContainer>
        {
          fav.length == 0 && cityInfo.length == 0 && (
            <h1>Select city above</h1>
          )
        }

        {
        /* Render favorites */
          fav.length > 0 && (
            fav.map((city, index) =>(
              <Weather
              key={index}
              city={city.name}
              temp={city.degrees}
              weather={city.weather}
              handleFav={removeFav}
              isFav
              
            />
            ))
            )
        }


        {cityInfo.length > 0 && selectedCity && (
          <Weather
            city={selectedCity}
            temp={cityInfo[0]?.degrees}
            weather={cityInfo[0]?.weather}
            handleFav={addToFav}

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
