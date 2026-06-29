import { useContext } from "react";

import { ProductContext } from "../context/ProductContext";

import ProductCard from "./ProductCard";

function ProductList() {

    const { filteredProducts } = useContext(ProductContext);

    if (filteredProducts.length === 0) {

        return (
            <h2>Nothing was found.</h2>
        );
    }

    return (

        <div className="product-list">

            {
                filteredProducts.map(product => (

                    <ProductCard
                        key={product.id}
                        product={product}
                    />

                ))
            }

        </div>

    );
}

export default ProductList;