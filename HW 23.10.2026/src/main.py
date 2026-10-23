import json
from datetime import datetime


class Product:
    def __init__(self, name: str, price: float) -> None:
        if not name.strip():
            raise ValueError("Product name cannot be empty.")
        if price < 0:
            raise ValueError("Product price cannot be negative.")
        self.name = name
        self.price = float(price)

    def __str__(self) -> str:
        return f"{self.name}: {self.price:.2f} UAH"


class LoggingMixin:
    def log(self, message: str) -> None:
        timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        print(f"[{timestamp}] [LOG] {message}")


class JsonMixin:
    def to_json(self) -> str:
        return json.dumps(
            {"name": self.name, "price": round(self.price, 2)},
            ensure_ascii=False,
            indent=2,
        )


class DiscountMixin:
    def apply_discount(self, percent: float) -> float:
        if not 0 <= percent <= 100:
            raise ValueError("Discount percent must be between 0 and 100.")
        return round(self.price * (1 - percent / 100), 2)


class ProductManager(Product, LoggingMixin, JsonMixin, DiscountMixin):
    def __init__(self, name: str, price: float) -> None:
        super().__init__(name, price)
        self.log(f"Created product '{self.name}' with price {self.price:.2f} UAH.")

    def show_discount(self, percent: float) -> None:
        discounted_price = self.apply_discount(percent)
        self.log(f"Applied {percent:g}% discount to '{self.name}'.")
        print(f"Product: {self.name}")
        print(f"Original price: {self.price:.2f} UAH")
        print(f"Discount: {percent:g}%")
        print(f"Discounted price: {discounted_price:.2f} UAH")


def main() -> None:
    print("=" * 58)
    print("ONLINE STORE - PRODUCT MANAGEMENT DEMO")
    print("=" * 58)

    product = ProductManager("Wireless Headphones", 1000)
    print("\n1. Product")
    print(product)

    print("\n2. LoggingMixin.log(message)")
    product.log("The product is available in the online store.")

    print("\n3. JsonMixin.to_json()")
    print(product.to_json())

    print("\n4. DiscountMixin.apply_discount(percent)")
    discounted_price = product.apply_discount(15)
    print(f"Price after 15% discount: {discounted_price:.2f} UAH")
    assert discounted_price == 850.00, "Expected discounted price: 850.00 UAH."

    print("\n5. ProductManager.show_discount(15)")
    product.show_discount(15)

    print("\n6. ProductManager.mro()")
    for index, cls in enumerate(ProductManager.mro(), start=1):
        print(f"{index}. {cls.__module__}.{cls.__name__}")

    print("\nMRO explanation:")
    print(
        "Python searches for methods in the order listed above. "
        "ProductManager comes first, followed by Product, LoggingMixin, "
        "JsonMixin, DiscountMixin, and finally object. This order is "
        "computed by Python's C3 linearization algorithm."
    )
    print("\nAll demonstrations completed successfully.")


if __name__ == "__main__":
    main()
