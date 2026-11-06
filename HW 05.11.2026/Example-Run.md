# Example Run

The exact image digest and download progress depend on the machine and registry.

```text
PS> docker build -t hw-centos-apache:latest .
[+] Building ...
 => [internal] load build definition from Dockerfile
 => [internal] load metadata for quay.io/centos/centos:stream9
 => [1/3] FROM quay.io/centos/centos:stream9
 => [2/3] RUN dnf -y update && dnf -y install httpd ...
 => [3/3] COPY site/ /var/www/html/
 => exporting to image
 => naming to docker.io/library/hw-centos-apache:latest
```

Start the container:

```text
PS> docker run -d --name hw-centos-apache -p 8080:80 hw-centos-apache:latest
<container-id>
```

Open `http://localhost:8080`. The page title is **CentOS + Apache HTTP Server** and the page confirms that Apache is running.
