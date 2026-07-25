import {
    Marker,
    RefillableMarker,
    Employee,
    StyledEmpTable
} from "./User.js";


// Task 1

const marker = new Marker(
    "blue",
    10
);

marker.print("Hello JavaScript World!");


const refillableMarker = new RefillableMarker(
    "red",
    5
);

refillableMarker.print("Programming");

refillableMarker.refill(20);

console.log(
    "Ink after refill:",
    refillableMarker.inkAmount + "%"
);


// Task 3 and 4

const employees = [

    new Employee(
        "John",
        "Manager",
        3500
    ),

    new Employee(
        "Alice",
        "Developer",
        4200
    ),

    new Employee(
        "Bob",
        "Designer",
        3000
    )
];


const table = new StyledEmpTable(
    employees
);


document.getElementById("output").innerHTML =
    table.getHtml();