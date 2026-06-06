const API_URL =
    "https://www.themealdb.com/api/json/v1/1";

const mealContainer =
    document.getElementById(
        "mealContainer"
    ) as HTMLDivElement;


window.addEventListener(
    "load",
    async () => {

        const params =
            new URLSearchParams(
                window.location.search
            );

        const mealId =
            params.get(
                "id"
            );

        if (
            mealId === null
        ) {

            mealContainer.innerHTML =

                `
                <div class="alert alert-danger">

                    Meal ID not found.

                </div>
                `;

            return;
        }

        await loadMealDetails(
            mealId
        );
    }
);


async function loadMealDetails(
    mealId: string
): Promise<void> {

    try {

        const response =
            await fetch(
                `${API_URL}/lookup.php?i=${mealId}`
            );

        const data =
            await response.json();

        if (
            !data.meals
        ) {

            mealContainer.innerHTML =

                `
                <div class="alert alert-warning">

                    Meal not found.

                </div>
                `;

            return;
        }

        const meal =
            data.meals[0];

        renderMeal(
            meal
        );

    } catch {

        mealContainer.innerHTML =

            `
            <div class="alert alert-danger">

                Error loading meal.

            </div>
            `;
    }
}


function renderMeal(
    meal: any
): void {

    const ingredients =
        getIngredients(
            meal
        );

    mealContainer.innerHTML =

        `
        <div class="meal-details">

            <div class="row">

                <div class="col-lg-5">

                    <img
                        src="${meal.strMealThumb}"
                        alt="${meal.strMeal}"
                        class="img-fluid">

                </div>

                <div class="col-lg-7">

                    <h1 class="meal-title">

                        ${meal.strMeal}

                    </h1>

                    <div class="meal-meta">

                        <strong>Category:</strong>
                        ${meal.strCategory}

                        <br>

                        <strong>Area:</strong>
                        ${meal.strArea}

                    </div>

                    <button
                        id="favoriteButton"
                        class="btn btn-warning">

                        ❤️ Add to Favorites

                    </button>

                </div>

            </div>

            <hr>

            <h3>

                Ingredients

            </h3>

            <ul class="ingredients-list">

                ${ingredients}

            </ul>

            <h3>

                Instructions

            </h3>

            <p class="instructions">

                ${meal.strInstructions}

            </p>

            ${
                meal.strYoutube
                ?
                `
                <a
                    href="${meal.strYoutube}"
                    target="_blank"
                    class="btn btn-danger youtube-button">

                    Watch on YouTube

                </a>
                `
                :
                ""
            }

        </div>
        `;

    const favoriteButton =
        document.getElementById(
            "favoriteButton"
        ) as HTMLButtonElement;

    favoriteButton.addEventListener(
        "click",
        () => {

            addToFavorites(
                meal
            );
        }
    );
}


function getIngredients(
    meal: any
): string {

    let html =
        "";

    for (
        let i = 1;
        i <= 20;
        i++
    ) {

        const ingredient =
            meal[
                `strIngredient${i}`
            ];

        const measure =
            meal[
                `strMeasure${i}`
            ];

        if (
            ingredient &&
            ingredient.trim() !== ""
        ) {

            html +=

                `
                <li>

                    ${measure}
                    ${ingredient}

                </li>
                `;
        }
    }

    return html;
}


function addToFavorites(
    meal: any
): void {

    const favorites =
        JSON.parse(
            localStorage.getItem(
                "favorites"
            ) || "[]"
        );

    const exists =
        favorites.some(
            (
                item: any
            ) => {

                return (
                    item.idMeal ===
                    meal.idMeal
                );
            }
        );

    if (
        exists
    ) {

        alert(
            "Meal already exists in favorites."
        );

        return;
    }

    favorites.push(
        {
            idMeal:
                meal.idMeal,

            strMeal:
                meal.strMeal,

            strMealThumb:
                meal.strMealThumb,

            strCategory:
                meal.strCategory,

            strArea:
                meal.strArea
        }
    );

    localStorage.setItem(
        "favorites",
        JSON.stringify(
            favorites
        )
    );

    alert(
        "Meal added to favorites."
    );
}