FROM nginx:alpine
COPY proxy.conf /etc/nginx/proxy.conf
COPY server.crt server.key /etc/ssl/certs/
COPY nginx.conf /etc/nginx/nginx.conf