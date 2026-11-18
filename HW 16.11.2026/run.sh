#!/usr/bin/env bash
set -e
python3 -m venv .venv
source .venv/bin/activate
python -m pip install --upgrade pip
pip install -r requirements.txt
python manage.py migrate
echo "Create admin: python manage.py createsuperuser"
echo "Run server:   python manage.py runserver"
