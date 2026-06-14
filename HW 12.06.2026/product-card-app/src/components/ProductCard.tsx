import type { ProductType } from "../types/ProductType";
import CartButton from "./CartButton";

type Props = {
    product: ProductType;
};

function ProductCard({ product }: Props) {

    const finalPrice = product.price - (product.price * product.discount) / 100;

    function handleAddToCart() {
        alert(product.title + " added to cart");
    }

    return (
        <div style={{
            border: "1px solid #ccc",
            padding: "15px",
            borderRadius: "10px",
            width: "300px",
            fontFamily: "Arial"
        }}>
            <img src={product.image} width="100%" />
            <h3>{product.title}</h3>
            <p>{product.description}</p>

            <p>
                <b>Price:</b>{" "}
                <span style={{ textDecoration: "line-through" }}>
                    {product.price}€
                </span>{" "}
                <span style={{ color: "green" }}>
                    {finalPrice}€
                </span>
            </p>

            <p>Discount: {product.discount}%</p>

            <CartButton onAdd={handleAddToCart} />
        </div>
    );
}

export default ProductCard;
