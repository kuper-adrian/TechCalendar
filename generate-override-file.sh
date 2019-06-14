#!/bin/sh

#define parameters which are passed in.
GMAIL_ADDRESS=$1
GMAIL_APP_PASSWORD=$2

#define the template.
cat  << EOF
version: '3.4'

services:

  web:
    environment:
      - Gmail__Address=$GMAIL_ADDRESS
      - Gmail__AppPassword=$GMAIL_APP_PASSWORD
    volumes:
      - /stream-schedule/Data/:/app/Data/

  nginx:
    volumes:
      - /etc/letsencrypt/:/etc/letsencrypt/
    ports:
      - 80:80
      - 443:443
EOF