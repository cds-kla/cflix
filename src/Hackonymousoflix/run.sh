#!/bin/bash
/usr/sbin/sshd
su livai.ackerman <<'EOF'
cd
while :
do
    node server.js && exit
done
exit
EOF
exit