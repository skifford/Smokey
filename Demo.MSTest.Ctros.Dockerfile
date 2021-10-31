FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /src
COPY . .
RUN dotnet build "./demo/Smokey.Demo.MSTest.Ctors/Smokey.Demo.MSTest.Ctors.csproj" --configuration Release

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS final
WORKDIR /tests
COPY --from=builder /src/demo/Smokey.Demo.MSTest.Ctors/bin/Release/net5.0 .

ARG REMOTE_HOST=${REMOTE_HOST}
ENV REMOTE_HOST=${REMOTE_HOST}

ENTRYPOINT exec dotnet test Smokey.Demo.MSTest.Ctors.dll || exit 1