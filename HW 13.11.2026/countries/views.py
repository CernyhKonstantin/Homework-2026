from django.shortcuts import render
from .services import RestCountriesError, get_all_countries, get_country_by_code, display_data, get_countries_by_region, get_languages, get_countries_by_language, get_capital_information

def error_page(request, error):
    return render(request, "countries/error.html", {"error_message":str(error)}, status=503)
def home(request):
    try: total=len(get_all_countries())
    except RestCountriesError: total=None
    return render(request, "countries/home.html", {"total_countries":total})
def country_list(request):
    try:
        items=[{"display":display_data(item)} for item in get_all_countries()]
        return render(request,"countries/country_list.html",{"country_items":items})
    except RestCountriesError as error: return error_page(request,error)
def country_detail(request,code):
    try:
        country=get_country_by_code(code)
        return render(request,"countries/country_detail.html",{"details":display_data(country)})
    except RestCountriesError as error: return error_page(request,error)
def region_list(request):
    try:
        regions=sorted({item.get("region") for item in get_all_countries() if item.get("region")})
        return render(request,"countries/region_list.html",{"regions":regions})
    except RestCountriesError as error: return error_page(request,error)
def region_detail(request,region_name):
    try:
        region=region_name.replace("-"," ")
        items=[{"display":display_data(item)} for item in get_countries_by_region(region)]
        return render(request,"countries/region_detail.html",{"region_name":region.title(),"country_items":items})
    except RestCountriesError as error: return error_page(request,error)
def capital_detail(request,capital_name):
    try:
        capital=capital_name.replace("-"," ")
        items=[{"display":display_data(item)} for item in get_capital_information(capital)]
        return render(request,"countries/capital_detail.html",{"capital_name":capital.title(),"capital_countries":items})
    except RestCountriesError as error: return error_page(request,error)
def language_list(request):
    try: return render(request,"countries/language_list.html",{"languages":get_languages()})
    except RestCountriesError as error: return error_page(request,error)
def language_detail(request,language_name):
    try:
        language=language_name.replace("-"," ")
        items=[{"display":display_data(item)} for item in get_countries_by_language(language)]
        return render(request,"countries/language_detail.html",{"language_name":language.title(),"country_items":items})
    except RestCountriesError as error: return error_page(request,error)
