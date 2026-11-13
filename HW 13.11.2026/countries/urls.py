from django.urls import path
from . import views
urlpatterns = [
 path("", views.home, name="home"),
 path("countries/", views.country_list, name="country_list"),
 path("countries/<str:code>/", views.country_detail, name="country_detail"),
 path("regions/", views.region_list, name="region_list"),
 path("regions/<str:region_name>/", views.region_detail, name="region_detail"),
 path("capitals/<path:capital_name>/", views.capital_detail, name="capital_detail"),
 path("languages/", views.language_list, name="language_list"),
 path("languages/<str:language_name>/", views.language_detail, name="language_detail"),
]
