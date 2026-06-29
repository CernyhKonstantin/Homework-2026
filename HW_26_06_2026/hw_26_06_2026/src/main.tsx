import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import App from "./App";
import "./index.css";

import { ProductProvider } from "./context/ProductContext";

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <ProductProvider>
            <App />
        </ProductProvider>
    </StrictMode>
);