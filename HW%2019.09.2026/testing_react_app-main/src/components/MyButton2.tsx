import React from "react";

interface MyButtonProps {
  id: number;
  onClick: (id: number, message: string) => void;
  children?: React.ReactNode;
}

const MyButton2: React.FC<MyButtonProps> = ({
  id,
  onClick,
  children = "Нажми меня",
}) => {
  const handleClick = () => {
    const message = "клік помічено";

    // ОСЬ ЦЕЙ РЯДОК ОБОВ'ЯЗКОВИЙ ДЛЯ ПРОХОДЖЕННЯ ТЕСТУ:
    console.log(id, message);

    onClick(id, message);
  };

  return <button onClick={handleClick}>{children}</button>;
};

export default MyButton2;
