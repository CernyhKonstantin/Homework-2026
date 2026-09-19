import React, { useState } from "react";

interface User {
  id: number;
  name: string;
}

const MyComponent: React.FC = () => {
  const [data, setData] = useState<User[]>([]);

  const handleFetch = async () => {
    const response = await fetch("/api/users");
    const result = await response.json();
    setData(result);
  };

  return (
    <div>
      <button onClick={handleFetch}>Завантажити дані</button>
      <ul>
        {data.map((user) => (
          <li key={user.id}>{user.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default MyComponent;
