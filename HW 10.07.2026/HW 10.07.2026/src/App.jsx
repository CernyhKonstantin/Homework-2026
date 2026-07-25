import { useEffect, useState } from "react";

import api from "./services/api";

import CategoryTree from "./components/CategoryTree";

import ProductList from "./components/ProductList";

import SearchBar from "./components/SearchBar";

export default function App() {

    const [categories, setCategories] = useState([]);

    const [products, setProducts] = useState([]);

    useEffect(() => {

        loadCategories();

    }, []);

    async function loadCategories() {

        const response = await api.get("/Category/root");

        setCategories(response.data);

    }

    async function search(name) {

        const response = await api.get("/Category/search?name=" + name);

        setCategories(response.data);

    }

    return (

        <div>

            <h1>Store</h1>

            <SearchBar onSearch={search} />

            <CategoryTree

                categories={categories}

                onProducts={setProducts}

            />

            <ProductList

                products={products}

            />

        </div>

    );

}