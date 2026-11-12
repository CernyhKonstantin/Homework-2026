# Build and run both homework containers.
$ErrorActionPreference = "Stop"
if (-not (Get-Command docker -ErrorAction SilentlyContinue)) { throw "Docker CLI was not found." }
Write-Host "Checking Docker Engine..." -ForegroundColor Cyan
docker info | Out-Null
if ($LASTEXITCODE -ne 0) { throw "Docker Engine is unavailable. Start Docker Desktop and retry." }

Write-Host "Building CentOS + Apache image..." -ForegroundColor Cyan
docker build -f Dockerfile.centos -t hw-centos-apache-env:latest .
if ($LASTEXITCODE -ne 0) { throw "CentOS image build failed." }

Write-Host "Building PHP information image..." -ForegroundColor Cyan
docker build -f Dockerfile.php -t hw-php-info:latest .
if ($LASTEXITCODE -ne 0) { throw "PHP image build failed." }

docker rm -f hw-centos-apache-env hw-php-info 2>$null | Out-Null
Write-Host "Starting CentOS + Apache..." -ForegroundColor Cyan
docker run -d --name hw-centos-apache-env -p 8081:80 `
  -e APP_TITLE="CentOS Apache Environment Demo" `
  -e APP_MESSAGE="This page was generated from container environment variables." `
  -e APP_ENV="homework" -e APP_VERSION="1.0.0" -e SERVER_OWNER="CS-Digital" `
  hw-centos-apache-env:latest
if ($LASTEXITCODE -ne 0) { throw "CentOS Apache container failed to start." }

Write-Host "Starting PHP information page..." -ForegroundColor Cyan
docker run -d --name hw-php-info -p 8082:80 hw-php-info:latest
if ($LASTEXITCODE -ne 0) { throw "PHP container failed to start." }

Write-Host "CentOS + Apache: http://localhost:8081" -ForegroundColor Green
Write-Host "PHP version/modules: http://localhost:8082" -ForegroundColor Green
