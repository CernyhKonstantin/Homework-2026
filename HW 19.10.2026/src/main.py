class Device:
    """Base class for household devices."""
    def __init__(self, brand="Unknown", model="Unknown", power_watts=0, is_on=False):
        self.brand, self.model, self.power_watts, self.is_on = brand, model, power_watts, is_on

    def turn_on(self):
        self.is_on = True
        return f"{self.brand} {self.model} is now on."

    def turn_off(self):
        self.is_on = False
        return f"{self.brand} {self.model} is now off."

    def get_info(self):
        return (f"Brand: {self.brand}\nModel: {self.model}\nPower: {self.power_watts} W\n"
                f"Status: {'On' if self.is_on else 'Off'}")

    def __str__(self):
        return self.get_info()


class CoffeeMachine(Device):
    def __init__(self, brand="Unknown", model="Unknown", power_watts=0,
                 water_tank_liters=1.0, coffee_type="Ground coffee", is_on=False):
        super().__init__(brand, model, power_watts, is_on)
        self.water_tank_liters, self.coffee_type = water_tank_liters, coffee_type

    def make_coffee(self):
        return f"Making coffee with {self.coffee_type}." if self.is_on else "Turn on the coffee machine first."

    def get_info(self):
        return f"{super().get_info()}\nWater tank: {self.water_tank_liters} L\nCoffee type: {self.coffee_type}"


class Blender(Device):
    def __init__(self, brand="Unknown", model="Unknown", power_watts=0, speed_levels=3, is_on=False):
        super().__init__(brand, model, power_watts, is_on)
        self.speed_levels, self.current_speed = speed_levels, 0

    def blend(self, speed=1):
        if not self.is_on:
            return "Turn on the blender first."
        if not isinstance(speed, int) or not 1 <= speed <= self.speed_levels:
            return f"Choose a speed from 1 to {self.speed_levels}."
        self.current_speed = speed
        return f"Blender is running at speed {speed}."

    def turn_off(self):
        self.current_speed = 0
        return super().turn_off()

    def get_info(self):
        return f"{super().get_info()}\nSpeed levels: {self.speed_levels}"


class MeatGrinder(Device):
    def __init__(self, brand="Unknown", model="Unknown", power_watts=0,
                 capacity_kg_per_minute=1.0, has_reverse=False, is_on=False):
        super().__init__(brand, model, power_watts, is_on)
        self.capacity_kg_per_minute, self.has_reverse = capacity_kg_per_minute, has_reverse

    def grind_meat(self):
        return (f"Grinding meat at up to {self.capacity_kg_per_minute} kg/min."
                if self.is_on else "Turn on the meat grinder first.")

    def reverse_jam(self):
        return "Reverse function activated." if self.has_reverse else "Reverse function is not available."

    def get_info(self):
        return (f"{super().get_info()}\nCapacity: {self.capacity_kg_per_minute} kg/min\n"
                f"Reverse function: {'Yes' if self.has_reverse else 'No'}")


class Ship:
    """Base class for ships."""
    def __init__(self, name="Unknown", displacement_tons=0, max_speed_knots=0, year_built=0):
        self.name, self.displacement_tons = name, displacement_tons
        self.max_speed_knots, self.year_built = max_speed_knots, year_built
        self.is_at_sea = False

    def set_sail(self):
        self.is_at_sea = True
        return f"{self.name} has set sail."

    def return_to_port(self):
        self.is_at_sea = False
        return f"{self.name} has returned to port."

    def get_info(self):
        return (f"Name: {self.name}\nDisplacement: {self.displacement_tons} tons\n"
                f"Maximum speed: {self.max_speed_knots} knots\nYear built: {self.year_built}\n"
                f"Status: {'At sea' if self.is_at_sea else 'In port'}")

    def __str__(self):
        return self.get_info()


class Frigate(Ship):
    def __init__(self, name="Unknown", displacement_tons=0, max_speed_knots=0,
                 year_built=0, missile_count=0, has_sonar=True):
        super().__init__(name, displacement_tons, max_speed_knots, year_built)
        self.missile_count, self.has_sonar = missile_count, has_sonar

    def patrol(self):
        return f"{self.name} is patrolling."

    def get_info(self):
        return f"{super().get_info()}\nMissiles: {self.missile_count}\nSonar: {'Yes' if self.has_sonar else 'No'}"


class Destroyer(Ship):
    def __init__(self, name="Unknown", displacement_tons=0, max_speed_knots=0,
                 year_built=0, missile_count=0, torpedo_tubes=0):
        super().__init__(name, displacement_tons, max_speed_knots, year_built)
        self.missile_count, self.torpedo_tubes = missile_count, torpedo_tubes

    def engage_target(self, target):
        return f"{self.name} has engaged target: {target}."

    def get_info(self):
        return f"{super().get_info()}\nMissiles: {self.missile_count}\nTorpedo tubes: {self.torpedo_tubes}"


