# CentOS Stream 9 with Apache HTTP Server
FROM quay.io/centos/centos:stream9

LABEL maintainer="HW 05.11.2026"
LABEL description="Apache HTTP Server running on CentOS Stream 9"

RUN dnf -y update && \
    dnf -y install httpd && \
    dnf clean all && \
    rm -rf /var/cache/dnf

COPY site/ /var/www/html/

EXPOSE 80

# Keep Apache in the foreground so Docker can manage the container.
CMD ["/usr/sbin/httpd", "-D", "FOREGROUND"]
