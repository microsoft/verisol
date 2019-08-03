# Installing and running VeriSol

 > We use "\\" to denote path separators for Windows. Substitute theseparator "/" for Linux/OSX in instructions below. 


## Dependencies

- Install **.NET Core** (version **2.2**) for Windows/Linux/OSX from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2#sdk-2.2.106) 
- Install **Solidity compiler** (version **0.5.10**) binaries for Windows/Linux/OSX from [here](https://github.com/ethereum/solidity/releases/tag/v0.5.10) into the **Tool** folder.
   - (Windows) Download `solc.exe`
   - (Linux) Download the executable `solc-static-linux` use command `chmod +x solc-static-linux` to grant execution permission.
   - (OSX) Download the executable `solc-mac`. use command `chmod +x solc-mac` to grant execution permission (if necessary)
- We use **Corral** and **Boogie** verifiers. Corral is present as a submodule, which in turn uses Boogie as a submodule. Run a recursive git submodule update command from the root folder
`git submodule update --recursive --init`
Make sure that Corral and Corral\Boogie folders are populated. Let us denote %CORRAL_DIR% as corral\bin\debug\ and %BOOGIE_DIR% as 
corral\boogie\binaries\ folders. 
   - (Windows) Follow instructions for [building Corral on Windows](https://github.com/boogie-org/corral#building-and-running-corral-on-windows) and [building Boogie on Windows](https://github.com/boogie-org/boogie#windows)
   - (Linux/OSX) Follow the [building Corral on Linux](https://github.com/boogie-org/corral#building-and-running-corral-on-linux-using-mono) and [building Boogie on Linux/OSX](https://github.com/boogie-org/boogie#linuxosx). Note the dependency on [Mono](https://www.mono-project.com). 
   - You need to build Boogie separately to generate **Boogie.exe** (not generated as part of the Corral build)
   - At this point, there should be a copy (Windows) or symbolic link (Linux/OSX) of [Z3 theorem prover](https://github.com/Z3Prover/z3/releases) **z3.exe** present in both %CORRAL_DIR% and %BOOGIE_DIR% folders. 

### (Optional) 
   - For Windows, we currently use  [ConcurrencyExplorer](https://github.com/LeeSanderson/Chess) in Corral\Tools\ to view traces (for Windows). It is unclear if one can build the sources of *ConcurencyExplorer* for Linux/OSX from [here](https://github.com/LeeSanderson/Chess). If that works, copy the *ConcurrencyExplorer.exe* binary to Corral\Tools\.

## Build VeriSol

Perform the following commands from the root folder:
<!-- Open the __Sources\SolToBoogie.sln__ file in Visual Studio (2017) and perform __Build Solution__. -->
<!-- - `msbuild corral\boogie\source\boogie.sln` Ignroe the errors as they don't affect VeriSol. They go away if you (optionally) build using Visual Studio. -->
<!-- - `msbuild corral\cba.sln` -->
`dotnet build Sources\SolToBoogie.sln`

## Running VeriSol

Assuming the root folder of this repository is *VERISOL_PATH*, run 

`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll`

to view options and their meanings. 

A common usage:

`dotnet %VERISOL_PATH%\Sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll foo.sol Bar /tryProof /tryRefutation:6`

where 
   - *foo.sol* is the top-level Solidity file
   - *Bar* is the name of the top-level contract to analyze
   - */tryProof* attempts to find proof of correctness of the specifications in foo.sol
   - */tryRefutation:6* attempts to find a violation of specifications in foo.sol up to *6* transactions to *Bar*.

  > For Windows, the tool output prints instructions to view the trace using *ConcurrencyExplorer.exe* binary. 

  > Documentation of the error trace (and a more concise list of transactions with arguments) coming soon!!

### Example with refutation ###
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\Error.sol AssertFalse /tryProof /tryRefutation:6`

### Example with verification ###
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\Mapping.sol Mapping /tryProof /tryRefutation:6`

### Example with Loop Invariants ###
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\LoopInvUsageExample.sol LoopFor /tryProof /tryRefutation:10`

### Example with Contract Invariants ###
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\ContractInvUsageExample.sol LoopFor /tryProof /tryRefutation:10`

### Example of higly simplified ERC20 ###
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\ERC20-simplified.sol ERC20 /tryProof /tryRefutation:10`

### Example of higly simplified TheDAO attack ###
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\DAO-Sim-Buggy.sol Mallory /tryProof /tryRefutation:10`
`dotnet %VERISOL_PATH%\sources\VeriSol\bin\Debug\netcoreapp2.2\VeriSol.dll Test\regressions\DAO-Sim-Fixed.sol Mallory /tryProof /tryRefutation:10`

## VeriSol Code Contracts library
The code contract library **VeriSolContracts.sol** is present [here](https://github.com/microsoft/verisol/blob/master/Test/regressions/Libraries/VeriSolContracts.sol). This allows adding loop invariants, contract invariants for proofs, and extending the assertion language.  

## Regression script

To run the regressions, let %VERISOL_PATH% denote path to the root of the installation on Windows, run:
-  `dotnet %VERISOL_PATH%\Sources\SolToBoogieTest\bin\Debug\netcoreapp2.2\SolToBoogieTest.dll %VERISOL_PATH% %VERISOL_PATH%\test\`
<!-- - (Linux/OSX) `dotnet $VeriSolPath/Sources/SolToBoogieTest/bin/Debug/netcoreapp2.2/SolToBoogieTest.dll $VeriSolPath $VeriSolPath/Test` -->

All regressions are expected to pass. 

To run a subset of examples during testing, add an optional parameter to limit the above run to a subset of tests that match a prefix string *<prefix>* (e.g. using Array will only run regresssions with Array in their prefix)

`dotnet %VERISOL_PATH%\Sources\SolToBoogieTest\bin\Debug\netcoreapp2.2\SolToBoogieTest.dll %VERISOL_PATH% %VERISOL_PATH%\test\ [<prefix>]`


