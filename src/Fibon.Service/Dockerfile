FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app

EXPOSE 5050/tcp
ENV ASPNETCORE_URLS http://*:5050
ENV ASPNETCORE_ENVIRONMENT docker

ENTRYPOINT dotnet Fibon.Service.dll