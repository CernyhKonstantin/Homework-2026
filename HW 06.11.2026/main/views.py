from datetime import date, timedelta
from django.shortcuts import render

DEMO_USERS = {
    "admin": {"password": "admin123", "access": "Administrator"},
    "user": {"password": "user123", "access": "Regular User"},
}

def home(request):
    return render(request, "main/home.html")

def login_view(request):
    result = error = None
    if request.method == "POST":
        username = request.POST.get("username", "").strip()
        password = request.POST.get("password", "")
        account = DEMO_USERS.get(username)
        if account and account["password"] == password:
            result = {"username": username, "access": account["access"]}
        else:
            error = "Incorrect username or password. Please try again."
    return render(request, "main/login.html", {"result": result, "error": error})

def calculator(request):
    result = error = None
    if request.method == "POST":
        try:
            numbers = [float(request.POST.get(f"number{i}", "")) for i in range(1, 4)]
            operation = request.POST.get("operation")
            if operation == "minimum":
                result = min(numbers)
            elif operation == "maximum":
                result = max(numbers)
            elif operation == "average":
                result = sum(numbers) / 3
            else:
                error = "Please select a valid calculation."
        except ValueError:
            error = "Please enter three valid numbers."
    return render(request, "main/calculator.html", {"result": result, "error": error})

def register(request):
    submitted = error = None
    if request.method == "POST":
        data = {key: request.POST.get(key, "").strip() for key in
                ["first_name", "last_name", "age", "email", "gender", "address"]}
        try:
            age = int(data["age"])
            if not all(data.values()) or not 1 <= age <= 120:
                raise ValueError
            if data["gender"] not in ["Female", "Male", "Other", "Prefer not to say"]:
                raise ValueError
            submitted = data
            submitted["age"] = age
            submitted["newsletter"] = request.POST.get("newsletter") == "on"
        except ValueError:
            error = "Please complete all fields with valid information."
    return render(request, "main/register.html", {"submitted": submitted, "error": error})

def programmers_day(request):
    result = error = None
    if request.method == "POST":
        try:
            year = int(request.POST.get("year", ""))
            if not 1 <= year <= 9998:
                raise ValueError
            day = date(year, 1, 1) + timedelta(days=255)
            result = {"year": year, "formatted": day.strftime("%d %B %Y"), "weekday": day.strftime("%A")}
        except ValueError:
            error = "Please enter a valid year between 1 and 9998."
    return render(request, "main/programmers_day.html", {"result": result, "error": error})