class Cruiser(Ship):
    def __init__(self, name="Unknown", displacement_tons=0, max_speed_knots=0,
                 year_built=0, missile_count=0, crew_capacity=0):
        super().__init__(name, displacement_tons, max_speed_knots, year_built)
        self.missile_count, self.crew_capacity = missile_count, crew_capacity

    def command_group(self):
        return f"{self.name} is coordinating the task group."

    def get_info(self):
        return f"{super().get_info()}\nMissiles: {self.missile_count}\nCrew capacity: {self.crew_capacity}"


class Money:
    """Represent one currency using whole and fractional parts."""
    def __init__(self, whole=0, fractional=0, currency="USD", minor_unit_name="cents"):
        if not isinstance(whole, int) or not isinstance(fractional, int):
            raise TypeError("Whole and fractional parts must be integers.")
        if not isinstance(currency, str) or not currency.strip():
            raise ValueError("Currency must be a non-empty string.")
        self.currency = currency.upper()
        self.minor_unit_name = minor_unit_name
        self._set_total(whole * 100 + fractional)

    def _set_total(self, total_minor):
        sign = -1 if total_minor < 0 else 1
        absolute = abs(total_minor)
        self.whole = sign * (absolute // 100)
        self.fractional = absolute % 100

    @property
    def total_minor_units(self):
        sign = -1 if self.whole < 0 else 1
        return self.whole * 100 + sign * self.fractional

    def set_values(self, whole, fractional):
        if not isinstance(whole, int) or not isinstance(fractional, int):
            raise TypeError("Whole and fractional parts must be integers.")
        self._set_total(whole * 100 + fractional)

    def set_whole(self, whole):
        if not isinstance(whole, int):
            raise TypeError("Whole part must be an integer.")
        self._set_total(whole * 100 + (self.fractional if self.total_minor_units >= 0 else -self.fractional))

    def set_fractional(self, fractional):
        if not isinstance(fractional, int):
            raise TypeError("Fractional part must be an integer.")
        self._set_total(self.whole * 100 + fractional)

    def display(self):
        sign = "-" if self.total_minor_units < 0 else ""
        return f"{sign}{abs(self.whole)}.{self.fractional:02d} {self.currency}"

    def __str__(self):
        return self.display()


class Product:
    """A product with a Money price."""
    def __init__(self, name, price):
        if not isinstance(price, Money):
            raise TypeError("price must be a Money object.")
        self.name = name
        self.price = Money(price.whole, price.fractional, price.currency, price.minor_unit_name)

    def reduce_price(self, amount):
        if isinstance(amount, Money):
            if amount.currency != self.price.currency:
                raise ValueError("Price reduction must use the same currency.")
            reduction = amount.total_minor_units
        elif isinstance(amount, (int, float)):
            reduction = round(amount * 100)
        else:
            raise TypeError("Reduction must be a Money object or a number.")
        if reduction < 0:
            raise ValueError("Price reduction cannot be negative.")
        self.price._set_total(max(0, self.price.total_minor_units - reduction))
        return self.price

    def get_info(self):
        return f"Product: {self.name}\nPrice: {self.price}"

    def __str__(self):
        return self.get_info()


def main():
    print("=== TASK 1: DEVICES ===")
    devices = [
        CoffeeMachine("DeLonghi", "Magnifica", 1450, 1.8, "Coffee beans"),
        Blender("Philips", "ProBlend", 800, 5),
        MeatGrinder("Bosch", "MFW", 500, 2.5, True),
    ]
    for device in devices:
        print(f"\n{type(device).__name__}\n{device.get_info()}")
        print(device.turn_on())
    print(devices[0].make_coffee())
    print(devices[1].blend(3))
    print(devices[2].grind_meat())
    print(devices[2].reverse_jam())

    print("\n=== TASK 2: SHIPS ===")
    ships = [
        Frigate("Northwind", 4200, 29, 2018, 16, True),
        Destroyer("Resolute", 9200, 32, 2020, 48, 2),
        Cruiser("Aurora", 12000, 30, 2016, 64, 600),
    ]
    for ship in ships:
        print(f"\n{type(ship).__name__}\n{ship.get_info()}")
        print(ship.set_sail())
    print(ships[0].patrol())
    print(ships[1].engage_target("surface vessel"))
    print(ships[2].command_group())

    print("\n=== TASK 3: MONEY AND PRODUCT ===")
    money = Money(19, 95, "EUR", "cents")
    print(f"Initial amount: {money}")
    money.set_values(20, 5)
    print(f"After setting values: {money}")
    product = Product("Coffee maker", money)
    print(product)
    product.reduce_price(2.50)
    print(f"After 2.50 EUR reduction: {product.price}")
    product.reduce_price(Money(1, 25, "EUR", "cents"))
    print(f"After another 1.25 EUR reduction: {product.price}")


if __name__ == "__main__":
    main()
