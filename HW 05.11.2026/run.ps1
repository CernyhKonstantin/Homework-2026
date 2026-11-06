# HW 05.11.2026 - Build and run Apache on CentOS
# Run in PowerShell: powershell -ExecutionPolicy Bypass -File ".\run.ps1"

$ErrorActionPreference = "Stop"
$ImageName = "hw-centos-apache:latest"
$ContainerName = "hw-centos-apache"
$HostPort = 8080

Write-Host "Building the CentOS + Apache Docker image..." -ForegroundColor Cyan
docker build -t $ImageName .
if ($LASTEXITCODE -ne 0) {
    throw "Docker image build failed."
}

Write-Host "Removing an older container with the same name, if present..." -ForegroundColor Cyan
docker rm -f $ContainerName 2>$null | Out-Null

Write-Host "Starting the Apache container..." -ForegroundColor Cyan
docker run -d --name $ContainerName -p "${HostPort}:80" $ImageName
if ($LASTEXITCODE -ne 0) {
    throw "Docker container could not be started."
}

Write-Host ""
Write-Host "The web server should now be available at:" -ForegroundColor Green
Write-Host "http://localhost:$HostPort" -ForegroundColor Yellow
Write-Host ""
Write-Host "Useful commands:" -ForegroundColor Cyan
Write-Host "  docker ps"
Write-Host "  docker logs $ContainerName"
Write-Host "  docker exec -it $ContainerName bash"
Write-Host "  docker rm -f $ContainerName"
