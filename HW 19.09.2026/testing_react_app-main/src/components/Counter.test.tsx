import { fireEvent, render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';
import Counter from './Counter';

describe('Counter component', () => {
  it('renders the initial value from props', () => {
    render(<Counter value={5} />);

    expect(screen.getByTestId('counter-value')).toHaveTextContent('5');
  });

  it('increases the counter by 1 when the button is clicked', () => {
    render(<Counter value={5} />);

    fireEvent.click(screen.getByRole('button', { name: /increase by 1/i }));

    expect(screen.getByTestId('counter-value')).toHaveTextContent('6');
  });

  it('can be increased multiple times', () => {
    render(<Counter value={0} />);

    const button = screen.getByRole('button', { name: /increase by 1/i });

    fireEvent.click(button);
    fireEvent.click(button);
    fireEvent.click(button);

    expect(screen.getByTestId('counter-value')).toHaveTextContent('3');
  });
});
