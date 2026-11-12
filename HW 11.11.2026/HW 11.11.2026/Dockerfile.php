FROM php:8.3-apache
LABEL description="PHP version and loaded modules information page"
WORKDIR /var/www/html
COPY php-info/site/index.php /var/www/html/index.php
EXPOSE 80
