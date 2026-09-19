import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';
import App from './App';

describe('App Component', () => {
  it('renders the homework title and counter', () => {
    render(<App />);

    expect(screen.getByRole('heading', { name: 'HW 19.09.2026' })).toBeInTheDocument();
    expect(screen.getByTestId('counter-value')).toHaveTextContent('10');
  });
});
