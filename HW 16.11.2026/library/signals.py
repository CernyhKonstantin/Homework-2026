from django.db.models.signals import post_migrate
from django.dispatch import receiver


@receiver(post_migrate)
def create_readers_group(sender, **kwargs):
    if sender.name != "library":
        return

    from django.contrib.auth.models import Group, Permission
    from django.contrib.contenttypes.models import ContentType
    from .models import Book

    group, _ = Group.objects.get_or_create(name="Readers")
    content_type = ContentType.objects.get_for_model(Book)
    permission = Permission.objects.get(content_type=content_type, codename="view_book")
    group.permissions.set([permission])
