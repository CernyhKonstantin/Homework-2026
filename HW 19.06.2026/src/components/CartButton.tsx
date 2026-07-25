import { FaShoppingCart } from "react-icons/fa";

interface CartButtonProps {
    onClick: () => void;
}

function CartButton({ onClick }: CartButtonProps) {
    return (
        <button className="cart-button" onClick={onClick}>
            <FaShoppingCart />
        </button>
    );
}

export default CartButton;