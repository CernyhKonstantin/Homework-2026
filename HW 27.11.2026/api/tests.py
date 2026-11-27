from rest_framework.test import APITestCase
from rest_framework import status
from django.urls import reverse

from .models import Category


class CategoryApiTests(APITestCase):
    def test_category_crud(self):
        collection_url = reverse("category_list_create")
        create_response = self.client.post(collection_url, {"name": "Books"}, format="json")
        self.assertEqual(create_response.status_code, status.HTTP_201_CREATED)
        category_id = create_response.data["id"]
        detail_url = reverse("category_detail", kwargs={"category_id": category_id})

        self.assertEqual(self.client.get(collection_url).status_code, status.HTTP_200_OK)
        self.assertEqual(self.client.get(detail_url).data["name"], "Books")
        self.assertEqual(self.client.patch(detail_url, {"name": "Updated Books"}, format="json").status_code, status.HTTP_200_OK)
        self.assertEqual(self.client.put(detail_url, {"name": "Reference Books"}, format="json").status_code, status.HTTP_200_OK)
        self.assertEqual(self.client.delete(detail_url).status_code, status.HTTP_204_NO_CONTENT)
        self.assertFalse(Category.objects.filter(id=category_id).exists())
