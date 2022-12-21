#!/usr/bin/env bash

cd jcpfrontend

npm install
npm run build-staging

cd ..

rm -rf  ./JCPBackend/wwwroot 

cp -r jcpfrontend/dist ./JCPBackend/wwwroot

cd JCPBackend

dotnet restore
dotnet publish -o ../publish/staging /p:Configuration=Release /p:EnvironmentName=Development

cd ..
