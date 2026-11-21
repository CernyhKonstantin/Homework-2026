from django import forms
from .models import Buyer, Seller, Product, Sale

class StyledModelForm(forms.ModelForm):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        for field in self.fields.values():
            field.widget.attrs.setdefault("class", "form-control")

class BuyerForm(StyledModelForm):
    class Meta:
        model = Buyer
        fields = ["first_name", "last_name", "phone", "email"]

class SellerForm(StyledModelForm):
    class Meta:
        model = Seller
        fields = ["first_name", "last_name", "phone", "email", "employment_date", "role"]
        widgets = {"employment_date": forms.DateInput(attrs={"type": "date"})}

class ProductForm(StyledModelForm):
    class Meta:
        model = Product
        fields = ["name", "description", "stock_quantity"]

class SaleForm(StyledModelForm):
    class Meta:
        model = Sale
        fields = ["buyer", "seller", "product", "sale_date", "sale_amount", "quantity", "notes"]
        widgets = {"sale_date": forms.DateInput(attrs={"type": "date"})}
