const users = [];


const form =
    document.getElementById(
        "registerForm"
    );


document.getElementById(
    "country"
).value = "Germany";


document.querySelector(
    "input[value='Male']"
).checked = true;


form.addEventListener(
    "submit",
    function (event) {

        event.preventDefault();

        validateForm();
    }
);


function validateForm() {

    let isValid = true;

    const firstName =
        document.getElementById(
            "firstName"
        );

    const lastName =
        document.getElementById(
            "lastName"
        );

    const email =
        document.getElementById(
            "email"
        );

    const password =
        document.getElementById(
            "password"
        );

    const birthDate =
        document.getElementById(
            "birthDate"
        );

    const phone =
        document.getElementById(
            "phone"
        );

    const comment =
        document.getElementById(
            "comment"
        );

    const agree =
        document.getElementById(
            "agree"
        );

    const result =
        document.getElementById(
            "result"
        );


    const elements =
        document.querySelectorAll(
            "input, textarea, select"
        );

    elements.forEach((element) => {

        element.classList.remove(
            "error"
        );
    });


    const nameRegex =
        /^[A-Za-z]{2,}$/;


    const emailRegex =
        /^\S+@\S+\.\S+$/;


    const phoneRegex =
        /^\+380\d{9}$/;


    if (
        !nameRegex.test(
            firstName.value.trim()
        )
    ) {

        firstName.classList.add(
            "error"
        );

        isValid = false;
    }


    if (
        !nameRegex.test(
            lastName.value.trim()
        )
    ) {

        lastName.classList.add(
            "error"
        );

        isValid = false;
    }


    if (
        !emailRegex.test(
            email.value.trim()
        )
    ) {

        email.classList.add(
            "error"
        );

        isValid = false;
    }


    if (
        password.value.length < 5 ||
        password.value.includes(" ")
    ) {

        password.classList.add(
            "error"
        );

        isValid = false;
    }


    if (
        birthDate.value === ""
    ) {

        birthDate.classList.add(
            "error"
        );

        isValid = false;
    }


    if (
        !phoneRegex.test(
            phone.value.trim()
        )
    ) {

        phone.classList.add(
            "error"
        );

        isValid = false;
    }


    const checkedSkills =
        document.querySelectorAll(
            ".checkbox-group input:checked"
        );

    if (
        checkedSkills.length < 2
    ) {

        document.querySelector(
            ".checkbox-group"
        ).classList.add(
            "error"
        );

        isValid = false;
    }


    if (
        comment.value.trim().length < 10 ||
        comment.value.trim().length > 150
    ) {

        comment.classList.add(
            "error"
        );

        isValid = false;
    }


    if (!agree.checked) {

        agree.classList.add(
            "error"
        );

        isValid = false;
    }


    if (isValid) {

        result.innerText =
            "Everything is filled correctly!";


        const user = {

            firstName:
                firstName.value.trim(),

            lastName:
                lastName.value.trim(),

            email:
                email.value.trim(),

            password:
                password.value,

            birthDate:
                birthDate.value,

            phone:
                phone.value,

            country:
                document.getElementById(
                    "country"
                ).value,

            gender:
                document.querySelector(
                    "input[name='gender']:checked"
                ).value,

            skills:
                Array.from(
                    checkedSkills
                ).map(
                    (skill) =>
                        skill.value
                ),

            comment:
                comment.value.trim()
        };


        users.push(user);


        console.log(users);


    } else {

        result.innerText =
            "Form contains errors!";
    }
}