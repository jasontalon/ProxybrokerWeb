FROM --platform=linux/amd64 python:3.9-bullseye

RUN \
    # Install .NET 6 SDK
    # https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian#debian-11-
    wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb \
    && apt-get update && apt-get install -y dotnet-sdk-6.0 \
    # Install Node 16.x
    # https://github.com/nodesource/distributions/blob/master/README.md#installation-instructions
    && curl -fsSL https://deb.nodesource.com/setup_16.x | bash - \
    && apt-get install -y nodejs \
    && npm install -g nodemon \
    # https://github.com/jasontalon/proxybroker2/blob/master/Dockerfile \    
    && git clone https://github.com/jasontalon/proxybroker2.git && cd proxybroker2 \
    && pip install -U poetry \
    && poetry config virtualenvs.create false \
    && poetry install --no-interaction --no-ansi --no-dev 

ENV DOTNET_USE_POLLING_FILE_WATCHER=1 \
    DOTNET_WATCH_SUPPRESS_EMOJIS=1 \
    DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER=1 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false \
    ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Development
    
WORKDIR /app
    
EXPOSE 8080

CMD ["nodemon", "--delay", "2500ms", "--watch", "ProxybrokerWeb/bin/Debug/net6.0/", "--legacy-watch",
    "--exec", 
    "dotnet", "run", "--configuration", "Debug", "--no-launch-profile", "--no-build", "--project", "ProxybrokerWeb"]