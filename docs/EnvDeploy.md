# Setup dev env

Prerequisites:

-   Google chrome latest version
-   Nodejs
-   Visual studio code
-   Visual studio 2022
-   https://desktop.github.com/

1. Clone the repo with git
2. Modify `jcpfrontend/.env.development` & `JCPBackend/appsettings.Development.json` to reflect environment
3. Run the `dev.bat` script (Optionally)
4. Open `JCPBackend/JCPBackend.sln` in Visual studio 2022
5. Fetch nuget packages (Should happen auto check tasks) (If dev.bat script was not ran)
6. Run the project
7. Open `jcpfrontend/` folder in visual studio code
8. Open the integrated terminal in visual studio code (`CTRL + ~`)
9. Run `npm install` to install deps
10. Run the command `npm run dev` -> Hot reloading will now work on frontend code. (Almost no limitations on what can be modified)
11. Press `F5` to start chrome & debug project

# Deploy

1. Modify `jcpfrontend/.env.production` & `JCPBackend/appsettings.Production.json` to reflect environment
2. Delete `publish` folder
3. Run `prod.bat`
4. Deploy all in `publish/prod` according to https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=netcore-cli
    - Note The section publish section in `Publish and deploy the app` is already taken care of in the script, the result is in `publish/prod` that folder needs to be moved to the server
5. Ensure WebDAV is disabled for the app.
