## Docker Command

``` bash
docker build -t <image-name>:<tag> --file /<path>/Dockerfile .
```

```bash
docker run -d --name <container-name> -p 8000:80 <image-name>:<tag>
```

## Image

```bash
docker build -t pos-api:latest --file ./POS.API/Dockerfile .
```

## Container

```bash
docker run -d --name pos-api -p 5001:80 pos-api:latest
```

