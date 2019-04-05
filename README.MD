## build image

```bash
docker build -t netcore-phantomjs .
```

## create container
```bash
docker run -d -p 5000:5000 -v $PWD/output:/app/output --name netcore-phantomjs netcore-phantomjs
```

## GET http://localhost:5000/api/values/
for testing, get dotnet version

## POST http://localhost:5000/api/values/
for generate screenshot

```json
{
    "url": "https://myifew.com",
    "filename": "myifew.png",
    "size_width": "1280",
    "size_heigh": "1500"
}
```