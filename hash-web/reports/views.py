from django.shortcuts import get_object_or_404
from django.http import HttpResponse
from django.contrib.auth.decorators import login_required
from reportlab.lib.pagesizes import letter, A4
from reportlab.lib import colors
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Table, TableStyle
from reportlab.platypus.flowables import PageBreak
from reportlab.lib.enums import TA_CENTER, TA_LEFT, TA_RIGHT
from io import BytesIO
from datetime import datetime
from core.models import FormularioHash


def generar_reporte_hash(request, formulario_id):
    """Genera el reporte PDF del acta de hash"""
    formulario = get_object_or_404(
        FormularioHash.objects.select_related(
            'oficial_entrega__jerarquia', 'oficial_recibe__jerarquia'
        ).prefetch_related('archivos'),
        id=formulario_id
    )
    
    # Crear el PDF
    buffer = BytesIO()
    doc = SimpleDocTemplate(buffer, pagesize=A4, topMargin=0.5*inch)
    story = []
    
    # Estilos
    styles = getSampleStyleSheet()
    title_style = ParagraphStyle(
        'CustomTitle',
        parent=styles['Heading1'],
        fontSize=16,
        spaceAfter=30,
        alignment=TA_CENTER,
        textColor=colors.black
    )
    
    header_style = ParagraphStyle(
        'CustomHeader',
        parent=styles['Heading2'],
        fontSize=12,
        spaceAfter=12,
        textColor=colors.black
    )
    
    normal_style = ParagraphStyle(
        'CustomNormal',
        parent=styles['Normal'],
        fontSize=10,
        spaceAfter=6
    )
    
    # Título del documento
    story.append(Paragraph("ACTA DE HASH DE ARCHIVOS DIGITALES", title_style))
    story.append(Spacer(1, 20))
    
    # Información del formulario
    info_data = [
        ["Número de Hash:", f"#{formulario.nro_hash}"],
        ["Tipo:", formulario.get_tipo_display()],
        ["Procedimiento:", formulario.procedimiento],
        ["Fecha y Hora:", formulario.fecha_creacion.strftime("%d/%m/%Y %H:%M:%S")],
        ["Estado:", formulario.get_estado_display()],
    ]
    
    info_table = Table(info_data, colWidths=[2*inch, 4*inch])
    info_table.setStyle(TableStyle([
        ('BACKGROUND', (0, 0), (0, -1), colors.lightgrey),
        ('TEXTCOLOR', (0, 0), (-1, -1), colors.black),
        ('ALIGN', (0, 0), (-1, -1), 'LEFT'),
        ('FONTNAME', (0, 0), (-1, -1), 'Helvetica'),
        ('FONTSIZE', (0, 0), (-1, -1), 10),
        ('GRID', (0, 0), (-1, -1), 1, colors.black),
        ('VALIGN', (0, 0), (-1, -1), 'TOP'),
    ]))
    
    story.append(info_table)
    story.append(Spacer(1, 20))
    
    # Información de oficiales
    story.append(Paragraph("OFICIALES INTERVINIENTES", header_style))
    
    oficiales_data = [
        ["Oficial que Entrega:", str(formulario.oficial_entrega)],
        ["Oficial que Recibe:", str(formulario.oficial_recibe)],
    ]
    
    oficiales_table = Table(oficiales_data, colWidths=[2*inch, 4*inch])
    oficiales_table.setStyle(TableStyle([
        ('BACKGROUND', (0, 0), (0, -1), colors.lightgrey),
        ('TEXTCOLOR', (0, 0), (-1, -1), colors.black),
        ('ALIGN', (0, 0), (-1, -1), 'LEFT'),
        ('FONTNAME', (0, 0), (-1, -1), 'Helvetica'),
        ('FONTSIZE', (0, 0), (-1, -1), 10),
        ('GRID', (0, 0), (-1, -1), 1, colors.black),
        ('VALIGN', (0, 0), (-1, -1), 'TOP'),
    ]))
    
    story.append(oficiales_table)
    story.append(Spacer(1, 20))
    
    # Resumen de archivos
    story.append(Paragraph("RESUMEN DE ARCHIVOS", header_style))
    
    resumen_data = [
        ["Tipo de Archivo", "Cantidad"],
        ["Imágenes", str(formulario.imagenes)],
        ["Videos", str(formulario.clips)],
        ["Audio", str(formulario.audio)],
        ["Documentos", str(formulario.texto)],
        ["Varios", str(formulario.varios)],
        ["TOTAL", str(formulario.total_archivos)],
        ["Peso Total", formulario.peso_total_formateado],
    ]
    
    resumen_table = Table(resumen_data, colWidths=[3*inch, 1.5*inch])
    resumen_table.setStyle(TableStyle([
        ('BACKGROUND', (0, 0), (-1, 0), colors.grey),
        ('TEXTCOLOR', (0, 0), (-1, 0), colors.whitesmoke),
        ('BACKGROUND', (0, -2), (-1, -1), colors.lightgrey),
        ('TEXTCOLOR', (0, 0), (-1, -1), colors.black),
        ('ALIGN', (0, 0), (-1, -1), 'CENTER'),
        ('FONTNAME', (0, 0), (-1, -1), 'Helvetica'),
        ('FONTSIZE', (0, 0), (-1, -1), 10),
        ('GRID', (0, 0), (-1, -1), 1, colors.black),
        ('FONTNAME', (0, -2), (-1, -1), 'Helvetica-Bold'),
    ]))
    
    story.append(resumen_table)
    story.append(Spacer(1, 20))
    
    # Lista detallada de archivos
    if formulario.archivos.exists():
        story.append(Paragraph("DETALLE DE ARCHIVOS Y HASH SHA-256", header_style))
        
        archivos_data = [["#", "Nombre del Archivo", "Extensión", "Tamaño", "Hash SHA-256"]]
        
        for archivo in formulario.archivos.all():
            archivos_data.append([
                str(archivo.nro_orden),
                archivo.nombre[:40] + "..." if len(archivo.nombre) > 40 else archivo.nombre,
                archivo.extension,
                archivo.peso_formateado,
                archivo.hash_sha256[:32] + "..."  # Truncar hash para que quepa
            ])
        
        archivos_table = Table(archivos_data, colWidths=[0.3*inch, 2.5*inch, 0.6*inch, 0.8*inch, 2.3*inch])
        archivos_table.setStyle(TableStyle([
            ('BACKGROUND', (0, 0), (-1, 0), colors.grey),
            ('TEXTCOLOR', (0, 0), (-1, 0), colors.whitesmoke),
            ('TEXTCOLOR', (1, 1), (-1, -1), colors.black),
            ('ALIGN', (0, 0), (-1, -1), 'LEFT'),
            ('ALIGN', (0, 0), (0, -1), 'CENTER'),  # Centrar números de orden
            ('FONTNAME', (0, 0), (-1, 0), 'Helvetica-Bold'),
            ('FONTNAME', (1, 1), (-1, -1), 'Helvetica'),
            ('FONTSIZE', (0, 0), (-1, -1), 8),
            ('GRID', (0, 0), (-1, -1), 0.5, colors.black),
            ('VALIGN', (0, 0), (-1, -1), 'TOP'),
        ]))
        
        story.append(archivos_table)
        story.append(Spacer(1, 20))
    
    # Firmas
    story.append(Spacer(1, 40))
    story.append(Paragraph("FIRMAS", header_style))
    
    firma_data = [
        ["", ""],
        ["_" * 30, "_" * 30],
        [str(formulario.oficial_entrega), str(formulario.oficial_recibe)],
        ["ENTREGA", "RECIBE"],
    ]
    
    firma_table = Table(firma_data, colWidths=[3*inch, 3*inch])
    firma_table.setStyle(TableStyle([
        ('ALIGN', (0, 0), (-1, -1), 'CENTER'),
        ('FONTNAME', (0, 2), (-1, -1), 'Helvetica-Bold'),
        ('FONTSIZE', (0, 0), (-1, -1), 10),
        ('VALIGN', (0, 0), (-1, -1), 'MIDDLE'),
    ]))
    
    story.append(firma_table)
    
    # Construir PDF
    doc.build(story)
    
    # Retornar respuesta
    buffer.seek(0)
    response = HttpResponse(buffer.getvalue(), content_type='application/pdf')
    response['Content-Disposition'] = f'attachment; filename="acta_hash_{formulario.nro_hash}.pdf"'
    
    return response


