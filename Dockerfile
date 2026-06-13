FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["QuestorTeste/QuestorTeste/QuestorTeste.csproj", "QuestorTeste/QuestorTeste/"]
RUN dotnet restore "QuestorTeste/QuestorTeste/QuestorTeste.csproj"

COPY . .

WORKDIR "/src/QuestorTeste/QuestorTeste"
RUN dotnet build "QuestorTeste.csproj" -c Release -o /app/build
RUN dotnet publish "QuestorTeste.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "QuestorTeste.dll"]