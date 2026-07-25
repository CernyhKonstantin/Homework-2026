// Task 1

class Marker {

    constructor(color, inkAmount) {

        this.color = color;

        this.inkAmount = inkAmount;
    }


    print(text) {

        let result = "";

        for (let char of text) {

            if (this.inkAmount <= 0) {

                break;
            }

            result += char;

            if (char !== " ") {

                this.inkAmount -= 0.5;
            }
        }

        document.write(`
            <p style="color:${this.color}">
                ${result}
            </p>
        `);

        console.log(
            "Remaining ink:",
            this.inkAmount + "%"
        );
    }
}



class RefillableMarker extends Marker {

    refill(amount) {

        this.inkAmount += amount;

        if (this.inkAmount > 100) {

            this.inkAmount = 100;
        }
    }
}


// Task 3

class Employee {

    constructor(name, position, salary) {

        this.name = name;

        this.position = position;

        this.salary = salary;
    }
}



class EmpTable {

    constructor(employees) {

        this.employees = employees;
    }


    getHtml() {

        let html = `
            <table>
                <tr>
                    <th>Name</th>
                    <th>Position</th>
                    <th>Salary</th>
                </tr>
        `;

        this.employees.forEach((employee) => {

            html += `
                <tr>
                    <td>${employee.name}</td>
                    <td>${employee.position}</td>
                    <td>${employee.salary}</td>
                </tr>
            `;
        });

        html += `</table>`;

        return html;
    }
}



class StyledEmpTable extends EmpTable {

    getStyles() {

        return `
            <style>

                table {
                    border-collapse: collapse;
                    width: 500px;
                    margin-top: 20px;
                }

                th, td {
                    border: 1px solid black;
                    padding: 10px;
                    text-align: center;
                }

                th {
                    background-color: lightgray;
                }

            </style>
        `;
    }


    getHtml() {

        return this.getStyles() +
            super.getHtml();
    }
}


export {
    Marker,
    RefillableMarker,
    Employee,
    StyledEmpTable
};