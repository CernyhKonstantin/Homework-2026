from django.db import migrations, models
import django.db.models.deletion

class Migration(migrations.Migration):
    initial = True
    dependencies = []
    operations = [
        migrations.CreateModel(name="Buyer", fields=[
            ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
            ("first_name", models.CharField(max_length=100)), ("last_name", models.CharField(max_length=100)),
            ("phone", models.CharField(max_length=30)), ("email", models.EmailField(max_length=254, unique=True)),
        ]),
        migrations.CreateModel(name="Seller", fields=[
            ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
            ("first_name", models.CharField(max_length=100)), ("last_name", models.CharField(max_length=100)),
            ("phone", models.CharField(max_length=30)), ("email", models.EmailField(max_length=254, unique=True)),
            ("employment_date", models.DateField()),
            ("role", models.CharField(choices=[("sales_rep","Sales Representative"),("senior","Senior Sales Representative"),("manager","Sales Manager")], default="sales_rep", max_length=20)),
        ]),
        migrations.CreateModel(name="Product", fields=[
            ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
            ("name", models.CharField(max_length=200)), ("description", models.TextField(blank=True)),
            ("stock_quantity", models.PositiveIntegerField(default=0)),
        ]),
        migrations.CreateModel(name="Sale", fields=[
            ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
            ("sale_date", models.DateField()), ("sale_amount", models.DecimalField(decimal_places=2, max_digits=10)),
            ("quantity", models.PositiveIntegerField(default=1)), ("notes", models.TextField(blank=True)),
            ("buyer", models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name="sales", to="sales.buyer")),
            ("seller", models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name="sales", to="sales.seller")),
            ("product", models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name="sales", to="sales.product")),
        ], options={"ordering":["-sale_date","-id"]}),
    ]
