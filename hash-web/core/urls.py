from django.urls import path, include
from rest_framework.routers import DefaultRouter
from . import views

# Router para API REST
router = DefaultRouter()
router.register(r'jerarquias', views.JerarquiaViewSet)
router.register(r'oficiales', views.OficialViewSet)
router.register(r'tipos-procedimiento', views.TipoProcedimientoViewSet)
router.register(r'formularios', views.FormularioHashViewSet)
router.register(r'archivos', views.ArchivoViewSet)

app_name = 'core'

urlpatterns = [
    # API endpoints
    path('api/', include(router.urls)),
    path('api/estadisticas/', views.estadisticas_dashboard, name='api_estadisticas'),
    
    # Template views - Hash
    path('', views.dashboard, name='dashboard'),
    path('formularios/', views.lista_formularios, name='lista_formularios'),
    path('formularios/nuevo/', views.crear_formulario, name='crear_formulario'),
    path('formularios/<int:formulario_id>/', views.ver_formulario, name='ver_formulario'),
    
    # Template views - Custodia
    path('custodias/', views.lista_custodias, name='lista_custodias'),
    path('custodias/nueva/', views.crear_custodia, name='crear_custodia'),
    path('custodias/<int:custodia_id>/', views.ver_custodia, name='ver_custodia'),
    
    # APIs específicas para custodia
    path('api/custodias/crear/', views.api_crear_custodia, name='api_crear_custodia'),
    path('api/custodias/<int:custodia_id>/personal/', views.api_agregar_personal_custodia, name='api_agregar_personal_custodia'),
]
