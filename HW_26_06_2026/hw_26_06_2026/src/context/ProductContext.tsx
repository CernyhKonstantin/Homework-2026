import { createContext, useState } from "react";
import type { ReactNode } from "react";

import type { Product } from "../types/Product";
import { products } from "../data/products";

interface ProductContextProps {
    filteredProducts: Product[];
    searchProducts: (text: string) => void;
}

export const ProductContext = createContext<ProductContextProps>({
    filteredProducts: [],
    searchProducts: () => { }
});

interface ProductProviderProps {
    children: ReactNode;
}

export function ProductProvider({ children }: ProductProviderProps) {

    const [filteredProducts, setFilteredProducts] = useState<Product[]>(products);

    function searchProducts(text: string) {

        if (text.trim() === "") {
            setFilteredProducts(products);
            return;
        }

        const result = products.filter(product =>
            product.name.toLowerCase().includes(text.toLowerCase())
        );

        setFilteredProducts(result);
    }

    return (
        <ProductContext.Provider
            value={{
                filteredProducts,
                searchProducts
            }}
        >
            {children}
        </ProductContext.Provider>
    );
}