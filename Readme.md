# INSTRUCTIONS OUTDATED AS OF 2022-12-21 WORKING ON NEW

# JCP

## Setting up git

For all steps make sure when installing to select add to PATH and install for all users.

1. Install https://git-scm.com/
2. Install the github CLI https://github.com/cli/cli/releases (Download the gh_x.x.x_windows_amd64.msi file and install)
3. You may need to log out/in or restart before continuing
4. Open a terminal and run `gh auth login` and follow instructions default settings are fine.
5. You are now logged in to github!

## Creating a local dev environment

1. Open terminal and CD to parent dir you want to use.
2. `git clone https://github.com/LouisCarma/JCP`
3. `cd JCP`
4. Open SLN in visual studio, and update NUGet Packages.
5. Open jcpfrontend folder in VSCode.
6. Run `npm install` in integrated terminal
7. Code is now ready to work on!

## Syncing changes from Github

1. With the `jcpfrontend` folder in vscode
2. `Ctrl+shift+P`
3. Type untill you can select `Git:Sync`
4. Changes should now be downloaded locally

## Pushing changes to github

1. With the `jcpfrontend` folder in vscode
2. CLick on the source control icon to the left
3. Stage changes by hovering over the files you want to push and clicking the + button
4. Type a commit message
5. Click the commit button or CTRL + enter
