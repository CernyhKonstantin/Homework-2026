from django.db import models
from django.db.models import Q
from django.utils import timezone


class Book(models.Model):
    title = models.CharField(max_length=200)
    author = models.CharField(max_length=160)
    publication_year = models.PositiveIntegerField()
    genre = models.CharField(max_length=100)
    publisher = models.CharField(max_length=160)
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ["title"]

    def __str__(self):
        return f"{self.title} — {self.author}"

    @property
    def is_available(self):
        return not self.loans.filter(returned_on__isnull=True).exists()

    @property
    def current_borrower(self):
        loan = self.loans.filter(returned_on__isnull=True).select_related("reader").first()
        return loan.reader if loan else None


class Reader(models.Model):
    first_name = models.CharField(max_length=100)
    last_name = models.CharField(max_length=100)
    phone = models.CharField(max_length=30)
    email = models.EmailField(unique=True)
    registration_date = models.DateField(default=timezone.localdate)

    class Meta:
        ordering = ["last_name", "first_name"]

    def __str__(self):
        return f"{self.first_name} {self.last_name}"


class Loan(models.Model):
    reader = models.ForeignKey(Reader, on_delete=models.CASCADE, related_name="loans")
    book = models.ForeignKey(Book, on_delete=models.CASCADE, related_name="loans")
    borrowed_on = models.DateField(default=timezone.localdate)
    returned_on = models.DateField(null=True, blank=True)

    class Meta:
        ordering = ["-borrowed_on", "book__title"]
        constraints = [
            models.UniqueConstraint(
                fields=["book"],
                condition=Q(returned_on__isnull=True),
                name="unique_active_loan_per_book",
            )
        ]

    def __str__(self):
        state = "active" if self.returned_on is None else "returned"
        return f"{self.book} / {self.reader} ({state})"
