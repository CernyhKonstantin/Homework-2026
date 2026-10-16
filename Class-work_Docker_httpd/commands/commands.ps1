# Docker Class Work — Windows PowerShell command checklist
docker version
docker pull httpd:latest
docker images
docker search httpd

# Start Apache: host port 8080 -> container port 80
docker run -d --name classwork-httpd -p 8080:80 httpd:latest

docker ps
docker ps -a
docker logs classwork-httpd
docker exec classwork-httpd httpd -v

# Container lifecycle
docker stop classwork-httpd
docker start classwork-httpd
docker restart classwork-httpd

# Build custom image from this project folder
docker build -t classwork-httpd:1.0 .
docker images

# Cleanup commands (uncomment only when you are ready)
# docker stop classwork-httpd
# docker rm classwork-httpd
# docker rmi httpd:latest
