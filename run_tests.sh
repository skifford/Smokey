#! /bin/bash
clear
docker build -f Demo.Dockerfile . -t smokey_demo
docker-compose -f docker-compose.demo.yml -p smokey up -d
echo
echo Waiting for tests running...
echo
docker logs smokey-test_runner_demo_chrome-1 -f