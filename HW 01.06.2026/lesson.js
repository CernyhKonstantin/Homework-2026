const apiKey =
    "feef6c71";


const movieInput =
    document.getElementById(
        "movieInput"
    );

const typeSelect =
    document.getElementById(
        "typeSelect"
    );

const searchButton =
    document.getElementById(
        "searchButton"
    );

const moviesContainer =
    document.getElementById(
        "moviesContainer"
    );

const pagination =
    document.getElementById(
        "pagination"
    );

const detailsContainer =
    document.getElementById(
        "detailsContainer"
    );


let currentTitle = "";

let currentType = "";

let currentPage = 1;

let totalPages = 1;


searchButton.addEventListener(
    "click",
    function () {

        currentPage = 1;

        searchMovies();
    }
);


async function searchMovies() {

    currentTitle =
        movieInput.value.trim();

    currentType =
        typeSelect.value;

    if (
        currentTitle === ""
    ) {

        alert(
            "Enter movie title"
        );

        return;
    }

    const url =

        `https://www.omdbapi.com/?apikey=${apiKey}&s=${currentTitle}&type=${currentType}&page=${currentPage}`;

    const response =
        await fetch(url);

    const data =
        await response.json();

    if (
        data.Response ===
        "False"
    ) {

        moviesContainer.innerHTML =
            "<h2>Movie not found!</h2>";

        pagination.innerHTML =
            "";

        return;
    }

    totalPages =
        Math.ceil(
            data.totalResults / 10
        );

    showMovies(
        data.Search
    );

    createPagination();
}


function showMovies(
    movies
) {

    moviesContainer.innerHTML =
        "";

    movies.forEach(
        function (movie) {

            const card =
                document.createElement(
                    "div"
                );

            card.className =
                "movie-card";

            card.innerHTML =

                `
                <img src="${movie.Poster}">

                <div class="movie-info">

                    <div>

                        <p>${movie.Type}</p>

                        <h3>${movie.Title}</h3>

                        <p>${movie.Year}</p>

                    </div>

                    <button
                        class="details-btn"
                        onclick="showDetails('${movie.imdbID}')">

                        Details

                    </button>

                </div>
                `;

            moviesContainer.append(
                card
            );
        }
    );
}


function createPagination() {

    pagination.innerHTML =
        "";

    const prev =
        document.createElement(
            "button"
        );

    prev.innerText =
        "<<";

    prev.className =
        "page-btn";

    prev.onclick =
        function () {

            if (
                currentPage > 1
            ) {

                currentPage--;

                searchMovies();
            }
        };

    pagination.append(
        prev
    );

    for (
        let i = 1;
        i <= totalPages &&
        i <= 10;
        i++
    ) {

        const btn =
            document.createElement(
                "button"
            );

        btn.innerText =
            i;

        btn.className =
            "page-btn";

        btn.onclick =
            function () {

                currentPage = i;

                searchMovies();
            };

        pagination.append(
            btn
        );
    }

    const next =
        document.createElement(
            "button"
        );

    next.innerText =
        ">>";

    next.className =
        "page-btn";

    next.onclick =
        function () {

            if (
                currentPage <
                totalPages
            ) {

                currentPage++;

                searchMovies();
            }
        };

    pagination.append(
        next
    );
}


async function showDetails(
    imdbID
) {

    const response =
        await fetch(

            `https://www.omdbapi.com/?apikey=${apiKey}&i=${imdbID}`

        );

    const movie =
        await response.json();

    detailsContainer.innerHTML =

        `
        <div class="details-box">

            <img src="${movie.Poster}">

            <div class="details-info">

                <div class="info-row">
                    <div class="info-title">Title:</div>
                    <div>${movie.Title}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Released:</div>
                    <div>${movie.Released}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Genre:</div>
                    <div>${movie.Genre}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Country:</div>
                    <div>${movie.Country}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Director:</div>
                    <div>${movie.Director}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Writer:</div>
                    <div>${movie.Writer}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Actors:</div>
                    <div>${movie.Actors}</div>
                </div>

                <div class="info-row">
                    <div class="info-title">Awards:</div>
                    <div>${movie.Awards}</div>
                </div>

            </div>

        </div>
        `;
}