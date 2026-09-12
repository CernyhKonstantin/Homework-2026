import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  items: []
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action) => {
      const product = action.payload;
      const existingItem = state.items.find(item => item.productId === product.id);

      if (existingItem) {
        existingItem.quantity += 1;
      } else {
        state.items.push({
          productId: product.id,
          name: product.name,
          price: Number(product.price),
          categoryName: product.categoryName,
          quantity: 1
        });
      }
    },
    removeFromCart: (state, action) => {
      state.items = state.items.filter(item => item.productId !== action.payload);
    },
    increaseQuantity: (state, action) => {
      const item = state.items.find(item => item.productId === action.payload);
      if (item) item.quantity += 1;
    },
    decreaseQuantity: (state, action) => {
      const item = state.items.find(item => item.productId === action.payload);
      if (!item) return;

      if (item.quantity > 1) {
        item.quantity -= 1;
      } else {
        state.items = state.items.filter(cartItem => cartItem.productId !== action.payload);
      }
    },
    clearCart: state => {
      state.items = [];
    }
  }
});

export const {
  addToCart,
  removeFromCart,
  increaseQuantity,
  decreaseQuantity,
  clearCart
} = cartSlice.actions;

export const selectCartItems = state => state.cart.items;
export const selectCartItemCount = state =>
  state.cart.items.reduce((total, item) => total + item.quantity, 0);
export const selectCartTotal = state =>
  state.cart.items.reduce((total, item) => total + item.price * item.quantity, 0);

export default cartSlice.reducer;
