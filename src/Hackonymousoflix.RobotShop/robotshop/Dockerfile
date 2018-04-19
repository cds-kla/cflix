FROM php:5-apache

RUN apt-get update \
    && apt-get dist-upgrade -y \
    && apt-get install -y libsqlite3-dev \
    && rm -rf /var/lib/apt/lists/* 

# COPY apache2.conf /etc/apache2/
COPY src /var/www/html

RUN chown www-data:www-data -R /var/www/html/challengeUtils
