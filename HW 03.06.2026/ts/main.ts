const API_URL =
    "https://www.themealdb.com/api/json/v1/1";

const mealsContainer =
    document.getElementById(
        "mealsContainer"
    ) as HTMLDivElement;

const categoriesContainer =
    document.getElementById(
        "categoriesContainer"
    ) as HTMLDivElement;

const searchInput =
    document.getElementById(
        "searchInput"
    ) as HTMLInputElement;

const searchButton =
    document.getElementById(
        "searchButton"
    ) as HTMLButtonElement;

const randomButton =
    document.getElementById(
        "randomButton"
    ) as HTMLButtonElement;


window.addEventListener(
    "load",
    async () => {

        await loadCategories();

        await loadPopularMeals();
    }
);


async function loadPopularMeals(): Promise<void> {

    const response =
        await fetch(
            `${API_URL}/search.php?s=`
        );

    const data =
        await response.json();

    renderMeals(
        data.meals || []
    );
}


searchButton.addEventListener(
    "click",
    async () => {

        const text =
            searchInput.value.trim();

        if (
            text.length === 0
        ) {

            alert(
                "Enter meal name"
            );

            return;
        }

        const response =
            await fetch(
                `${API_URL}/search.php?s=${text}`
            );

        const data =
            await response.json();

        renderMeals(
            data.meals || []
        );
    }
);


async function loadCategories(): Promise<void> {

    const response =
        await fetch(
            `${API_URL}/categories.php`
        );

    const data =
        await response.json();

    categoriesContainer.innerHTML =
        "";

    data.categories.forEach(
        (
            category: any
        ) => {

            const col =
                document.createElement(
                    "div"
                );

            col.className =
                "col-md-3";

            col.innerHTML =

                `
                <button
                    class="btn btn-outline-primary w-100 category-btn">

                    ${category.strCategory}

                </button>
                `;

            const button =
                col.querySelector(
                    "button"
                ) as HTMLButtonElement;

            button.addEventListener(
                "click",
                async () => {

                    await loadMealsByCategory(
                        category.strCategory
                    );
                }
            );

            categoriesContainer.append(
                col
            );
        }
    );
}


async function loadMealsByCategory(
    category: string
): Promise<void> {

    const response =
        await fetch(
            `${API_URL}/filter.php?c=${category}`
        );

    const data =
        await response.json();

    renderMeals(
        data.meals || []
    );
}


randomButton.addEventListener(
    "click",
    async () => {

        const response =
            await fetch(
                `${API_URL}/random.php`
            );

        const data =
            await response.json();

        const meal =
            data.meals[0];

        showRandomMeal(
            meal
        );
    }
);

function showRandomMeal(
    meal: any
): void {

    const title =
        document.getElementById(
            "randomMealTitle"
        ) as HTMLHeadingElement;

    const body =
        document.getElementById(
            "randomMealBody"
        ) as HTMLDivElement;

    title.innerText =
        meal.strMeal;

    body.innerHTML =

        `
        <img
            src="${meal.strMealThumb}"
            class="img-fluid rounded mb-3">

        <h5>
            Category:
            ${meal.strCategory}
        </h5>

        <h5>
            Area:
            ${meal.strArea}
        </h5>

        <p>
            ${meal.strInstructions}
        </p>
        `;

    const modal =
        new (window as any).bootstrap.Modal(
            document.getElementById(
                "randomMealModal"
            )
        );

    modal.show();
}


function renderMeals(
    meals: any[]
): void {

    mealsContainer.innerHTML =
        "";

    if (
        meals.length === 0
    ) {

        mealsContainer.innerHTML =

            `
            <div class="col-12 text-center">

                <h3>
                    No meals found
                </h3>

            </div>
            `;

        return;
    }

    meals.forEach(
        (
            meal: any
        ) => {

            const col =
                document.createElement(
                    "div"
                );

            col.className =
                "col-md-4";

            col.innerHTML =

                `
                <div class="card meal-card h-100">

                    <img
                        src="${meal.strMealThumb}"
                        class="card-img-top">

                    <div class="card-body">

                        <h5 class="card-title">

                            ${meal.strMeal}

                        </h5>

                        <div class="card-buttons">

                            <a
                                href="meal.html?id=${meal.idMeal}"
                                class="btn btn-primary">

                                Details

                            </a>

                            <button
                                class="btn btn-warning favorite-btn">

                                Favorite

                            </button>

                        </div>

                    </div>

                </div>
                `;

            const favoriteButton =
                col.querySelector(
                    ".favorite-btn"
                ) as HTMLButtonElement;

            favoriteButton.addEventListener(
                "click",
                () => {

                    addToFavorites(
                        meal
                    );
                }
            );

            mealsContainer.append(
                col
            );
        }
    );
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
            "Already added"
        );

        return;
    }

    favorites.push(
        meal
    );

    localStorage.setItem(
        "favorites",
        JSON.stringify(
            favorites
        )
    );

    alert(
        "Added to favorites"
    );
}