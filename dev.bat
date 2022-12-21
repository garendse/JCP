cd jcpfrontend

call npm install
call npm run build-dev

cd ..

del /Q .\JCPBackend\wwwroot\assets 
del /Q .\JCPBackend\wwwroot 

xcopy /E/H jcpfrontend\dist .\JCPBackend\wwwroot

cd JCPBackend

call dotnet restore
call dotnet publish -o ../publish/dev /p:Configuration=Release /p:EnvironmentName=Development

cd ..
