$ErrorActionPreference = "Stop"
if (!(Test-Path ".venv")) { py -m venv .venv }
& ".\.venv\Scripts\Activate.ps1"
python -m pip install -r requirements.txt
python manage.py migrate
Write-Host "Create an admin account with: python manage.py createsuperuser"
Write-Host "Then start the server with: python manage.py runserver"