def generar_reporte_custodia(request, formulario_id):
    """Genera el reporte PDF del acta de custodia"""
    formulario = get_object_or_404(
        FormularioHash.objects.select_related(
            'oficial_entrega__jerarquia', 'oficial_recibe__jerarquia'
        ),
        id=formulario_id
    )
    
    # Crear el PDF
    buffer = BytesIO()
    doc = SimpleDocTemplate(buffer, pagesize=A4, topMargin=0.5*inch)
    story = []
    
    # Estilos
    styles = getSampleStyleSheet()
    title_style = ParagraphStyle(
        'CustomTitle',
        parent=styles['Heading1'],
        fontSize=16,
        spaceAfter=30,
        alignment=TA_CENTER,
        textColor=colors.black
    )
    
    # Título del documento
    story.append(Paragraph("ACTA DE CADENA DE CUSTODIA", title_style))
    story.append(Spacer(1, 20))
    
    # Información básica
    info_text = f"""
    <b>Número de Hash:</b> #{formulario.nro_hash}<br/>
    <b>Procedimiento:</b> {formulario.procedimiento}<br/>
    <b>Fecha y Hora:</b> {formulario.fecha_creacion.strftime("%d/%m/%Y %H:%M:%S")}<br/>
    <b>Total de Archivos:</b> {formulario.total_archivos}<br/>
    <b>Peso Total:</b> {formulario.peso_total_formateado}
    """
    
    story.append(Paragraph(info_text, styles['Normal']))
    story.append(Spacer(1, 30))
    
    # Declaración de custodia
    declaracion = """
    Por medio del presente documento, se hace constar que los archivos digitales 
    detallados en el Acta de Hash correspondiente, han sido entregados bajo estricta 
    cadena de custodia, manteniendo su integridad mediante el cálculo de hash SHA-256.
    
    La cadena de custodia garantiza que los archivos no han sido alterados desde 
    su procesamiento inicial hasta su entrega final.
    """
    
    story.append(Paragraph(declaracion, styles['Normal']))
    story.append(Spacer(1, 40))
    
    # Tabla de firmas con más espacio
    firma_data = [
        ["ENTREGA", "RECIBE"],
        ["", ""],
        ["", ""],
        ["", ""],
        ["_" * 40, "_" * 40],
        [str(formulario.oficial_entrega), str(formulario.oficial_recibe)],
        [f"Fecha: ___/___/______", f"Fecha: ___/___/______"],
        [f"Hora: ___:___", f"Hora: ___:___"],
    ]
    
    firma_table = Table(firma_data, colWidths=[3*inch, 3*inch], rowHeights=[0.3*inch, 0.8*inch, 0.8*inch, 0.8*inch, 0.3*inch, 0.3*inch, 0.3*inch, 0.3*inch])
    firma_table.setStyle(TableStyle([
        ('ALIGN', (0, 0), (-1, -1), 'CENTER'),
        ('FONTNAME', (0, 0), (-1, 0), 'Helvetica-Bold'),
        ('FONTNAME', (0, 5), (-1, 5), 'Helvetica-Bold'),
        ('FONTSIZE', (0, 0), (-1, -1), 12),
        ('VALIGN', (0, 0), (-1, -1), 'MIDDLE'),
        ('GRID', (0, 0), (-1, -1), 1, colors.black),
    ]))
    
    story.append(firma_table)
    
    # Construir PDF
    doc.build(story)
    
    # Retornar respuesta
    buffer.seek(0)
    response = HttpResponse(buffer.getvalue(), content_type='application/pdf')
    response['Content-Disposition'] = f'attachment; filename="acta_custodia_{formulario.nro_hash}.pdf"'
    
    return response
