from django.contrib import admin
from django.urls import path
from account import views
urlpatterns=[path("admin/",admin.site.urls),path("",views.home,name="home"),path("rules/",views.rules,name="rules"),path("statistics/",views.statistics,name="statistics"),path("settings/",views.settings_page,name="settings"),path("clear-name/",views.clear_name,name="clear_name")]
