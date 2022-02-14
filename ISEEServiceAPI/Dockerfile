#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update
RUN apt-get install -y libgdiplus libc6-dev libharfbuzz-dev
#Add these two lines
#RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
#RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig
#RUN apt-get update; apt-get install -y fontconfig fonts-liberation
#RUN fc-cache -f -v

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["ISEEServiceAPI/ISEEServiceAPI.csproj", "ISEEServiceAPI/"]
COPY ["ISEEService.DataContract/ISEEService.DataContract.csproj", "ISEEService.DataContract/"]
COPY ["ITUtility/ITUtility.csproj", "ITUtility/"]
COPY ["ISEEService.BusinessLogic/ISEEService.BusinessLogic.csproj", "ISEEService.BusinessLogic/"]
COPY ["ISEEService.DataAccess/ISEEService.DataAccess.csproj", "ISEEService.DataAccess/"]
COPY ["DataAccessUtility/DataAccessUtility.csproj", "DataAccessUtility/"]
RUN dotnet restore "ISEEServiceAPI/ISEEServiceAPI.csproj"
COPY . .
WORKDIR "/src/ISEEServiceAPI"
RUN dotnet build "ISEEServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ISEEServiceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ISEEServiceAPI.dll"]
