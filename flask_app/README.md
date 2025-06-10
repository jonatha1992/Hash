# Flask Hash Web App

This simple Flask application mimics the basic hashing functionality of the original Windows Forms application. Users can upload multiple files and the server will compute a SHA-1 hash for each file and display summary statistics.

After hashing, a CSV report can be downloaded with the same columns as the
original desktop report (orden, nombre, extensión, tamaño en bytes,
tamaño legible y hash SHA-1).

## Setup

1. Create a virtual environment (optional):
   ```bash
   python3 -m venv venv
   source venv/bin/activate
   ```
2. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```
3. Run the application (no template files are required as the HTML is
   embedded in `app.py`):
   ```bash
   python app.py
   ```
4. Navigate to `http://localhost:5000` in a browser to use the tool.
