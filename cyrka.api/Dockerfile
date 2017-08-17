# Build stage
FROM microsoft/dotnet as build

WORKDIR /cyrkaapi
COPY . .
RUN dotnet publish ./cyrka.api.web/cyrka.api.web.csproj -c Release -o out

# Test stage


# Deploy stage
FROM microsoft/dotnet:runtime as deploy
WORKDIR /out
COPY --from=build /cyrkaapi/cyrka.api.web/out /out
ENTRYPOINT ["dotnet", "cyrka.api.web.dll"]
