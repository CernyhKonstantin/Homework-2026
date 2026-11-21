from django.urls import path
from . import views
urlpatterns = [
    path("", views.dashboard, name="dashboard"),
    path("buyers/", views.buyers, name="buyers"),
    path("sellers/", views.sellers, name="sellers"),
    path("products/", views.products, name="products"),
    path("sales/", views.sales, name="sales"),
    path("sales/report/", views.sales_report, name="sales_report"),
]
