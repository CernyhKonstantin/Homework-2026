import "./App.css";

import Search from "./components/Search";
import ProductList from "./components/ProductList";

function App() {
    return (
        <div className="container">

            <h1>Product Search</h1>

            <Search />

            <ProductList />

        </div>
    );
}

export default App;