class Stadium:
    def __init__(
        self,
        name="Unknown",
        opening_date="Unknown",
        country="Unknown",
        city="Unknown",
        capacity=0,
    ):
        self.name = name
        self.opening_date = opening_date
        self.country = country
        self.city = city
        self.capacity = capacity

    @property
    def name(self):
        return self.__name

    @name.setter
    def name(self, value):
        if not value or not value.strip():
            raise ValueError("Stadium name cannot be empty")
        self.__name = value.strip()

    @property
    def opening_date(self):
        return self.__opening_date

    @opening_date.setter
    def opening_date(self, value):
        self.__opening_date = value.strip() if value and value.strip() else "Unknown"

    @property
    def country(self):
        return self.__country

    @country.setter
    def country(self, value):
        if not value or not value.strip():
            raise ValueError("Country cannot be empty")
        self.__country = value.strip()

    @property
    def city(self):
        return self.__city

    @city.setter
    def city(self, value):
        if not value or not value.strip():
            raise ValueError("City cannot be empty")
        self.__city = value.strip()

    @property
    def capacity(self):
        return self.__capacity

    @capacity.setter
    def capacity(self, value):
        if not isinstance(value, int) or value < 0:
            raise ValueError("Capacity must be a non-negative integer")
        self.__capacity = value

    def input_data(self):
        print("\nEnter stadium details. Press Enter to keep current values.")

        value = input("Stadium name: ").strip()
        if value:
            try:
                self.name = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Opening date (DD.MM.YYYY): ").strip()
        if value:
            self.opening_date = value

        value = input("Country: ").strip()
        if value:
            try:
                self.country = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("City: ").strip()
        if value:
            try:
                self.city = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Capacity: ").strip()
        if value:
            try:
                self.capacity = int(value)
            except ValueError as error:
                print(f"Error: {error}")

    def show_data(self):
        print("\nStadium Details")
        print("---------------")
        print(f"Name: {self.name}")
        print(f"Opening date: {self.opening_date}")
        print(f"Country: {self.country}")
        print(f"City: {self.city}")
        print(f"Capacity: {self.capacity:,}")

    def __str__(self):
        return (
            f"Stadium: {self.name}, "
            f"Opening date: {self.opening_date}, "
            f"Country: {self.country}, "
            f"City: {self.city}, "
            f"Capacity: {self.capacity:,}"
        )

