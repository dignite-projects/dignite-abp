#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p d56d56b8-b1df-41ce-a102-9e8c458b58de -t
    fi
    cd ../
fi

docker-compose up -d
