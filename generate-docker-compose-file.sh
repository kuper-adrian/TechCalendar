#!/bin/sh

#define parameters which are passed in.
IMAGE_TAG=$1

#define the template.
cat  << EOF
version: '3.4'

services:
  
  web:
    restart: always
    image: registry.gitlab.com/kuper-adrian/stream-schedule.io/web:$IMAGE_TAG
    build:
      context: .
      dockerfile: TechCalendar.Web/Dockerfile
    container_name: stream-schedule-web
    environment:
      DOTNET_RUNNING_IN_CONTAINER: "true"
      ASPNETCORE_URLS: http://+:6000
  
  nginx:
    restart: always
    image: registry.gitlab.com/kuper-adrian/stream-schedule.io/nginx:$IMAGE_TAG
    build:
      context: .
      dockerfile: nginx/Dockerfile
    container_name: stream-schedule-nginx
EOF