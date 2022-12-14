FROM ghcr.io/dysnomia-studio/net-sdk-7-0:main AS build-env
WORKDIR /app

ARG SONAR_HOST_URL
ARG SONAR_TOKEN
ARG GITHUB_BRANCH
ARG GHP_USER
ARG GHP_TOKEN

# Build Project
COPY . ./

RUN dotnet sonarscanner begin /k:"downstatus" /d:sonar.host.url="$SONAR_HOST_URL" /d:sonar.login="$SONAR_TOKEN" /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Test*.cs" /d:sonar.branch.name="$GITHUB_BRANCH"
RUN dotnet restore Dysnomia.DownStatus.sln --ignore-failed-sources /p:EnableDefaultItems=false
RUN dotnet publish Dysnomia.DownStatus.sln --no-restore -c Release -o out
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
RUN dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"

# Build runtime image
FROM ghcr.io/dysnomia-studio/net-runtime-7-0:main
WORKDIR /app
COPY --from=build-env /app/out .
HEALTHCHECK --interval=2m --timeout=3s CMD curl -f http://localhost/ && curl -f http://localhost/count || exit 1
ENTRYPOINT ["dotnet", "Dysnomia.DownStatus.WebApp.dll"]