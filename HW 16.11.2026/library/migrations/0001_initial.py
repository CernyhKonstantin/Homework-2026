from django.db import migrations, models
import django.db.models.deletion
from django.db.models import Q
import django.utils.timezone

class Migration(migrations.Migration):
    initial = True
    dependencies = [("contenttypes", "0002_remove_content_type_name")]
    operations = [
        migrations.CreateModel(
            name="Book",
            fields=[
                ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
                ("title", models.CharField(max_length=200)),
                ("author", models.CharField(max_length=160)),
                ("publication_year", models.PositiveIntegerField()),
                ("genre", models.CharField(max_length=100)),
                ("publisher", models.CharField(max_length=160)),
                ("created_at", models.DateTimeField(auto_now_add=True)),
            ],
            options={"ordering": ["title"]},
        ),
        migrations.CreateModel(
            name="Reader",
            fields=[
                ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
                ("first_name", models.CharField(max_length=100)),
                ("last_name", models.CharField(max_length=100)),
                ("phone", models.CharField(max_length=30)),
                ("email", models.EmailField(max_length=254, unique=True)),
                ("registration_date", models.DateField(default=django.utils.timezone.localdate)),
            ],
            options={"ordering": ["last_name", "first_name"]},
        ),
        migrations.CreateModel(
            name="Loan",
            fields=[
                ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
                ("borrowed_on", models.DateField(default=django.utils.timezone.localdate)),
                ("returned_on", models.DateField(blank=True, null=True)),
                ("book", models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name="loans", to="library.book")),
                ("reader", models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name="loans", to="library.reader")),
            ],
            options={"ordering": ["-borrowed_on", "book__title"], "constraints": [
                models.UniqueConstraint(condition=Q(("returned_on__isnull", True)), fields=("book",), name="unique_active_loan_per_book")
            ]},
        ),
    ]
