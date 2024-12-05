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

```
docker run -d --name upload-api -p 5001:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e TZ=Asia/Yangon --mount type=bind,source=C:/my-folder,target=/app/wwwroot/uploads --rm upload-api:latest
```

```
docker run -d --name upload-api -p 5001:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e TZ=Asia/Yangon --rm -v image-uploads:/app/wwwroot/uploads --mount type=bind,source=C:\my-folder,target=/app/wwwroot/uploads upload-api:latest
```


