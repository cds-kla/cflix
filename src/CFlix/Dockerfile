FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY CFlix/CFlix.csproj CFlix/CFlix.csproj
COPY CFlix.ActiveDirectory.NETStandard/CFlix.ActiveDirectory.NETStandard.csproj CFlix.ActiveDirectory.NETStandard/CFlix.ActiveDirectory.NETStandard.csproj
COPY *.sln ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80
COPY --from=build /src/CFlix/out ./
ENTRYPOINT ["dotnet", "CFlix.dll"]
