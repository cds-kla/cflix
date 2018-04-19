#!/bin/bash
/usr/sbin/sshd
su negan <<'EOF'
dotnet CFlix.ImageViewer.dll
exit
EOF
exit