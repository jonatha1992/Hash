from django.urls import path
from . import views

app_name = 'reports'

urlpatterns = [
    path('hash/<int:formulario_id>/', views.generar_reporte_hash, name='reporte_hash'),
    path('custodia/<int:formulario_id>/', views.generar_reporte_custodia, name='reporte_custodia'),
]
