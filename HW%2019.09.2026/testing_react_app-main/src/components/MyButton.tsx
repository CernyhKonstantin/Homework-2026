import React from "react";

interface MyButtonProps {
  onClick: () => void;
  children?: React.ReactNode;
}

const MyButton: React.FC<MyButtonProps> = ({
  onClick,
  children = "Нажми меня",
}) => {
  return <button onClick={onClick}>{children}</button>;
};

export default MyButton;
