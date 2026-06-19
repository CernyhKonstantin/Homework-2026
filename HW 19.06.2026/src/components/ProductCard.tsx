import { Product } from "../types/Product";

import FavoriteButton from "./FavoriteButton";
import CartButton from "./CartButton";

import {
    addToFavorites,
    addToCart
} from "../services/storage";

interface ProductCardProps {
    product: Product;
}

function ProductCard({
    product
}: ProductCardProps) {

    return (

        <div className="product-card">

            <img
                src={product.image}
                alt={product.title}
            />

            <h3>{product.title}</h3>

            <p>⭐ {product.rating}</p>

            <p>
                <del>{product.oldPrice} €</del>
            </p>

            <h2>{product.price} €</h2>

            <p>{product.discount}% OFF</p>

            <div className="button-row">

                <FavoriteButton
                    onClick={() => addToFavorites(product)}
                />

                <CartButton
                    onClick={() => addToCart(product)}
                />

            </div>

        </div>

    );
}

export default ProductCard;