### Build stage
FROM microsoft/dotnet:sdk as build
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 ASPNETCORE_ENVIRONMENT=Production
WORKDIR /cyrkaapi
COPY . .
RUN dotnet publish ./cyrka.api.web/cyrka.api.web.csproj -c Release -o out

### Test stage

### Deploy stage
FROM microsoft/dotnet:aspnetcore-runtime as deploy
WORKDIR /out
COPY --from=build /cyrkaapi/cyrka.api.web/out /out
ENTRYPOINT ["dotnet", "cyrka.api.web.dll"]
