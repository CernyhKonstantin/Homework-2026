import { useState } from 'react';

export interface CounterProps {
  value: number;
}

const Counter = ({ value }: CounterProps) => {
  const [count, setCount] = useState(value);

  const increment = () => {
    setCount((currentCount) => currentCount + 1);
  };

  return (
    <section className="counter-card">
      <h2>Counter</h2>
      <p className="counter-value" data-testid="counter-value">
        {count}
      </p>
      <button type="button" onClick={increment}>
        Increase by 1
      </button>
    </section>
  );
};

export default Counter;
