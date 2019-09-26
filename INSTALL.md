# Installing and running VeriSol

 > We use "\\" to denote path separators for Windows. Substitute theseparator "/" for Linux/OSX in instructions below. 


## Dependencies

- Install **.NET Core** (version **2.2**) for Windows/Linux/OSX from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106) 
- Install the following **Solidity compiler** (version **0.5.10**) binaries for Windows/Linux/OSX from [here](https://github.com/ethereum/solidity/releases/tag/v0.5.10) into the **Binaries** folder.
   - (Windows) Download `solc.exe`
   - (Linux) Download `solc-static-linux` binary and use command `chmod +x solc-static-linux` 
   - (OSX) You need to build `solc` from the source code. The easiest way is to use the [HomeBrew](http://brew.sh/) installer system. Follow the instructions [here](https://solidity.readthedocs.io/en/v0.5.11/installing-solidity.html). Essentially, you need to perform the following commands: `brew update & brew upgrade & brew tap ethereum/ethereum & brew install solidity`. Homebrew will install `solc` binaries in the folder `/usr/local/Cellar/solidity/0.5.11` (or the number corresponding to the installed `solc` version). Copy the `solc` file to  **Binaries** folder and rename it to  `solc-mac`. 
   
- Install [Z3 theorem prover](https://github.com/Z3Prover/z3/releases) (ver 4.8.4) binary **z3.exe** (respectively, **z3**) for Windows (respectively, Linux/OSX) into **Binaries** 
   - For Linux/OSX, create a symbolic link to z3 as follows:
   
      `ln -S Binaries/z3 Binaries/z3.exe`

- We use **corral** and **boogie** verifiers, which are present as submodules. Run a recursive git submodule update from the root folder 

      git submodule update --recursive --init


### (Optional) 
   - For Windows, we currently use  [ConcurrencyExplorer](https://github.com/LeeSanderson/Chess) in Corral\Tools\ to step through traces. It is unclear if one can build the sources of *ConcurencyExplorer* for Linux/OSX from [here](https://github.com/LeeSanderson/Chess). If that works, copy the *ConcurrencyExplorer.exe* binary to Corral\Tools\.

## Build VeriSol

Perform the following commands from the root folder:

    dotnet build Sources\SolToBoogie.sln

## Running VeriSol

Assuming in the root folder of this repository, run 

`dotnet Binaries\VeriSol.dll`

to view options and their meanings. 

A common usage:

`dotnet Binaries\VeriSol.dll foo.sol Bar /tryProof /tryRefutation:6 /printTransactionSequence`

where 
   - *foo.sol* is the top-level Solidity file
   - *Bar* is the name of the top-level contract to analyze
   - */tryProof* attempts to find proof of correctness of the specifications in foo.sol
   - */tryRefutation:6* attempts to find a violation of specifications in foo.sol up to *6* transactions to *Bar*.
   - */printTransactionSequence* prints the transaction sequence of a defect on console (default false)

  > For Windows, the tool output prints instructions to step through the trace using *ConcurrencyExplorer.exe* binary. 

### Example with refutation ###
`dotnet Binaries\VeriSol.dll Test\regressions\Error.sol AssertFalse /tryProof /tryRefutation:6 /printTransactionSequence`

### Example with verification ###
`dotnet Binaries\VeriSol.dll Test\regressions\Mapping.sol Mapping /tryProof /tryRefutation:6 /printTransactionSequence`

### Example with Loop Invariants ###
`dotnet Binaries\VeriSol.dll Test\regressions\LoopInvUsageExample.sol LoopFor /tryProof /tryRefutation:10 /printTransactionSequence`

### Example with Contract Invariants ###
`dotnet Binaries\VeriSol.dll Test\regressions\ContractInvUsageExample.sol LoopFor /tryProof /tryRefutation:10 /printTransactionSequence`

### Example of higly simplified ERC20 ###
`dotnet Binaries\VeriSol.dll Test\regressions\ERC20-simplified.sol ERC20 /tryProof /tryRefutation:10 /printTransactionSequence`

### Example of higly simplified TheDAO attack ###
`dotnet Binaries\VeriSol.dll Test\regressions\DAO-Sim-Buggy.sol Mallory /tryProof /tryRefutation:10 /printTransactionSequence`

`dotnet Binaries\VeriSol.dll Test\regressions\DAO-Sim-Fixed.sol Mallory /tryProof /tryRefutation:10 /printTransactionSequence`

## VeriSol Code Contracts library
The code contract library **VeriSolContracts.sol** is present [here](https://github.com/microsoft/verisol/blob/master/Test/regressions/Libraries/VeriSolContracts.sol). This allows adding loop invariants, contract invariants for proofs, and extending the assertion language.  

## Regression script

To run the regressions from the root (%VERISOL_PATH%) of the installation, run:
-  `dotnet Binaries\SolToBoogieTest.dll  %VERISOL_PATH%\Test\`

All regressions are expected to pass. 

To run a subset of examples during testing, add an optional parameter to limit the above run to a subset of tests that match a prefix string *<prefix>* (e.g. using Array will only run regresssions with Array in their prefix)

`dotnet Binaries\SolToBoogieTest.dll %VERISOL_PATH%\Test\ [<prefix>]`


