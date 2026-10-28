class Product:
    """Store the title, image URL, price, rating, and optional ID."""
    def __init__(self, title, image, price, rating, product_id=None):
        if not title.strip(): raise ValueError("Product title cannot be empty.")
        if float(price) < 0: raise ValueError("Price cannot be negative.")
        if not 0 <= float(rating) <= 5: raise ValueError("Rating must be between 0 and 5.")
        self.id, self.title, self.image = product_id, title, image
        self.price, self.rating = float(price), float(rating)
