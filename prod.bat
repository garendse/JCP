cd jcpfrontend

call npm install
call npm run build

cd ..

del /Q .\JCP\JCP\assets 

copy /Y jcpfrontend\dist\index.html .\JCP\JCP\Views\Home\Index.cshtml

copy /Y jcpfrontend\dist\assets .\JCP\JCP\assets

cd ..

pause