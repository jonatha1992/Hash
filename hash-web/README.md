# Hash Web Application

Aplicación web para gestión de hash de archivos forenses.

## Setup Rápido

### Windows:
```bash
setup.bat
```

### Linux/Mac:
```bash
chmod +x setup.sh
./setup.sh
```

### Manual:
```bash
# 1. Crear entorno virtual
python -m venv venv

# 2. Activar entorno virtual
# Windows:
venv\Scripts\activate
# Linux/Mac:
source venv/bin/activate

# 3. Instalar dependencias
pip install -r requirements.txt

# 4. Ejecutar migraciones
python manage.py migrate

# 5. Crear superusuario
python manage.py createsuperuser

# 6. Ejecutar servidor
python manage.py runserver
```

## Acceso

- **Aplicación**: http://127.0.0.1:8000/
- **Admin Panel**: http://127.0.0.1:8000/admin/
- **API Docs**: http://127.0.0.1:8000/api/docs/

## Credenciales por defecto
- Usuario: `admin`
- Contraseña: `admin123`

## Características

- ✅ Cálculo de hash SHA-256
- ✅ Gestión de oficiales y jerarquías  
- ✅ Upload múltiple de archivos
- ✅ Generación de reportes PDF
- ✅ API REST completa
- ✅ Panel de administración
- ✅ Deploy rápido

## Deployment

### Desarrollo
```bash
python manage.py runserver 0.0.0.0:8000
```

### Producción
```bash
pip install gunicorn
gunicorn hash_project.wsgi:application --bind 0.0.0.0:8000
```

### Docker
```bash
docker build -t hash-web .
docker run -p 8000:8000 hash-web
```
