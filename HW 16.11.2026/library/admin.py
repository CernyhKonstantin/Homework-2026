from django.contrib import admin
from .models import Book, Reader, Loan


@admin.register(Book)
class BookAdmin(admin.ModelAdmin):
    list_display = ("title", "author", "publication_year", "genre", "publisher", "availability")
    search_fields = ("title", "author", "publisher")
    list_filter = ("genre", "publication_year")

    @admin.display(description="Availability")
    def availability(self, obj):
        return "Available" if obj.is_available else "Borrowed"


@admin.register(Reader)
class ReaderAdmin(admin.ModelAdmin):
    list_display = ("first_name", "last_name", "email", "phone", "registration_date")
    search_fields = ("first_name", "last_name", "email")


@admin.register(Loan)
class LoanAdmin(admin.ModelAdmin):
    list_display = ("book", "reader", "borrowed_on", "returned_on")
    list_filter = ("borrowed_on", "returned_on")
    search_fields = ("book__title", "reader__first_name", "reader__last_name")
