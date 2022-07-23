FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /src
COPY . .
RUN dotnet build "./demo/Smokey.Demo/Smokey.Demo.csproj" --configuration Release

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /tests
COPY --from=builder /src/demo/Smokey.Demo/bin/Release/net6.0 .

ARG REMOTE_HOST=${REMOTE_HOST}
ENV REMOTE_HOST=${REMOTE_HOST}

ENTRYPOINT sleep 10 && exec dotnet test Smokey.Demo.dll || exit 1