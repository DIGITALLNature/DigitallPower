dotnet build $PSScriptRoot\src\dgt.power\dgt.power.csproj
dotnet pack $PSScriptRoot\src\dgt.power\dgt.power.csproj
dotnet tool install dgt.power --add-source $PSScriptRoot\packages --global
dotnet tool update dgt.power --add-source $PSScriptRoot\packages --global 