type CartButtonProps = {
    onAdd: () => void;
}

function CartButton({ onAdd }: CartButtonProps) {
    return (
        <button onClick={onAdd}>
            Add to cart 🛒
        </button>
    )
}

export default CartButton;
