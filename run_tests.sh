#! /bin/bash
clear
docker build -f Demo.MSTest.DI.Dockerfile . -t smokey_demo_mstest_di
docker build -f Demo.MSTest.Ctros.Dockerfile . -t smokey_demo_mstest_ctors
docker-compose -f docker-compose.demo.yml -p smokey up -d