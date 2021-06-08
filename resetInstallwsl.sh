dotnet.exe tool uninstall --global VeriSol
dotnet.exe tool uninstall --global SolToBoogieTest
dotnet.exe build Sources/VeriSol.sln
dotnet.exe tool install VeriSol --version 0.1.1-alpha --global --add-source $(wslpath -w ./nupkg/)
dotnet.exe tool install --global SolToBoogieTest --version 0.1.1-alpha --add-source $(wslpath -w ./nupkg/)
