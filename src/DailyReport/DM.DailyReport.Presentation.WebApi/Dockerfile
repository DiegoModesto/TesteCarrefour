FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DailyReport/DM.DailyReport.Presentation.WebApi/DM.DailyReport.Presentation.WebApi.csproj", "src/DailyReport/DM.DailyReport.Presentation.WebApi/"]
COPY ["src/DM.SharedKernel/DM.SharedKernel.csproj", "src/DM.SharedKernel/"]
COPY ["src/DailyReport/DM.DailyReport.Application/DM.DailyReport.Application.csproj", "src/DailyReport/DM.DailyReport.Application/"]
COPY ["src/DailyReport/DM.DailyReport.Domain/DM.DailyReport.Domain.csproj", "src/DailyReport/DM.DailyReport.Domain/"]
COPY ["src/DailyReport/DM.DailyReport.Infra/DM.DailyReport.Infra.csproj", "src/DailyReport/DM.DailyReport.Infra/"]
COPY ["src/DailyReport/DM.DailyReport.Presentation/DM.DailyReport.Presentation.csproj", "src/DailyReport/DM.DailyReport.Presentation/"]
RUN dotnet restore "src/DailyReport/DM.DailyReport.Presentation.WebApi/DM.DailyReport.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/DailyReport/DM.DailyReport.Presentation.WebApi"
RUN dotnet build "DM.DailyReport.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DM.DailyReport.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DM.DailyReport.Presentation.WebApi.dll"]
