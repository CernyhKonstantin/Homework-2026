class Book:
    def __init__(
        self,
        title="Unknown",
        publication_year=1,
        publisher="Unknown",
        genre="Unknown",
        author="Unknown",
        price=0.0,
    ):
        self.title = title
        self.publication_year = publication_year
        self.publisher = publisher
        self.genre = genre
        self.author = author
        self.price = price

    @property
    def title(self):
        return self.__title

    @title.setter
    def title(self, value):
        if not value or not value.strip():
            raise ValueError("Title cannot be empty")
        self.__title = value.strip()

    @property
    def publication_year(self):
        return self.__publication_year

    @publication_year.setter
    def publication_year(self, value):
        if not isinstance(value, int) or value < 1 or value > 2100:
            raise ValueError("Invalid publication year")
        self.__publication_year = value

    @property
    def publisher(self):
        return self.__publisher

    @publisher.setter
    def publisher(self, value):
        if not value or not value.strip():
            raise ValueError("Publisher cannot be empty")
        self.__publisher = value.strip()

    @property
    def genre(self):
        return self.__genre

    @genre.setter
    def genre(self, value):
        if not value or not value.strip():
            raise ValueError("Genre cannot be empty")
        self.__genre = value.strip()

    @property
    def author(self):
        return self.__author

    @author.setter
    def author(self, value):
        if not value or not value.strip():
            raise ValueError("Author cannot be empty")
        self.__author = value.strip()

    @property
    def price(self):
        return self.__price

    @price.setter
    def price(self, value):
        if value < 0:
            raise ValueError("Price cannot be negative")
        self.__price = value

    def input_data(self):
        print("\nEnter book details. Press Enter to keep current values.")

        value = input("Book title: ").strip()
        if value:
            try:
                self.title = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Publication year: ").strip()
        if value:
            try:
                self.publication_year = int(value)
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Publisher: ").strip()
        if value:
            try:
                self.publisher = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Genre: ").strip()
        if value:
            try:
                self.genre = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Author: ").strip()
        if value:
            try:
                self.author = value
            except ValueError as error:
                print(f"Error: {error}")

        value = input("Price: ").strip()
        if value:
            try:
                self.price = float(value.replace(",", "."))
            except ValueError as error:
                print(f"Error: {error}")

    def show_data(self):
        print("\nBook Details")
        print("------------")
        print(f"Title: {self.title}")
        print(f"Publication year: {self.publication_year}")
        print(f"Publisher: {self.publisher}")
        print(f"Genre: {self.genre}")
        print(f"Author: {self.author}")
        print(f"Price: {self.price:.2f}")

    def __str__(self):
        return (
            f"Book: {self.title}, "
            f"Year: {self.publication_year}, "
            f"Publisher: {self.publisher}, "
            f"Genre: {self.genre}, "
            f"Author: {self.author}, "
            f"Price: {self.price:.2f}"
        )
