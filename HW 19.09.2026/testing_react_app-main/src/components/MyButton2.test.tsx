import { render, fireEvent } from "@testing-library/react";
import { vi } from "vitest";
import MyButton2 from "./MyButton2";

test("виводить значення в консоль при кліку", () => {
  // 1. "Шпигуємо" за console.log
  const consoleSpy = vi.spyOn(console, "log").mockImplementation(() => {});

  const handleClick = vi.fn();
  const { getByRole } = render(<MyButton2 id={42} onClick={handleClick} />);

  const button = getByRole("button");
  fireEvent.click(button);

  // 2. Перевіряємо, що console.log був викликаний саме з цими параметрами:
  expect(consoleSpy).toHaveBeenCalledWith(42, "клік помічено");

  // 3. Відновлюємо стандартну поведінку console.log
  consoleSpy.mockRestore();
});
