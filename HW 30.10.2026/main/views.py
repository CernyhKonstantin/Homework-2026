from django.shortcuts import render

def index(request):
    context = {
        "profile": {
            "first_name": "Alex", "last_name": "Morgan",
            "role": "Junior Python Developer", "city": "Berlin, Germany",
            "email": "alex.morgan@example.com", "phone": "+49 151 12345678",
            "photo_url": "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=600&q=85",
            "summary": "Motivated and detail-oriented technology enthusiast interested in Python development, web applications, and continuous learning. I enjoy turning ideas into practical, user-friendly digital solutions.",
            "qualities": ["Reliable", "Analytical thinking", "Teamwork", "Adaptability", "Problem-solving"],
            "goal": "To join a collaborative development team, strengthen my backend skills, and contribute to useful, high-quality web applications."
        },
        "education": [
            {"institution": "Example Technical University", "specialization": "Computer Science", "years": "2022 – 2025", "description": "Coursework in programming fundamentals, databases, and web technologies."},
            {"institution": "Online Learning Academy", "specialization": "Python and Web Development", "years": "2025 – Present", "description": "Practical training in Python, object-oriented programming, Django, and Git."}
        ],
        "programming_skills": ["Python", "HTML5", "CSS3", "SQL basics"],
        "software_skills": ["Visual Studio Code", "PyCharm", "Git", "GitHub", "Docker basics"],
        "languages": [{"name": "English", "level": "B2"}, {"name": "German", "level": "B1"}],
        "projects": [
            {"title": "Personal CV Website", "description": "A responsive personal resume website with profile, education, skills, and project sections.", "technologies": ["Python", "Django", "Bootstrap 5", "HTML", "CSS"], "result": "Created reusable Django templates and a mobile-friendly CV layout."},
            {"title": "Online Store Product Catalog", "description": "A small product catalog with product listings and individual detail pages.", "technologies": ["Python", "Flask", "HTML", "CSS"], "result": "Implemented product listing, detail pages, and in-memory product management."},
            {"title": "Docker Networking Lab", "description": "A container networking exercise using a network troubleshooting image.", "technologies": ["Docker", "PowerShell", "Netshoot"], "result": "Practiced container inspection, network modes, interfaces, and routing commands."}
        ]
    }
    return render(request, "main/index.html", context)
