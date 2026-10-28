from product import Product

class ProductModel:
    """In-memory repository for online store products."""
    def __init__(self):
        self._products = [
            Product("Wireless Headphones", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=900&q=80", 79.99, 4.7, 1),
            Product("Smart Watch", "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=900&q=80", 129.50, 4.5, 2),
            Product("Camera", "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?auto=format&fit=crop&w=900&q=80", 499.00, 4.8, 3),
            Product("Running Shoes", "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=900&q=80", 89.00, 4.6, 4),
            Product("Backpack", "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?auto=format&fit=crop&w=900&q=80", 54.95, 4.4, 5),
            Product("Coffee Maker", "https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?auto=format&fit=crop&w=900&q=80", 99.00, 4.3, 6),
        ]
        self._next_id = 7
    def get_all_products(self):
        return list(self._products)
    def get_product_by_id(self, product_id):
        return next((p for p in self._products if p.id == product_id), None)
    def create_product(self, product):
        if not isinstance(product, Product): raise TypeError("product must be a Product instance")
        if product.id is None:
            product.id, self._next_id = self._next_id, self._next_id + 1
        elif self.get_product_by_id(product.id) is not None:
            raise ValueError(f"Product ID {product.id} already exists.")
        else: self._next_id = max(self._next_id, product.id + 1)
        self._products.append(product)
        return product
