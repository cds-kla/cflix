FROM postgres:alpine
# RUN addgroup -S postgres && adduser -S -G postgres cflix-db-admin
# USER cflix-db-admin
COPY "./SQL_Scripts/*" /docker-entrypoint-initdb.d/
