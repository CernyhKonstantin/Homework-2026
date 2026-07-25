const favoritesContainer =
    document.getElementById(
        "favoritesContainer"
    ) as HTMLDivElement;

const emptyMessage =
    document.getElementById(
        "emptyMessage"
    ) as HTMLDivElement;


window.addEventListener(
    "load",
    () => {

        loadFavorites();
    }
);


function loadFavorites(): void {

    const favorites =
        JSON.parse(
            localStorage.getItem(
                "favorites"
            ) || "[]"
        );

    favoritesContainer.innerHTML =
        "";

    if (
        favorites.length === 0
    ) {

        emptyMessage.classList.remove(
            "d-none"
        );

        return;
    }

    emptyMessage.classList.add(
        "d-none"
    );

    favorites.forEach(
        (
            meal: any
        ) => {

            createFavoriteCard(
                meal
            );
        }
    );
}


function createFavoriteCard(
    meal: any
): void {

    const col =
        document.createElement(
            "div"
        );

    col.className =
        "col-md-4";

    col.innerHTML =

        `
        <div class="card favorite-card h-100">

            <img
                src="${meal.strMealThumb}"
                class="card-img-top"
                alt="${meal.strMeal}">

            <div class="card-body">

                <h5 class="card-title">

                    ${meal.strMeal}

                </h5>

                <p class="card-text">

                    Category:
                    ${meal.strCategory || "Unknown"}

                    <br>

                    Area:
                    ${meal.strArea || "Unknown"}

                </p>

                <div class="d-grid gap-2">

                    <a
                        href="meal.html?id=${meal.idMeal}"
                        class="btn btn-primary">

                        Details

                    </a>

                    <button
                        class="btn btn-danger remove-btn">

                        Remove Favorite

                    </button>

                </div>

            </div>

        </div>
        `;

    const removeButton =
        col.querySelector(
            ".remove-btn"
        ) as HTMLButtonElement;

    removeButton.addEventListener(
        "click",
        () => {

            removeFavorite(
                meal.idMeal
            );
        }
    );

    favoritesContainer.append(
        col
    );
}


function removeFavorite(
    mealId: string
): void {

    let favorites =
        JSON.parse(
            localStorage.getItem(
                "favorites"
            ) || "[]"
        );

    favorites =
        favorites.filter(
            (
                meal: any
            ) => {

                return (
                    meal.idMeal !==
                    mealId
                );
            }
        );

    localStorage.setItem(
        "favorites",
        JSON.stringify(
            favorites
        )
    );

    loadFavorites();
}


function clearFavorites(): void {

    localStorage.removeItem(
        "favorites"
    );

    loadFavorites();
}


console.log(
    "Favorites page loaded."
);