# Installing and running VeriSol

 > We use "\\" to denote path separators for Windows. Substitute theseparator "/" for Linux/OSX in instructions below. 


## Dependencies
### Dev/Runtime dependencies
- Install **.NET Core** (version **2.2**) for Windows/Linux/OSX from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106)

### Dynamically installed at VeriSol runtime (the first time only)
- [Solidity compiler](https://github.com/ethereum/solidity/releases/tag/v0.5.10)
- [Z3 theorem prover](https://github.com/Z3Prover/z3/releases)
- We use **corral** and **boogie** verifiers, which are installed as dotnet cli tools

## Build VeriSol

Perform the following commands from the root folder:

    dotnet build Sources\SolToBoogie.sln

## Install VeriSol as dotnet CLI tool
Install to the global dotnet CLI tools cache so that you can run command  `VeriSol` from anywhere:
```
dotnet tool install VeriSol --version 0.1.0 --global --add-source %VERISOL_PATH%\nupkg\
```
Or install to a local directory and access the command with full path `%Installed_Path%\VeriSol`
```
dotnet tool install VeriSol --version 0.1.0 --tool-path %Installed_Path%  --add-source %VERISOL_PATH%\nupkg\
```

## Running VeriSol

Assuming VeriSol is installed globally, run 

`VeriSol`

to view options and their meanings. 

A common usage:

`VeriSol foo.sol Bar /tryProof /tryRefutation:6 /printTransactionSequence`

where 
   - *foo.sol* is the top-level Solidity file
   - *Bar* is the name of the top-level contract to analyze
   - */tryProof* attempts to find proof of correctness of the specifications in foo.sol
   - */tryRefutation:6* attempts to find a violation of specifications in foo.sol up to *6* transactions to *Bar*.
   - */printTransactionSequence* prints the transaction sequence of a defect on console (default false)

  > For Windows, the tool output prints instructions to step through the trace using *ConcurrencyExplorer.exe* binary. 

### Example with refutation ###
`VeriSol Test\regressions\Error.sol AssertFalse /tryProof /tryRefutation:6 /printTransactionSequence`

### Example with verification ###
`VeriSol Test\regressions\Mapping.sol Mapping /tryProof /tryRefutation:6 /printTransactionSequence`

### Example with Loop Invariants ###
`VeriSol Test\regressions\LoopInvUsageExample.sol LoopFor /tryProof /tryRefutation:10 /printTransactionSequence`

### Example with Contract Invariants ###
`VeriSol Test\regressions\ContractInvUsageExample.sol LoopFor /tryProof /tryRefutation:10 /printTransactionSequence`

### Example of higly simplified ERC20 ###
`VeriSol Test\regressions\ERC20-simplified.sol ERC20 /tryProof /tryRefutation:10 /printTransactionSequence`

### Example of higly simplified TheDAO attack ###
`VeriSol Test\regressions\DAO-Sim-Buggy.sol Mallory /tryProof /tryRefutation:10 /printTransactionSequence`

`VeriSol Test\regressions\DAO-Sim-Fixed.sol Mallory /tryProof /tryRefutation:10 /printTransactionSequence`

## VeriSol Code Contracts library
The code contract library **VeriSolContracts.sol** is present [here](https://github.com/microsoft/verisol/blob/master/Test/regressions/Libraries/VeriSolContracts.sol). This allows adding loop invariants, contract invariants for proofs, and extending the assertion language.  

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


