from flask import Flask, render_template, request, send_file
import hashlib
import os
import io
import csv
from werkzeug.utils import secure_filename

app = Flask(__name__)

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
        return render_template('result.html', results=results, counters=counters, total_size=total_size)
    return render_template('index.html')


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
