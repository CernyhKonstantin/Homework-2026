from django.test import TestCase
from django.urls import reverse
class PersonalAccountTests(TestCase):
    def setUp(self): self.client.cookies["accepted_rules"]="yes"
    def test_rules_redirect_without_cookie(self):
        client=self.client_class(); response=client.get(reverse("home")); self.assertRedirects(response,reverse("rules"),fetch_redirect_response=False)
    def test_accept_rules(self):
        client=self.client_class(); response=client.post(reverse("rules")); self.assertEqual(response.cookies["accepted_rules"].value,"yes")
    def test_name_cookie_seven_days(self):
        response=self.client.post(reverse("home"),{"name":"Андрій"}); self.assertEqual(response.cookies["user_name"].value,"Андрій"); self.assertEqual(int(response.cookies["user_name"]["max-age"]),604800)
    def test_preferences(self):
        response=self.client.post(reverse("settings"),{"theme":"light","language":"en"}); self.assertEqual(response.cookies["theme"].value,"light"); self.assertEqual(response.cookies["language"].value,"en")
    def test_statistics_uses_cookies(self):
        self.client.cookies.update({"user_name":"Andrii","visit_count":"7","theme":"light"}); response=self.client.get(reverse("statistics")); self.assertContains(response,"Andrii"); self.assertContains(response,"7")
    def test_delete_name_cookie(self):
        response=self.client.get(reverse("clear_name")); self.assertEqual(response.cookies["user_name"]["max-age"],0)
    def test_visit_count_and_last_visit(self):
        self.client.cookies["visit_count"]="2"; response=self.client.get(reverse("home")); self.assertEqual(response.cookies["visit_count"].value,"3"); self.assertIn("last_visit",response.cookies)
    def test_rate_limit(self):
        client=self.client_class(); client.cookies["accepted_rules"]="yes"
        for _ in range(10): response=client.get(reverse("home"))
        response=client.get(reverse("home")); self.assertEqual(response.status_code,429)
