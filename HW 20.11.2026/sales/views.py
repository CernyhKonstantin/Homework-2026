from django.shortcuts import render, redirect
from django.db.models import Sum, Count
from .models import Buyer, Seller, Product, Sale
from .forms import BuyerForm, SellerForm, ProductForm, SaleForm

def dashboard(request):
    context = {
        "buyer_count": Buyer.objects.count(),
        "seller_count": Seller.objects.count(),
        "product_count": Product.objects.count(),
        "sale_count": Sale.objects.count(),
        "total_revenue": Sale.objects.aggregate(total=Sum("sale_amount"))["total"] or 0,
        "recent_sales": Sale.objects.select_related("buyer", "seller", "product")[:8],
    }
    return render(request, "sales/dashboard.html", context)

def buyers(request):
    form = BuyerForm(request.POST or None)
    if request.method == "POST" and form.is_valid():
        form.save()
        return redirect("buyers")
    return render(request, "sales/list_form.html", {"title": "Buyers", "items": Buyer.objects.order_by("last_name", "first_name"), "form": form, "columns": ["first_name", "last_name", "phone", "email"]})

def sellers(request):
    form = SellerForm(request.POST or None)
    if request.method == "POST" and form.is_valid():
        form.save()
        return redirect("sellers")
    return render(request, "sales/list_form.html", {"title": "Sellers", "items": Seller.objects.order_by("last_name", "first_name"), "form": form, "columns": ["first_name", "last_name", "phone", "email", "employment_date", "role"]})

def products(request):
    form = ProductForm(request.POST or None)
    if request.method == "POST" and form.is_valid():
        form.save()
        return redirect("products")
    return render(request, "sales/list_form.html", {"title": "Products", "items": Product.objects.order_by("name"), "form": form, "columns": ["name", "description", "stock_quantity"]})

def sales(request):
    form = SaleForm(request.POST or None)
    if request.method == "POST" and form.is_valid():
        form.save()
        return redirect("sales")
    queryset = Sale.objects.select_related("buyer", "seller", "product").order_by("-sale_date")
    return render(request, "sales/sales.html", {"title": "Sales", "items": queryset, "form": form})

def sales_report(request):
    # Django ORM query: fetch each sale with its buyer, seller and product in joined SQL.
    records = Sale.objects.select_related("buyer", "seller", "product").order_by("-sale_date")
    totals_by_product = Sale.objects.values("product__name").annotate(sales_count=Count("id"), revenue=Sum("sale_amount")).order_by("-revenue")
    return render(request, "sales/report.html", {"records": records, "totals_by_product": totals_by_product})
