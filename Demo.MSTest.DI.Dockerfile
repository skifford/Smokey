FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /src
COPY . .
RUN dotnet build "./demo/Smokey.Demo.MSTest.DI/Smokey.Demo.MSTest.DI.csproj" --configuration Release

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS final
WORKDIR /tests
COPY --from=builder /src/demo/Smokey.Demo.MSTest.DI/bin/Release/net5.0 .

ARG REMOTE_HOST=${REMOTE_HOST}
ENV REMOTE_HOST=${REMOTE_HOST}

ENTRYPOINT exec dotnet test Smokey.Demo.MSTest.DI.dll || exit 1