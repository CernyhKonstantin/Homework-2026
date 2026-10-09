#!/usr/bin/env bash
set -euo pipefail

docker compose up -d --build
docker compose ps

echo
echo "Web demo: http://localhost:8080"
echo "CentOS release:"
docker compose exec centos-demo cat /etc/centos-release
