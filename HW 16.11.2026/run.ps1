$ErrorActionPreference = "Stop"
Write-Host "Preparing HW 16.11.2026..." -ForegroundColor Cyan
if (-not (Test-Path ".venv")) { py -m venv .venv }
$python = ".\.venv\Scripts\python.exe"
& $python -m pip install --upgrade pip
if ($LASTEXITCODE -ne 0) { throw "pip upgrade failed." }
& $python -m pip install -r requirements.txt
if ($LASTEXITCODE -ne 0) { throw "Dependency installation failed." }
& $python manage.py migrate
if ($LASTEXITCODE -ne 0) { throw "Database migration failed." }
Write-Host "`nSetup complete." -ForegroundColor Green
Write-Host "Create admin: $python manage.py createsuperuser"
Write-Host "Run server:   $python manage.py runserver"
