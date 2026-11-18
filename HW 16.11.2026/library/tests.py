from django.contrib.auth.models import Group, User
from django.test import TestCase
from django.urls import reverse
from .models import Book, Reader, Loan


class LibraryTests(TestCase):
    @classmethod
    def setUpTestData(cls):
        cls.available = Book.objects.create(
            title="Open Book", author="A Writer", publication_year=2020,
            genre="Fiction", publisher="Example Press"
        )
        cls.borrowed = Book.objects.create(
            title="On Loan", author="B Writer", publication_year=2021,
            genre="History", publisher="Example Press"
        )
        reader = Reader.objects.create(
            first_name="Jamie", last_name="Reader", phone="+4912345",
            email="jamie@example.com"
        )
        Loan.objects.create(reader=reader, book=cls.borrowed)
        cls.member = User.objects.create_user(username="member", password="TestPass123!")
        cls.member.groups.add(Group.objects.get(name="Readers"))
        cls.staff = User.objects.create_user(
            username="librarian", password="TestPass123!", is_staff=True
        )

    def test_availability_property(self):
        self.assertTrue(self.available.is_available)
        self.assertFalse(self.borrowed.is_available)

    def test_all_books_mode(self):
        self.client.force_login(self.member)
        response = self.client.get(reverse("book-list"))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Open Book")
        self.assertContains(response, "On Loan")

    def test_available_books_mode(self):
        self.client.force_login(self.member)
        response = self.client.get(reverse("book-list"), {"availability": "available"})
        self.assertContains(response, "Open Book")
        self.assertNotContains(response, "On Loan")

    def test_reader_group_cannot_view_reader_pages(self):
        self.client.force_login(self.member)
        self.assertEqual(self.client.get(reverse("reader-list")).status_code, 403)

    def test_staff_can_view_reader_pages(self):
        self.client.force_login(self.staff)
        self.assertEqual(self.client.get(reverse("reader-list")).status_code, 200)

    def test_anonymous_redirects_to_login(self):
        response = self.client.get(reverse("book-list"))
        self.assertEqual(response.status_code, 302)
        self.assertIn("/accounts/login/", response.url)
