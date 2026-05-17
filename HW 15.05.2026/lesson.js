// Task 3
// Hide / Show Text

const toggleButton =
    document.getElementById("toggleButton");

const text =
    document.getElementById("text");


toggleButton.addEventListener("click", () => {

    text.classList.toggle("hidden");
});


// Task 4
// Tabs

const buttons =
    document.querySelectorAll(".tab-button");

const contents =
    document.querySelectorAll(".tab-content");


buttons.forEach((button) => {

    button.addEventListener("click", () => {

        const tabName =
            button.dataset.tab;


        contents.forEach((content) => {

            content.classList.remove("active");
        });


        document
            .getElementById(tabName)
            .classList.add("active");
    });
});