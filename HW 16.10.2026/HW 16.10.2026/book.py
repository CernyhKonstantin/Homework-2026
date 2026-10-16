from datetime import datetime


class Book:
    def __init__(
        self,
        title: str = "Unknown",
        author: str = "Unknown",
        publication_year: int = 1,
        pages: int = 1,
        publisher: str = "Unknown",
        genre: str = "Unknown",
        price: float = 0.0,
    ):
        self.title = title
        self.author = author
        self.publication_year = publication_year
        self.pages = pages
        self.publisher = publisher
        self.genre = genre
        self.price = price


    @property
    def title(self) -> str:
        return self.__title

    @title.setter
    def title(self, value: str):
        if not value or not str(value).strip():
            raise ValueError("Title cannot be empty")
        self.__title = str(value).strip()

    @property
    def author(self) -> str:
        return self.__author

    @author.setter
    def author(self, value: str):
        if not value or not str(value).strip():
            raise ValueError("Author cannot be empty")
        self.__author = str(value).strip()

    @property
    def publication_year(self) -> int:
        return self.__publication_year

    @publication_year.setter
    def publication_year(self, value: int):
        current_year = datetime.now().year
        if not isinstance(value, int) or value < 1 or value > current_year:
            raise ValueError(
                f"Invalid publication year ({value}). Year must be between 1 and {current_year}."
            )
        self.__publication_year = value

    @property
    def pages(self) -> int:
        return self.__pages

    @pages.setter
    def pages(self, value: int):
        if not isinstance(value, int) or value <= 0:
            raise ValueError(
                f"Invalid page count ({value}). Pages must be a positive integer."
            )
        self.__pages = value

    @property
    def publisher(self) -> str:
        return self.__publisher

    @publisher.setter
    def publisher(self, value: str):
        if not value or not str(value).strip():
            raise ValueError("Publisher cannot be empty")
        self.__publisher = str(value).strip()

    @property
    def genre(self) -> str:
        return self.__genre

    @genre.setter
    def genre(self, value: str):
        if not value or not str(value).strip():
            raise ValueError("Genre cannot be empty")
        self.__genre = str(value).strip()

    @property
    def price(self) -> float:
        return self.__price

    @price.setter
    def price(self, value: float):
        if value < 0:
            raise ValueError("Price cannot be negative")
        self.__price = float(value)

    @property
    def year(self) -> int:
        return self.publication_year

    @year.setter
    def year(self, value: int):
        self.publication_year = value


    def is_new(self) -> bool:
        """Returns True if the book was published in 2020 or later."""
        return self.publication_year >= 2020

    def input_data(self):
        """Interactive input/update of book data."""
        print("\nEnter book details. Press Enter to keep current values.")

        value = input(f"Book title [{self.title}]: ").strip()
        if value:
            try:
                self.title = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input(f"Author [{self.author}]: ").strip()
        if value:
            try:
                self.author = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input(f"Publication year [{self.publication_year}]: ").strip()
        if value:
            try:
                self.publication_year = int(value)
            except ValueError as error:
                print(f"Error: {error}")

        value = input(f"Page count [{self.pages}]: ").strip()
        if value:
            try:
                self.pages = int(value)
            except ValueError as error:
                print(f"Error: {error}")

        value = input(f"Publisher [{self.publisher}]: ").strip()
        if value:
            try:
                self.publisher = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input(f"Genre [{self.genre}]: ").strip()
        if value:
            try:
                self.genre = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input(f"Price [{self.price:.2f}]: ").strip()
        if value:
            try:
                self.price = float(value.replace(",", "."))
            except ValueError as error:
                print(f"Error: {error}")

    def show_data(self):
        """Display detailed information about the book."""
        print("\nBook Details")
        print("------------")
        print(f"Title: {self.title}")
        print(f"Author: {self.author}")
        print(f"Publication year: {self.publication_year}")