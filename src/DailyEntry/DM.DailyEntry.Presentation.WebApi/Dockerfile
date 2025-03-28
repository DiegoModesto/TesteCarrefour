FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DailyEntry/DM.DailyEntry.Presentation.WebApi/DM.DailyEntry.Presentation.WebApi.csproj", "src/DailyEntry/DM.DailyEntry.Presentation.WebApi/"]
COPY ["src/DM.SharedKernel/DM.SharedKernel.csproj", "src/DM.SharedKernel/"]
COPY ["src/DailyEntry/DM.DailyEntry.Application/DM.DailyEntry.Application.csproj", "src/DailyEntry/DM.DailyEntry.Application/"]
COPY ["src/DailyEntry/DM.DailyEntry.Domain/DM.DailyEntry.Domain.csproj", "src/DailyEntry/DM.DailyEntry.Domain/"]
COPY ["src/DailyEntry/DM.DailyEntry.Infra/DM.DailyEntry.Infra.csproj", "src/DailyEntry/DM.DailyEntry.Infra/"]
COPY ["src/DailyEntry/DM.DailyEntry.Presentation/DM.DailyEntry.Presentation.csproj", "src/DailyEntry/DM.DailyEntry.Presentation/"]
RUN dotnet restore "src/DailyEntry/DM.DailyEntry.Presentation.WebApi/DM.DailyEntry.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/DailyEntry/DM.DailyEntry.Presentation.WebApi"
RUN dotnet build "DM.DailyEntry.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DM.DailyEntry.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DM.DailyEntry.Presentation.WebApi.dll"]
