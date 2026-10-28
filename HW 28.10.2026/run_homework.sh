#!/usr/bin/env bash
set -e

IMAGE="nicolaka/netshoot"
CONTAINER_HOST="netshoot-host-container"
CONTAINER_NONE="netshoot-none-container"

echo "=== Pulling image $IMAGE ==="
docker pull $IMAGE

echo -e "\n=== 1. Creating and inspecting container in HOST network ==="
# Cleanup any old instance
docker rm -f $CONTAINER_HOST 2>/dev/null || true

# Run container with host network
docker run -d --name $CONTAINER_HOST --net=host $IMAGE sleep 3600

echo "--- Docker Inspect (Host Network) ---"
docker inspect $CONTAINER_HOST | grep -A 10 "NetworkSettings"

echo "--- Exec 'ip a' inside Host Container ---"
docker exec $CONTAINER_HOST ip a

echo -e "\n=== 2. Creating and inspecting container in NULL (NONE) network ==="
# Cleanup any old instance
docker rm -f $CONTAINER_NONE 2>/dev/null || true

# Run container with none network
docker run -d --name $CONTAINER_NONE --net=none $IMAGE sleep 3600

echo "--- Docker Inspect (Null/None Network) ---"
docker inspect $CONTAINER_NONE | grep -A 10 "NetworkSettings"

echo "--- Exec 'ip a' inside Null Container ---"
docker exec $CONTAINER_NONE ip a

echo -e "\n=== Cleanup ==="
docker rm -f $CONTAINER_HOST $CONTAINER_NONE
echo "Completed successfully."
