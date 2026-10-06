def generate_odd_numbers(start: int, end: int):
    """Yield every odd number in the inclusive range."""
    for number in range(start, end + 1):
        if number % 2 != 0:
            yield number


def generate_multiples_of_five(start: int, end: int):
    """Yield every number divisible by five in the inclusive range."""
    for number in range(start, end + 1):
        if number % 5 == 0:
            yield number


def generate_palindromes(start: int, end: int):
    """Yield every palindromic number in the inclusive range."""
    for number in range(start, end + 1):
        text = str(number)
        if text == text[::-1]:
            yield number
