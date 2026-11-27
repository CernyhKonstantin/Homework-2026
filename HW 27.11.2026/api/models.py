import uuid

from django.db import models


class Product(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4)
    name = models.CharField(max_length=255)
    price = models.FloatField()
    description = models.TextField(null=True)

    class Meta:
        db_table = "drf_products"

    def __str__(self):
        return self.name


class Category(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    name = models.CharField(max_length=255)

    class Meta:
        db_table = "drf_categories"
        ordering = ["name"]

    def __str__(self):
        return self.name
