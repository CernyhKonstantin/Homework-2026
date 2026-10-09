import time
from functools import wraps


def print_items(title, items):
    print(f"\n{title}\n{'-' * len(title)}")
    if not items:
        print("(empty)")
    else:
        for item in sorted(items, key=str.casefold):
            print(item)


def count_fruit():
    fruits = ("apple", "banana", "orange", "apple", "kiwi", "banana", "apple")
    print_items("Available fruits", fruits)
    name = input("Enter a fruit name to count: ").strip()
    count = sum(1 for fruit in fruits if fruit.casefold() == name.casefold())
    print(f'"{name}" appears {count} time(s) in the tuple.')


def show_fruit_tuple():
    fruits = ("apple", "banana", "orange", "apple", "kiwi", "banana", "apple")
    print("Fruit tuple, including repeated fruits:")
    print(fruits)
    print("Tuples are immutable, so repeated values are included when creating the tuple.")


def replace_car_manufacturer():
    cars = ["Lamborghini", "Ferrari", "Bentley", "Rolls-Royce", "Bugatti", "Ferrari", "Lamborghini", "Bugatti", "Bentley"]
    print(f"Current car manufacturers: {cars}")
    manufacturer = input("Enter the manufacturer to replace: ").strip()
    replacement = input("Enter the replacement word: ").strip()
    if not manufacturer or not replacement:
        print("The manufacturer and replacement cannot be empty.")
        return
    count = sum(1 for car in cars if car.casefold() == manufacturer.casefold())
    cars = [replacement if car.casefold() == manufacturer.casefold() else car for car in cars]
    print(f"Replaced {count} exact match(es).")
    print(f"Updated list: {cars}")


def manage_countries():
    countries = {"Germany", "Ukraine", "France", "Italy", "Japan"}
    while True:
        print("\nCountry Set Manager\n1. Show countries\n2. Add a country\n3. Remove a country\n4. Search countries by characters\n5. Check whether a country exists\n0. Return to main menu")
        choice = input("Choose an option: ").strip()
        if choice == "1":
            print_items("Countries", countries)
        elif choice == "2":
            country = input("Country to add: ").strip()
            if not country:
                print("Country name cannot be empty.")
            elif any(c.casefold() == country.casefold() for c in countries):
                print(f'"{country}" already exists.')
            else:
                countries.add(country)
                print(f'Added "{country}".')
        elif choice == "3":
            country = input("Country to remove: ").strip()
            found = next((c for c in countries if c.casefold() == country.casefold()), None)
            if found is None:
                print(f'"{country}" was not found.')
            else:
                countries.remove(found)
                print(f'Removed "{found}".')
        elif choice == "4":
            query = input("Enter characters to search for: ").strip().casefold()
            print_items("Search results", {c for c in countries if query in c.casefold()})
        elif choice == "5":
            country = input("Country to check: ").strip()
            found = any(c.casefold() == country.casefold() for c in countries)
            print(f'"{country}" {"is" if found else "is not"} in the set.')
        elif choice == "0":
            return
        else:
            print("Invalid option. Please choose again.")


def read_city_set(prompt):
    value = input(prompt).strip()
    return {city.strip() for city in value.split(",") if city.strip()} if value else set()


def city_set_operations():
    print("Enter city names separated by commas.")
    first = read_city_set("Cities in the first set: ")
    second = read_city_set("Cities in the second set: ")
    print_items("First city set", first)
    print_items("Second city set", second)
    print_items("Task 2 - Cities in both sets", first & second)
    print_items("Task 3 - Cities only in the first set", first - second)
    print_items("Task 4 - Cities only in the second set", second - first)
    print_items("Task 5 - Unique cities in either set", first ^ second)


