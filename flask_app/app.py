from flask import Flask, render_template, request
import hashlib
import os
from werkzeug.utils import secure_filename

app = Flask(__name__)

ALLOWED_EXTENSIONS = set(['txt', 'pdf', 'png', 'jpg', 'jpeg', 'gif', 'mp3', 'wav', 'flac',
                          'mp4', 'avi', 'mov', 'wmv', 'bmp', 'doc', 'docx'])

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
                    'name': filename,
                    'ext': extension,
                    'size': size,
                    'hash': file_hash
                })
        return render_template('result.html', results=results, counters=counters, total_size=total_size)
    return render_template('index.html')


if __name__ == '__main__':
    app.run(debug=True)
