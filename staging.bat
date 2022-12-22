cd jcpfrontend

call npm install
call npm run build-staging

cd ..

del /Q .\JCPBackend\wwwroot\assets 
del /Q .\JCPBackend\wwwroot 

xcopy /E/H jcpfrontend\dist .\JCPBackend\wwwroot

cd JCPBackend

call dotnet restore
call dotnet publish -o ../publish/staging /p:Configuration=Release /p:EnvironmentName=Staging

cd ..