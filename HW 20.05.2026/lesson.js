const monthInput =
    document.getElementById(
        "monthInput"
    );

const yearInput =
    document.getElementById(
        "yearInput"
    );

const generateButton =
    document.getElementById(
        "generateButton"
    );

const calendar =
    document.getElementById(
        "calendar"
    );


const monthNames = [

    "January",
    "February",
    "March",
    "April",
    "May",
    "June",

    "July",
    "August",
    "September",
    "October",
    "November",
    "December"
];


function generateCalendar() {

    const month =
        Number(monthInput.value);

    const year =
        Number(yearInput.value);


    let firstDay =
        new Date(
            year,
            month - 1,
            1
        ).getDay();


    if (firstDay === 0) {

        firstDay = 7;
    }


    const daysInMonth =
        new Date(
            year,
            month,
            0
        ).getDate();


    let table = `

        <h2>
            ${monthNames[month - 1]}, ${year}
        </h2>

        <table>

            <tr>

                <th>MON</th>
                <th>TUE</th>
                <th>WED</th>
                <th>THU</th>
                <th>FRI</th>
                <th>SAT</th>
                <th>SUN</th>

            </tr>

            <tr>
    `;


    for (
        let i = 1;
        i < firstDay;
        i++
    ) {

        table += "<td></td>";
    }


    let dayCounter = firstDay - 1;


    for (
        let day = 1;
        day <= daysInMonth;
        day++
    ) {

        table += `<td>${day}</td>`;


        dayCounter++;


        if (
            dayCounter % 7 === 0
        ) {

            table += "</tr><tr>";
        }
    }


    table += "</tr></table>";


    calendar.innerHTML = table;


    console.log(
        "Calendar generated"
    );
}


generateButton.addEventListener(
    "click",
    generateCalendar
);


generateCalendar();