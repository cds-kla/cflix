FROM node:7

RUN apt-get update \
    && apt-get dist-upgrade -y \
    && apt-get install -y openssh-server gdb \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir /var/run/sshd \
    && useradd -ms /bin/bash livai.ackerman \
    && useradd -ms /home/hackonymousoflix/.shell.sh hackonymousoflix \
    && useradd -ms /bin/bash will.byers \
    && chmod o-r /tmp \
    && chmod 755 /var/run/sshd

COPY src /etc

# chall 9 Exploit cookie
WORKDIR /home/livai.ackerman
COPY src-chall09 .
RUN npm install \
    && chown livai.ackerman:livai.ackerman -R * \
    && chmod 700 *  \
    && chmod -w .
EXPOSE 1337

# chall 10 Jail JS
WORKDIR /home/hackonymousoflix
COPY src-chall10 .
RUN npm install \
    && chown hackonymousoflix:hackonymousoflix -R .shell.sh .ssh * \
    && chmod 600 .ssh/authorized_keys \
    && chmod 700 .shell.sh \
    && chmod 700 vm.js \
    && chmod -w . \
    && echo "source /etc/peda/peda.py" >> .gdbinit
EXPOSE 845

# chall 11 Exploit C
WORKDIR /home/will.byers
COPY src-chall11 .
RUN gcc -o exploit exploit.c -fno-stack-protector -Wl,-z,relro,-z,now,-z,noexecstack \
    && rm exploit.c \
    && chown will.byers:will.byers -R * \
    && chmod 4755 exploit \
    && chmod 700 MyLittleToDoList.txt \
    && chown hackonymousoflix:hackonymousoflix -R /etc/peda \
    && chmod +r /etc/peda/* \
    && chmod -w .

# container launch
WORKDIR /app
COPY run.sh .
RUN chmod 700 /app/run.sh

CMD ["/app/run.sh"]
