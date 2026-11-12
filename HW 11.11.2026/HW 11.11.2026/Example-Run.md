# Example Run

The exact image download output, PHP patch version, and module list depend on the machine and image version.

Run:
```text
PS> powershell -ExecutionPolicy Bypass -File ".\run.ps1"
Checking Docker Engine...
Building CentOS + Apache image...
Building PHP information image...
Starting CentOS + Apache...
Starting PHP information page...
CentOS + Apache: http://localhost:8081
PHP version/modules: http://localhost:8082
```

## Browser result 1
Open `http://localhost:8081`. The page shows the configured title, message, application environment, application version, and server owner. The values are passed through Docker environment variables and substituted by `envsubst` when the container starts.

## Browser result 2
Open `http://localhost:8082`. The page displays the running PHP version and loaded PHP modules.

## Check
```powershell
docker ps
docker logs hw-centos-apache-env
docker logs hw-php-info
```
