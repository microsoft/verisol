# Installing and running VeriSol

 > We use "\\" to denote path separators for Windows. Substitute theseparator "/" for Linux/OSX in instructions below. 


## Install
- Install **.NET Core** (version **2.2**) for Windows/Linux/OSX from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106)

The following are dynamically installed at VeriSol runtime (the first time only) [No action needed]
- [Solidity compiler](https://github.com/ethereum/solidity/releases/tag/v0.5.10)
- [Z3 theorem prover](https://github.com/Z3Prover/z3/releases)
- [Corral](https://github.com/boogie-org/corral) and [Boogie](https://github.com/boogie-org/boogie) verifiers, which are installed as dotnet cli tools

Follow either **Nuget** package installation directly, or after building the **sources**.

#### Install from nuget.org
Install to the **global** dotnet CLI tools cache so that you can run command  `VeriSol` from anywhere:
```
dotnet tool install VeriSol --version 0.1.1-alpha --global
```

#### Install from sources

Perform the following set of commands:
```
git clone https://github.com/microsoft/verisol.git
dotnet build Sources\VeriSol.sln
```

Let %VERISOL_PATH% be the root folder of the repository. 

Install to the **global** dotnet CLI tools cache so that you can run command  `VeriSol` from anywhere:
```
dotnet tool install VeriSol --version 0.1.1-alpha --global --add-source %VERISOL_PATH%\nupkg\
```

Or, Install to a **local** directory and access the command with full path `%Installed_Path%\VeriSol`
```
dotnet tool install VeriSol --version 0.1.1-alpha --tool-path %Installed_Path%  --add-source %VERISOL_PATH%\nupkg\
```

### Special instruction for MacOS/OSx (for both installation)
- Follow the instructions [here](https://solidity.readthedocs.io/en/v0.5.11/installing-solidity.html). Perform the following commands using [HomeBrew](http://brew.sh/) installer system: `brew update & brew upgrade & brew tap ethereum/ethereum & brew install solidity`. Homebrew will install `solc` binaries in the folder `/usr/local/Cellar/solidity/<version>/bin`. Copy the `solc` binary to the folder where VeriSol is installed (either dotnet global tools cache or local install directory specified above). 

## Running VeriSol

Assuming VeriSol is in the path, run 

`VeriSol`

to view options and their meanings. 

A common usage:

`VeriSol foo.sol Bar`

where 
   - *foo.sol* is the top-level Solidity file
   - *Bar* is the name of the top-level contract to analyze

  > For Windows, the tool output prints instructions to step through the trace using [**ConcurrencyExplorer.exe**](https://github.com/boogie-org/corral/tree/master/tools) binary, whose sources are [here](https://github.com/LeeSanderson/Chess)

For the examples below, change directory to Test\regressions\ folder.

### Example with refutation ###
`VeriSol Error.sol AssertFalse`

### Example with verification ###
`VeriSol Mapping.sol Mapping`

### Example with Loop Invariants ###
`VeriSol LoopInvUsageExample.sol LoopFor`

### Example with Contract Invariants ###
`VeriSol ContractInvUsageExample.sol LoopFor`

### Example of higly simplified ERC20 ###
`VeriSol ERC20-simplified.sol ERC20`

### Example of higly simplified TheDAO attack ###
`VeriSol DAO-Sim-Buggy.sol Mallory`

`VeriSol DAO-Sim-Fixed.sol Mallory`

## VeriSol Code Contracts library
The code contract library **VeriSolContracts.sol** is present [here](/Test/regressions/Libraries/VeriSolContracts.sol). This allows adding loop invariants, contract invariants for proofs, and extending the assertion language.  

## Regression script 
First, follow the installation instructions from **sources**

To run the regressions test, first install SolToBoogieTest
```
dotnet tool install --global SolToBoogieTest --version 0.1.1-alpha --add-source %VERISOL_PATH%\nupkg\
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




