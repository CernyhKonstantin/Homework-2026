import { render, fireEvent } from "@testing-library/react";
import { vi } from "vitest";
import MyButton from "./MyButton";

test("клік по кнопці викликає функцію", () => {
  //const handleClick = vi.fn(); — створюється фейкова функція-заглушка
  // (mock-функція) за допомогою Vitest, яка вміет відстежувати,
  // скільки разів і з якими аргументами її викликали.
  const handleClick = vi.fn();
  const { getByRole } = render(<MyButton onClick={handleClick} />);
  const button = getByRole("button");

  fireEvent.click(button);
  expect(handleClick).toHaveBeenCalledTimes(1);
});
