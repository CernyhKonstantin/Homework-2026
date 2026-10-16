class MyIntArray:
    def __init__(self, elements=None):
        if elements is None:
            self._data = []
        else:
            if not all(isinstance(x, int) and not isinstance(x, bool) for x in elements):
                raise TypeError("MyIntArray accepts integer values only.")
            self._data = list(elements)

    def __str__(self) -> str:
        pairs = [f"{idx}: {val}" for idx, val in enumerate(self._data)]
        return f"[{', '.join(pairs)}]"

    def __add__(self, other):
        if isinstance(other, MyIntArray):
            if len(self._data) != len(other._data):
                raise ValueError("Arrays must be of the same size to perform addition.")
            return MyIntArray([a + b for a, b in zip(self._data, other._data)])
        elif isinstance(other, int) and not isinstance(other, bool):
            return MyIntArray([a + other for a in self._data])
        else:
            return NotImplemented

    def __mul__(self, other):
        if isinstance(other, MyIntArray):
            if len(self._data) != len(other._data):
                raise ValueError("Arrays must be of the same size to perform multiplication.")
            return MyIntArray([a * b for a, b in zip(self._data, other._data)])
        elif isinstance(other, int) and not isinstance(other, bool):
            return MyIntArray([a * other for a in self._data])
        else:
            return NotImplemented

    def __eq__(self, other) -> bool:
        if isinstance(other, MyIntArray):
            return self._data == other._data
        return False

    def __getitem__(self, index: int) -> int:
        return self._data[index]

    def __setitem__(self, index: int, value: int):
        if not isinstance(value, int) or isinstance(value, bool):
            raise TypeError("Only integer values can be assigned.")
        self._data[index] = value

    def __len__(self) -> int:
        return len(self._data)


if __name__ == "__main__":
    arr1 = MyIntArray([10, 20, 30])
    arr2 = MyIntArray([1, 2, 3])

    print("arr1:", arr1)
    print("arr2:", arr2)
    print("arr1 + arr2:", arr1 + arr2)
    print("arr1 + 5:", arr1 + 5)
    print("arr1 * arr2:", arr1 * arr2)
    print("arr1 * 2:", arr1 * 2)
    
    arr3 = MyIntArray([10, 20, 30])
    print("arr1 == arr3:", arr1 == arr3)
    
    arr1[0] = 99
    print("arr1 after modification:", arr1)