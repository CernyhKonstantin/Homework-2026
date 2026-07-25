// TASK 3

function washDishes() {

    return new Promise(
        (resolve) => {

            setTimeout(() => {

                resolve(
                    "Dishes are washed"
                );

            }, 2000);
        }
    );
}


function cleanRoom() {

    return new Promise(
        (resolve) => {

            setTimeout(() => {

                resolve(
                    "Room is cleaned"
                );

            }, 4000);
        }
    );
}


function makeDinner() {

    return new Promise(
        (resolve) => {

            setTimeout(() => {

                resolve(
                    "Dinner is ready"
                );

            }, 7000);
        }
    );
}


document
    .getElementById(
        "startTasksButton"
    )
    .addEventListener(
        "click",
        function () {

            const result =
                document.getElementById(
                    "task3Result"
                );

            result.innerHTML = "";


            washDishes()

                .then((message) => {

                    result.innerHTML +=
                        message + "<br>";

                    return cleanRoom();
                })

                .then((message) => {

                    result.innerHTML +=
                        message + "<br>";

                    return makeDinner();
                })

                .then((message) => {

                    result.innerHTML +=
                        message;
                });
        }
    );

// TASK 4

function sortArray(array) {

    return new Promise(
        (resolve, reject) => {

            if (
                array.length === 0
            ) {

                reject(
                    "Array is empty"
                );

            } else {

                setTimeout(() => {

                    const sorted =
                        array.sort(
                            (a, b) =>
                                a - b
                        );

                    // Save to localStorage

                    localStorage.setItem(
                        "sortedArray",
                        JSON.stringify(
                            sorted
                        )
                    );

                    resolve(sorted);

                }, 2000);
            }
        }
    );
}


document
    .getElementById(
        "sortButton"
    )
    .addEventListener(
        "click",
        function () {

            const result =
                document.getElementById(
                    "task4Result"
                );

            const numbers =
                [8, 3, 1, 9, 5];

            sortArray(numbers)

                .then((sorted) => {

                    result.innerHTML =
                        "Sorted Array: " +
                        sorted.join(", ");

                    console.log(
                        "Saved to localStorage:"
                    );

                    console.log(
                        localStorage.getItem(
                            "sortedArray"
                        )
                    );
                })

                .catch((error) => {

                    result.innerHTML =
                        error;
                });
        }
    );

// TASK 5

function multiplyAsync(a, b) {

    return new Promise(
        (resolve, reject) => {

            if (
                typeof a !== "number" ||
                typeof b !== "number"
            ) {

                reject(
                    "Incorrect values"
                );

            } else {

                setTimeout(() => {

                    resolve(a * b);

                }, 2000);
            }
        }
    );
}


async function main() {

    const resultDiv =
        document.getElementById(
            "task5Result"
        );

    try {

        const result =
            await multiplyAsync(
                6,
                9
            );

        resultDiv.innerHTML =
            "Result: " + result;

    } catch (error) {

        resultDiv.innerHTML =
            error;
    }
}


document
    .getElementById(
        "multiplyButton"
    )
    .addEventListener(
        "click",
        main
    );