const themeSelect =
    document.getElementById(
        "themeSelect"
    );

const languageSelect =
    document.getElementById(
        "languageSelect"
    );

const range =
    document.getElementById(
        "range"
    );

const valueText =
    document.getElementById(
        "valueText"
    );


range.addEventListener(
    "input",
    function () {

        valueText.innerText =
            range.value + "%";
    }
);


function setCookie(
    name,
    value,
    days
) {

    const date =
        new Date();

    date.setTime(
        date.getTime() +
        days * 24 * 60 * 60 * 1000
    );

    const expires =
        "expires=" +
        date.toUTCString();

    document.cookie =
        `${name}=${value};${expires};path=/`;
}


function getCookie(name) {

    const cookieName =
        name + "=";

    const cookies =
        document.cookie.split(";");


    for (
        let cookie of cookies
    ) {

        cookie = cookie.trim();

        if (
            cookie.indexOf(
                cookieName
            ) === 0
        ) {

            return cookie.substring(
                cookieName.length
            );
        }
    }

    return "";
}


function applyTheme(theme) {

    document.body.classList.remove(
        "dark",
        "light"
    );

    document.body.classList.add(
        theme
    );

    themeSelect.value =
        theme;
}


function applyLanguage(lang) {

    languageSelect.value =
        lang;


    if (lang === "ua") {

        document.getElementById(
            "title"
        ).innerText =
            "Реєстраційна форма";

        document.getElementById(
            "subtitle"
        ).innerText =
            "Приклад сучасної HTML форми";

        document.getElementById(
            "themeLabel"
        ).innerText =
            "Тема";

        document.getElementById(
            "languageLabel"
        ).innerText =
            "Мова";

        document.getElementById(
            "firstNameLabel"
        ).innerText =
            "Ім'я";

        document.getElementById(
            "lastNameLabel"
        ).innerText =
            "Прізвище";

        document.getElementById(
            "emailLabel"
        ).innerText =
            "Email";

        document.getElementById(
            "passwordLabel"
        ).innerText =
            "Пароль";

        document.getElementById(
            "birthLabel"
        ).innerText =
            "Дата народження";

        document.getElementById(
            "phoneLabel"
        ).innerText =
            "Телефон";

        document.getElementById(
            "countryLabel"
        ).innerText =
            "Країна";

        document.getElementById(
            "genderLabel"
        ).innerText =
            "Стать";

        document.getElementById(
            "maleText"
        ).innerText =
            "Чоловік";

        document.getElementById(
            "femaleText"
        ).innerText =
            "Жінка";

        document.getElementById(
            "skillsLabel"
        ).innerText =
            "Навички";

        document.getElementById(
            "experienceLabel"
        ).innerText =
            "Рівень досвіду";

        document.getElementById(
            "fileLabel"
        ).innerText =
            "Завантажити файл";

        document.getElementById(
            "commentLabel"
        ).innerText =
            "Коментар";

        document.getElementById(
            "agreeText"
        ).innerText =
            "Я погоджуюсь з умовами";

        document.getElementById(
            "submitButton"
        ).innerText =
            "Відправити";

        document.getElementById(
            "resetButton"
        ).innerText =
            "Очистити";

    } else {

        location.reload();
    }
}


themeSelect.addEventListener(
    "change",
    function () {

        const theme =
            themeSelect.value;

        applyTheme(theme);

        setCookie(
            "theme",
            theme,
            30
        );
    }
);


languageSelect.addEventListener(
    "change",
    function () {

        const language =
            languageSelect.value;

        applyLanguage(language);

        setCookie(
            "language",
            language,
            30
        );
    }
);


const savedTheme =
    getCookie("theme") || "dark";

const savedLanguage =
    getCookie("language") || "en";


applyTheme(savedTheme);

applyLanguage(savedLanguage);