from flask import Flask, abort, redirect, render_template, request, url_for
from product import Product
from models.product_model import ProductModel

app = Flask(__name__)
product_model = ProductModel()

@app.get("/")
def home():
    return render_template("index.html", products=product_model.get_all_products())

@app.get("/product/<int:product_id>")
def product_detail(product_id):
    product = product_model.get_product_by_id(product_id)
    if product is None: abort(404)
    return render_template("product_detail.html", product=product)

@app.route("/add-product", methods=["GET", "POST"])
def add_product():
    error = None
    if request.method == "POST":
        title, image = request.form.get("title", "").strip(), request.form.get("image", "").strip()
        try:
            price, rating = float(request.form.get("price", "")), float(request.form.get("rating", ""))
            if not title or not image or price < 0 or not 0 <= rating <= 5: raise ValueError
            product_model.create_product(Product(title, image, price, rating))
            return redirect(url_for("home"))
        except ValueError:
            error = "Enter a title, image URL, non-negative price, and rating between 0 and 5."
    return render_template("add_product.html", error=error)

@app.errorhandler(404)
def not_found(_error):
    return render_template("404.html"), 404

if __name__ == "__main__":
    app.run(debug=True)
