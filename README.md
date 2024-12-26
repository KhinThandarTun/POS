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

## Upload Image Store Mount in Docker

```
docker run -d --name upload-api -p 5001:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e TZ=Asia/Yangon --mount type=bind,source=C:/my-folder,target=/app/wwwroot/uploads --rm upload-api:latest
```

```
docker run -d --name upload-api -p 5001:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e TZ=Asia/Yangon --rm -v image-uploads:/app/wwwroot/uploads --mount type=bind,source=C:\my-folder,target=/app/wwwroot/uploads upload-api:latest
```
## Multiple Mount Folder

```
docker run -d --name dev_qf_ms_customer_api -p 5020:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e TZ=Asia/Yangon --mount type=bind,source=C:\kyc_verify\identityfront,target=/app/wwwroot/images/kyc_verify/identityfront --mount type=bind,source=C:\kyc_verify\identityback,target=/app/wwwroot/images/kyc_verify/identityback --mount type=bind,source=C:\kyc_verify\facefront,target=/app/wwwroot/images/kyc_verify/facefront --mount type=bind,source=C:\kyc_verify\faceleft,target=/app/wwwroot/images/kyc_verify/faceleft  --mount type=bind,source=C:\kyc_verify\faceright,target=/app/wwwroot/images/kyc_verify/faceright -v web-logs:/app/logs dev_qf_ms_customer_api:dev
696df8b8326701d0a17feffd07aba87bc634cf8c8247fdb5f9464c2deec0f149
```

## Create the necessary directory and set the permissions before switching users 

```
RUN mkdir -p /app/wwwroot/images/kyc_verify && chmod -R 777 /app/wwwroot/images/kyc_verify
```

```
RUN mkdir -p /app/wwwroot/images/credit_request && chmod -R 777 /app/wwwroot/images/credit_request
```

## Docker Image
```
docker build -t dev_qf_ms_customer_api:dev --file ./src/Microservices/QuickFood.Microservices.CustomerAPI/Dockerfile .
```

## Container Including Mount
```
docker run -d --name dev_qf_ms_customer_api -p 5020:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e TZ=Asia/Yangon --mount type=bind,source=C:\kyc_verify,target=/app/wwwroot/images/kyc_verify -v web-logs:/app/logs dev_qf_ms_customer_api:dev
```

## Project Scaffold
```
dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch4;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f --project path/to/YourProject.csproj
```


