import { useContext, useState } from "react";

import { ProductContext } from "../context/ProductContext";

function Search() {

    const { searchProducts } = useContext(ProductContext);

    const [text, setText] = useState("");

    function handleSearch() {
        searchProducts(text);
    }

    return (

        <div className="search-container">

            <input
                type="text"
                placeholder="Search products..."
                value={text}
                onChange={(e) => setText(e.target.value)}
            />

            <button onClick={handleSearch}>
                Search
            </button>

        </div>

    );
}

export default Search;