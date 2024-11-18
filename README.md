## Docker Command

## Image

```bash
docker build -t pos-api:latest --file ./POS.API/Dockerfile .
```

## Container

```bash
docker run -d --name pos-api -p 5001:80 pos-api:latest
```

