FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY CFlix.ImageViewer.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0

RUN apt-get update \
    && apt-get install -y openssh-server tcpdump \
    && apt-get dist-upgrade -y \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir /var/run/sshd \
    && chmod 0755 /var/run/sshd \
    && groupadd pcap \
    && chgrp pcap /usr/sbin/tcpdump \
    && chmod 750 /usr/sbin/tcpdump \
    && useradd -ms /bin/bash -p saTZPg3BnObts cartman \
    && useradd -ms /bin/bash negan \
    && usermod -a -G pcap cartman \
    && ln -s /usr/sbin/tcpdump /usr/local/bin/tcpdump

COPY [ "Dockerfiles/motd-challenge", "Dockerfiles/motd", "/etc/" ]
COPY Dockerfiles/sshd_config /etc/ssh/sshd_config

WORKDIR /app
ARG source
COPY --from=build /src/out ./
COPY Dockerfiles/run.sh Dockerfiles/capture_interessante.pcap ./

RUN chown -R negan:negan /app/* \
    && chmod -w /home/cartman \
    && chmod -w /home/negan \
    && chmod o-r /tmp \
    && chown cartman:cartman /app/capture_interessante.pcap \
    && chmod 400 /app/capture_interessante.pcap \
    && chown root:root /app/run.sh \
    && chmod 700 /app/run.sh \
    && chmod +r /app/wwwroot/images/6/*
    # && chown -R negan:negan /etc/ssh \
    # && chown -R negan:negan /etc/ssh/sshd_config

# USER negan

EXPOSE 22222
EXPOSE 5000

WORKDIR /app

# ENTRYPOINT ["dotnet", "CFlix.ImageViewer.dll"]
CMD ["./run.sh"]
