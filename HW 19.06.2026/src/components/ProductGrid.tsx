import { Product } from "../types/Product";
import ProductCard from "./ProductCard";

interface ProductGridProps {
    products: Product[];
}

function ProductGrid({
    products
}: ProductGridProps) {
    return (
        <div className="product-grid">

            {products.map((product) => (
                <ProductCard
                    key={product.id}
                    product={product}
                />
            ))}

        </div>
    );
}

export default ProductGrid;