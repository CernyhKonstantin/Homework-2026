from django import forms
from .models import Contact

class ContactForm(forms.ModelForm):
    class Meta:
        model = Contact
        fields = ["first_name", "last_name", "email", "phone", "notes"]
        widgets = {
            "first_name": forms.TextInput(attrs={"placeholder": "First name"}),
            "last_name": forms.TextInput(attrs={"placeholder": "Last name"}),
            "email": forms.EmailInput(attrs={"placeholder": "name@example.com"}),
            "phone": forms.TextInput(attrs={"placeholder": "+49 123 456789"}),
            "notes": forms.Textarea(attrs={"rows": 4, "placeholder": "Additional notes"}),
        }
