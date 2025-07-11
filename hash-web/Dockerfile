FROM python:3.11-slim

WORKDIR /app

# Instalar dependencias del sistema
RUN apt-get update && apt-get install -y \
    gcc \
    && rm -rf /var/lib/apt/lists/*

# Copiar requirements
COPY requirements.txt .

# Instalar dependencias Python
RUN pip install --no-cache-dir -r requirements.txt

# Copiar código
COPY . .

# Variables de entorno
ENV DJANGO_SETTINGS_MODULE=hash_project.settings
ENV PYTHONPATH=/app

# Exponer puerto
EXPOSE 8000

# Crear directorios necesarios
RUN mkdir -p staticfiles media

# Comando por defecto
CMD ["sh", "-c", "python manage.py migrate && python manage.py collectstatic --noinput && gunicorn hash_project.wsgi:application --bind 0.0.0.0:8000"]
