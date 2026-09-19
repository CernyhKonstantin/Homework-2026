import { expect, test } from "vitest";
import { sum } from "./sum";

test("adds 1 + 2 to equal 3", () => {
  expect(sum(1, 2)).toBe(3);
});
test("adds 2 + (-7) to equal -5", () => {
  expect(sum(2, -7)).toBe(-5);
});
