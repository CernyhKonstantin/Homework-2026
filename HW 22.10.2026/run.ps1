$ErrorActionPreference = "Stop"

Write-Host "Pulling the official Apache HTTP Server image..." -ForegroundColor Cyan
docker pull httpd:latest
if ($LASTEXITCODE -ne 0) { throw "docker pull failed. Check that Docker Desktop is running." }

$sitePath = (Resolve-Path ".\site").Path
docker rm -f hw-httpd 2>$null | Out-Null

Write-Host "Starting Apache on http://localhost:8081 ..." -ForegroundColor Cyan
docker run -d --name hw-httpd -p 8081:80 -e APP_ENV=homework -v "${sitePath}:/usr/local/apache2/htdocs:ro" httpd:latest
if ($LASTEXITCODE -ne 0) {
    Write-Host "If port 8081 is busy, change 8081:80 to 8082:80." -ForegroundColor Yellow
    throw "docker run failed."
}
Write-Host "Done. Open http://localhost:8081 in your browser." -ForegroundColor Green
