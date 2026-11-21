from django import template
register = template.Library()
@register.filter
def get_attribute(obj, name):
    value = getattr(obj, name, "")
    if callable(value):
        value = value()
    if name == "role" and hasattr(obj, "get_role_display"):
        return obj.get_role_display()
    return value
