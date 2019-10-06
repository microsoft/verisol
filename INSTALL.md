# Installing and running VeriSol

 > We use "\\" to denote path separators for Windows. Substitute theseparator "/" for Linux/OSX in instructions below. 


## Dependencies
### Dev/Runtime dependencies
- Install **.NET Core** (version **2.2**) for Windows/Linux/OSX from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106)

### Dynamically installed at VeriSol runtime (the first time only) [No action needed]
- [Solidity compiler](https://github.com/ethereum/solidity/releases/tag/v0.5.10)
- [Z3 theorem prover](https://github.com/Z3Prover/z3/releases)
- We use **corral** and **boogie** verifiers, which are installed as dotnet cli tools

### Special instruction for MacOS/OSx
- Follow the instructions [here](https://solidity.readthedocs.io/en/v0.5.11/installing-solidity.html). Perform the following commands using [HomeBrew](http://brew.sh/) installer system: `brew update & brew upgrade & brew tap ethereum/ethereum & brew install solidity`. Homebrew will install `solc` binaries in the folder `/usr/local/Cellar/solidity/0.5.11` (or the number corresponding to the installed `solc` version). Copy the `solc.exe` file to the folder where VeriSol is installed (either dotnet global tools cache or local install directory). 

## Build VeriSol

Perform the following commands from the root folder:

    dotnet build Sources\VeriSol.sln

## Install VeriSol as dotnet CLI tool
Install to the **global** dotnet CLI tools cache so that you can run command  `VeriSol` from anywhere:
```
dotnet tool install VeriSol --version 0.1.0 --global --add-source %VERISOL_PATH%\nupkg\
```

Or, Install to a **local** directory and access the command with full path `%Installed_Path%\VeriSol`
```
dotnet tool install VeriSol --version 0.1.0 --tool-path %Installed_Path%  --add-source %VERISOL_PATH%\nupkg\
```

## Running VeriSol

Assuming VeriSol is in the path, run 

`VeriSol`

to view options and their meanings. 

A common usage:

`VeriSol foo.sol Bar`

where 
   - *foo.sol* is the top-level Solidity file
   - *Bar* is the name of the top-level contract to analyze

  > For Windows, the tool output prints instructions to step through the trace using *ConcurrencyExplorer.exe* binary. 

### Example with refutation ###
`VeriSol Test\regressions\Error.sol AssertFalse`

### Example with verification ###
`VeriSol Test\regressions\Mapping.sol Mapping`

### Example with Loop Invariants ###
`VeriSol Test\regressions\LoopInvUsageExample.sol LoopFor`

### Example with Contract Invariants ###
`VeriSol Test\regressions\ContractInvUsageExample.sol LoopFor`

### Example of higly simplified ERC20 ###
`VeriSol Test\regressions\ERC20-simplified.sol ERC20`

### Example of higly simplified TheDAO attack ###
`VeriSol Test\regressions\DAO-Sim-Buggy.sol Mallory`

`VeriSol Test\regressions\DAO-Sim-Fixed.sol Mallory`

## VeriSol Code Contracts library
The code contract library **VeriSolContracts.sol** is present [here](/Test/regressions/Libraries/VeriSolContracts.sol). This allows adding loop invariants, contract invariants for proofs, and extending the assertion language.  

## Regression script

To run the regressions test, first install SolToBoogieTest
```
dotnet tool install --global SolToBoogieTest --version 0.1.0 --add-source %VERISOL_PATH%\nupkg\
```

Then run command `VeriSolRegressionRunner`
```
VeriSolRegressionRunner %VERISOL_PATH%\Test\
```

All regressions are expected to pass. 

To run a subset of examples during testing, add an optional parameter to limit the above run to a subset of tests that match a prefix string *<prefix>* (e.g. using Array will only run regresssions with Array in their prefix)

```
VeriSolRegressionRunner %VERISOL_PATH%\Test\ [<prefix>]
```


