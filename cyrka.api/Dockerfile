### Build stage
FROM microsoft/aspnetcore-build as build
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /cyrkaapi
COPY . .
RUN dotnet publish ./cyrka.api.web/cyrka.api.web.csproj -c Release -o out

### Test stage

### Deploy stage
FROM microsoft/aspnetcore as deploy
WORKDIR /out
COPY --from=build /cyrkaapi/cyrka.api.web/out /out
ENTRYPOINT ["dotnet", "cyrka.api.web.dll"]
