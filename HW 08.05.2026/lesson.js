// Task 1
// Create car object

const car = {
    manufacturer: "BMW",
    model: "M5",
    year: 2022,
    averageSpeed: 100
};


// Function to show car information

function showCarInfo(carObject) {

    console.log("Car Information");
    console.log("Manufacturer:", carObject.manufacturer);
    console.log("Model:", carObject.model);
    console.log("Year:", carObject.year);
    console.log("Average speed:", carObject.averageSpeed + " km/h");
}


// Function to calculate travel time

function calculateTravelTime(distance, speed) {

    let time = distance / speed;

    let breaks = Math.floor(time / 4);

    time += breaks;

    return time;
}


// Task 2
// Create printMachine object

const printMachine = {

    fontSize: "20px",

    fontColor: "Blue",

    fontFamily: "Arial",

    print(text) {

        alert(
            "Font size: " + this.fontSize +
            "\\nFont color: " + this.fontColor +
            "\\nFont family: " + this.fontFamily +
            "\\nText: " + text
        );
    }
};


// Run tasks

function runTasks() {

    // Task 1

    showCarInfo(car);

    const distance = 850;

    const travelTime = calculateTravelTime(distance, car.averageSpeed);

    console.log("----------------");

    console.log("Distance:", distance + " km");

    console.log("Travel time:", travelTime + " hours");


    // Task 2

    setTimeout(() => {

        printMachine.print("Hello from Print Machine!");

    }, 5000);


    alert("Check console and wait 5 seconds!");
}