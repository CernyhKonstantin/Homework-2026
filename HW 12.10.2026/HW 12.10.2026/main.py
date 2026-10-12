from car import Car
from book import Book
from stadium import Stadium


def manage_object(obj, object_name):
    while True:
        print(f"\n{object_name.upper()} MANAGEMENT")
        print("1. Enter or update data")
        print("2. Display data")
        print("3. Display object using __str__")
        print("0. Back to main menu")

        choice = input("Choose an option: ").strip()

        if choice == "1":
            obj.input_data()
        elif choice == "2":
            obj.show_data()
        elif choice == "3":
            print(obj)
        elif choice == "0":
            break
        else:
            print("Invalid option. Please choose again.")


def main():
    car = Car()
    book = Book()
    stadium = Stadium()

    while True:
        print("\nOBJECT-ORIENTED PROGRAMMING")
        print("HW 12.10.2026")
        print("---------------------------")
        print("1. Car management")
        print("2. Book management")
        print("3. Stadium management")
        print("0. Exit")

        choice = input("Choose an option: ").strip()

        if choice == "1":
            manage_object(car, "Car")
        elif choice == "2":
            manage_object(book, "Book")
        elif choice == "3":
            manage_object(stadium, "Stadium")
        elif choice == "0":
            print("Goodbye!")
            break
        else:
            print("Invalid option. Please choose again.")


if __name__ == "__main__":
    main()
