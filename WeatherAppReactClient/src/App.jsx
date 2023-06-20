import React, { useEffect, useState } from "react";
import styled from "styled-components";
import Weather from "./Weather";
import Dropdown from "./Dropdown";
import ErrorPopup from "./components/ErrorPopup";
import {
  handleError,
  clearErrors,
  getErrorsFromLocalStorage
} from "./utils/ErrorUtils";
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
  const [fav, setFav] = useState([]);
  const [errors, setErrors] = useState([]);

  const [cityList, setCityList] = useState([]);

  useEffect(() => {
    axios
      .get(`${host}/api/cities/get`)
      .then((res) => res.data.cities)
      .then(async (cities) => {
        console.log(cities);

        const cityList = cities
          .filter((city) => !city.isFavorite)
          .map((city) => city.name);
        const favoriteList = cities.filter((city) => city.isFavorite);

        const fetchFavoriteWeatherData = favoriteList.map(async (city) => {
          const response = await axios.get(
            `${host}/api/weather/${city.name.toLowerCase()}`
          );
          return response.data;
        });

        try {
          const favoriteWeatherData = await Promise.all(
            fetchFavoriteWeatherData
          );
          setFav(favoriteWeatherData);
        } catch (error) {
          handleError("Error fetching favorite weather data:", error);
        }

        setCityList(cityList);
      })
      .catch((error) => {
        handleError(
          `Not able to fetch data from API. Please try again later.`,
          error
        );
      });
  }, []);

  useEffect(() => {
    if (selectedCity) {
      Promise.all(
        cityHistory.map((city) =>
          axios
            .get(`${host}/api/weather/${city.toLowerCase()}`)
            .then((response) => response.data)
            .catch((error) => {
              if (error.response && error.response.status === 404) {
                handleError("City not found", error);
              } else {
                handleError(
                  "An error occurred. The server may be down.",
                  error
                );
              }
              return null;
            })
        )
      )
        .then((cityDataArray) => {
          setCityInfo(cityDataArray.filter(Boolean));
        })
        .catch((error) => {
          handleError(
            "An error occurred while fetching weather information.",
            error
          );
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
  };

  useEffect(() => {
    setErrors(getErrorsFromLocalStorage());
  }, []);

  const handleCloseError = () => {
    setErrors([]);
    clearErrors();
  };

  //Add city as favorite
  const addToFav = () => {
    const currentCity = cityInfo[0];

    const updatedFav = [...fav, currentCity];

    setFav(updatedFav);
    setSelectedCity("");
    axios.get(`${host}/api/favorite/add/${currentCity.name}`);

    setCityList((prevCityList) =>
      prevCityList.filter((city) => city !== currentCity.name)
    );
  };

  //Remove city as favorite
  const removeFav = async (cityNameInput) => {
    console.log(cityNameInput);
    // To display placeholder text if last favorite is removed
    if (selectedCity.length == 0 && fav.length == 1) {
      setCityInfo([]);
    }

    setFav((prevFav) => prevFav.filter((city) => city.name !== cityNameInput));
    setCityList((prevCityList) => [...prevCityList, cityNameInput]);

    axios.get(`${host}/api/favorite/remove/${cityNameInput}`);
  };

  return (
    <MainConatiner>
      <WeatherConatiner>
        <CityDropdownContainer>
          <Dropdown options={cityList} onSelect={handleCitySelect} />
        </CityDropdownContainer>
        {fav.length == 0 && cityInfo.length == 0 && <h1>Select city above</h1>}

        {
          /* Render favorites */
          fav.length > 0 &&
            fav.map((city, index) => (
              <Weather
                key={index}
                city={city.name}
                temp={city.degrees}
                weather={city.weather}
                handleFav={removeFav}
                isFav
              />
            ))
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
      {errors.length > 0 && (
        <ErrorPopup
          messages={errors.map((error) => error.message)}
          onClose={handleCloseError}
          length={errors.length}
        />
      )}
    </MainConatiner>
  );
}

export default App;
