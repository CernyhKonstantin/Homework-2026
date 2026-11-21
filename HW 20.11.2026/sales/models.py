from django.db import models

class Buyer(models.Model):
    first_name = models.CharField(max_length=100)
    last_name = models.CharField(max_length=100)
    phone = models.CharField(max_length=30)
    email = models.EmailField(unique=True)
    def __str__(self):
        return f"{self.first_name} {self.last_name}"

class Seller(models.Model):
    class Role(models.TextChoices):
        SALES_REP = "sales_rep", "Sales Representative"
        SENIOR = "senior", "Senior Sales Representative"
        MANAGER = "manager", "Sales Manager"
    first_name = models.CharField(max_length=100)
    last_name = models.CharField(max_length=100)
    phone = models.CharField(max_length=30)
    email = models.EmailField(unique=True)
    employment_date = models.DateField()
    role = models.CharField(max_length=20, choices=Role.choices, default=Role.SALES_REP)
    def __str__(self):
        return f"{self.first_name} {self.last_name} ({self.get_role_display()})"

class Product(models.Model):
    name = models.CharField(max_length=200)
    description = models.TextField(blank=True)
    stock_quantity = models.PositiveIntegerField(default=0)
    def __str__(self):
        return self.name

class Sale(models.Model):
    buyer = models.ForeignKey(Buyer, on_delete=models.PROTECT, related_name="sales")
    seller = models.ForeignKey(Seller, on_delete=models.PROTECT, related_name="sales")
    product = models.ForeignKey(Product, on_delete=models.PROTECT, related_name="sales")
    sale_date = models.DateField()
    sale_amount = models.DecimalField(max_digits=10, decimal_places=2)
    quantity = models.PositiveIntegerField(default=1)
    notes = models.TextField(blank=True)
    class Meta:
        ordering = ["-sale_date", "-id"]
    def __str__(self):
        return f"{self.product} sold on {self.sale_date}"
