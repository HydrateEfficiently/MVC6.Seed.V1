Initialization steps

1) Rename .sln
2) Rename **/*.*.xproj, **/*.*.xproj.user
3) Replace placeholders in .sln
4) Confirm solution loads all project
5) In wwwroot, replace 'AngularComponentNamePrefix' e.g. with 'Xyz'
6) In wwwroot, replace 'angular-namespace-prefix' e.g. with 'xyz'
7) Replace '<RootNamespace>MVC6.Seed.V1' with '<RootNamespace>[proj_name]'
8) Replace 'mvc6:seed:v1' e.g. with 'xyz'
9) In package.json, change name to project name
8) Restore packages (don't know how to do this gracefully)