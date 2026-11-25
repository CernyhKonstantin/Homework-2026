from django.shortcuts import render,redirect
from .middleware import AGE_30_DAYS
TEXT={
"uk":{"title":"Особистий кабінет","first":"Введіть своє ім'я, щоб персоналізувати кабінет.","save":"Зберегти ім'я","stats":"Статистика","settings":"Налаштування","rules":"Правила","accept":"Приймаю правила","theme":"Тема","language":"Мова","light":"Світла","dark":"Темна","ukrainian":"Українська","english":"Англійська","save_settings":"Зберегти налаштування","clear":"Видалити ім'я","visits":"Кількість відвідувань","last":"Останнє відвідування","name":"Ім'я","rules_text":"Будь ласка, прийміть правила сайту, щоб продовжити."},
"en":{"title":"Personal Account","first":"Enter your name to personalize your account.","save":"Save name","stats":"Statistics","settings":"Settings","rules":"Rules","accept":"I accept the rules","theme":"Theme","language":"Language","light":"Light","dark":"Dark","ukrainian":"Ukrainian","english":"English","save_settings":"Save settings","clear":"Clear name","visits":"Total visits","last":"Last visit","name":"Name","rules_text":"Please accept the site rules to continue."}}
def lang(request):
    value=request.COOKIES.get("language","uk")
    return value if value in ("uk","en") else "uk"
def home(request):
    language=lang(request); labels=TEXT[language]
    if request.method=="POST":
        name=request.POST.get("name","").strip()[:60]
        response=redirect("home")
        if name: response.set_cookie("user_name",name,max_age=7*24*60*60,httponly=True,samesite="Lax")
        return response
    return render(request,"account/home.html",{"t":labels,"lang":language,"name":request.COOKIES.get("user_name",""),"theme":request.COOKIES.get("theme","dark")})
def rules(request):
    language=lang(request)
    if request.method=="POST":
        response=redirect("home"); response.set_cookie("accepted_rules","yes",max_age=AGE_30_DAYS,httponly=True,samesite="Lax"); return response
    return render(request,"account/rules.html",{"t":TEXT[language],"lang":language,"theme":request.COOKIES.get("theme","dark")})
def statistics(request):
    language=lang(request)
    return render(request,"account/statistics.html",{"t":TEXT[language],"lang":language,"theme":request.COOKIES.get("theme","dark"),"name":request.COOKIES.get("user_name","—"),"visits":request.COOKIES.get("visit_count","0"),"language_value":request.COOKIES.get("language","uk"),"last_visit":request.COOKIES.get("last_visit","—")})
def settings_page(request):
    language=lang(request)
    if request.method=="POST":
        theme=request.POST.get("theme","dark"); language_value=request.POST.get("language","uk")
        if theme not in ("light","dark"): theme="dark"
        if language_value not in ("uk","en"): language_value="uk"
        response=redirect("settings"); response.set_cookie("theme",theme,max_age=AGE_30_DAYS,samesite="Lax"); response.set_cookie("language",language_value,max_age=AGE_30_DAYS,samesite="Lax"); return response
    return render(request,"account/settings.html",{"t":TEXT[language],"lang":language,"theme":request.COOKIES.get("theme","dark"),"language_value":request.COOKIES.get("language","uk")})
def clear_name(request):
    response=redirect("home"); response.delete_cookie("user_name"); return response
