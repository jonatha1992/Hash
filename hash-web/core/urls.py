from django.urls import path
from . import views

app_name = 'core'

urlpatterns = [
    # Dashboard
    path('', views.dashboard, name='dashboard'),
    
    # Hash
    path('hash/', views.hash, name='hash'),
    path('hash/<int:formulario_id>/', views.hash, name='hash_editar'),

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
    
    # Quick Add APIs
    path('api/crear-oficial-rapido/', views.api_crear_oficial_rapido, name='api_crear_oficial_rapido'),
    path('api/crear-jerarquia-rapida/', views.api_crear_jerarquia_rapida, name='api_crear_jerarquia_rapida'),
    path('api/crear-destino-rapido/', views.api_crear_destino_rapido, name='api_crear_destino_rapido'),
    path('api/obtener-opciones-formulario/', views.api_obtener_opciones_formulario, name='api_obtener_opciones_formulario'),
]
