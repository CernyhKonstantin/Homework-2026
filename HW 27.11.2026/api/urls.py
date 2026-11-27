from django.urls import path
from rest_framework.routers import DefaultRouter

from . import views

router = DefaultRouter()
router.register("products", views.ProductViewSet, "products_view_set")

urlpatterns = [
    path("sample/", views.ExampleView.as_view(), name="sample_view"),
    path("products/", views.ProductListView.as_view(), name="productlist_view"),
    path("categories/", views.CategoryListCreateView.as_view(), name="category_list_create"),
    path("categories/<uuid:category_id>/", views.CategoryDetailView.as_view(), name="category_detail"),
]
