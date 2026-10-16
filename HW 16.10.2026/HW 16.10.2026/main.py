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
        print(f"Pages: {self.pages}")
        print(f"Publisher: {self.publisher}")
        print(f"Genre: {self.genre}")
        print(f"Price: {self.price:.2f} €")


    def __str__(self) -> str:
        """User-friendly string representation of the object."""
        return (
            f"'{self.title}' by {self.author} ({self.publication_year}) - "
            f"{self.pages} pages, Price: {self.price:.2f} €"
        )

    def __repr__(self) -> str:
        """Formal representation for debugging."""
        return (
            f"Book(title='{self.title}', author='{self.author}', "
            f"publication_year={self.publication_year}, pages={self.pages}, "
            f"publisher='{self.publisher}', genre='{self.genre}', price={self.price})"
        )

    def __len__(self) -> int:
        """Returns the number of pages."""
        return self.pages

    def __eq__(self, other) -> bool:
        """Checks equality by title and author."""
        if isinstance(other, Book):
            return (
                self.title.strip().lower() == other.title.strip().lower()
                and self.author.strip().lower() == other.author.strip().lower()
            )
        return False

    def __lt__(self, other) -> bool:
        """Compares books by page count (for sorting)."""
        if isinstance(other, Book):
            return self.pages < other.pages
        return NotImplemented

    def __add__(self, other):
        """Adds page counts of two books or a book and an integer."""
        if isinstance(other, Book):
            return self.pages + other.pages
        elif isinstance(other, int):
            return self.pages + other
        return NotImplemented

    def __radd__(self, other):
        """Support for summation via sum()."""
        if isinstance(other, int):
            return self.pages + other
        return NotImplemented

    def __contains__(self, item: str) -> bool:
        """Checks if a word is present in the book's title."""
        if isinstance(item, str):
            return item.lower() in self.title.lower()
        return False

    def __bool__(self) -> bool:
        """Checks if the book is non-empty (pages > 0)."""
        return self.pages > 0


if __name__ == "__main__":
    b1 = Book(
        title="Dune",
        author="Frank Herbert",
        publication_year=1965,
        pages=896,
        price=19.99,
    )
    b2 = Book(
        title="The Midnight Library",
        author="Matt Haig",
        publication_year=2020,
        pages=304,
        price=14.50,
    )
    b3 = Book(
        title="Project Hail Mary",
        author="Andy Weir",
        publication_year=2021,
        pages=496,
        price=16.00,
    )

    print(b1)
    print(f"Is '{b1.title}' new? {b1.is_new()}")
    print(f"Is '{b2.title}' new? {b2.is_new()}")
    print(f"Is 'Library' in title? {'Library' in b2}")
    print(f"Total pages of Dune + Project Hail Mary: {b1 + b3}")

    library = [b1, b2, b3]
    library.sort()
    print("\nSorted by page count:")
    for book in library:
        print(f" - {book.title}: {book.pages} pages")