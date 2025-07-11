#!/bin/bash
# Script para setup rápido

echo "=== Configurando entorno virtual ==="
python -m venv venv

# Activar entorno virtual
if [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "win32" ]]; then
    source venv/Scripts/activate
else
    source venv/bin/activate
fi

echo "=== Instalando dependencias ==="
pip install -r requirements.txt

echo "=== Creando proyecto Django ==="
django-admin startproject hash_project .

echo "=== Creando apps ==="
cd hash_project
python manage.py startapp core
python manage.py startapp reports

echo "=== Configurando base de datos ==="
python manage.py makemigrations
python manage.py migrate

echo "=== Creando superusuario ==="
echo "from django.contrib.auth import get_user_model; User = get_user_model(); User.objects.create_superuser('admin', 'admin@test.com', 'admin123')" | python manage.py shell

echo "=== Listo! Ejecutar: python manage.py runserver ==="
