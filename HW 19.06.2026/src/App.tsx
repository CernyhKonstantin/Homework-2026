import { useEffect, useState } from "react";

import "./App.css";

import { products } from "./data/products";

import ProductGrid from "./components/ProductGrid";
import SearchBar from "./components/SearchBar";
import CategoryFilter from "./components/CategoryFilter";

function App() {

    const [search, setSearch] = useState("");

    const [category, setCategory] = useState("All");

    const [favoritesCount, setFavoritesCount] = useState(0);

    const [cartCount, setCartCount] = useState(0);

    function updateCounters() {

        const favorites = JSON.parse(
            localStorage.getItem("favorites") || "[]"
        );

        const cart = JSON.parse(
            localStorage.getItem("cart") || "[]"
        );

        setFavoritesCount(favorites.length);

        let quantity = 0;

        cart.forEach((item: any) => {
            quantity += item.quantity;
        });

        setCartCount(quantity);
    }

    useEffect(() => {

        updateCounters();

        window.addEventListener(
            "storageUpdated",
            updateCounters
        );

        return () => {
            window.removeEventListener(
                "storageUpdated",
                updateCounters
            );
        };

    }, []);

    const filteredProducts = products.filter(product => {

        const matchesSearch =
            product.title
                .toLowerCase()
                .includes(search.toLowerCase());

        const matchesCategory =
            category === "All" ||
            product.category === category;

        return matchesSearch && matchesCategory;

    });

    return (

        <div className="container">

            <h1>HW 19.06.2026</h1>

            <div className="statistics">

                <div className="stat-card">
                    ❤️ Favorites: {favoritesCount}
                </div>

                <div className="stat-card">
                    🛒 Cart: {cartCount}
                </div>

            </div>

            <SearchBar
                search={search}
                setSearch={setSearch}
            />

            <CategoryFilter
                category={category}
                setCategory={setCategory}
            />

            <ProductGrid
                products={filteredProducts}
            />

        </div>

    );

}

export default App;