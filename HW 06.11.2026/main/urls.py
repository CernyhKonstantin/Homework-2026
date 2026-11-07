from django.urls import path
from . import views
urlpatterns = [
 path("", views.home, name="home"),
 path("login/", views.login_view, name="login"),
 path("calculator/", views.calculator, name="calculator"),
 path("register/", views.register, name="register"),
 path("programmers-day/", views.programmers_day, name="programmers_day"),
]
