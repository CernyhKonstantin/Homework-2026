from django.shortcuts import render

def show(request, page, title, intro, cards):
    return render(request, "city/page.html", {
        "page": page, "title": title, "intro": intro, "cards": cards
    })

def home(request):
    return show(request, "home", "Welcome to Burghausen",
        "Discover the city, its local news, history, landmarks and useful service contacts.",
        [("City news", "Read announcements and updates from the city.", "news"),
         ("City management", "Learn about local administration and leadership.", "management"),
         ("City history", "Explore the town's past and historical places.", "history")])

def news(request):
    return show(request, "news", "City News",
        "A sample news overview for the Burghausen city information project.",
        [("Town centre", "Check local notices and public information before visiting city-centre events.", None),
         ("Culture and events", "The city and local organisations offer cultural events throughout the year.", None),
         ("Public services", "Residents can use municipal services for current announcements.", None)])

def management(request):
    return show(request, "management", "City Management",
        "An introduction to municipal administration and local government.",
        [("Mayor", "The mayor represents the city and leads the municipal administration.", None),
         ("City council", "The elected city council makes important municipal decisions.", None),
         ("Administration", "The city administration supports residents with municipal services.", None)])

def facts(request):
    return show(request, "facts", "Facts About the City",
        "Burghausen is a town in Upper Bavaria near the Austrian border.",
        [("Location", "Burghausen is in the district of Altötting in Upper Bavaria.", None),
         ("Landmark", "Burghausen Castle is famous for its long castle complex above the old town.", None),
         ("River", "The Salzach forms part of the border between Germany and Austria near Burghausen.", None)])

def phones(request):
    return show(request, "phones", "City Service Phone Numbers",
        "Important general contact numbers. Confirm municipal office contacts on the official city website.",
        [("Police emergency", "110 — Police emergency number in Germany.", None),
         ("Fire brigade and ambulance", "112 — Fire and medical emergency number in Germany.", None),
         ("Medical on-call service", "116 117 — Non-emergency medical assistance outside regular practice hours.", None),
         ("Municipal offices", "Check the official Burghausen city website for current office numbers and opening hours.", None)])

def history(request):
    return show(request, "history", "History of Burghausen",
        "Burghausen developed along the Salzach and is especially known for its historic old town and castle complex. The castle reflects the town's importance in the region over many centuries.",
        [("Medieval period", "The castle and settlement grew in importance during the Middle Ages.", None),
         ("Trade and the Salzach", "The river supported trade and shaped the town's development.", None)])

def history_people(request):
    return show(request, "people", "Famous People Connected with Burghausen",
        "Use this section to present verified notable people connected with the city.",
        [("Local cultural figures", "Add a documented person from Burghausen's cultural history here.", None),
         ("Historical personalities", "Add a historically documented resident or person connected with the town here.", None)])

def history_photos(request):
    return show(request, "photos", "Historical Photo Gallery",
        "The local artwork below is illustrative placeholder imagery. Replace it with sourced historical photographs for a factual gallery.",
        [("Castle and old town", "Illustrative archival-style artwork — replace with a historical photograph.", None),
         ("The Salzach riverfront", "Illustrative archival-style artwork — replace with a historical photograph.", None)])
