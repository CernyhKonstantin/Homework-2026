$ErrorActionPreference = "Stop"

Write-Host "Setting up the Phone Book project..." -ForegroundColor Cyan

if (-not (Test-Path ".venv")) {
    py -m venv .venv
    if ($LASTEXITCODE -ne 0) { throw "Virtual environment creation failed." }
}

$python = Join-Path $PWD ".venv\Scripts\python.exe"
& $python -m pip install -r requirements.txt
if ($LASTEXITCODE -ne 0) { throw "Dependency installation failed." }

& $python manage.py migrate
if ($LASTEXITCODE -ne 0) { throw "Database migration failed." }

Write-Host "Starting the development server at http://127.0.0.1:8000/" -ForegroundColor Green
& $python manage.py runserver
