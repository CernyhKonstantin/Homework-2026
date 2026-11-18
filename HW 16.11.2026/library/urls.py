from django.urls import path
from . import views

urlpatterns = [
    path("", views.home, name="home"),
    path("books/", views.book_list, name="book-list"),
    path("books/<int:pk>/", views.book_detail, name="book-detail"),
    path("readers/", views.reader_list, name="reader-list"),
    path("readers/<int:pk>/", views.reader_detail, name="reader-detail"),
    path("loans/", views.loan_list, name="loan-list"),
]
