from src.cheshire_cat.app import create_app

app = create_app()

if __name__ == '__main__':
    app.run(debug=True)
