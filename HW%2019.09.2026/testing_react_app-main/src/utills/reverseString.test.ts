import { describe, expect, it } from 'vitest';
import { reverseString } from './reverseString';

describe('reverseString', () => {
  it('reverses a simple string', () => {
    expect(reverseString('hello')).toBe('olleh');
  });

  it('reverses a string with spaces and punctuation', () => {
    expect(reverseString('Hello, world!')).toBe('!dlrow ,olleH');
  });

  it('returns an empty string for empty input', () => {
    expect(reverseString('')).toBe('');
  });
});
