FROM --platform=linux/amd64 python:3.9-bullseye

RUN \
    # Install .NET 6 SDK
    # https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian#debian-11-
    wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb \
    && apt-get update && apt-get install -y dotnet-sdk-6.0 \
    # https://github.com/jasontalon/proxybroker2/blob/master/Dockerfile
    && git clone https://github.com/jasontalon/proxybroker2.git && cd proxybroker2 \
    && pip install -U poetry \
    && poetry config virtualenvs.create false \
    && poetry install --no-interaction --no-ansi --no-dev
    
CMD ["bash"]

        