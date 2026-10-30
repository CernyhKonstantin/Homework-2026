# HW 30.10.2026 — Personal CV Website

## Erklärung auf Deutsch

Dieses Projekt erstellt eine responsive persönliche CV-Webseite mit Django und Bootstrap 5. Der gesamte Programmcode, die Variablennamen, Kommentare und sichtbaren Seitentexte sind auf Englisch.

### Erfüllte Anforderungen
- Django-Projekt `config` und Django-App `main`
- Routing über `config/urls.py` und `main/urls.py`
- Basis-Template `layout.html` und Startseite `index.html`
- Template-Vererbung mit `{% extends %}` sowie `{% block title %}` und `{% block content %}`
- Bootstrap 5 CDN mit Navbar, Card, Container, Row und Column
- Responsive Darstellung für Desktop, Tablet und Smartphone
- Persönliche Informationen, Foto-URL, Wohnort und Kontakt
- Über mich, persönliche Eigenschaften und berufliches Ziel
- Ausbildung, Programmierkenntnisse, Softwarekenntnisse und Fremdsprachen
- Beispielprojekte mit Technologien und Ergebnissen

### Eigene Daten eintragen
Die Beispieldaten befinden sich in `main/views.py` in der Funktion `index()`. Ersetze dort Name, Foto-URL, Stadt, E-Mail, Telefonnummer, Ausbildung, Kenntnisse und Projekte durch deine tatsächlichen Angaben. Die Beispielperson und die Kontaktdaten sind Platzhalter.

### Installation und Start unter Windows
1. ZIP-Datei entpacken.
2. PowerShell im Ordner `HW 30.10.2026` öffnen.
3. Virtuelle Umgebung erstellen und aktivieren:

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
```

Falls PowerShell die Aktivierung blockiert:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
.\.venv\Scripts\Activate.ps1
```

4. Django installieren und Projekt prüfen:

```powershell
python -m pip install -r requirements.txt
python manage.py check
```

5. Server starten:

```powershell
python manage.py runserver
```

6. Browser öffnen: http://127.0.0.1:8000/

### Example Run

```text
System check identified no issues (0 silenced).
Starting development server at http://127.0.0.1:8000/
Quit the server with CTRL-BREAK.
```

Im Browser wird die CV-Webseite mit Navigation, Profilfoto, About, Education, Skills und Project Cards angezeigt.

Hinweis: Bootstrap und das Foto werden von externen URLs geladen; dafür ist beim Seitenaufruf eine Internetverbindung erforderlich. Der Django-Entwicklungsserver ist für lokale Entwicklung gedacht, nicht für den produktiven öffentlichen Betrieb.
