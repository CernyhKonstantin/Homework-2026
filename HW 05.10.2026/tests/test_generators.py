import unittest

from generators import (
    generate_odd_numbers,
    generate_multiples_of_five,
    generate_palindromes,
)


class GeneratorTests(unittest.TestCase):
    def test_odd_numbers(self):
        self.assertEqual(
            list(generate_odd_numbers(1, 10)),
            [1, 3, 5, 7, 9],
        )

    def test_multiples_of_five(self):
        self.assertEqual(
            list(generate_multiples_of_five(1, 30)),
            [5, 10, 15, 20, 25, 30],
        )

    def test_palindromes(self):
        self.assertEqual(
            list(generate_palindromes(1, 150)),
            [1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 22, 33, 44, 55, 66, 77, 88, 99, 101, 111, 121],
        )


if __name__ == "__main__":
    unittest.main()
