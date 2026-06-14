import ProductCard from "./components/ProductCard";
import type { ProductType } from "./types/ProductType";

function App() {

  const product: ProductType = {
    id: 1,
    title: "Wireless Earbuds",
    description: "Bluetooth in-ear headphones with high quality sound.",
    price: 100,
    discount: 20,
    image: "https://via.placeholder.com/300x200"
  };

  return (
    <div style={{
      display: "flex",
      justifyContent: "center",
      marginTop: "50px"
    }}>
      <ProductCard product={product} />
    </div>
  );
}

export default App;
