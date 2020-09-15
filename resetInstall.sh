dotnet tool uninstall --global VeriSol
dotnet tool uninstall --global SolToBoogieTest
dotnet build Sources/VeriSol.sln
dotnet tool install VeriSol --version 0.1.1-alpha --global --add-source ./nupkg/
dotnet tool install --global SolToBoogieTest --version 0.1.1-alpha --add-source ./nupkg/
