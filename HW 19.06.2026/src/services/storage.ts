import { Product } from "../types/Product";

export interface CartItem extends Product {
    quantity: number;
}

function updateStorageEvent() {
    window.dispatchEvent(new Event("storageUpdated"));
}

export function addToFavorites(product: Product): void {
    const favorites: Product[] = JSON.parse(
        localStorage.getItem("favorites") || "[]"
    );

    const exists = favorites.find(
        item => item.id === product.id
    );

    if (!exists) {
        favorites.push(product);

        localStorage.setItem(
            "favorites",
            JSON.stringify(favorites)
        );

        updateStorageEvent();
    }
}

export function addToCart(product: Product): void {

    const cart: CartItem[] = JSON.parse(
        localStorage.getItem("cart") || "[]"
    );

    const index = cart.findIndex(
        item => item.id === product.id
    );

    if (index !== -1) {
        cart[index].quantity++;
    } else {
        cart.push({
            ...product,
            quantity: 1
        });
    }

    localStorage.setItem(
        "cart",
        JSON.stringify(cart)
    );

    updateStorageEvent();
}