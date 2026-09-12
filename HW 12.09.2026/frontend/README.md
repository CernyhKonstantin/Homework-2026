# HW 12.09.2026 Frontend

React + Vite frontend for the Category and Product creation assignment.

## Requirements
- Node.js 20+
- Running ASP.NET Core API

## Install and run

```powershell
npm install
npm run dev
```

Open the Vite URL shown in the terminal, normally `http://localhost:5173`.

## Features
- Login with the API JWT.
- Category creation with `react-hook-form`.
- Product creation with `react-hook-form`.
- Product image upload using `FormData`.
- Client-side validation.
- Refreshes and displays categories/products after successful creation.
- Data is persisted by the ASP.NET Core backend into SQL Server.

## Delivery addresses

Authenticated users can attach multiple delivery addresses to their account.

The frontend uses `react-hook-form` for the address form and calls:

```http
GET    /api/v1/users/me/addresses
GET    /api/v1/users/me/addresses/{addressId}
POST   /api/v1/users/me/addresses
DELETE /api/v1/users/me/addresses/{addressId}
```

Each request is authorized with the JWT access token stored after login.

## Redux Shopping Cart

The frontend uses Redux Toolkit and React Redux for centralized shopping cart state.

### Store

- `src/store/store.js` configures the Redux store.
- `src/store/cartSlice.js` contains the cart reducer, actions, and selectors.

### Actions

- `addToCart(product)`
- `removeFromCart(productId)`
- `increaseQuantity(productId)`
- `decreaseQuantity(productId)`
- `clearCart()`

The `ProductCatalog` dispatches `addToCart(product)`, while the `Cart` component dispatches the remaining cart actions.
