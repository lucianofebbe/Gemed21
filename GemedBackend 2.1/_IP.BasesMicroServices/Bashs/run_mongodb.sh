#!/bin/bash

docker run -d \
  --name mongodbCache \
  --restart always \
  -p 27018:27017 \
  -v mongodb_data:/data/db \
  -e MONGO_INITDB_ROOT_USERNAME=Gemed2022 \
  -e MONGO_INITDB_ROOT_PASSWORD=Gemed2022Teste \
  -e MONGO_INITDB_DATABASE=dbCache \
  mongo
