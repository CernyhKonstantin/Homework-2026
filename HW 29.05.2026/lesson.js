const countrySelect =
    document.getElementById(
        "countrySelect"
    );

const citySelect =
    document.getElementById(
        "citySelect"
    );

const weatherButton =
    document.getElementById(
        "weatherButton"
    );

const weatherResult =
    document.getElementById(
        "weatherResult"
    );


const weatherApiKey =
    "0006fae8c55ab1d0d3917f71859c8058";


const countries =
[
    {
        name: "Germany",
        cities: [
            "Berlin",
            "Hamburg",
            "Munich"
        ]
    },

    {
        name: "Ukraine",
        cities: [
            "Kyiv",
            "Lviv",
            "Odesa"
        ]
    },

    {
        name: "USA",
        cities: [
            "New York",
            "Chicago",
            "Los Angeles"
        ]
    },

    {
        name: "France",
        cities: [
            "Paris",
            "Lyon",
            "Marseille"
        ]
    }
];


function loadCountries() {

    countries.forEach(
        (country) => {

            const option =
                document.createElement(
                    "option"
                );

            option.value =
                country.name;

            option.innerText =
                country.name;

            countrySelect.append(
                option
            );
        }
    );
}


countrySelect.addEventListener(
    "change",
    function () {

        citySelect.innerHTML =

            `
            <option value="">
                Select City
            </option>
            `;

        const selectedCountry =
            countries.find(
                (country) =>

                    country.name ===
                    countrySelect.value
            );


        if (selectedCountry) {

            selectedCountry.cities.forEach(
                (city) => {

                    const option =
                        document.createElement(
                            "option"
                        );

                    option.value =
                        city;

                    option.innerText =
                        city;

                    citySelect.append(
                        option
                    );
                }
            );
        }
    }
);


function getWeatherIcon(description) {

    description =
        description.toLowerCase();


    if (
        description.includes(
            "clear"
        )
    ) {

        return "☀️";
    }


    if (
        description.includes(
            "rain"
        )
    ) {

        return "🌧️";
    }


    if (
        description.includes(
            "cloud"
        )
    ) {

        return "☁️";
    }


    if (
        description.includes(
            "snow"
        )
    ) {

        return "❄️";
    }


    return "🌍";
}


async function getWeather() {

    const city =
        citySelect.value;


    if (city === "") {

        weatherResult.innerHTML =

            `
            <p>
                Please select a city
            </p>
            `;

        return;
    }


    try {

        const response =
            await fetch(

                `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${weatherApiKey}&units=metric`

            );


        if (!response.ok) {

            throw new Error(
                "Weather not found"
            );
        }


        const data =
            await response.json();


        const temperature =
            data.main.temp;

        const feelsLike =
            data.main.feels_like;

        const humidity =
            data.main.humidity;

        const description =
            data.weather[0].description;

        const country =
            data.sys.country;

        const icon =
            getWeatherIcon(
                description
            );


        weatherResult.innerHTML =

            `
            <div class="weather-card">

                <h2>
                    ${city}, ${country}
                </h2>

                <div class="weather-icon">
                    ${icon}
                </div>

                <div class="temperature">
                    ${temperature}°C
                </div>

                <p>
                    Weather:
                    ${description}
                </p>

                <p>
                    Feels Like:
                    ${feelsLike}°C
                </p>

                <p>
                    Humidity:
                    ${humidity}%
                </p>

            </div>
            `;


        console.log(data);

    } catch (error) {

        weatherResult.innerHTML =

            `
            <p>
                ${error.message}
            </p>
            `;
    }
}


weatherButton.addEventListener(
    "click",
    getWeather
);


loadCountries();