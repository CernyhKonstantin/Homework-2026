from django.contrib.auth.decorators import login_required, user_passes_test
from django.core.exceptions import PermissionDenied
from django.shortcuts import get_object_or_404, redirect, render
from .models import Book, Reader, Loan


def staff_required(user):
    return user.is_authenticated and (user.is_staff or user.is_superuser)


def book_access(user):
    return user.is_authenticated and (
        user.is_staff or user.is_superuser or user.groups.filter(name="Readers").exists()
    )


@login_required
def home(request):
    return redirect("book-list")


@login_required
def book_list(request):
    if not book_access(request.user):
        raise PermissionDenied
    mode = request.GET.get("availability", "all")
    books = Book.objects.all().prefetch_related("loans")
    if mode == "available":
        books = [book for book in books if book.is_available]
    else:
        mode = "all"
    return render(request, "library/book_list.html", {"books": books, "mode": mode})


@login_required
def book_detail(request, pk):
    if not book_access(request.user):
        raise PermissionDenied
    book = get_object_or_404(Book, pk=pk)
    active_loan = book.loans.filter(returned_on__isnull=True).select_related("reader").first()
    return render(request, "library/book_detail.html", {"book": book, "active_loan": active_loan})


@login_required
@user_passes_test(staff_required)
def reader_list(request):
    return render(request, "library/reader_list.html", {"readers": Reader.objects.all()})


@login_required
@user_passes_test(staff_required)
def reader_detail(request, pk):
    reader = get_object_or_404(Reader.objects.prefetch_related("loans__book"), pk=pk)
    return render(request, "library/reader_detail.html", {"reader": reader})


@login_required
@user_passes_test(staff_required)
def loan_list(request):
    loans = Loan.objects.select_related("book", "reader")
    return render(request, "library/loan_list.html", {"loans": loans})
