import { render } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { vi, test, expect } from "vitest";
import MyComponent from "./MyComponent";

test("MyComponent правильно відображає дані після fetch", async () => {
  const mockData = [{ id: 1, name: "John Doe" }];

  // Мокування global.fetch для Vitest
  vi.stubGlobal(
    "fetch",
    vi.fn(() =>
      Promise.resolve({
        json: () => Promise.resolve(mockData),
      } as Response),
    ),
  );

  const { getByText, findByText } = render(<MyComponent />);

  // Ініціалізуємо userEvent
  const user = userEvent.setup();
  await user.click(getByText("Завантажити дані"));

  // findByText сам зачекає оновлення DOM без setTimeout
  const userName = await findByText("John Doe");
  expect(userName).toBeInTheDocument();
});
