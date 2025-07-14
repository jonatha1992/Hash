from django.urls import path
from . import views

app_name = 'core'

urlpatterns = [
    # Dashboard
    path('', views.dashboard, name='dashboard'),
    
    # Hash
    path('form_hash/', views.form_hash, name='form_hash'),
    path('form_hash/<int:formulario_id>/', views.form_hash, name='form_hash_editar'),

    # Listas
    path('hashes/', views.lista_hashes, name='lista_hashes'),
    path('hashes/<int:formulario_id>/eliminar/', views.eliminar_hash, name='eliminar_hash'),
    path('custodias/', views.lista_custodias, name='lista_custodias'),
    
    # Custodia
    path('custodias/nueva/', views.crear_custodia, name='crear_custodia'),
    path('custodias/<int:custodia_id>/', views.ver_custodia, name='ver_custodia'),
    
    # APIs
    path('api/crear-custodia/', views.api_crear_custodia, name='api_crear_custodia'),
    path('api/custodias/<int:custodia_id>/agregar-personal/', views.api_agregar_personal_custodia, name='api_agregar_personal_custodia'),
    path('api/procesar-carpeta/', views.procesar_carpeta, name='api_procesar_carpeta'),
    path('api/hashes/<int:formulario_id>/detalles/', views.api_obtener_detalles_formulario, name='api_obtener_detalles'),
    path('api/hashes/<int:formulario_id>/agregar-archivos/', views.agregar_archivos_formulario, name='api_agregar_archivos'),
    path('api/hashes/<int:formulario_id>/eliminar/', views.api_eliminar_formulario, name='api_eliminar_formulario'),
    
    # Oficial APIs
    path('api/oficiales/crear/', views.api_crear_oficial, name='api_crear_oficial'),
    path('api/oficiales/buscar/', views.api_buscar_oficiales, name='api_buscar_oficiales'),
    path('api/jerarquias-destinos/', views.api_obtener_jerarquias_destinos, name='api_jerarquias_destinos'),
]
