class Car:
    def __init__(
        self,
        model_name="Unknown",
        production_year=2000,
        manufacturer="Unknown",
        engine_capacity=0.0,
        color="Unknown",
        price=0.0,
    ):
        self.model_name = model_name
        self.production_year = production_year
        self.manufacturer = manufacturer
        self.engine_capacity = engine_capacity
        self.color = color
        self.price = price

    @property
    def model_name(self):
        return self.__model_name

    @model_name.setter
    def model_name(self, value):
        if not value or not value.strip():
            raise ValueError("Model name cannot be empty")
        self.__model_name = value.strip()

    @property
    def production_year(self):
        return self.__production_year

    @production_year.setter
    def production_year(self, value):
        if not isinstance(value, int) or value < 1886 or value > 2100:
            raise ValueError("Invalid production year")
        self.__production_year = value

    @property
    def manufacturer(self):
        return self.__manufacturer

    @manufacturer.setter
    def manufacturer(self, value):
        if not value or not value.strip():
            raise ValueError("Manufacturer cannot be empty")
        self.__manufacturer = value.strip()

    @property
    def engine_capacity(self):
        return self.__engine_capacity

    @engine_capacity.setter
    def engine_capacity(self, value):
        if value < 0:
            raise ValueError("Engine capacity cannot be negative")
        self.__engine_capacity = value

    @property
    def color(self):
        return self.__color

    @color.setter
    def color(self, value):
        if not value or not value.strip():
            raise ValueError("Color cannot be empty")
        self.__color = value.strip()

    @property
    def price(self):
        return self.__price

    @price.setter
    def price(self, value):
        if value < 0:
            raise ValueError("Price cannot be negative")
        self.__price = value

    def input_data(self):
        print("\nEnter car details. Press Enter to keep current values.")

        value = input("Model name: ").strip()
        if value:
            try:
                self.model_name = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Production year: ").strip()
        if value:
            try:
                self.production_year = int(value)
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Manufacturer: ").strip()
        if value:
            try:
                self.manufacturer = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Engine capacity in liters: ").strip()
        if value:
            try:
                self.engine_capacity = float(value.replace(",", "."))
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Color: ").strip()
        if value:
            try:
                self.color = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Price: ").strip()
        if value:
            try:
                self.price = float(value.replace(",", "."))
            except ValueError as error:
                print(f"Error: {error}")

    def show_data(self):
        print("\nCar Details")
        print("-----------")
        print(f"Model: {self.model_name}")
        print(f"Production year: {self.production_year}")
        print(f"Manufacturer: {self.manufacturer}")
        print(f"Engine capacity: {self.engine_capacity:.1f} L")
        print(f"Color: {self.color}")
        print(f"Price: {self.price:.2f}")

    def __str__(self):
        return (
            f"Car: {self.model_name}, "
            f"Year: {self.production_year}, "
            f"Manufacturer: {self.manufacturer}, "
            f"Engine: {self.engine_capacity:.1f} L, "
            f"Color: {self.color}, "
            f"Price: {self.price:.2f}"
        )
