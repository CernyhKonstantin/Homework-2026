from django.test import SimpleTestCase
from django.urls import reverse
class RouteTests(SimpleTestCase):
    def test_main_routes(self):
        self.assertEqual(reverse("home"), "/")
        self.assertEqual(reverse("country_list"), "/countries/")
        self.assertEqual(reverse("region_list"), "/regions/")
        self.assertEqual(reverse("language_list"), "/languages/")
