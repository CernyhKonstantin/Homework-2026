# Docker Class Work — Apache HTTP Server (`httpd`)

Windows PowerShell + Docker Desktop instructions for practicing the required Docker commands.

## 1. Check Docker
```powershell
docker version
docker info
```
If the engine is unavailable, start Docker Desktop and wait until it is ready.

## 2. Image commands
```powershell
docker search httpd
docker pull httpd:latest
docker images
docker image ls
```

Remove an image only after removing containers that depend on it:
```powershell
docker ps -a
docker rmi httpd:latest
```

Build the custom image included in this project:
```powershell
docker build -t classwork-httpd:1.0 .
docker images
```

## 3. Start the Apache container and publish a port
```powershell
docker run -d --name classwork-httpd -p 8080:80 httpd:latest
docker ps
```
Open **http://localhost:8080** in your browser. `-d` runs the container in the background, `--name` assigns its name, and `-p 8080:80` maps host port 8080 to container port 80. If port 8080 is busy, use `-p 8081:80` and visit `http://localhost:8081`.

## 4. Container commands
```powershell
docker ps
docker ps -a
docker logs classwork-httpd
docker logs --tail 50 classwork-httpd
docker exec classwork-httpd httpd -v
docker exec -it classwork-httpd sh
```
Type `exit` to leave the container shell. Use `Ctrl+C` to stop following logs; it does not stop the container.

Start/stop/restart:
```powershell
docker stop classwork-httpd
docker start classwork-httpd
docker restart classwork-httpd
```

Remove the container:
```powershell
docker stop classwork-httpd
docker rm classwork-httpd
```
`docker rm` removes the container, not the image.

## 5. Options
- `-d`: run in the background.
- `--rm`: remove the container automatically when it exits. Example short test:
  ```powershell
  docker run --rm httpd:latest httpd -v
  ```
- `--name`: set a readable container name.
- `-p HOST_PORT:CONTAINER_PORT`: publish a container port on the host, e.g. `-p 8080:80`.

## 6. Suggested sequence
```powershell
docker pull httpd:latest
docker images
docker search httpd
docker run -d --name classwork-httpd -p 8080:80 httpd:latest
docker ps
docker logs classwork-httpd
docker exec classwork-httpd httpd -v
```
Then open `http://localhost:8080` and test:
```powershell
docker stop classwork-httpd
docker ps
docker ps -a
docker start classwork-httpd
docker restart classwork-httpd
```

## 7. Troubleshooting
- **Docker engine unavailable:** start Docker Desktop.
- **Container name already exists:** inspect `docker ps -a`. Only if safe, remove the old container using `docker rm -f classwork-httpd`.
- **Port in use:** publish a different host port, e.g. `-p 8081:80`.
- **Browser cannot connect:** check `docker ps` and `docker logs classwork-httpd`.
- **Image is in use:** remove the relevant container first if you no longer need it.

## 8. Screenshots for submission
Add real screenshots to the `screenshots/` folder after running the commands. Recommended:
1. `docker images` showing `httpd`.
2. `docker ps` showing the running container and port mapping.
3. Browser at `http://localhost:8080`.
4. `docker logs classwork-httpd`.
5. `docker ps -a` after stopping the container.
6. `docker images` showing your custom image.

No screenshots or terminal results have been fabricated.

## Project contents
- `Dockerfile`: custom image based on official `httpd`.
- `index.html`: custom Apache landing page.
- `commands/commands.ps1`: command checklist for PowerShell.
- `screenshots/`: add your own evidence.
