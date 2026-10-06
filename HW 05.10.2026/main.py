from generators import (
    generate_odd_numbers,
    generate_multiples_of_five,
    generate_palindromes,
)


def main():
    start = 1
    end = 100

    print("HW 05.10.2026")
    print("=" * 40)

    print("\n1. Odd numbers:")
    print(list(generate_odd_numbers(start, end)))

    print("\n2. Numbers divisible by five:")
    print(list(generate_multiples_of_five(start, end)))

    print("\n3. Palindromic numbers:")
    print(list(generate_palindromes(start, end)))


if __name__ == "__main__":
    main()
