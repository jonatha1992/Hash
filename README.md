# Hash Web - Aplicación Web para Gestión de Hash

## 📋 Descripción

Hash Web es una aplicación Django que simula la funcionalidad de la aplicación de escritorio Hash Copy. Permite procesar archivos, calcular hashes SHA-256, categorizar archivos por tipo y gestionar formularios de hash y custodia.

## 🚀 Instalación Rápida

### Opción 1: Inicio Automático (Windows)
```bash
start_quick.bat
```

### Opción 2: Configuración Manual
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

# 5. Cargar datos de demostración
python setup_demo.py

# 6. Iniciar servidor
python manage.py runserver
```

## 📱 Uso de la Aplicación

### Acceso
- **URL**: http://localhost:8000
- **Credenciales**: admin/admin123

### Funcionalidades Principales

#### 1. Dashboard
- Vista general del sistema
- Estadísticas de formularios
- Acceso rápido a funciones

#### 2. Modo Avanzado (Simula App de Escritorio)
- Procesamiento de múltiples archivos
- Cálculo automático de hashes SHA-256
- Categorización por tipo de archivo
- Interfaz similar a la aplicación de escritorio

#### 3. Gestión de Formularios
- Crear formularios de hash
- Ver detalles con estadísticas
- Generar reportes PDF

#### 4. Gestión de Custodia
- Crear actas de custodia
- Asignar oficiales
- Seguimiento de elementos

## 🛠️ Comandos Útiles

```bash
# Verificación del sistema
python verify_setup.py

# Limpieza del proyecto
python cleanup.py

# Inicio rápido del servidor
start_server.bat

# Cargar datos de demostración
python setup_demo.py

# Crear superusuario
python manage.py createsuperuser
```

## 📁 Estructura del Proyecto

```
hash-web/
├── core/                 # App principal
│   ├── models.py        # Modelos de datos
│   ├── views.py         # Vistas y lógica
│   ├── urls.py          # URLs de la app
│   └── templates/       # Plantillas HTML
├── reports/             # Generación de PDFs
├── hash_project/        # Configuración Django
├── static/              # Archivos estáticos
├── media/               # Archivos subidos
└── templates/           # Plantillas base
```

## 🔧 Configuración de Desarrollo

### Archivos de Configuración
- `.vscode/settings.json` - Configuración VS Code
- `.eslintrc.json` - Configuración ESLint
- `pyproject.toml` - Configuración Python
- `hash-web.code-workspace` - Workspace VS Code

### Nota sobre Errores del Linter
Los errores que ves en los archivos `.html` son **falsos positivos** del linter de JavaScript. Esto es normal en proyectos Django y no afecta el funcionamiento.

## 📊 Características

### Similitudes con la App de Escritorio
- ✅ Procesamiento de múltiples archivos
- ✅ Cálculo de hashes SHA-256
- ✅ Categorización por tipo de archivo
- ✅ Gestión de oficiales y jerarquías
- ✅ Formularios de hash y custodia
- ✅ Generación de reportes PDF
- ✅ Interfaz similar al modo avanzado

### Funcionalidades Web Adicionales
- 🌐 Acceso desde cualquier navegador
- 📱 Interfaz responsive
- 🔐 Sistema de autenticación
- 📊 Dashboard con estadísticas
- 📄 API REST para integración
- 🖨️ Generación automática de PDFs

## 🐛 Solución de Problemas

### Errores del Linter
Los errores en templates HTML son normales. Ver la documentación de configuración para más detalles.

### Verificación del Sistema
```bash
python verify_setup.py
```

### Reiniciar Servidor
```bash
python manage.py runserver
```

## 📝 Licencia

Este proyecto es parte del sistema Hash Copy para gestión de archivos digitales.

---

**Hash Web** - Versión web de la aplicación de escritorio Hash Copy 