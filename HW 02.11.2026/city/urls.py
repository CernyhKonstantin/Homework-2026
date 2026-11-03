from django.urls import path
from . import views

urlpatterns = [
    path("", views.home, name="home"),
    path("news", views.news, name="news"),
    path("news/", views.news),
    path("news/<path:extra>", views.news),
    path("management", views.management, name="management"),
    path("management/", views.management),
    path("management/<path:extra>", views.management),
    path("facts", views.facts, name="facts"),
    path("facts/", views.facts),
    path("facts/<path:extra>", views.facts),
    path("phones", views.phones, name="phones"),
    path("phones/", views.phones),
    path("phones/<path:extra>", views.phones),
    path("history", views.history, name="history"),
    path("history/", views.history),
    path("history/people", views.history_people, name="history_people"),
    path("history/people/", views.history_people),
    path("history/photos", views.history_photos, name="history_photos"),
    path("history/photos/", views.history_photos),
    path("history/<path:extra>", views.history),
]
