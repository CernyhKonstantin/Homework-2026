import { Money } from "./Money";
import { render, screen } from "@testing-library/react";
test("Money should format 20 with two decimals 20.00", () => {
  const amount = 20; //arrange
  render(<Money amount={amount}></Money>); //act
  const formatted = screen.getByText("20.00"); //assert
  expect(formatted).toBeInTheDocument(); //assert
});