def manage_capitals():
    capitals = {"Germany": "Berlin", "Ukraine": "Kyiv", "France": "Paris", "Italy": "Rome", "Japan": "Tokyo"}
    while True:
        print("\nCountry and Capital Dictionary\n1. Show all\n2. Add country and capital\n3. Delete country\n4. Search country\n5. Replace capital\n0. Return to main menu")
        choice = input("Choose an option: ").strip()
        if choice == "1":
            if not capitals:
                print("(empty)")
            else:
                for country in sorted(capitals, key=str.casefold):
                    print(f"{country} -> {capitals[country]}")
        elif choice == "2":
            country, capital = input("Country name: ").strip(), input("Capital city: ").strip()
            if not country or not capital:
                print("Both country and capital are required.")
            elif any(c.casefold() == country.casefold() for c in capitals):
                print(f'"{country}" already exists. Use replace to change its capital.')
            else:
                capitals[country] = capital
                print(f"Added: {country} -> {capital}")
        elif choice == "3":
            country = input("Country to delete: ").strip()
            found = next((c for c in capitals if c.casefold() == country.casefold()), None)
            if found:
                del capitals[found]
                print(f'Deleted "{found}".')
            else:
                print(f'"{country}" was not found.')
        elif choice == "4":
            country = input("Country to search for: ").strip()
            found = next((c for c in capitals if c.casefold() == country.casefold()), None)
            print(f"{found} -> {capitals[found]}" if found else f'"{country}" was not found.')
        elif choice == "5":
            country = input("Country whose capital should change: ").strip()
            found = next((c for c in capitals if c.casefold() == country.casefold()), None)
            if not found:
                print(f'"{country}" was not found. Add the country first.')
                continue
            capital = input("New capital city: ").strip()
            if capital:
                capitals[found] = capital
                print(f"Updated: {found} -> {capital}")
            else:
                print("Capital city cannot be empty.")
        elif choice == "0":
            return
        else:
            print("Invalid option. Please choose again.")


def show_horizontal_line(symbol):
    print(symbol * 20)


def show_vertical_line(symbol):
    for _ in range(10):
        print(symbol)


def show_line(symbol, function_to_call):
    """Higher-order function: call the supplied line-drawing function."""
    function_to_call(symbol)


def line_menu():
    print("\nLine Drawing\n1. Horizontal line\n2. Vertical line")
    entered = input("Enter a symbol: ")
    if not entered:
        print("Please enter a symbol.")
        return
    choice = input("Choose a line type (1 or 2): ").strip()
    if choice == "1":
        show_line(entered[0], show_horizontal_line)
    elif choice == "2":
        show_line(entered[0], show_vertical_line)
    else:
        print("Invalid choice.")


def measure_time(function):
    """Decorator that prints the wrapped function's execution time."""
    @wraps(function)
    def wrapper(*args, **kwargs):
        start_time = time.time()
        result = function(*args, **kwargs)
        end_time = time.time()
        print(f"Execution time: {end_time - start_time:.6f} seconds")
        return result
    return wrapper


@measure_time
def calculate_sum(n):
    """Calculate the sum of all integers from 1 through n."""
    return sum(range(1, n + 1))


def run_calculate_sum():
    result = calculate_sum(1_000_000)
    print(f"Sum of numbers: {result}")


def main():
    while True:
        print("\n" + "=" * 52)
        print("HW 09.10.2026")
        print("Python: Tuples, Sets, Dictionaries, Functions")
        print("=" * 52)
        print("\nPART 1 - TUPLES AND LISTS\n1. Count a fruit in a tuple\n2. Display a tuple with repeated fruits\n3. Replace a car manufacturer")
        print("\nPART 2 - SETS AND DICTIONARIES\n4. Manage a set of countries\n5. Perform city set operations\n6. Manage country-capital dictionary")
        print("\nPART 3 - HIGHER-ORDER FUNCTIONS AND DECORATORS\n7. Draw a horizontal or vertical line\n8. Calculate sum and measure execution time\n0. Exit")
        choice = input("\nChoose a task: ").strip()
        actions = {"1": count_fruit, "2": show_fruit_tuple, "3": replace_car_manufacturer, "4": manage_countries, "5": city_set_operations, "6": manage_capitals, "7": line_menu, "8": run_calculate_sum}
        if choice == "0":
            print("Goodbye!")
            break
        action = actions.get(choice)
        if action:
            action()
        else:
            print("Invalid option. Please choose again.")


if __name__ == "__main__":
    main()
