from django.contrib import messages
from django.db.models import Q
from django.shortcuts import get_object_or_404, redirect, render
from .forms import ContactForm
from .models import Contact

def contact_list(request):
    query = request.GET.get("q", "").strip()
    contacts = Contact.objects.all()
    if query:
        contacts = contacts.filter(Q(first_name__icontains=query) | Q(last_name__icontains=query))
    return render(request, "main/contact_list.html", {"contacts": contacts, "query": query, "total_count": Contact.objects.count()})

def contact_create(request):
    if request.method == "POST":
        form = ContactForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, "Contact was added successfully.")
            return redirect("contact_list")
    else:
        form = ContactForm()
    return render(request, "main/contact_form.html", {"form": form, "page_title": "Add contact"})

def contact_update(request, pk):
    contact = get_object_or_404(Contact, pk=pk)
    if request.method == "POST":
        form = ContactForm(request.POST, instance=contact)
        if form.is_valid():
            form.save()
            messages.success(request, "Contact was updated successfully.")
            return redirect("contact_list")
    else:
        form = ContactForm(instance=contact)
    return render(request, "main/contact_form.html", {"form": form, "page_title": "Edit contact", "contact": contact})

def contact_delete(request, pk):
    contact = get_object_or_404(Contact, pk=pk)
    if request.method == "POST":
        contact.delete()
        messages.success(request, "Contact was deleted.")
        return redirect("contact_list")
    return render(request, "main/contact_confirm_delete.html", {"contact": contact})
