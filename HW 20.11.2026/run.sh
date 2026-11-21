#!/usr/bin/env bash
set -e
python3 -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
python manage.py migrate
echo "Create an admin: python manage.py createsuperuser"
echo "Start server: python manage.py runserver"
