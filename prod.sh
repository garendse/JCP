#!/usr/bin/env bash

cd jcpfrontend

npm install
npm run build

cd ..

rm -rf  ./JCPBackend/wwwroot 

cp -r jcpfrontend/dist ./JCPBackend/wwwroot

cd JCPBackend

dotnet restore
dotnet publish -o ../publish/prod /p:Configuration=Release /p:EnvironmentName=Production

cd ..