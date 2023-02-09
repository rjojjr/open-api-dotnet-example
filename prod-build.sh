#!/bin/sh

docker build -t rjojjr91/open-api-broker:stable -t "rjojjr91/open-api-broker:$1" .
docker push "rjojjr91/open-api-broker:$1"
docker push rjojjr91/open-api-broker:stable

