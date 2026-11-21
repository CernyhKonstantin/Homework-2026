from django.contrib import admin
from .models import Buyer, Seller, Product, Sale
@admin.register(Buyer)
class BuyerAdmin(admin.ModelAdmin):
    list_display = ("first_name", "last_name", "phone", "email")
    search_fields = ("first_name", "last_name", "email")
@admin.register(Seller)
class SellerAdmin(admin.ModelAdmin):
    list_display = ("first_name", "last_name", "role", "employment_date", "email")
    list_filter = ("role",)
    search_fields = ("first_name", "last_name", "email")
@admin.register(Product)
class ProductAdmin(admin.ModelAdmin):
    list_display = ("name", "stock_quantity")
    search_fields = ("name",)
@admin.register(Sale)
class SaleAdmin(admin.ModelAdmin):
    list_display = ("sale_date", "product", "buyer", "seller", "quantity", "sale_amount")
    list_filter = ("sale_date", "seller")
    search_fields = ("product__name", "buyer__last_name", "seller__last_name")
