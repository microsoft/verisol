# Installing and running VeriSol

## Dependencies

The following dependecies are needed to run VeriSol on a Solidity program. There are two categories of dependencies:
   - Translating Solidity to Boogie program
   - Run the verifier
   - Viewing the Corral defect trace in Solidity source 

> NOTE: We currently provide instructions for Windows. Instructions for Linux coming soon!

### Depedencies for translating Solidity to Boogie 
   - __Solidity compiler__. Download the Solc binary for Windows or Linux from [here](https://github.com/ethereum/solidity/releases/tag/v0.4.24). We have currently tested with version __0.4.24__. Place the executable (solc.exe for Windows, or solc-static-linux for Linux) in the **Tool** folder.
   
### Dependencies for running verifier

   - __Boogie verifier__. Download and build the Boogie sources from [here](https://github.com/boogie-org/boogie
), and denote _%BOOGIEDIR%_ as the path to the folder containing **Boogie.exe**(and other dlls).
   - __Corral verifier__. Download and build the Corral sources from [here](https://github.com/boogie-org/corral
), and denote _%CORRALDIR%_ as the path to the folder containing **Corral.exe** (and other dlls).
   - __Z3 theorem prover__. Unless you already have **z3.exe** installed as part of Boogie/Corral download, 
   download **z3.exe** from [here](https://github.com/Z3Prover/z3), and place it in both _%BOOGIEDIR%_ and _%CORRALDIR%_. We have only tested versions **4.8.0** or below.
   
### Dependencies for viewing Corral defect traces in source code
   - __Concurrency explorer__. Downlaod the sources and build the sources of **ConcurencyExplorer** from [here](https://github.com/LeeSanderson/Chess), and denote _%CONCURRENCYEXPLORERDIR%_ as the path containing **ConcurrencyExplorer.exe**.

## Build VeriSol

Open the __Sources\SolToBoogie.sln__ file in Visual Studio (2017) and perform __Build Solution__. 

## Running VeriSol

### Translate Solidity to Boogie
Assuming the root folder is *VERISOL_PATH*, run 

> %VERISOL_PATH%\Sources\SolToBoogie\bin\Debug\netstandard2.0\SolToBoogie.dll a.sol %VERISOL_PATH% out.bpl

### Run verifier
Coming soon!

### View traces from Corral

## Regression script

Coming soon!
