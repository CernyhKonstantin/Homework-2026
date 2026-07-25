import type { Product } from "../types/Product";

interface ProductCardProps {
    product: Product;
}

function ProductCard({ product }: ProductCardProps) {
    return (
        <div className="card">

            <img
                src={product.image}
                alt={product.name}
                className="product-image"
            />

            <h3>{product.name}</h3>

            <p>${product.price}</p>

        </div>
    );
}

export default ProductCard;