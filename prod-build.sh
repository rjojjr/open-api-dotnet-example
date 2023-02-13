#!/usr/bin/env bash

docker build -t rjojjr91/open-ai-broker:stable -t "rjojjr91/open-ai-broker:$1" .
docker push "rjojjr91/open-ai-broker:$1"
docker push rjojjr91/open-ai-broker:stable

