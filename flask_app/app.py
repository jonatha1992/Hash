from flask import Flask, render_template_string, request, send_file
import hashlib
import os
import io
import csv
from werkzeug.utils import secure_filename

app = Flask(__name__)

# inline HTML templates to avoid extra files
INDEX_HTML = """
<!doctype html>
<html lang=\"en\">
<head>
    <meta charset=\"utf-8\">
    <title>Hash Uploader</title>
</head>
<body>
    <h1>Cargar archivos</h1>
    <form method=\"post\" enctype=\"multipart/form-data\">
        <input type=\"file\" name=\"files\" multiple>
        <button type=\"submit\">Procesar</button>
    </form>
</body>
</html>
"""

RESULT_HTML = """
<!doctype html>
<html lang=\"en\">
<head>
    <meta charset=\"utf-8\">
    <title>Resultado</title>
</head>
<body>
    <h1>Resultado del Hash</h1>
    <p>Total size: {{ total_size }} bytes</p>
    <ul>
        <li>Imagenes: {{ counters['image'] }}</li>
        <li>Clips: {{ counters['clip'] }}</li>
        <li>Audio: {{ counters['audio'] }}</li>
        <li>Texto: {{ counters['text'] }}</li>
        <li>Varios: {{ counters['varios'] }}</li>
    </ul>
    <table border=\"1\">
        <tr>
            <th>Nro Orden</th>
            <th>Nombre</th>
            <th>Extensión</th>
            <th>Peso (bytes)</th>
            <th>Peso</th>
            <th>SHA1</th>
        </tr>
        {% for r in results %}
        <tr>
            <td>{{ r.order }}</td>
            <td>{{ r.name }}</td>
            <td>{{ r.ext }}</td>
            <td>{{ r.size }}</td>
            <td>{{ r.size_readable }}</td>
            <td>{{ r.hash }}</td>
        </tr>
        {% endfor %}
    </table>
    <p><a href=\"/report\">Descargar reporte</a></p>
    <p><a href=\"/\">Volver</a></p>
</body>
</html>
"""

# store latest results for report generation
last_results = []
last_counters = {}
last_total_size = 0

ALLOWED_EXTENSIONS = set(['txt', 'pdf', 'png', 'jpg', 'jpeg', 'gif', 'mp3', 'wav', 'flac',
                          'mp4', 'avi', 'mov', 'wmv', 'bmp', 'doc', 'docx'])

def human_size(num_bytes):
    """Return a human readable file size like the Windows app."""
    kilobytes = num_bytes / 1024.0
    if kilobytes <= 1024:
        return f"{kilobytes:.2f} KB"
    megabytes = kilobytes / 1024.0
    if megabytes <= 1024:
        return f"{megabytes:.2f} MB"
    gigabytes = megabytes / 1024.0
    return f"{gigabytes:.2f} GB"

def allowed_file(filename):
    return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


def file_category(extension):
    if extension in ['jpg', 'jpeg', 'png', 'gif', 'bmp']:
        return 'image'
    if extension in ['mp4', 'avi', 'mov', 'wmv']:
        return 'clip'
    if extension in ['mp3', 'wav', 'flac']:
        return 'audio'
    if extension in ['pdf', 'txt', 'doc', 'docx']:
        return 'text'
    return 'varios'


def sha1_for_file(f):
    sha1 = hashlib.sha1()
    while True:
        data = f.read(1048576)
        if not data:
            break
        sha1.update(data)
    return sha1.hexdigest()


@app.route('/', methods=['GET', 'POST'])
def index():
    if request.method == 'POST':
        files = request.files.getlist('files')
        results = []
        counters = {
            'image': 0,
            'clip': 0,
            'audio': 0,
            'text': 0,
            'varios': 0,
        }
        total_size = 0
        order = 1
        for file in files:
            if file and allowed_file(file.filename):
                filename = secure_filename(file.filename)
                extension = filename.rsplit('.', 1)[1].lower()
                category = file_category(extension)
                counters[category] += 1
                file.seek(0)
                file_hash = sha1_for_file(file)
                file.seek(0, os.SEEK_END)
                size = file.tell()
                total_size += size
                results.append({
                    'order': order,
                    'name': filename,
                    'ext': extension,
                    'size': size,
                    'size_readable': human_size(size),
                    'hash': file_hash,
                    'si': 'SI'
                })
                order += 1
        global last_results, last_counters, last_total_size
        last_results = results
        last_counters = counters
        last_total_size = total_size
        return render_template_string(RESULT_HTML, results=results, counters=counters, total_size=total_size)
    return render_template_string(INDEX_HTML)


@app.route('/report')
def report():
    """Download the last hash result as a CSV report."""
    if not last_results:
        return "No hay resultados para generar reporte", 400
    output = io.StringIO()
    writer = csv.writer(output)
    writer.writerow(['Nro_Orden', 'Nombre', 'Extension', 'Peso', 'PesoArchivo', 'SHA1', 'SI'])
    for r in last_results:
        writer.writerow([
            r['order'],
            r['name'],
            r['ext'],
            r['size'],
            r['size_readable'],
            r['hash'],
            r['si'],
        ])
    output.seek(0)
    return send_file(
        io.BytesIO(output.getvalue().encode('utf-8')),
        mimetype='text/csv',
        as_attachment=True,
        download_name='hash_report.csv'
    )


if __name__ == '__main__':
    app.run(debug=True)
