from django.test import TestCase
from django.urls import reverse
from datetime import date
from .models import Buyer, Seller, Product, Sale

class SalesAppTests(TestCase):
    def setUp(self):
        self.buyer = Buyer.objects.create(first_name="Alex", last_name="Buyer", phone="123", email="buyer@example.com")
        self.seller = Seller.objects.create(first_name="Sam", last_name="Seller", phone="456", email="seller@example.com", employment_date=date(2024, 1, 1))
        self.product = Product.objects.create(name="Laptop", description="Test item", stock_quantity=5)
        self.sale = Sale.objects.create(buyer=self.buyer, seller=self.seller, product=self.product, sale_date=date(2026, 11, 20), sale_amount=950, quantity=1)
    def test_dashboard_loads(self):
        response = self.client.get(reverse("dashboard"))
        self.assertEqual(response.status_code, 200)
    def test_sales_page_joins_related_data(self):
        response = self.client.get(reverse("sales"))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Laptop")
        self.assertContains(response, "Alex Buyer")
        self.assertContains(response, "Sam Seller")
    def test_report_displays_product_buyer_seller(self):
        response = self.client.get(reverse("sales_report"))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Laptop")
        self.assertContains(response, "Alex Buyer")
        self.assertContains(response, "Sam Seller")
