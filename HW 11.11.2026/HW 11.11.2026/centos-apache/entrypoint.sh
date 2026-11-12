#!/bin/sh
set -eu
: "${APP_TITLE:=CentOS Apache Environment Demo}"
: "${APP_MESSAGE:=Apache is running inside a CentOS Stream 9 container.}"
: "${APP_ENV:=development}"
: "${APP_VERSION:=1.0.0}"
: "${SERVER_OWNER:=CS-Digital}"
export APP_TITLE APP_MESSAGE APP_ENV APP_VERSION SERVER_OWNER
envsubst '${APP_TITLE} ${APP_MESSAGE} ${APP_ENV} ${APP_VERSION} ${SERVER_OWNER}' \
  < /var/www/html/index.html.template > /var/www/html/index.html
exec /usr/sbin/httpd -DFOREGROUND
