
# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 as builder

WORKDIR /app

COPY ./ src/

RUN dotnet nuget add source \
    "https://nuget.pkg.github.com/intelligent-growth-solutions/index.json" \
    --name "igs-github" \
    --store-password-in-clear-text \
    --username "igs-github" \

RUN dotnet publish src --verbosity minimal --configuration release

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as release

COPY --from=builder /app/src/bin/release/net8.0/publish/ /app
ARG OTEL_VERSION=1.4.0
ADD https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/download/v${OTEL_VERSION}/otel-dotnet-auto-install.sh otel-dotnet-auto-install.sh
RUN apt-get update && apt-get install -y curl unzip && \
    OTEL_DOTNET_AUTO_HOME="/otel-dotnet-auto" sh otel-dotnet-auto-install.sh && \
    chmod +x /otel-dotnet-auto/instrument.sh
ENV OTEL_DOTNET_AUTO_HOME="/otel-dotnet-auto"
WORKDIR /app
ENTRYPOINT ["/otel-dotnet-auto/instrument.sh", "dotnet", "GrafanaOtelTest.dll"]
